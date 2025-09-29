using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public int score;
    public TMP_Text scoreDisplay;
    // public int addScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // score = score + addScore;
        // addScore = 0;
        scoreDisplay.text = score.ToString();
    }

    public void addScore(int gain)
    {
        score = score + gain;
    }
}
