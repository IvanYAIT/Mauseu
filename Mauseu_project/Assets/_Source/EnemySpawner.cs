using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] enemySpawnPoints;

    void Start()
    {
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.position, Quaternion.identity);
        }
    }
}
