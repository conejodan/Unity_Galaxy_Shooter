using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;

    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                gameOver = false;
                Instantiate(player, Vector3.zero, Quaternion.identity);
                _uiManager.ShowTitleScreen(false);
            }
        }
    }
}
