using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float timeToSpawn = 5f;
    private float spawnCounter;

    // Ограничение по количеству врагов
    public int maxEnemies = 20;
    private List<GameObject> currentEnemies = new List<GameObject>();

    // Зона, в которой может произойти спавн
    public Vector2 spawnAreaX = new Vector2(-50f, 50f);
    public Vector2 spawnAreaZ = new Vector2(-50f, 50f);

    void Start()
    {
        spawnCounter = timeToSpawn;
    }

    void Update()
    {
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;

            // Удаляем уничтоженных врагов из списка
            currentEnemies.RemoveAll(enemy => enemy == null);

            if (currentEnemies.Count < maxEnemies)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(spawnAreaX.x, spawnAreaX.y),
                    transform.position.y, // предполагается, что враги на этом уровне Y
                    Random.Range(spawnAreaZ.x, spawnAreaZ.y)
                );

                GameObject newEnemy = Instantiate(enemyToSpawn, randomPosition, Quaternion.identity);
                currentEnemies.Add(newEnemy);
            }
        }
    }
}
