using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableScore", menuName = "Snake/ScriptableScore", order = 5)]
public class ScriptableScore : ScriptableObject {
	public int value;
	public int highScore;

	public bool CheckNewRecord() {
		if (value > highScore) {
			highScore = value;
			return true;
		}
		return false;
	}
}