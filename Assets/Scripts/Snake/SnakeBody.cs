#pragma warning disable 649
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D), typeof (Animator))]
public class SnakeBody : MonoBehaviour {

	public Rigidbody2D rb { get; private set; }
	public ScriptableDirection direction { get; private set; }
	private Animator animator;

	private bool isTail;

	// Start is called before the first frame update
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	public void UpdateSnakeBody (Vector2Int position, ScriptableDirection direction) {
		UpdateSnakeAnimation (position, direction);
		rb.MovePosition (position);
		this.direction = direction;
	}

	private void UpdateSnakeAnimation (Vector2Int position, ScriptableDirection dir) {
		if (isTail) {
			animator.SetTrigger (dir.name);
			return;
		}

		if (direction != dir) {
			int nextStep = GetTurnSide(dir.Side);
			animator.SetInteger (String.Format ("From{0}", direction.Side), nextStep);
		}
	}

	private int GetTurnSide (WorldDirection to) {
		return (to == WorldDirection.Up || to == WorldDirection.Right) ? 1 : -1;
	}
}