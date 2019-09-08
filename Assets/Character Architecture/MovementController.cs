using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Character character; //[SerializeField] private IMove character;

    void Update()
    {
        character.transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal") * Time.deltaTime * character.TurnSpeed);
        character.transform.position += Input.GetAxis("Vertical") * character.transform.forward * Time.deltaTime * character.MovementSpeed;
    }
}
