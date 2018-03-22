using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    private Vector3 _playerStartPos;

    public GameObject cam;
    public LevelGenerator level;

	// Use this for initialization
	void Start ()
    {
        _playerStartPos = player.transform.position;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ResartGame()
    {
        StartCoroutine("RestartGameCoRoutine");
    }

    private IEnumerator RestartGameCoRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        player.transform.position = _playerStartPos;
        cam.transform.position = _playerStartPos;
        level.Start();
    }
}
