using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Animator))]
public class Menu : MonoBehaviour {

	[SerializeField] private Animator currentMenu = null;
	[SerializeField] private FadeScreen blackScreen = null;
	[SerializeField] private AudioManager audioManager = null;

	public void LoadScene (string sceneName) {
		AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
		async.allowSceneActivation = false;
		StartCoroutine(ChangeScene(async));
	}

	private IEnumerator ChangeScene(AsyncOperation async) {
		blackScreen.Fade(true);
		audioManager.Sleep();
		yield return new WaitForSeconds(Constants.FADE_TIME);
		async.allowSceneActivation = true;
	}

	public void ChangeMenuTo (Animator menu) {
		currentMenu.SetTrigger ("Exit");
		menu.SetTrigger ("Enter");

		currentMenu = menu;
	}
}