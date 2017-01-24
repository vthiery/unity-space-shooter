using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    public int initialScore;
    public int currentScore;

    public GUIText scoreText;

    public void Start()
    {
        currentScore = initialScore;
    }

    public void Update()
    {
        scoreText.text = "Score: " + currentScore;
    }

    public void AddScore(int increment)
    {
        currentScore += increment;
    }
}
