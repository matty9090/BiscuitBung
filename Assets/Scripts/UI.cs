using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Game Game = null;
    [SerializeField] private Text Score = null; 
    [SerializeField] private Text Best = null;

    public void OnScoreChanged()
    {
        Score.text = "Score: " + Game.Score;
    }

    public void OnBestChanged()
    {
        Best.text = "Best: " + Game.Best;
    }
}
