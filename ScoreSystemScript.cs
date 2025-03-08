using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystemScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    private float score;
    private bool shouldCount = true;

    // Update is called once per frame
    void Update()
    {
        if(!shouldCount) { return ;}

        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public void StartTimer()
    {
        shouldCount = true;
    }

    public int EndTimer()
    {
        shouldCount = false;

        scoreText.text = string.Empty;

        return Mathf.FloorToInt(score);
    }
}
