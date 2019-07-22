using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableScore", menuName = "Snake/ScriptableScore", order = 5)]
public class ScriptableScore : ScriptableObject, ISerializationCallbackReceiver {
	public int value;
	public int highScore;

	public void OnAfterDeserialize() {}

	public void OnBeforeSerialize() {
		value = 0;
	}

	public bool CheckNewRecord() {
		if (value > highScore) {
			highScore = value;
			return true;
		}
		return false;
	}
}