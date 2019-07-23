using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	[SerializeField] private Text gameOver;
	[SerializeField] private Text scoreText;
	[SerializeField] private Text highScoreText;
	[SerializeField] private ScriptableScore score;

    // Start is called before the first frame update
    void Start()
    {
		if(score.CheckNewRecord()) {
			gameOver.text = "CONGRATS!\nNEW RECORD!";
			gameOver.color = Color.yellow;
		}
        scoreText.text = score.value.ToString();
		highScoreText.text = score.highScore.ToString();

    }
}
