using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBallMegaStrategy :  IMagicStrategy
{
 
    public void Firing(Transform firePoint, int damage, float force)
    {
       
        
            GameObject lightball = ObjectPooler.Instance.SpawnFromPool("EarthBallMega",firePoint.transform.position,firePoint.transform.rotation);
            Rigidbody rb = lightball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(firePoint.transform.forward * force,ForceMode.Impulse);
            }
        
    }
}
