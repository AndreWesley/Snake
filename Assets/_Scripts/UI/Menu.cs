using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Animator))]
public class Menu : MonoBehaviour {

	[SerializeField] private Animator currentMenu = null;

	public void ChangeMenuTo (Animator menuAnimator) {
		currentMenu.SetTrigger ("Exit");
		menuAnimator.SetTrigger ("Enter");

		currentMenu = menuAnimator;
	}
}