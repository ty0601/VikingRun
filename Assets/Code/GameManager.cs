using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _score;
    private float _timef;

    private VikingController _viking;
    public static GameManager Instance;

    [SerializeField] Text scoreText;
    [SerializeField] Text timeText;

    private void Awake()
    {
        Instance = this;
        _viking = GameObject.FindObjectOfType<VikingController>();
    }

    public void AddScore()
    {
        _score++;
        scoreText.text = Convert.ToString(_score / 10) + Convert.ToString(_score % 10);
    }

    public int GetScore()
    {
        return _score;
    }

    public int GetTime()
    {
        return (int)_timef;
    }

    private void Update()
    {
        if (_viking._isStart && _viking.isAlive)
        {
            _timef += Time.deltaTime;
            timeText.text = "Time : " + Convert.ToString((int) _timef);
        }
    }
}