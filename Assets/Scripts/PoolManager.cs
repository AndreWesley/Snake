#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

	[SerializeField] private int amount;
	[SerializeField] private GameObject prefab;
	[SerializeField] private List<GameObject> pool;
	

    void Start()
    {
		for (int i = 0; i < amount; i++)
		{
			pool.Add(Instantiate(prefab, transform) as GameObject);
			pool[i].SetActive(false);
		}
        
    }

	public T Spawn<T>(Vector3Int position) where T : Object {
		for (int i = 0; i < amount; i++)
		{
			if (!pool[i].activeInHierarchy) {
				pool[i].transform.position = position;
				pool[i].SetActive(true);
				return pool[i].GetComponent<T>();
			}
		}
		return null;
	}
}
