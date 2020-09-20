using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	[SerializeField] private Text gameOver = null;
	[SerializeField] private Text scoreText = null;
	[SerializeField] private Text highScoreText = null;
	[SerializeField] private ScriptableScore score = null;

	private void Start()
    {
		//if you beat the high score, don't show
		//game over, but shows a congrats message
		if(score.CheckNewRecord()) {
			gameOver.text = Constants.NEW_RECORD;
			gameOver.color = Color.yellow;
		}

        scoreText.text = score.value.ToString();
		highScoreText.text = score.highScore.ToString();
    }
}
