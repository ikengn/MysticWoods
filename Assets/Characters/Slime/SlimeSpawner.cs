using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab;

    void Start() {
        InvokeRepeating("SpawnSlime", 0f, 5f);
    }

    // Update is called once per frame
    void SpawnSlime()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0);
        Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
    }
}
