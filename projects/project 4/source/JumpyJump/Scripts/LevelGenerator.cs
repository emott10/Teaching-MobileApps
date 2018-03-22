using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int numPlatforms = 10;
    public float max_y = 2.5f;
    public float min_y = .2f;
    public float width = 3f;

    private Vector3 platformPosition;
    private float _topPlat;
    [SerializeField]
    private GameObject _platformPreFab;

    [SerializeField]
    private Transform _Player;

    // Use this for initialization
    public void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
        Instantiate(_platformPreFab, Vector3.down, Quaternion.identity);
        platformPosition = new Vector3();
        _topPlat = 0f;
        CreatePlatforms();
    }

    private void Update()
    {
        if (_Player.position.y >= (_topPlat - 6f))
        {
            CreatePlatforms();
        }
    }

    private void CreatePlatforms()
    {
        for (int i = 0; i <= numPlatforms; i++)
        {
            platformPosition.y += Random.Range(min_y, max_y);
            platformPosition.x = Random.Range(-width, width);
            Instantiate(_platformPreFab, platformPosition, Quaternion.identity);

            if (i == numPlatforms)
            {
                _topPlat = platformPosition.y;
            }
        }
    }
}