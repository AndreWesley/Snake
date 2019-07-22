using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	[SerializeField] private Text scoreText;
	[SerializeField] private ScriptableScore score;
	[SerializeField] private ScriptableOptions options;

	public void AddScore() {
		score.value += options.gameLevel;
		scoreText.text = score.value.ToString();
	}

}
