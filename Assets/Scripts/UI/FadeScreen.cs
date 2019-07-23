using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeScreen : MonoBehaviour
{
	private Image blackScreen;

    private void Start()
    {
		blackScreen = GetComponent<Image>();
		blackScreen.color = Color.black;
		blackScreen.raycastTarget = false;
		Fade(false);
    }

	public void Fade(bool darken) {
		float fadeFactor = darken ? 1f : 0f;
		blackScreen.CrossFadeAlpha (fadeFactor, Constants.FADE_TIME, true);
	}
}
