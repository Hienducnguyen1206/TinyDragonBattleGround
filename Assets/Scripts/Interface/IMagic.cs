using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagicStrategy 
{
   public void Firing(Transform firePoint,int damage,float force);
}
