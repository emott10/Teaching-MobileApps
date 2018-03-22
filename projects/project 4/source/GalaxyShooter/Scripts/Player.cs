using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    public bool tripleShot;
    public bool speedBoast;
    public bool shieldActive;
    public int life = 3;

    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPreFab;
    [SerializeField]
    private GameObject _explosionPreFab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private float fireRate = 0.25f;
    private float fireTimer;
    [SerializeField]
    private float speed = 8.0f;
    [SerializeField]
    private float _speedBoastValue = 1.5f;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    // Use this for initialization
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_uiManager != null)
        {
            _uiManager.updateLives(life);
        }

        if (_spawnManager != null)
        {
            _spawnManager.startSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {


            if (Time.time > fireTimer)
            {
                if (tripleShot)
                {
                    Instantiate(_tripleShotPreFab, transform.position, Quaternion.identity);
                }

                else
                {
                    Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                }

                fireTimer = Time.time + fireRate;
            }
        }
    }

    private void Movement()
    {
        float horizontalTouch = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalTouch = CrossPlatformInputManager.GetAxis("Vertical");


        if (speedBoast)
        {
            transform.Translate(Vector3.right * speed * _speedBoastValue * horizontalTouch * Time.deltaTime);
            transform.Translate(Vector3.up * speed * _speedBoastValue * verticalTouch * Time.deltaTime);
        }

        transform.Translate(Vector3.right * speed * horizontalTouch * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalTouch * Time.deltaTime);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y < -4.35)
        {
            transform.position = new Vector3(transform.position.x, -4.35f, 0);
        }

        if (transform.position.x < -8.95)
        {
            transform.position = new Vector3(8.8f, transform.position.y, 0);
        }

        else if (transform.position.x > 9)
        {
            transform.position = new Vector3(-8.95f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (shieldActive)
        {
            shieldActive = false;
            _shieldGameObject.SetActive(false);
        }

        else
        {
            life--;
            
            if (life >= 0)
            {
                _uiManager.updateLives(life);
            }

            else
            {
                killPlayer();
            }
        }
    }

    public void killPlayer()
    {
        Instantiate(_explosionPreFab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        _gameManager.gameOver = true;
        _uiManager.showTitle();
        _uiManager.hideLives();
    }

    public void enableShield()
    {
        shieldActive = true;
        _shieldGameObject.SetActive(true);
    }

    public void tripleShotTurnOn()
    {
        tripleShot = true;
        StartCoroutine(TripleShotTurnOff());
    }

    public IEnumerator TripleShotTurnOff()
    {
        yield return new WaitForSeconds(5);
        tripleShot = false;
    }

    public void speedBoastTurnOn()
    {
        speedBoast = true;
        StartCoroutine(SpeedBoastTurnOff());
    }

    public IEnumerator SpeedBoastTurnOff()
    {
        yield return new WaitForSeconds(5);
        speedBoast = false;
    }

}
