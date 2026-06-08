using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    [SerializeField]
    private GameObject[] _prefabs;
    [SerializeField]
    private int _poolSize = 1;
    private List<GameObject>[] _objectPoolList;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitObjPool();
    }

    private void InitObjPool()
    {
        // 풀링이 필요한 오브젝트들 만큼의 배열크기
        _objectPoolList = new List<GameObject>[_prefabs.Length];

        for (int i = 0; i < _prefabs.Length; i++)
        {
            _objectPoolList[i] = new List<GameObject>();
            for(int j = 0; j < _poolSize; j++)
            {
                GameObject obj = Instantiate(_prefabs[i]);
                obj.SetActive(false);
                _objectPoolList[i].Add(obj);
            }
        }
    }

    public GameObject ActivateObj(int p_index)
    {
        GameObject obj = null;

        for(int i = 0; i < _objectPoolList[p_index].Count; i++)
        {
            // 저장되어있는 오브젝트가 켜져있지 않다면
            if (!_objectPoolList[p_index][i].activeInHierarchy)
            {
                obj = _objectPoolList[p_index][i];
                obj.SetActive(true);
                return obj;
            }
        }

        obj = Instantiate(_prefabs[p_index]);
        _objectPoolList[p_index].Add(obj);
        obj.SetActive(true);

        return obj;
    }
}
