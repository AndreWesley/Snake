using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
	[SerializeField] private ScriptableGameEvent onChangeScene = null;

	public void LoadScene (string sceneName) {
		AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
		async.allowSceneActivation = false;
		StartCoroutine (ChangeScene (async));
	}

	private IEnumerator ChangeScene (AsyncOperation async) {
		onChangeScene.Call ();
		yield return new WaitForSeconds (Constants.FADE);
		async.allowSceneActivation = true;
	}
}