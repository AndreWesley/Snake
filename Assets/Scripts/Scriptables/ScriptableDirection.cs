#pragma warning disable 649
using UnityEngine;

[CreateAssetMenu (fileName = "Direction", menuName = "Snake/Direction", order = 0)]
public class ScriptableDirection : ScriptableObject {
	[SerializeField] private Vector2Int value;
	[SerializeField] private WorldDirection side;

	public Vector2Int Value {
		get { return value; }
	}

	public WorldDirection Side {
		get { return side; }
	}
}