﻿using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
	[SerializeField] private int amount = default (int);
	[SerializeField] private GameObject prefab = null;
	private List<GameObject> pool = null;

	#region MonoBehaviour Method
	private void Start () {
		pool = new List<GameObject> ();

		for (int i = 0; i < amount; i++) {
			pool.Add (Instantiate (prefab, transform) as GameObject);
			pool[i].gameObject.SetActive (false);
		}

	}
	#endregion

	#region Spawn
	public T Spawn<T> (Vector3Int position, Quaternion rotation) where T : MonoBehaviour {
		Vector2Int spawnPosition = new Vector2Int (position.x, position.y);
		return Spawn<T> (spawnPosition, rotation);
	}

	public T Spawn<T> (Vector2Int position, Quaternion rotation) where T : MonoBehaviour {
		for (int i = 0; i < amount; i++) {
			if (!pool[i].activeInHierarchy) {
				pool[i].transform.position = new Vector3 (position.x, position.y);
				pool[i].transform.rotation = rotation;
				pool[i].SetActive (true);
				return pool[i].GetComponent<T> ();
			}
		}
		return null;
	}

	public GameObject Spawn (Vector3Int position, Quaternion rotation) {
		Vector2Int pos = new Vector2Int (position.x, position.y);
		return Spawn (pos, rotation);
	}

	public GameObject Spawn (Vector2Int position, Quaternion rotation) {
		//serach on pool for a disabled object to be spawned
		for (int i = 0; i < amount; i++) {
			if (!pool[i].activeInHierarchy) {
				pool[i].transform.position = new Vector3 (position.x, position.y);
				pool[i].transform.rotation = rotation;
				pool[i].SetActive (true);
				return pool[i];
			}
		}
		return null;
	}
	#endregion
}