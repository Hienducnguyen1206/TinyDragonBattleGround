using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class Pool
{
    public string tag;                // Tag nhận dạng pool
    public GameObject prefab;         // Prefab cần được quản lý
    public int size;                  // Kích thước khởi tạo của pool
    public bool expandable = true;    // Pool có tự mở rộng khi hết đối tượng không
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; // Singleton cho phép truy cập dễ dàng
    public List<Pool> pools;             // Danh sách các pool
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // Nếu pool hết object và expandable được bật
        if (objectToSpawn == null && pools.Find(p => p.tag == tag)?.expandable == true)
        {
            Pool pool = pools.Find(p => p.tag == tag);
            objectToSpawn = Instantiate(pool.prefab);
        }

        // Kích hoạt và thiết lập đối tượng
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        PooledObject pooledObj = objectToSpawn.GetComponent<PooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn); // Đưa object trở lại queue

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject obj, string tag)
    {
        obj.SetActive(false);

        if (poolDictionary.ContainsKey(tag))
        {
            poolDictionary[tag].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist. Object will not be returned.");
        }
    }
}
