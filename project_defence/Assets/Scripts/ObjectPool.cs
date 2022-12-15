using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public string objectName;
    public GameObject perfab;
    public int count;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    ObjectInfo[] objectInfos = null;

    [Header("오브젝트 풀의 위치")]
    [SerializeField]
    Transform tfPoolParent;

    public List<Queue<GameObject>> objectPoolList;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        objectPoolList = new List<Queue<GameObject>>();
        ObjectPoolState();
    }

    void ObjectPoolState()
    {
        if (objectInfos != null)
        {
            for (int i = 0; i < objectInfos.Length; i++)
            {
                objectPoolList.Add(InsertQueue(objectInfos[i]));
            }
        }
    }

    Queue<GameObject> InsertQueue(ObjectInfo perfab_objectInfo)
    {
        Queue<GameObject> test_queue = new Queue<GameObject>();

        for (int i = 0; i < perfab_objectInfo.count; i++)
        {
            GameObject objectClone = Instantiate(perfab_objectInfo.perfab) as GameObject;
            objectClone.SetActive(false);
            objectClone.transform.SetParent(tfPoolParent);
            test_queue.Enqueue(objectClone);
        }

        return test_queue;
    }

    public void InsertQueue(int index)
    {
        GameObject objectClone = Instantiate(objectInfos[index].perfab) as GameObject;
        objectClone.SetActive(false);
        objectClone.transform.SetParent(tfPoolParent);
        objectPoolList[index].Enqueue(objectClone);
    }
}