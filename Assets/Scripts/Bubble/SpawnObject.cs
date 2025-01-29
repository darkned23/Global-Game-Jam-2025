using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private bool spawnInfinite = false;
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private float spawnInterval = 2f;

    void Start()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("No object to spawn assigned! Please assign a prefab in the inspector.");
            return;
        }
        StartCoroutine(SpawnObjectsWithDelay());
    }

    IEnumerator SpawnObjectsWithDelay()
    {
        Vector3 spawnPosition = transform.position + Vector3.up;

        if (spawnInfinite)
        {
            while (true)
            {
                GameObject newObject = Instantiate(objectToSpawn, spawnPosition, transform.rotation);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        else
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject newObject = Instantiate(objectToSpawn, spawnPosition, transform.rotation);
                yield return new WaitForSeconds(spawnInterval);
            }
        }

    }
}