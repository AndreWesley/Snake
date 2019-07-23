using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	[SerializeField] private Text scoreText = null;
	[SerializeField] private ScriptableScore score = null;
	[SerializeField] private ScriptableOptions options = null;

	private void Start() {
		score.value = 0;
	}

	public void AddScore() {
		score.value += options.gameLevel;
		scoreText.text = score.value.ToString();
	}

}
