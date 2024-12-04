using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect; 
    [SerializeField] private float destroyDelay = 2f; 

    private void OnTriggerEnter(Collider other) 
    {
      
        Explode();

       
        Destroy(gameObject);
    }

    private void Explode()
    {
       
        ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

      
        Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constantMax);
    }
}
