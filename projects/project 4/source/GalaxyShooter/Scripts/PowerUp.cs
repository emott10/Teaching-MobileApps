using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float speed = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (tag == "Triple_Shot")
                {
                    player.tripleShotTurnOn();
                    Destroy(this.gameObject);
                }

                else if (tag == "Speed_Boast")
                {
                    player.speedBoastTurnOn();
                    Destroy(this.gameObject);
                }

                else if (tag == "Shield")
                {
                    player.enableShield();
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void Update ()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= -8)
        {
            Destroy(this.gameObject);
        }
    }
}
