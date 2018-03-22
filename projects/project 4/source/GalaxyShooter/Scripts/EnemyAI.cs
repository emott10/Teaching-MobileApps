using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float _disappear = -7;
    [SerializeField]
    private float _appear = 7;
    [SerializeField]
    private GameObject _enemyExplosionPreFab;
    private UIManager _uiManager;
    private GameManager _gameManager;

    private float _randx;

    private void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                killEnemy();
            }
        }

        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            killEnemy();
        }
    }

    void Update ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < _disappear)
        {
            _randx = Random.Range(-7.5f, 7.5f);
            transform.position = new Vector3(_randx, _appear, 0);
        }

        if (_gameManager.gameOver == true && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
		

	}

    public void killEnemy()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager != null)
        {
            _uiManager.updateScore();
        }

        Instantiate(_enemyExplosionPreFab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
