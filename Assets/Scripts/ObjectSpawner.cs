using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject tablesParentObject;
    [SerializeField] GameObject[] beerPrefabs;

    [Header("Settings")]
    [SerializeField] float beerSpawnRate = 3.1f;

    private List<GameObject> tables = new List<GameObject>();
    private float beerTimer;

    private ObjectPool<GameObject> objectPool;
    private int poolDefaultCapacity = 20;
    private int poolMaxSize = 100;
    
    void Start()
    {
        SetUpObjectPool();

        foreach(Transform child in tablesParentObject.transform)
        {
            tables.Add(child.gameObject);
        }

        beerTimer = beerSpawnRate;
    }

    void OnEnable()
    {
        Actions.OnBeerGrabbed += ReturnObjectToPool;
    }

    void OnDisable()
    {
        Actions.OnBeerGrabbed -= ReturnObjectToPool;
    }

    void Update()
    {
        if (PauseControl.Instance.GameIsPaused) { return; }

        beerTimer -= Time.deltaTime;

        if (beerTimer < 0)
        {
            SpawnBeer();
        }
    }

    void SetUpObjectPool()
    {
        objectPool = new ObjectPool<GameObject>(() => {
            return Instantiate(beerPrefabs[Random.Range(0, beerPrefabs.Length)]);
        }, obj => {
            obj.SetActive(true);
        }, obj => {
            obj.SetActive(false);
        }, obj => {
            Destroy(obj);
        }, /* collectionCheck= */ false, poolDefaultCapacity, poolMaxSize);
    }

    void SpawnBeer()
    {
        int tableIndex = Random.Range(0, tables.Count);
        GameObject table = tables[tableIndex];
        
        GameObject newBeer = objectPool.Get();
        newBeer.transform.position = GetSpawnPosition(table, tableIndex == 0);
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

    private void ReturnObjectToPool(GameObject obj)
    {
        objectPool.Release(obj);
    }
}
