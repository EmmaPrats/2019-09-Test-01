
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;

    public static Player CurrentPlayer { get; private set; }

    //Movement attributes
    //TODO refactor and move movement attributes out of state class
    private float movementSpeed = 11f;
    private float turnSpeed = 180f; //degrees/s

    private void Awake()
    {
        if (CurrentPlayer == null)
            CurrentPlayer = this;
    }

    void Start()
    {
        Debug.Log("Player start.");
    }
    
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);
        transform.position += Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * movementSpeed;
    }
}
