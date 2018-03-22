using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager;
    public bool gameOver = true;
    public GameObject player;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update ()
    {
		if(gameOver && Input.GetMouseButtonDown(0))
        {
            Instantiate(player, Vector3.zero, Quaternion.identity);
            gameOver = false;

            _uiManager.hideTitle();
            _uiManager.showScore();
            _uiManager.showLives();
            _uiManager.resetScore();

        }
	}

    public bool getGameOver()
    {
        return gameOver;
    }
}
