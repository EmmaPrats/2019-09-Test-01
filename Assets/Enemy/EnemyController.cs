using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Enemy> enemyList;
    public int minInterval = 1;
    public int maxInterval = 5;
    private float timer;

    public Enemy enemyPrefab;

    private void Start()
    {
        enemyList = new List<Enemy>();
        timer = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Instantiate (enemyPrefab,
                         new Vector3 (Random.Range(-20, 20), 0, Random.Range(-20, 20)),
                         Quaternion.identity);
            timer += Random.Range(minInterval, maxInterval);
        }
        timer -= Time.deltaTime;
    }
}
