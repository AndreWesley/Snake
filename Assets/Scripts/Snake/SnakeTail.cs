using UnityEngine;

public class SnakeTail : SnakePart {
	protected override void UpdateSnakeVisual (Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection = null) {
		if (currentDirection != toDirection) {
			rb.MoveRotation (toDirection.Angle - Constants.SPRITES_ANGLE_OFFSET);
		}
	}
}