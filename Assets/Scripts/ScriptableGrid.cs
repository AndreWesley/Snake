using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName = "ScriptableGrid", menuName = "Snake/ScriptableGrid", order = 0)]
public class ScriptableGrid : ScriptableObject {

	private HashSet<Vector2Int> avaliableCells = default (HashSet<Vector2Int>);

	public void GenerateGrid (Vector2Int origin, int rows, int columns) {
		avaliableCells = new HashSet<Vector2Int> ();
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < columns; c++) {
				int x = origin.x + c;
				int y = origin.y + r;
				avaliableCells.Add (new Vector2Int (x, y));
			}
		}
	}

	public Vector2Int GetRandomFreePoint () {
		return avaliableCells.ElementAt (Random.Range (0, avaliableCells.Count));
	}

	public void Remove (Vector2Int cell) {
		avaliableCells.Remove (cell);
	}

	public void Remove (Vector3Int cell) {
		avaliableCells.Remove (new Vector2Int (cell.x, cell.y));
	}

	public void Add (Vector2Int cell) {
		avaliableCells.Add (cell);
	}

	public void DrawGizmos () {
		Gizmos.color = new Color (1f, 0f, 0f, .25f);
		if (avaliableCells != default (HashSet<Vector2Int>)) {
			foreach (var item in avaliableCells) {
				Vector3 v = new Vector3 (item.x, item.y, 1f);
				Gizmos.DrawCube (v, Vector3.one * 0.9f);
			}
		}
	}
}