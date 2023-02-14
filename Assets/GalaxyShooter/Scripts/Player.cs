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
        private float _fireRate = 0.25f;
        [SerializeField]
        private float _canFire = 0.0f;

        private byte _lives = 3;

        private bool _canTripleShoot=false;
        private bool _isSpeedBoostActive=false;
        
        
        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector3(0, 0, 0);
        }

        // Update is called once per frame
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
            _lives--;
            if (_lives < 0)
            {
                Instantiate(explosionPrefab, transform.position, quaternion.identity);
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
    }
}
