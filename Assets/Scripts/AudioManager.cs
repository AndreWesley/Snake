using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	[SerializeField] private AudioMixer mixer = null;
	[SerializeField] private ScriptableOptions options = null;

	private AudioSource audioSource;
	private Dictionary<string, float> audioDictionary;

	#region MonoBehaviour methods
	private void Awake () {
		audioDictionary = new Dictionary<string, float> ();
	}

	private void Start () {
		audioSource = GetComponent<AudioSource> ();

		//Add music and sfx keys if they doesn't exists
		if (!audioDictionary.ContainsKey (Constants.Settings.MUSIC_KEY)) {
			audioDictionary.Add (Constants.Settings.MUSIC_KEY, options.musicVolume);
		}
		if (!audioDictionary.ContainsKey (Constants.Settings.SFX_KEY)) {
			audioDictionary.Add (Constants.Settings.SFX_KEY, options.sfxVolume);
		}

		//increase music and sfx volume from 0 to maximum determinated on options
		StartCoroutine (AwakeAudio (Constants.Settings.MUSIC_KEY, options.musicVolume));
		StartCoroutine (AwakeAudio (Constants.Settings.SFX_KEY, options.sfxVolume));
	}
	#endregion

	#region sleep-awake
	#region coroutines
	private IEnumerator AwakeAudio (string volumeKey, float volumeTarget) {
		//not zero to avoid problems with decibels conversion
		float vol = Constants.EPSILON;

		//increase audio with time until hit expected value
		while (Mathf.Abs (vol) < volumeTarget) {
			vol += Time.fixedDeltaTime;
			SetAudioVolume (volumeKey, vol);
			yield return new WaitForFixedUpdate ();
		}

		//update audio to exactly expected
		//audio to avoid some minor error
		SetAudioVolume (volumeKey, volumeTarget);
	}

	private IEnumerator SleepAudio (string volumeKey) {
		float vol = audioDictionary[volumeKey];

		//decrease audio to zero 
		while (vol > Constants.EPSILON) {
			float factor = Time.fixedDeltaTime / Constants.FADE;

			//don't allow volume be 0 to avoid problems with decibels converstion
			vol -= Mathf.Clamp (vol - factor, Constants.EPSILON, options.musicVolume);
			SetAudioVolume (volumeKey, vol);
			yield return new WaitForFixedUpdate ();
		}
		SetAudioVolume (volumeKey, Constants.EPSILON);
	}
	#endregion

	public void Sleep () {
		StartCoroutine (SleepAudio (Constants.Settings.MUSIC_KEY));
		StartCoroutine (SleepAudio (Constants.Settings.SFX_KEY));
	}
	#endregion

	#region audio handle
	//the math here is to make the right converstion to decibels
	private void SetAudioVolume (string key, float value) {
		audioDictionary[key] = value;
		float newVolume = Mathf.Log10 (value) * 20f;
		mixer.SetFloat (key, newVolume);
	}

	public void UpdateAudio () {
		SetAudioVolume (Constants.Settings.MUSIC_KEY, options.musicVolume);
		SetAudioVolume (Constants.Settings.SFX_KEY, options.sfxVolume);
	}

	public void GameOverMusic () {
		StartCoroutine (SlowAudio ());
	}

	private IEnumerator SlowAudio () {
		//slow down the audio speed to make a
		//better game feel for game over time
		while (audioSource.pitch > Constants.FADE) {
			audioSource.pitch -= Time.fixedDeltaTime / Constants.FADE;
			yield return new WaitForFixedUpdate ();
		}
	}
	#endregion
}