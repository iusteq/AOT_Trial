using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //singleton
    public static ObjectPools Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objPool);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject ObjToThrow(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool doesn't exist");
            return null;
        }

        GameObject objToThrow = poolDictionary[tag].Dequeue();

        objToThrow.SetActive(true);
        objToThrow.transform.position = position;
        objToThrow.transform.rotation = rotation;

        //StartCoroutine(BackToPool(objToThrow));
        poolDictionary[tag].Enqueue(objToThrow);

        return objToThrow;
    }

    IEnumerator BackToPool(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(1f);

        poolDictionary[tag].Enqueue(obj);
    }
}
