using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text numberOfNuts;
    public TMP_Text timeText;


    private void Start()
    {
        UpdateScore();
    }
    private void OnEnable()
    {
        Player.OnWin += UpdateScore;
    }

    private void OnDisable()
    {
        Player.OnWin -= UpdateScore;
    }
    public void UpdateScore()
    {
        int scorePoints = PlayerPrefs.GetInt("Score");
        score.text = "Score: " + scorePoints;
        string nutCount = PlayerPrefs.GetString("Nuts");
        numberOfNuts.text = nutCount;
        string timeCount = PlayerPrefs.GetString("timeCount");
        timeText.text = "Time: " + timeCount;
    }

}
