using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
	[Header ("Options")]
	[SerializeField] private ScriptableOptions options = null;

	[Header ("Game Difficulty")]
	[SerializeField] private Slider difficulty = null;
	[SerializeField] private Text difficultyText = null;

	[Header ("Audio")]
	[SerializeField] private ScriptableGameEvent onAudioChange = null;
	[SerializeField] private Slider music = null;
	[SerializeField] private Slider sfx = null;

	private void Start () {
		//setup difficulty UI
		difficultyText.text = options.gameLevel.ToString ();
		difficulty.value = options.gameLevel;

		//setup audio UI
		music.value = options.musicVolume;
		sfx.value = options.sfxVolume;
	}

	#region options handlers
	public void SetDifficulty () {
		options.gameLevel = (int) difficulty.value;
		difficultyText.text = options.gameLevel.ToString ();
	}

	public void SetMusicVolume () {
		options.musicVolume = music.value;
		onAudioChange.Call ();
	}

	public void SetSFXVolume () {
		options.sfxVolume = sfx.value;
		onAudioChange.Call ();
	}
	#endregion

}