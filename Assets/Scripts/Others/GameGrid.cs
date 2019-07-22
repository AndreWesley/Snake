using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour {

	[SerializeField] private bool showGizmosOnEditor = true;

	[SerializeField] private ScriptableGrid grid = null;
	[SerializeField] private PoolManager foodPool = null;
	[SerializeField] private Transform origin = null;
	[SerializeField] private int rows = default (int);
	[SerializeField] private int columns = default (int);

	private void Awake () {
		Vector2 temp = origin.position;
		Vector2Int originPos = Vector2Int.FloorToInt (temp);
		grid.GenerateGrid (originPos, rows, columns);
	}

	private void Start() {
		GenerateFood();
	}

	public void GenerateFood () {
		Vector2Int pos = grid.GetRandomFreePoint ();
		foodPool.Spawn (pos, Quaternion.identity);
	}

	private void OnDrawGizmos() {
		if (showGizmosOnEditor) grid.DrawGizmos();
	}
}