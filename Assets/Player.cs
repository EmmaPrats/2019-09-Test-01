
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int baseHealth = 100;

    [SerializeField] private float health;

    public static Player CurrentPlayer { get; private set; }
    public float Health
    {
        get
        {
            return health;
        }
    }


    //Movement attributes
    //TODO refactor and move movement attributes out of state class
    private float movementSpeed = 11f;
    private float turnSpeed = 180f; //degrees/s

    public TextMeshProUGUI healthUI;

    private void Awake()
    {
        if (CurrentPlayer == null)
            CurrentPlayer = this;
    }

    void Start()
    {
        Debug.Log("Player start.");
        health = baseHealth/2;
    }
    
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);
        transform.position += Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * movementSpeed;
        healthUI.text = "" + Health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0);
    }

    public void Heal (float amount)
    {
        health += amount;
        health = Mathf.Min(health, baseHealth);
    }
}
