using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
	[SerializeField] private AudioMixer mixer = null;
	[SerializeField] private ScriptableOptions options = null;
	private Dictionary<string, float> audioDictionary;

	private void Start () {
		audioDictionary = new Dictionary<string, float>();
		audioDictionary.Add(Constants.Settings.MUSIC_KEY, options.musicVolume);
		audioDictionary.Add(Constants.Settings.SFX_KEY, options.sfxVolume);

		StartCoroutine(AwakeAudio(Constants.Settings.MUSIC_KEY, options.musicVolume));
		StartCoroutine(AwakeAudio(Constants.Settings.SFX_KEY, options.sfxVolume));
	}

	private IEnumerator AwakeAudio(string volumeKey, float volumeTarget) {
		float vol = 0f;
		float targetVolume = Mathf.Abs(volumeTarget) - Constants.EPSILON;
		
		while (Mathf.Abs(vol) < targetVolume) {
			vol += Time.fixedDeltaTime;
			SetAudioVolume (volumeKey, vol);
			yield return new WaitForFixedUpdate();
		}
		SetAudioVolume (volumeKey, volumeTarget);
	}

	private IEnumerator SleepAudio(string volumeKey) {
		float vol = audioDictionary[volumeKey];
		
		while (vol > Constants.EPSILON) {
			float factor = Time.fixedDeltaTime / Constants.FADE_TIME;
			vol = Mathf.Clamp (vol - factor, Constants.EPSILON, options.musicVolume);
			SetAudioVolume (volumeKey, vol);
			yield return new WaitForFixedUpdate();
		}
		SetAudioVolume (volumeKey, Constants.EPSILON);
	}

	public void Sleep() {
		StartCoroutine(SleepAudio(Constants.Settings.MUSIC_KEY));
		StartCoroutine(SleepAudio(Constants.Settings.SFX_KEY));
	}

	private void SetAudioVolume (string key, float value) {
		audioDictionary[key] = value;
		float newVolume = Mathf.Log10 (value) * 20f;
		mixer.SetFloat (key, newVolume);
	}

	public void UpdateAudio() {
		SetAudioVolume(Constants.Settings.MUSIC_KEY, options.musicVolume);
		SetAudioVolume(Constants.Settings.SFX_KEY, options.sfxVolume);
	}
}