using UnityEngine;

public class SnakeTail : SnakePart {
	protected override void UpdateSnakeVisual (Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection = null) {
		if (currentDirection != toDirection) {
			float angle = toDirection.Angle - Constants.SPRITES_ANGLE_OFFSET;
			//spriteRend.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
			rb.MoveRotation(angle);
		}
	}
}