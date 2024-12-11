using UnityEngine;
using DG.Tweening; 

public class Fireball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Explode();
        Debug.Log(other.gameObject.name);

        if (other != null)
        {
            DragonHealthController health = other.gameObject.GetComponent<DragonHealthController>();
            if (health != null)
            {
                health.TakeDamage(20);
            }
        }

        ObjectPooler.Instance.ReturnToPool(this.gameObject, "FireBallTiny");
    }

    private void Explode()
    {
      
        GameObject obj = ObjectPooler.Instance.SpawnFromPool("FireExplosionTiny", transform.position, Quaternion.identity);
        ParticleSystem explosion = obj.GetComponent<ParticleSystem>();

    
        float explosionDuration = explosion.main.duration + explosion.main.startLifetime.constantMax;

        DOVirtual.DelayedCall(explosionDuration, () =>
        {
            ObjectPooler.Instance.ReturnToPool(obj, "FireExplosionTiny");
        });
    }
}
