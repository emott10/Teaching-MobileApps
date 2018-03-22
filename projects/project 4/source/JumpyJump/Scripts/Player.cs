using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Transform cameraPos;
    public GameManager gameManager;
    public float movementSpeed = 2.5f;
    private float left_x = -3.65f;
    private float right_x = 3.65f;

    Rigidbody2D rb;
    private float move = 0f;
    // Use this for initialization
    void Start()
    {
        cameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, 3, 0);
    }
	
	// Update is called once per frame
	void Update () 
    {
        movement();

        if (transform.position.y < cameraPos.position.y - 5.5f)
        {
            //gameManager.ResartGame();
        }
	}

	private void FixedUpdate()
	{
        Vector2 velocity = rb.velocity;
        velocity.x = move;
        rb.velocity = velocity;
	}

    private void movement()
    {
        float horizontalTouch = CrossPlatformInputManager.GetAxis("Horizontal") * movementSpeed;
        transform.Translate(Vector3.right * movementSpeed * horizontalTouch * Time.deltaTime);

        //transform.Translate(new Vector3(Input.acceleration.x, Input.acceleration.y, 0) * 3.5f * Time.deltaTime);

        move = Input.GetAxis("Horizontal") * movementSpeed;

        if (transform.position.x < left_x)
        {
            transform.position = new Vector3(right_x, transform.position.y, transform.position.z);
        }

        if (transform.position.x > right_x)
        {
            transform.position = new Vector3(left_x, transform.position.y, transform.position.z);
        }
    }
}
