using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Button continuteButton;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private ScoreSystemScript scoreSystem;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private TMP_Text gameOverText;

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {
        AdManager.Instance.ShowAd(this);

        continuteButton.interactable = false;
    }

    public void EndGame()
    {
        asteroidSpawner.enabled = false;

        gameOverDisplay.gameObject.SetActive(true);

        int finalScore = scoreSystem.EndTimer();
        gameOverText.text = $"Your Score: {finalScore}";
    }

    public void ContinueGame()
    {
        scoreSystem.StartTimer();

        asteroidSpawner.enabled = true;

        gameOverDisplay.gameObject.SetActive(false);

        player.transform.position = Vector3.zero;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.SetActive(true);
    }
}
