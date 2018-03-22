using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] livesImages;
    public Image livesDisplay;
    public Text scoreText;
    public GameObject scoreGameObject;
    public GameObject livesGameObject;
    public GameObject titleGameObject;
    public int score;

    public void updateLives(int lives)
    {
        livesDisplay.sprite = livesImages[lives];
    }

    public void updateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void resetScore()
    {
        score = 0;
        scoreText.text = "Score: 0";
    }

    public void hideTitle()
    {
        titleGameObject.SetActive(false);
    }

    public void showTitle()
    {
        titleGameObject.SetActive(true);
    }

    public void hideScore()
    {
        scoreGameObject.SetActive(false);
    }

    public void showScore()
    {
        scoreGameObject.SetActive(true);
    }

    public void hideLives()
    {
        livesGameObject.SetActive(false);
    }

    public void showLives()
    {
        livesGameObject.SetActive(true);
    }
}
