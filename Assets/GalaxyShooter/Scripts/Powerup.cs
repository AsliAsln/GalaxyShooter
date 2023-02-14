using System;
using UnityEngine;

namespace GalaxyShooter.Scripts
{
    public class Powerup : MonoBehaviour
    {
        private float _speed=5f;
        [SerializeField] 
        private byte powerupId; //0 for triple shot, 1 for speed, 2 for shield
        private void Update()
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                
                Player player = other.GetComponent < Player>();
                if (player != null)
                {
                    //enable triple shoot
                    if (powerupId == 0)
                    {
                        player.TripleShotPowerupOn();
                    }
                    //enable speed boost
                    if (powerupId == 1)
                    {
                        player.SpeedBoostPowerupOn();
                    }
                    //enable shields
                    if (powerupId == 2)
                    {
                        
                    }

                    
                }
                
                Destroy(this.gameObject);
            }
             
        }
    }
}
