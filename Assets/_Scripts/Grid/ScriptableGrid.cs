using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu (fileName = "ScriptableGrid", menuName = "Snake/ScriptableGrid", order = 0)]
public class ScriptableGrid : ScriptableObject {

	[Header("Main Tile")]
	[SerializeField] private TileBase mainTile = null;
	[Range(0f,1f)] [SerializeField] private float mainTileProbability = 0.5f;
	[SerializeField] private TileBase[] tiles = null;
	private HashSet<Vector2Int> availableCells = default (HashSet<Vector2Int>);

	#region generate-draw
	public void GenerateGrid (Vector2Int origin, int rows, int columns, Tilemap tilemap) {
		availableCells = new HashSet<Vector2Int> ();
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < columns; c++) {
				int x = origin.x + c;
				int y = origin.y + r;

				availableCells.Add (new Vector2Int(x, y));
				Vector3Int pos = new Vector3Int(x-1, y, 0);

				bool isMainTile = Random.value < mainTileProbability;

				TileBase t = isMainTile ? mainTile : tiles[Random.Range(0, tiles.Length)];
				tilemap.SetTile(pos, t);
			}
		}
	}

	public void DrawGizmos () {
		Gizmos.color = new Color (1f, 0f, 0f, .25f);
		if (availableCells != default (HashSet<Vector2Int>)) {
			//this foreach will not affected game build
			//because these gizmos are used only on editor
			foreach (var item in availableCells) {
				Vector3 v = new Vector3 (item.x, item.y, 1f);
				Gizmos.DrawCube (v, Vector3.one * 0.9f);
			}
		}
	}
	#endregion

	#region available cell handler
	public int GetAvailableCellsCount()
	{
		return availableCells.Count;
	}

	public Vector2Int GetRandomFreePoint () {
		if (availableCells.Count > 0)
		{
			return availableCells.ElementAt (Random.Range (0, availableCells.Count));
		}
		else
		{
			return Vector2Int.one * -1000;
		}
	}

	public void RemoveAvaliableCell (Vector2Int cell) {
		availableCells.Remove (cell);
	}

	public void RemoveAvaliableCell (Vector3Int cell) {
		RemoveAvaliableCell (new Vector2Int (cell.x, cell.y));
	}

	public void AddAvaliableCell (Vector2Int cell) {
		availableCells.Add (cell);
	}

	public void AddAvaliableCell (Vector3Int cell) {
		Vector2Int newCell = new Vector2Int(cell.x, cell.y);
		AddAvaliableCell (newCell);
	}
	#endregion
}