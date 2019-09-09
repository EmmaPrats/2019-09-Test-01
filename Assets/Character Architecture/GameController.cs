using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController instance;

    private int score = 0;

    private float timePlayed = 0;

    private float startTime;
    private float currentTime;

    public Character player;

    public int Score
    {
        get => score;
        private set
        {
            score = value;
        }
    }

    public float TimePlayed => currentTime - startTime;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        player.onCharacterDies.RemoveListener(GameOver);
        player.onCharacterDies.AddListener(GameOver);

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timePlayed += Time.deltaTime;
        currentTime = Time.time;


    }

    public void IncreaseScore()
    {
        Score++;
    }

    public void ListenToThis(Enemy character)
    {
        character.onEnemyDie.AddListener(OnEnemyDie);
    }

    private void OnEnemyDie(Enemy character)
    {
        Score++;
    }

    private void GameOver(Character character)
    {
        Debug.Log("Game Over.");
    }
}
