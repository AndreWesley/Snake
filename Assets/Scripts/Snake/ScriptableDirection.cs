using UnityEngine;

[CreateAssetMenu (fileName = "Direction", menuName = "Snake/Direction", order = 0)]
public class ScriptableDirection : ScriptableObject {
	[SerializeField] private Vector2Int value = default(Vector2Int);
	[SerializeField] private WorldDirection side = default(WorldDirection);
	[SerializeField] private float angle = default(float);

	public Vector2Int Value {
		get { return value; }
	}

	public WorldDirection Side {
		get { return side; }
	}

	public float Angle {
		get { return angle; }
	}
}