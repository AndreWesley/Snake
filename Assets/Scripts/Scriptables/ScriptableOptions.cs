using UnityEngine;

[CreateAssetMenu(fileName = "Options", menuName = "Snake/Options", order = 0)]
public class ScriptableOptions : ScriptableObject {
	
	[Range(1,9)] public int gameLevel = 1;
	[Range(0f,1f)] public float musicVolume = 1f;
	[Range(0f,1f)] public float sfxVolume = 1f;

}