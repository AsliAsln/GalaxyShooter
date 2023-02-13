using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GalaxyShooter.Scripts
{
    public class Laser : MonoBehaviour
    {
    
        [SerializeField]
        float speed = 10f;

       

        private void Update()
        {

            
            transform.Translate(Vector3.up * (speed * Time.deltaTime));
                
            if (transform.position.y > 5.3)
            {
                Destroy(gameObject);
            }
        }
    }
}
