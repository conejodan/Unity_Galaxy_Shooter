using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyShipPrefab;

    public GameObject[] powerups;

    public float generateEnemyTime = 11;
    public float generatePowerupTime = 2;
    private float timeGenerateEnemy = 0;
    private float timeGeneratePowerup = 0;

    public GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        timeGenerateEnemy = Time.time + generateEnemyTime;
        Debug.Log("Time" + timeGenerateEnemy);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.gameOver) {
            GenerateEnemy();
            GeneratePowerup();
        }
    }

    void GenerateEnemy() {
        if (Time.time > timeGenerateEnemy)
        {
            timeGenerateEnemy = Time.time + generateEnemyTime;
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7, 8), 12), Quaternion.identity);
        }
    }

    void GeneratePowerup() {
        if (Time.time > timeGeneratePowerup)
        {
            timeGeneratePowerup = Time.time + generatePowerupTime;
            int powerupRange = Random.Range(0, powerups.Count());
            Instantiate(powerups[powerupRange], new Vector3(Random.Range(-7, 8), 12), Quaternion.identity);
        }
    }
}
