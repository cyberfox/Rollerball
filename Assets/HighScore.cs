using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore ();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }
}
