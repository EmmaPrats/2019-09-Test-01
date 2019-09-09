using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Character characterPrefab;

    [SerializeField] private List<Character> characterList;

    [Tooltip("TODO")] //TODO tooltip
    [SerializeField] private float minInterval = 1f;
    [SerializeField] private float maxInterval = 5f;

    private float spawnTimer;

    void Start()
    {
        if (characterList == null)
            characterList = new List<Character>();
        spawnTimer = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
        if (spawnTimer <= 0)
        {
            Enemy newCharacter = (Enemy) Instantiate(characterPrefab,
                        new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20)), //TODO generalize this
                        Quaternion.identity, transform);
            newCharacter.onEnemyDie.AddListener(RemoveCharacter);
            characterList.Add(newCharacter);
            spawnTimer += Random.Range(minInterval, maxInterval);
        }
        spawnTimer -= Time.deltaTime;
    }

    private void RemoveCharacter(Enemy character)
    {
        characterList.Remove(character);
    }
}
