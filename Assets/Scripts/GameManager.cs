using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text[] TextArray;
    public Text LinesCount;
    public Text ScoreL;
    public int LineCount;
    private int score;

    private void Start()
    {
        LineCount = 0;
        score = 0;
        LinesCountP();
        ScoreLine(score);
    }
    // Start is called before the first frame update
    public void AddPoint(int i)
    {
        TextArray[i].text = (Convert.ToInt32(TextArray[i].text) + 1).ToString();
    }

    public void LinesCountP()
    {
        LinesCount.text = "LINES - " + LineCount.ToString();
    }

    public void ScoreLine(int scoreI)
    {
        score += scoreI;
        ScoreL.text = "SCORE - " + score.ToString();
    }
}
