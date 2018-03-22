using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPreFab;
    [SerializeField]
    private GameObject[] _powerUps;
    private float _randx;
    private int _randPowerUp;
    private int _shipSpawnTimer = 10;
    private int _shipCounter;
    private GameManager _gameManager;

    // Use this for initialization
    void Start ()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void startSpawn()
    {
        StartCoroutine(spawnEnemy());
        StartCoroutine(spawnPowerUp());
    }
	
    public IEnumerator spawnEnemy()
    {
        while (_gameManager.gameOver == false)
        {
            if(_shipCounter % 5 == 0 && _shipSpawnTimer > 2)
            {
                _shipSpawnTimer--;
            }
            yield return new WaitForSeconds(_shipSpawnTimer);
            _randx = Random.Range(-7, 7);
            Instantiate(_enemyShipPreFab, transform.position = new Vector3(_randx, 7, 0), Quaternion.identity);
            _shipCounter++;
        }
    }

    public IEnumerator spawnPowerUp()
    {
        while (_gameManager.gameOver == false)
        {
            yield return new WaitForSeconds(15);
            _randx = Random.Range(-7, 7);
            _randPowerUp = Random.Range(0, 3);
            Instantiate(_powerUps[_randPowerUp], transform.position = new Vector3(_randx, 7, 0), Quaternion.identity);
        }
    }

}
