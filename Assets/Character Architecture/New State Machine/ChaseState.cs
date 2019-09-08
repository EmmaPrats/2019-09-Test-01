using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonoBehaviour
{
    [SerializeField] private Transform detectionOrigin;
    [SerializeField] private float detectionRadius = 10f;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private MonoBehaviour onEnemyInRange;
    [SerializeField] private MonoBehaviour onNoEnemiesFound;


    [SerializeField] private Material material;


    private float _rayDistance = 5f;

    private Character character;
    private IMove characterAsMove;
    private IHaveAI characterAsAI;

    void Start()
    {
        character = GetComponent<Character>();
        characterAsMove = GetComponent<Character>() as IMove;
        if (characterAsMove == null)
            Debug.LogWarning("Chase State set on an object (" + name + ") that does not have a Character component that implements IMove.");
        characterAsAI = GetComponent<Character>() as IHaveAI;
        if (characterAsAI == null)
            Debug.LogWarning("Chase State set on an object (" + name + ") that does not have a Character component that implements IHaveAI.");

        SetMaterial();
    }

    void Update()
    {
        //Comprovar si el jugador s'ha allunyat
        if (Vector3.Distance(transform.position, characterAsAI.Target.position) > detectionRadius * 1.2f)
        {
            characterAsAI.Target = null;
            onNoEnemiesFound.enabled = true;
            this.enabled = false;
            return;
        }

        Vector3 desiredDirection = characterAsAI.Target.position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * characterAsMove.TurnSpeed);

        if (IsForwardBlocked())
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * characterAsMove.TurnSpeed);
        else
            transform.Translate(Vector3.forward * Time.deltaTime * characterAsMove.MovementSpeed);

        Debug.DrawRay(transform.position, transform.forward, Color.black);
        Debug.DrawRay(transform.position, desiredDirection, Color.red);

        //TODO deal with IsPathBlocked and IsForwardBlocked

        if (Vector3.Distance(transform.position, characterAsAI.Target.position) <= character.AttackRange)
        {
            onEnemyInRange.enabled = true;
            this.enabled = false;
            return;
        }
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, layerMask);
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
