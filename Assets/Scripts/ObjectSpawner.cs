using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject tablesParentObject;
    [SerializeField] GameObject[] beerPrefabs;

    [Header("Settings")]
    [SerializeField] float beerSpawnRate = 3.3f;

    private List<GameObject> tables = new List<GameObject>();
    private float beerTimer;
    
    void Start()
    {
        foreach(Transform child in tablesParentObject.transform)
        {
            tables.Add(child.gameObject);
        }

        beerTimer = beerSpawnRate;
    }

    void Update()
    {
        beerTimer -= Time.deltaTime;

        if (beerTimer < 0)
        {
            SpawnBeer();
        }
    }

    void SpawnBeer()
    {
        int tableIndex = Random.Range(0, tables.Count);
        GameObject table = tables[tableIndex];
        // TODO: Consider object pooling.
        GameObject newBeer = Instantiate(
            beerPrefabs[Random.Range(0, beerPrefabs.Length)],
            GetSpawnPosition(table, tableIndex == 0),
            Quaternion.identity);
        newBeer.transform.parent = table.transform;

        beerTimer = beerSpawnRate;
    }

    private Vector2 GetSpawnPosition(GameObject table, bool isHorizontalTable)
    {
        // TODO: Create Table script that contains the table's bounds for spawning.
        float xRange = isHorizontalTable ? 0.94f : 0.7f;
        float yRange = isHorizontalTable ? 0.42f : 0.8f;

        float randomX = Random.Range(-xRange, xRange);
        float randomY = Random.Range(-yRange, yRange);
        return new Vector2(
            table.transform.position.x + randomX,
            table.transform.position.y + randomY);
    }
}
