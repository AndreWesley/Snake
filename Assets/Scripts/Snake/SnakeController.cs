#pragma warning disable 649
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

	[SerializeField] private ScriptableOptions options;
	[SerializeField] private ScriptableDirection startDirection;
	[SerializeField] private LayerMask layerMask;

	private float movementTimeout;
	private ScriptableDirection direction;
	private ScriptableDirection currentDirection;
	private Rigidbody2D rb;
	private List<SnakeBody> snakeBodies;
	private Transform artsTransform;

	private void Start () {
		float movementTimoutDivisor = 10f;
		movementTimeout = 1f - options.gameLevel / movementTimoutDivisor;
		currentDirection = startDirection;
		Direction = startDirection;
		snakeBodies = new List<SnakeBody> ();

		artsTransform = transform.GetChild(0);
		rb = GetComponent<Rigidbody2D> ();

		StartCoroutine (Movement ());
	}

	private IEnumerator Movement () {
		while (true) {
			yield return new WaitForSeconds (movementTimeout);
			if (CheckPath ()) {
				UpdateSnakeTransforms();
			}
		}
	}

	private void UpdateSnakeTransforms() {
		for (int i = snakeBodies.Count - 1; i >= 0; i--) {
			int nextX;
			int nextY;
			ScriptableDirection bodyDirection;
			if (i != 0) {
				// neck
				nextX = (int) rb.position.x;
				nextY = (int) rb.position.y;
				bodyDirection = currentDirection;
			} else {
				// body and tail
				nextX = (int) snakeBodies[i - 1].transform.position.x;
				nextY = (int) snakeBodies[i - 1].transform.position.y;
				bodyDirection = snakeBodies[i - 1].direction;
			}
			snakeBodies[i].UpdateSnakeBody (new Vector2Int (nextX, nextY), bodyDirection);
		}
		UpdateHeadDirection ();
		rb.MovePosition(rb.position + currentDirection.Value);
	}

	private void UpdateHeadDirection () {
		currentDirection = direction;
		float angle = default(float);

		switch (currentDirection.Side) {
			case WorldDirection.Up:
				angle = 0f;
				break;
			case WorldDirection.Down:
				angle = 180f;
				break;
			case WorldDirection.Left:
				angle = 90f;
				break;
			case WorldDirection.Right:
				angle = 270f;
				break;
		}
		artsTransform.rotation = Quaternion.Euler(0f,0f,angle);
	}

	private bool CheckPath () {
		Vector2 start = rb.position + (Vector2) direction.Value * 0.5f;
		Vector2 end = rb.position + direction.Value;
		return Physics2D.Linecast (start, end, layerMask);
	}

	public ScriptableDirection Direction {
		set {
			Vector2 currentDir = currentDirection.Value;
			if (value.Value != -currentDir)
				direction = value;
		}
	}

	private void OnDrawGizmos () {
		Vector2 dir = (direction == null) ? startDirection.Value : direction.Value;
		Gizmos.color = Color.blue;

		Vector2 start = (Vector2) transform.position + dir * 0.5f;
		Vector2 end = (Vector2) transform.position + dir;
		Gizmos.DrawLine (start, end);
	}
}