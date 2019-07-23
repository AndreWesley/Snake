using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeScreen : MonoBehaviour
{
	[SerializeField] private bool startDarkAndFade = true;
	private Image blackScreen;

    private void Start()
    {
		blackScreen = GetComponent<Image>();
		blackScreen.raycastTarget = false;

		if(startDarkAndFade) {
		blackScreen.color = Color.black;
		Fade(false);
		} else {
			blackScreen.color = Color.clear;
		}
    }

	public void Fade(bool darken) {
		float fadeFactor = darken ? 1f : 0f;
		blackScreen.CrossFadeAlpha (fadeFactor, Constants.FADE, true);
	}
}
