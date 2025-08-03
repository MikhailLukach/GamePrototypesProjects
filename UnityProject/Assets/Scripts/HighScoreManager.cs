using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    private TMP_Text _scoreText;
    private TMP_Text _highscoreText;
    int _score = 0;
    int _highscore = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _highscore = PlayerPrefs.GetInt("highscore", 0);
        _scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        _highscoreText = GameObject.Find("HighScoreText").GetComponent<TMP_Text>();
        if (_scoreText == null) 
        {
            return;
        }
        if (_highscoreText == null)
        {
            return;
        }
        _scoreText.text = "Score: " + _score.ToString();
        _highscoreText.text = "Highscore: " + _highscore.ToString();
    }

    // Update is called once per frame
    public void AddScore()
    {
        _score += 100;
        _scoreText.text = "Score: " + _score.ToString();
        if(_highscore < _score)
        {
            PlayerPrefs.SetInt("highscore", _score);
        }
    }
}
