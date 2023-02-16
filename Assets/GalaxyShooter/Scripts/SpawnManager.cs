using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyShip;
    [SerializeField] private GameObject[] powerups;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

       
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            Instantiate(enemyShip, new Vector3(Random.Range(-7, 7), 7, 0),quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (true)
        {
            int powerupRand = Random.Range(0, 3);
            Instantiate(powerups[powerupRand], new Vector3(Random.Range(-7, 7), 7, 0), quaternion.identity);
            yield return new WaitForSeconds(7f);


        }
    }

    //starting spawn the objects
    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }
    
}
