using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public bool gameOver=true;
    [SerializeField] private GameObject player;
    private UIManager _uiManager;
    
    void Start()
    {
        
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
    
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, quaternion.identity);
                gameOver = false;
                _uiManager.HideTitleScreen();
                _uiManager.score=0;
                _uiManager.UpdateScore();
            }
        }
    }
    
}
