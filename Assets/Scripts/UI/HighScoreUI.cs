using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
	[SerializeField] private ScriptableScore score = null;
	[SerializeField] private Text highScoreText = null;

    void Start()
    {
        highScoreText.text = score.highScore.ToString();
    }
}
