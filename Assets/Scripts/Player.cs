using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{

    public bool canTripleShoot = false;

    public bool shieldEnabled = false;
    public bool speedEnabled = false;
    [SerializeField]
    private float speed = 5f;

    public float fireRate = 0.5f;
    private float canFire = 0.0f;

    public GameObject[] _engines;

    
    private float horizontalInput;
    private float verticalInput;

    public GameObject explotionPrefab;

    public GameObject laserPrefab;
    public GameObject tripleShootPrefab;
    public GameObject shieldGameObject;

    private GameManager _gameManager;

    public int lives = 3;

    private UIManager _uiManager;
    private AudioSource _audioSource;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _anim = GetComponent<Animator>();
        if (_uiManager) {
            _uiManager.UpdateLives(lives);
        }
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
            Shoot();            
        }
    }

    public void Damage() {
        if (shieldEnabled) {
            shieldEnabled = false;
            shieldGameObject.SetActive(false);
            return;
        }
        lives--;
        if (lives == 2) {
            _engines[0].SetActive(true);
        }
        if (lives == 1) {
            _engines[1].SetActive(true);
        }

        if (_uiManager)
        {
            _uiManager.UpdateLives(lives);
        }
        if (lives < 1) {
            Instantiate(explotionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen(true);
            Destroy(gameObject);
        }

    }
    private void Shoot() {
        if (Time.time > canFire)
        {
            _audioSource.Play();
            if (canTripleShoot)
            {
                Instantiate(tripleShootPrefab, transform.position, Quaternion.identity);
            }
            else {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f), Quaternion.identity);
            }
            
            canFire = Time.time + fireRate;
        }
    }

    private void Movement() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput < 0) {
            _anim.SetBool("Left", true);
            _anim.SetBool("Right", false);
        }
        if (horizontalInput > 0)
        {
            _anim.SetBool("Left", false);
            _anim.SetBool("Right", true);
        }
        if (horizontalInput == 0) {
            _anim.SetBool("Left", false);
            _anim.SetBool("Right", false);
        }

        if (speedEnabled) {
            transform.Translate(new Vector3(horizontalInput, verticalInput) * Time.deltaTime * speed * 2f);
        } else {
            transform.Translate(new Vector3(horizontalInput, verticalInput) * Time.deltaTime * speed);
        }
        

        if (transform.position.y > 2)
        {
            transform.position = new Vector3(transform.position.x, 2);
        }
        if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4);
        }
        if (transform.position.x > 8)
        {
            transform.position = new Vector3(8, transform.position.y);
        }
        if (transform.position.x < -8)
        {
            transform.position = new Vector3(-8, transform.position.y);
        }
    }

    public void TripleShootPowerupOn() {
        canTripleShoot = true;
        StartCoroutine(TripleShootPowerupDown());
    }

    private IEnumerator TripleShootPowerupDown() {
        yield return new WaitForSeconds(5f);
        canTripleShoot = false;
    }

    public void SpeedPowerupOn()
    {
        speedEnabled = true;
        StartCoroutine(SpeedPowerupDown());
    }

    private IEnumerator SpeedPowerupDown()
    {
        yield return new WaitForSeconds(5f);
        speedEnabled = false;
    }

    public void ShieldPowerupOn()
    {
        shieldEnabled = true;
        shieldGameObject.SetActive(true);
    }
}
