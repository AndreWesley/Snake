using System;
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
	[SerializeField] private Slider music = null;
	[SerializeField] private Slider sfx = null;
	[SerializeField] private ScriptableGameEvent onAudioChange = null;

	private void Start () {
		//setup difficulty UI
		difficultyText.text = options.gameLevel.ToString ();
		difficulty.value = options.gameLevel;
		difficulty.onValueChanged.AddListener(SetDifficulty);

		//setup music UI
		music.value = Mathf.RoundToInt(music.maxValue * options.musicVolume);
		music.onValueChanged.AddListener(SetMusicVolume);

		//setup sfx UI
		sfx.value = Mathf.RoundToInt(sfx.maxValue * options.sfxVolume);
		sfx.onValueChanged.AddListener(SetSFXVolume);
	}

	#region options handlers
	public void SetDifficulty (float f) {
		options.gameLevel = (int) f;
		difficultyText.text = options.gameLevel.ToString();
	}

	public void SetMusicVolume (float f) {
		float vol = f / music.maxValue;
		options.musicVolume = Mathf.Clamp(vol,Constants.EPSILON,1f);
		onAudioChange.Call ();
	}

	public void SetSFXVolume (float f) {
		float vol = f / sfx.maxValue;
		options.sfxVolume = Mathf.Clamp(vol, Constants.EPSILON, 1f);
		onAudioChange.Call ();
	}
	#endregion

}