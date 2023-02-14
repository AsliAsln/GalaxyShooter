using System;
using UnityEngine;

namespace GalaxyShooter.Scripts
{
    public class Powerup : MonoBehaviour
    {
        private float _speed;

        private void Update()
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                //Activate tripe shoot
                Player player = other.GetComponent < Player>();
                if (player != null)
                {
                    player._canTripleShoot = true;
                    player.TripleShotPowerupOn();
                    
                }
                
                Destroy(this.gameObject);
            }
             
        }
    }
}
