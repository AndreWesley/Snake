using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : SnakePart {

	[SerializeField] private ScriptableGameEvent onEetFood = null;
	[SerializeField] private ScriptableGameEvent onGameOver = null;
	[SerializeField] private ScriptableGrid grid = null;
	[SerializeField] private ScriptableOptions options = null;
	[SerializeField] private ScriptableDirection startDirection = null;

	[SerializeField] private LayerMask layerMask = default (LayerMask);
	[SerializeField] private GameObject TailPrefab = null;
	[SerializeField] private PoolManager snakePool = null;

	private float movementTimeout;
	private ScriptableDirection nextDirection;
	private List<SnakePart> snakeParts;
	private bool justAte;
	private bool isAlive;

	private void Awake () {
		justAte = false;
		isAlive = true;
		float movementTimoutDivisor = 10f;
		movementTimeout = 1f - options.gameLevel / movementTimoutDivisor;
		currentDirection = startDirection;
		Direction = startDirection;
		snakeParts = new List<SnakePart> ();
	}

	new private void Start () {
		base.Start ();
		CreateSnakeParts ();
		grid.Remove (new Vector2Int ((int) rb.position.x, (int) rb.position.y));
		StartCoroutine (SnakeUpdate ());
	}

	private void CreateSnakeParts () {
		//add body
		Vector3Int pos = Vector3Int.FloorToInt (transform.position);
		pos.x -= startDirection.Value.x;
		pos.y -= startDirection.Value.y;
		snakeParts.Add (snakePool.Spawn<SnakePart> (pos, transform.rotation));

		//add tail
		pos.x -= startDirection.Value.x;
		pos.y -= startDirection.Value.y;
		SnakePart tail = Instantiate (TailPrefab, pos, transform.rotation).GetComponent<SnakePart> ();
		snakeParts.Add (tail);
	}

	private IEnumerator SnakeUpdate () {
		yield return new WaitForSeconds (movementTimeout);
		while(isAlive) {
			if (CheckPath ()) {
				UpdateSnake ();
			} else {
				isAlive = false;
			}
			yield return new WaitForSeconds (movementTimeout);
		}
		onGameOver.Call();
	}

	private void UpdateSnake () {
		int nextX;
		int nextY;

		SnakePart tail = snakeParts[snakeParts.Count - 1];
		SnakePart lastBodyPart = snakeParts[snakeParts.Count - 2];

		//Update Tail
		if (!justAte) {
			nextX = (int) lastBodyPart.transform.position.x;
			nextY = (int) lastBodyPart.transform.position.y;
			grid.Add (Vector2Int.FloorToInt (tail.rb.position));
			tail.UpdateSnakePart (new Vector2Int (nextX, nextY), lastBodyPart.currentDirection);
		} else {
			justAte = false;
		}

		//update Neck
		nextX = (int) transform.position.x;
		nextY = (int) transform.position.y;
		lastBodyPart.UpdateSnakePart (new Vector2Int (nextX, nextY), nextDirection, currentDirection);
		snakeParts.Remove (lastBodyPart);
		snakeParts.Insert (0, lastBodyPart);

		//Update head
		nextX = (int) rb.position.x + nextDirection.Value.x;
		nextY = (int) rb.position.y + nextDirection.Value.y;
		Vector2Int newPos = new Vector2Int (nextX, nextY);
		UpdateSnakePart (newPos, nextDirection, currentDirection);
		grid.Remove (newPos);
	}

	protected override void UpdateSnakeVisual (Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection) {
		if (fromDirection != toDirection) {
			rb.MoveRotation (toDirection.Angle - Constants.SPRITES_ANGLE_OFFSET);
		}
	}

	private bool CheckPath () {
		Vector2 start = rb.position + (Vector2) nextDirection.Value * 0.5f;
		Vector2 end = rb.position + nextDirection.Value;
		RaycastHit2D hit = Physics2D.Linecast (start, end, layerMask);
		return hit && !hit.collider.CompareTag ("Player");
	}

	public ScriptableDirection Direction {
		set {
			Vector2 currentDir = currentDirection.Value;
			if (value.Value != -currentDir)
				nextDirection = value;
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Food")) {
			other.gameObject.SetActive(false);
			ToEat ();
		}
	}

	private void ToEat () {
		justAte = true;

		int lastBodyPartIndex = snakeParts.Count - 2;
		Vector2 lastBodyPartPosition = snakeParts[lastBodyPartIndex].rb.position;
		Vector2Int position = Vector2Int.FloorToInt (lastBodyPartPosition);
		Quaternion rotation = snakeParts[lastBodyPartIndex].transform.rotation;

		SnakePart newPart = snakePool.Spawn<SnakePart> (position, rotation);
		newPart.currentDirection = snakeParts[lastBodyPartIndex].currentDirection;
		snakeParts.Insert (lastBodyPartIndex, newPart);
		onEetFood.Call ();
	}

	private void OnDrawGizmos () {
		Vector2 dir = (nextDirection == null) ? startDirection.Value : nextDirection.Value;
		Gizmos.color = Color.blue;

		Vector2 start = (Vector2) transform.position + dir * 0.5f;
		Vector2 end = (Vector2) transform.position + dir;
		Gizmos.DrawLine (start, end);
	}

}