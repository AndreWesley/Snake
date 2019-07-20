using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour {
	[SerializeField] private Transform origin;
	[SerializeField] private int rows, columns;
	public HashSet<Vector2Int> grid;
	public List<BoxCollider2D> boxes;

	void Start () {
		grid = new HashSet<Vector2Int> ();
		SetupGrid();
	}

	private void SetupGrid () {
		for (int r = 0; r < columns; r++) {
			for (int c = 0; c < rows; c++) {
				int x = (int) origin.position.x + c;
				int y = (int) origin.position.y + r;
				grid.Add (new Vector2Int (x, y));

				BoxCollider2D box = gameObject.AddComponent<BoxCollider2D> ();
				box.offset = origin.localPosition + new Vector3(c,r,0f);
				box.size = Vector2.one * .9f;
				boxes.Add(box);
			}
		}
	}

	public Vector2Int GetRandomFreePoint () {
		return grid.ElementAt (Random.Range (0, grid.Count));
	}
	
	private void OnTriggerEnter2D (Collider2D other) {
//		Vector2 pos = other.transform.position;
//		grid.Add(new Vector2Int((int) pos.x, (int) pos.y));

		for (int i = 0; i < boxes.Count; i++)
		{
 			Vector2 p = (Vector2) other.transform.position;

			print("boxes: " + boxes[i].offset);
			print("posic: " + new Vector2Int((int) p.x, (int) p.y));
			print("---------------------------------------");
			if (boxes[i].offset == (Vector2) (other.transform.position + origin.localPosition)) {
				boxes[i].size = Vector2.one * 0.5f;
			}
		}
	}
	private void OnTriggerExit2D (Collider2D other) {
//		Vector2 pos = other.transform.position;
//		grid.Remove(new Vector2Int ((int) pos.x, (int) pos.y));

		for (int i = 0; i < boxes.Count; i++)
		{
			if (boxes[i].offset == (Vector2) (other.transform.position - origin.localPosition)) {
				boxes[i].size = Vector2.one * 0.9f;
			}
		}
	}
}