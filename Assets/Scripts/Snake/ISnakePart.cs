using UnityEngine;

public interface ISnakePart
{
	void UpdateSnakePart(Vector2Int position, ScriptableDirection toDirection, ScriptableDirection fromDirection = null);
}