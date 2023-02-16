using System;
using System.Collections;
using System.Collections.Generic;
using GalaxyShooter.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private float _speed=3f;

    [SerializeField] 
    private GameObject enemyExplosion;

    private UIManager _uiManager;
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*(_speed*Time.deltaTime));
        if (transform.position.y < -6)
        {
            float randomX = Random.Range(-7, 7);
            transform.position = new Vector3(randomX, 6, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(gameObject);
            Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

        }
    }
}
