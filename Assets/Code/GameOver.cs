using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Text Text;

    private int _score;
    private int _time;
    
    void ShowText()
    {
        Text.text = "Your Score : " + Convert.ToString(_score) + Environment.NewLine + "Time : "+Convert.ToString(_time);
    }

    // Update is called once per frame
    void Update()
    {
        _score = GameManager.Instance.GetScore();
        _time = GameManager.Instance.GetTime();
        ShowText();
    }
}
