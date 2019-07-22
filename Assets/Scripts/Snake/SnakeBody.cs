using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SnakeBody : SnakePart {

	[SerializeField] private Sprite baseSprite = null;
	[SerializeField] private Sprite turnSprite = null;
	private SpriteRenderer spriteRend;

	new private void Start () {
		base.Start ();
		spriteRend = GetComponentInChildren<SpriteRenderer> ();
		UnityEngine.Assertions.Assert.IsNotNull (spriteRend);
	}

	protected override void UpdateSnakeVisual (Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection) {
		float angle;
		if (fromDirection == toDirection) {
			spriteRend.sprite = baseSprite;
			angle = toDirection.Angle - Constants.SPRITES_ANGLE_OFFSET;
		} else {
			WorldDirection from = fromDirection.Side;
			WorldDirection to = toDirection.Side;
			angle = GetAngleBetweenDirections (from, to);
			spriteRend.sprite = turnSprite;
		}
		rb.MoveRotation (angle);
	}

	private float GetAngleBetweenDirections (WorldDirection from, WorldDirection to) {
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

	private bool GetTurnSide (WorldDirection to) {
		return to == WorldDirection.Up || to == WorldDirection.Right;
	}
}