using Photon.Pun;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] enemySpawnPoints;

    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            PhotonNetwork.Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].name, spawnPoint.position, Quaternion.identity);
        }
    }
}
