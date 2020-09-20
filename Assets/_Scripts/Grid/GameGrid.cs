using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour {

	[SerializeField] private Transform food = null;

	[Header("Grid Settings")]
	[SerializeField] private ScriptableGrid grid = null;
	[SerializeField] private Transform origin = null;
	[SerializeField] private int rows = default (int);
	[SerializeField] private int columns = default (int);
	[SerializeField] private bool showGizmosOnEditor = false;

	#region MonoBehaviour Methods
	private void Awake () {
		Vector2 temp = origin.position;
		Vector2Int originPos = Vector2Int.FloorToInt (temp);
		Tilemap tiles = GetComponent<Tilemap>();
		grid.GenerateGrid (originPos, rows, columns, tiles);
	}

	private void Start () {
		UpdateFoodPosition ();
	}

	void OnDrawGizmos () {
		if (showGizmosOnEditor) grid.DrawGizmos ();
	}
	#endregion

	public void UpdateFoodPosition () {
		if (grid.GetAvailableCellsCount() > 0)
		{
			Vector2Int pos = grid.GetRandomFreePoint ();
			food.position = new Vector3(pos.x, pos.y, 0f);
		}
		else
		{
			food.gameObject.SetActive(false);
		}
	}

}