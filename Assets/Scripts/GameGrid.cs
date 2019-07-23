using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour {

	[SerializeField] private ScriptableGrid grid = null;
	[SerializeField] private PoolManager foodPool = null;

	[Header("Grid Settings")]
	[SerializeField] private Transform origin = null;
	[SerializeField] private int rows = default (int);
	[SerializeField] private int columns = default (int);
	[SerializeField] private bool showGizmosOnEditor = false;

	#region MonoBehaviour Methods
	private void Awake () {
		Vector2 temp = origin.position;
		Vector2Int originPos = Vector2Int.FloorToInt (temp);
		grid.GenerateGrid (originPos, rows, columns);
	}

	private void Start () {
		GenerateFood ();
	}

	void OnDrawGizmos () {
		if (showGizmosOnEditor) grid.DrawGizmos ();
	}
	#endregion

	public void GenerateFood () {
		Vector2Int pos = grid.GetRandomFreePoint ();
		foodPool.Spawn (pos, Quaternion.identity);
	}

}