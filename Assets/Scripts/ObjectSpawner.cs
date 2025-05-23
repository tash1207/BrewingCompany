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

    private List<Table> tables = new List<Table>();
    private float beerTimer;

    private ObjectPool<GameObject> objectPool;
    private List<GameObject> objectPoolObjects = new List<GameObject>();
    private int poolDefaultCapacity = 20;
    private int poolMaxSize = 100;
    
    void Start()
    {
        SetUpObjectPool();

        foreach(Transform child in tablesParentObject.transform)
        {
            if (child.TryGetComponent(out Table table))
            {
                tables.Add(table);
            }
        }

        SpawnInitialBeers();
        beerTimer = beerSpawnRate;
    }

    void OnEnable()
    {
        Actions.OnItemPickedUp += ReturnObjectToPool;
        Actions.ResetLevel += ResetState;
    }

    void OnDisable()
    {
        Actions.OnItemPickedUp -= ReturnObjectToPool;
        Actions.ResetLevel -= ResetState;
    }

    void Update()
    {
        if (PauseControl.Instance.GameIsPaused) { return; }

        beerTimer -= Time.deltaTime;

        if (beerTimer < 0)
        {
            SpawnRandomBeer();
        }
    }

    void SetUpObjectPool()
    {
        objectPool = new ObjectPool<GameObject>(() => {
            GameObject newObject = Instantiate(beerPrefabs[Random.Range(0, beerPrefabs.Length)]);
            objectPoolObjects.Add(newObject);
            return newObject;
        }, obj => {
            obj.SetActive(true);
        }, obj => {
            obj.SetActive(false);
        }, obj => {
            Destroy(obj);
        }, /* collectionCheck= */ false, poolDefaultCapacity, poolMaxSize);
    }

    void SpawnInitialBeers()
    {
        SpawnBeer(0, 1f);
        SpawnBeer(2, 0.5f);
        SpawnBeer(3, 1f);
        SpawnBeer(4, 0f);
        SpawnBeer(4, 0f);
        SpawnBeer(4, 0f);

        if (tables.Count > 5)
        {
            SpawnBeer(5, 0f);
            SpawnBeer(6, 0.5f);
        }
        if (tables.Count > 10)
        {
            SpawnBeer(7, 1f);
            SpawnBeer(8, 0.5f);
            SpawnBeer(11, 0f);
            SpawnBeer(11, 0f);
            SpawnBeer(11, 0f);
        }
    }

    void SpawnRandomBeer()
    {
        SpawnBeer(Random.Range(0, tables.Count), 1f);

        beerTimer = beerSpawnRate;
    }

    void SpawnBeer(int tableIndex, float fill)
    {
        Table table = tables[tableIndex];
        GameObject newBeer = objectPool.Get();
        newBeer.transform.position = GetSpawnPosition(table);
        newBeer.transform.parent = table.transform;

        if (fill != 1f && newBeer.TryGetComponent(out BeerGlass beerGlass))
        {
            beerGlass.SetBeerFill(fill);
        }
    }

    private Vector2 GetSpawnPosition(Table table)
    {
        float xRange = table.getXRange();
        float yRange = table.getYRange();

        float randomX = Random.Range(-xRange, xRange);
        float randomY = Random.Range(-yRange, yRange);
        return new Vector2(
            table.transform.position.x + randomX,
            table.transform.position.y + randomY);
    }

    private void ReturnObjectToPool(GameObject obj)
    {
        if (obj.TryGetComponent(out BeerGlass beerGlass))
        {
            objectPool.Release(obj);
            beerGlass.ResetBeerFill();
        }
    }

    private void ResetState()
    {
        foreach(GameObject obj in objectPoolObjects)
        {
            ReturnObjectToPool(obj);
        }

        SpawnInitialBeers();
        beerTimer = beerSpawnRate;
    }
}
