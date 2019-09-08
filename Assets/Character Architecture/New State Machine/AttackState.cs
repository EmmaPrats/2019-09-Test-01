using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour
{
    [SerializeField] private MonoBehaviour onEnemyOutOfRange;
    [SerializeField] private MonoBehaviour onNoEnemiesFound;

    [SerializeField] private Material material;


    [SerializeField] private float detectionRadius = 10f;

    private Character character;
    private IMove characterAsMove;
    private IHaveAI characterAsAI;

    private IHaveStats target;

    private float attackCounter = 0f;

    void Start()
    {
        character = GetComponent<Character>();
        characterAsMove = GetComponent<Character>() as IMove;
        if (characterAsMove == null)
            Debug.LogWarning("Chase State set on an object (" + name + ") that does not have a Character component that implements IMove.");
        characterAsAI = GetComponent<Character>() as IHaveAI;
        if (characterAsAI == null)
            Debug.LogWarning("Chase State set on an object (" + name + ") that does not have a Character component that implements IHaveAI.");
        target = characterAsAI.Target.gameObject.GetComponent<Character>() as IHaveStats;
        if (target == null)
            Debug.LogWarning("Target set on an object (" + name + ") that does not have a Character component that implements IHaveStats.");

        SetMaterial();
    }


    void Update()
    {
        //Comprovar si el jugador s'ha allunyat
        if (Vector3.Distance(transform.position, characterAsAI.Target.position) > character.AttackRange * 1.1f)
        {
            onEnemyOutOfRange.enabled = true;
            this.enabled = false;
            return;
        }

        if (attackCounter <= 0)
        {
            target.TakeDamage(character.Strength);
            attackCounter = character.AttackSpeed;
        }
        else
        {
            attackCounter -= Time.deltaTime;

            if (Vector3.Distance(transform.position, characterAsAI.Target.position) > character.AttackRange / 2)
            {
                Vector3 desiredDirection = characterAsAI.Target.position - transform.position;
                Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection);

                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * characterAsMove.TurnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * characterAsMove.MovementSpeed);
            }
        }
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
