using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate && GameManager.Instance.difficultyLevel == 2 && !GameManager.Instance.isGameOver)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 viewportPosition = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 0);
        Vector3 worldPosition = Camera.main.ScreenToViewportPoint(viewportPosition);

        GameObject enemy = Instantiate(enemyPrefab, worldPosition, Quaternion.identity);

        enemy.transform.localScale = Vector3.zero;
    }
}
