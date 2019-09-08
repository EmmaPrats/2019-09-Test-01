using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WanderState : MonoBehaviour
{
    [SerializeField] private Transform detectionOrigin;
    [SerializeField] private float detectionRadius = 5f;

    //Wander attributes
    [SerializeField] private float circleDistance = 3f;
    [SerializeField] private float circleRadius = 0.5f;
    [Tooltip("in radians")]
    [SerializeField] private float wanderAngle = 1f; //in radians
    private float displacementAngle = 0f; //in radians

    [SerializeField] private MonoBehaviour onEnemyFound;
    [SerializeField] private MonoBehaviour onEnemyInRange;


    [SerializeField] private LayerMask layerMask;

    private float _attacRange = 3f;
    private float _rayDistance = 5f;
    private float _stoppingDistance = 1.5f;

    private Vector3? _destination;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private Character _target;

    private IMove characterAsMove;
    private IHaveAI characterAsAI;


    [SerializeField] private Material material;

    //THIS ONLY GETS CALLED THE FIRST TIME IT'S ACTIVATED
    void Start()
    {
        characterAsMove = GetComponent<Character>() as IMove;
        if (characterAsMove == null)
            Debug.LogWarning("Wander State set on an object (" + name + ") that does not have a Character component that implements IMove.");
        characterAsAI = GetComponent<Character>() as IHaveAI;
        if (characterAsAI == null)
            Debug.LogWarning("Wander State set on an object (" + name + ") that does not have a Character component that implements IHaveAI.");

        SetMaterial();
    }

    void Update()
    {
        // Check if it detected a target
        var chaseTarget = CheckForAggro();
        if (chaseTarget != null)
        {
            characterAsAI.Target = chaseTarget;
            onEnemyFound.enabled = true;
            this.enabled = false;
            return;
        }

        //Wander
        displacementAngle += Random.Range(-wanderAngle, wanderAngle);
        Vector3 circlePosInObjectCoordinates = circleDistance * transform.forward;
        Vector3 displacement = new Vector3(circleRadius * Mathf.Cos(displacementAngle),
                                           0f,
                                           circleRadius * Mathf.Sin(displacementAngle));
        Vector3 desiredDirection = circlePosInObjectCoordinates + displacement;
        Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * characterAsMove.TurnSpeed);

        if (IsForwardBlocked())
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * characterAsMove.TurnSpeed);
        else
            transform.Translate(Vector3.forward * Time.deltaTime * characterAsMove.MovementSpeed);

        //TODO deal with IsPathBlocked and IsForwardBlocked
    }
    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, layerMask);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, _direction);
        //var hitSomething = Physics.RaycastAll(ray, _rayDistance, _layerMask);
        //return hitSomething.Any();
        return Physics.SphereCast(ray, 0.5f, _rayDistance, layerMask);
    }

    private bool NeedsDestination()
    {
        if (_destination == Vector3.zero)
            return true;

        var distance = Vector3.Distance(transform.position, _destination.Value);
        if (distance <= _stoppingDistance)
            return true;

        return false;
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = (transform.position + transform.forward * 4f) +
                                new Vector3(Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
        _destination = new Vector3(testPosition.x, 0f, testPosition.z);
        _direction = Vector3.Normalize(_destination.Value - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    private void GetDestination()
    {
        Vector3 testPosition = (transform.position + transform.forward * 4f) +
                                new Vector3(Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
        _destination = new Vector3(testPosition.x, 0f, testPosition.z);
        _direction = Vector3.Normalize(_destination.Value - transform.position);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        Vector3 pos = detectionOrigin.position;
        for (int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, detectionRadius, layerMask))
            {
                var drone = hit.collider.GetComponent<Character>();
                if (drone != null) //TODO afegir condició de que siga enemic
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return drone.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            Debug.DrawRay(pos, direction * detectionRadius, Color.white);
            direction = stepAngle * direction;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + transform.forward * circleDistance, circleRadius);
        Debug.DrawRay(transform.position, transform.forward * circleDistance, Color.black);
        Vector3 displacement = new Vector3(circleRadius * Mathf.Cos(displacementAngle),
                                           0f,
                                           circleRadius * Mathf.Sin(displacementAngle));
        Debug.DrawRay(transform.position + transform.forward * circleDistance, displacement, Color.black);
    }

    public void SetMaterial()
    {
        transform.GetComponentInChildren<MeshRenderer>().material = material;
    }

    private void OnEnable()
    {
        SetMaterial();
    }
}
