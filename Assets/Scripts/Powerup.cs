using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    public float speed = 1;
    public int powerupId;
    public AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -7) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();

            if (player)
            {
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                if (powerupId == 1) {
                    player.TripleShootPowerupOn();
                }
                if (powerupId == 2)
                {
                    player.SpeedPowerupOn();
                }
                if (powerupId == 3)
                {
                    player.ShieldPowerupOn();
                }

                Destroy(this.gameObject);
            }
        }
    }
}
