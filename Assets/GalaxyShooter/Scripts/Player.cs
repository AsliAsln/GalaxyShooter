using Unity.Mathematics;
using UnityEngine;

namespace GalaxyShooter.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 5.0f;

        [SerializeField] 
        private GameObject laserPrefab;
        private float _fireRate = 0.25f;
        private float _canFire = 0.0f;
        
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
                    //spawn laser
                    Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0),
                        quaternion.identity);
                    
                    _canFire = Time.time + _fireRate;
                }

        }
        private void Movement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            
            //vertical move
            transform.Translate(Vector3.right * (_speed * horizontalInput * Time.deltaTime));
            //horizontal move
            transform.Translate(Vector3.up * (_speed * verticalInput * Time.deltaTime));

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
        
    }
}
