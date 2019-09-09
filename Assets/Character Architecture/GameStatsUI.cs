using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatsUI : MonoBehaviour
{
    public GameController game;

    public TextMeshProUGUI timePlayedUI;
    public TextMeshProUGUI scoreUI;

    void Update()
    {
        timePlayedUI.text = FormatTime(game.TimePlayed);
        scoreUI.text = "" + game.Score;
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
        //return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
