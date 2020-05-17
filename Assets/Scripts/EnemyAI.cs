using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;

    public GameObject explotion;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6.25) {
            transform.position = new Vector3(Random.Range(-7, 8), 6.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser") {
            Destroy(other.gameObject);
            Instantiate(explotion,transform.position, Quaternion.identity );
            if (_uiManager) {
                _uiManager.UpdateScore();
            }
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player) {
                player.Damage();
                Instantiate(explotion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
