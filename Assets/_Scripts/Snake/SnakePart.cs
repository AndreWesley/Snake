using UnityEngine;

[RequireComponent (typeof (Rigidbody2D), typeof (BoxCollider2D))]
public abstract class SnakePart : MonoBehaviour {

	public Rigidbody2D rb { get; protected set; }

	[HideInInspector] public ScriptableDirection currentDirection;

	protected void OnEnable () {
		if (!rb) {
			rb = GetComponent<Rigidbody2D> ();
		}
	}

	public virtual void UpdateSnakePart (Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection = null) {
		UpdateSnakeVisual (position, toDirection, fromDirection);
		rb.MovePosition (position);
		this.currentDirection = toDirection;
	}

	protected abstract void UpdateSnakeVisual (Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection = null);
}