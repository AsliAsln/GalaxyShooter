using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace GalaxyShooter.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5.0f;
        [SerializeField]
        private float _boostSpeed = 1.5f;
        [SerializeField] 
        private GameObject laserPrefab;
        [SerializeField] 
        private GameObject tripleShotPrefab;
        [SerializeField] 
        private GameObject explosionPrefab;
        [SerializeField] 
        private GameObject Shields;
        [SerializeField]
        private float _fireRate = 0.25f;
        [SerializeField]
        private float _canFire = 0.0f;

        private UIManager _uiManager;
        
        private int _lives = 3;

        private bool _canTripleShoot=false;
        private bool _isSpeedBoostActive=false;
        public bool _isShieldActive=false;
        private GameManager _gameManager;
        private SpawnManager _spawnManager;

        private AudioSource _laserSound;
        
        void Start()
        {
            transform.position = new Vector3(0, 0, 0);
            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            _laserSound = GetComponent<AudioSource>();
            
            if (_uiManager != null)
            {
                _uiManager.UpdateLives(_lives);
            }
            if (_spawnManager != null)
            {
                _spawnManager.StartSpawnRoutines();
            }
        }

        void Update()
        {
            Movement();
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        

        private void Shoot()
        {
            //cool down bullet
                if (Time.time > _canFire)
                {
                    _laserSound.Play();
                    var position = transform.position;
                    if (_canTripleShoot)
                    {
                         //spawn triple laser
                         Instantiate(tripleShotPrefab, position + new Vector3(0, 0, 0),
                             quaternion.identity);
                    }
                    else
                    {
                        //spawn one laser
                        Instantiate(laserPrefab, position + new Vector3(0, 1, 0),
                            quaternion.identity);
                    }
                    _canFire = Time.time + _fireRate;
                }

        }

        private void Movement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (_isSpeedBoostActive)
            {
                transform.Translate(Vector3.right * (_speed * _boostSpeed * horizontalInput * Time.deltaTime));
                //horizontal move
                transform.Translate(Vector3.up * (_speed * _boostSpeed * verticalInput * Time.deltaTime));  
            }
            else
            {
                //vertical move
                transform.Translate(Vector3.right * (_speed * horizontalInput * Time.deltaTime));
                //horizontal move
                transform.Translate(Vector3.up * (_speed * verticalInput * Time.deltaTime));
            }
            


            //player bounds check -8<x<8  -4<y<0
            if (transform.position.x  < -8)
            {
                transform.position = new Vector3(-8, transform.position.y, 0);

            }
            else if (transform.position.x  > 8)
            {
                transform.position = new Vector3(8, transform.position.y, 0);

            }
            
            if (transform.position.y  < -4)
            {
                transform.position = new Vector3(transform.position.x, -4, 0);

            }
            else if (transform.position.y  > 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);

            }

        }

        public void Damage()
        {
            if (Shields.activeSelf)
            {
                Debug.Log("kapat kalkanı ulen");
                _isShieldActive = false;
                Shields.SetActive(false);
                return;
            }
            _lives--;
            _uiManager.UpdateLives(_lives);
            if (_lives <= 0)
            {
                Instantiate(explosionPrefab, transform.position, quaternion.identity);
                _gameManager.gameOver = true;
                _uiManager.ShowTitleScreen();
                Destroy(gameObject);
                
            }
        }

        public void TripleShotPowerupOn()
        {
            _canTripleShoot = true;
            StartCoroutine(TripleShotPowerDownRoutine());
        }

        public void SpeedBoostPowerupOn()
        {
            _isSpeedBoostActive = true;
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        public void ShieldPowerupOn()
        {
            _isShieldActive = true;
            Shields.SetActive(true);
            StartCoroutine(ShieldPowerDownRoutine());
        }

        IEnumerator TripleShotPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _canTripleShoot = false;
        }

        IEnumerator SpeedBoostPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedBoostActive = false;
        }
        IEnumerator ShieldPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isShieldActive = false;
            Shields.SetActive(false);
        }
    }
}
