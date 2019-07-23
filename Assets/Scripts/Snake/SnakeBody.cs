using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SnakeBody : SnakePart {

	[SerializeField] private Sprite baseSprite = null;
	[SerializeField] private Sprite turnedSnakeSprite = null;
	private SpriteRenderer spriteRend;

	#region MonoBehaviour Method
	new protected void OnEnable () {
		base.OnEnable ();

		if (!spriteRend) {
			spriteRend = GetComponentInChildren<SpriteRenderer> ();
		}
	}
	#endregion

	#region snake body methods
	protected override void UpdateSnakeVisual (Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection) {
		float angle;
		if (fromDirection == toDirection) {
			spriteRend.sprite = baseSprite;
			angle = toDirection.Angle - Constants.SPRITES_ANGLE_OFFSET;
		} else {
			spriteRend.sprite = turnedSnakeSprite;
			angle = GetTurnedSnakeAngle (fromDirection.Side, toDirection.Side);
		}
		//spriteRend.transform.rotation = Quaternion.Euler (Vector3.forward * angle);
		rb.MoveRotation (angle);
	}

	/// <summary>
	/// return the angle for turned sprite between "from" and "to" directions
	/// </summary>
	/// <param name="from">direction 'from' where it is coming</param>
	/// <param name="to">direction 'to' where it is going</param>
	private float GetTurnedSnakeAngle (WorldDirection from, WorldDirection to) {
		switch (from) {
			case WorldDirection.Up:
				return GetTurnSide (to) ?
					Constants.AngleFromToDirection.UP_TO_RIGHT :
					Constants.AngleFromToDirection.UP_TO_LEFT;
			case WorldDirection.Down:
				return GetTurnSide (to) ?
					Constants.AngleFromToDirection.DOWN_TO_RIGHT :
					Constants.AngleFromToDirection.DOWN_TO_LEFT;
			case WorldDirection.Left:
				return GetTurnSide (to) ?
					Constants.AngleFromToDirection.DOWN_TO_RIGHT :
					Constants.AngleFromToDirection.UP_TO_RIGHT;
			case WorldDirection.Right:
				return GetTurnSide (to) ?
					Constants.AngleFromToDirection.DOWN_TO_LEFT :
					Constants.AngleFromToDirection.UP_TO_LEFT;
		}
		return 0f;
	}

	/// <summary>
	/// returns true if the snake is going to up or right or false to left or down
	/// </summary>
	/// <param name="to">direction to where the snake is going</param>
	private bool GetTurnSide (WorldDirection to) {
		return to == WorldDirection.Up || to == WorldDirection.Right;
	}
	#endregion
}