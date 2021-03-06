﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : SnakePart {

	#region Inspector fields
	[Header ("Snake Setup")]
	[SerializeField] private ScriptableDirection startDirection = null;
	[SerializeField] private ScriptableOptions options = null;

	[Header ("Snake Parts")]
	[SerializeField] private GameObject TailPrefab = null;
	[SerializeField] private PoolManager snakePool = null;

	[Header ("Grid navigation")]
	[SerializeField] private LayerMask layerMask = default (LayerMask);
	[SerializeField] private ScriptableGrid grid = null;

	[Header ("Events")]
	[SerializeField] private ScriptableGameEvent onEetFood = null;
	[SerializeField] private ScriptableGameEvent onGameOver = null;
	#endregion

	#region hided fields
	private ScriptableDirection nextDirection;
	private List<SnakePart> snakeParts;
	private SpriteRenderer spriteRenderer;
	private float movementTimeout;
	private int movementTimeoutLevel;
	private int eatedFoods;
	private bool justAte;
	private bool isAlive;
	private bool updateSnake;
	#endregion

	#region MonoBehaviour Methods
	private void Awake () {
		updateSnake = false;
		justAte = false;
		isAlive = true;
		eatedFoods = 0;

		movementTimeoutLevel = options.gameLevel;
		SetMovementTimeout (movementTimeoutLevel);

		snakeParts = new List<SnakePart> ();
		currentDirection = startDirection;
		Direction = startDirection;
	}

	private void Start () {
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

		CreateSnakeParts ();
		grid.RemoveAvaliableCell (Vector2Int.FloorToInt (rb.position));
		StartCoroutine (SnakeUpdateCoroutine ());
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Food")) {
			ToEat ();
		}
	}

	private void OnDrawGizmos () {
		Vector2 dir = (nextDirection == null) ? startDirection.Value : nextDirection.Value;
		Gizmos.color = Color.blue;

		Vector2 start = (Vector2) transform.position + dir * 0.5f;
		Vector2 end = (Vector2) transform.position + dir;
		Gizmos.DrawLine (start, end);
	}
	#endregion

	private void CreateSnakeParts () {
		//add body
		Vector3Int pos = Vector3Int.FloorToInt (transform.position);
		pos.x -= startDirection.Value.x;
		pos.y -= startDirection.Value.y;
		snakeParts.Add (snakePool.Spawn<SnakePart> (pos, transform.rotation));
		grid.RemoveAvaliableCell (pos);

		//add tail
		pos.x -= startDirection.Value.x;
		pos.y -= startDirection.Value.y;
		SnakePart tail = Instantiate (TailPrefab, pos, transform.rotation).GetComponent<SnakePart> ();
		snakeParts.Add (tail);
		grid.RemoveAvaliableCell (pos);
	}

	private IEnumerator SnakeUpdateCoroutine () {
		while(isAlive) {
			yield return new WaitForSeconds (movementTimeout);
			updateSnake = true;
		}
		onGameOver.Call ();
	}


	private void FixedUpdate () {
		//this is here, on update and not no SnakeUpateCoroutine
		//because a rendering issue for, even the WaitForFixedUpdate
		//doesn't fixed. But rotating and change the sprite together
		//on fixed update has fixed it
		if (isAlive && updateSnake) {
			if (CheckPath ()) {
				SnakeUpdate ();
				updateSnake = false;
			} else {
				isAlive = false;
			}
		}
	}

	private void SnakeUpdate () {
		SnakePart tail = snakeParts[snakeParts.Count - 1];
		SnakePart lastBodyPart = snakeParts[snakeParts.Count - 2];

		//Update Tail
		if (!justAte) {
			grid.AddAvaliableCell (Vector2Int.FloorToInt (tail.rb.position));
			Vector2Int tailPosition = Vector2Int.FloorToInt (lastBodyPart.rb.position);
			tail.UpdateSnakePart (tailPosition, lastBodyPart.currentDirection);
		} else {
			justAte = false;
		}

		//update Neck
		lastBodyPart.UpdateSnakePart (Vector2Int.FloorToInt (rb.position), nextDirection, currentDirection);
		snakeParts.Remove (lastBodyPart);
		snakeParts.Insert (0, lastBodyPart);

		//Update head
		Vector2Int headPosition = Vector2Int.FloorToInt (rb.position) + nextDirection.Value;
		UpdateSnakePart (headPosition, nextDirection, currentDirection);
		grid.RemoveAvaliableCell (headPosition);
	}

	protected override void UpdateSnakeVisual (Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection) {
		if (fromDirection != toDirection) {
			float angle = toDirection.Angle - Constants.SPRITES_ANGLE_OFFSET;
			//spriteRenderer.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
			rb.MoveRotation (angle);
		}
	}

	/// <summary>
	/// return true if the snake can move
	/// </summary>
	private bool CheckPath () {
		Vector2 start = rb.position + (Vector2) nextDirection.Value * 0.5f;
		Vector2 end = rb.position + nextDirection.Value;

		RaycastHit2D hit = Physics2D.Linecast (start, end, layerMask);

		return hit && !hit.collider.CompareTag ("Player");
	}

	public ScriptableDirection Direction {
		set {
			Vector2 currentDir = currentDirection.Value;
			//on can change direction if the new direction
			//is not the opposite of the current direction
			if (value.Value != -currentDir)
				nextDirection = value;
		}
	}

	private void ToEat () {
		justAte = true;
		eatedFoods++;

		//check eated foods to increase speed
		if (eatedFoods % 3 == 0) {
			movementTimeoutLevel = Mathf.Clamp (movementTimeoutLevel + 1, Constants.MIN_LEVEL, Constants.MAX_LEVEL);
			SetMovementTimeout (movementTimeoutLevel);
		}

		//get the last body and tail
		int tailIndex = snakeParts.Count - 1;
		int lastBodyIndex = snakeParts.Count - 2;
		SnakePart tail = snakeParts[tailIndex];
		SnakePart lastBody = snakeParts[lastBodyIndex];

		//get position and rotation for new body part
		Vector2Int pos = Vector2Int.FloorToInt (lastBody.rb.position);
		Quaternion rot = lastBody.transform.rotation;

		//get directions for new body part
		ScriptableDirection from = snakeParts[tailIndex].currentDirection;
		ScriptableDirection to = snakeParts[lastBodyIndex].currentDirection;

		//spawning and setting up the new body part
		SnakePart newPart = snakePool.Spawn<SnakePart> (pos, Quaternion.identity);
		newPart.currentDirection = from;
		newPart.UpdateSnakePart (pos, to, from);
		snakeParts.Insert (lastBodyIndex, newPart);
		onEetFood.Call ();
	}

	private void SetMovementTimeout (int level) {
		movementTimeout = 1f - level / Constants.MOVEMENT_TIMEOUT_DIVISOR;
	}

}