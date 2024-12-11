using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DragonCurrentStats))]
public class DragonAttackController : MonoBehaviour
{
    private DragonCurrentStats _dragonStats;

    private void Awake()
    {
        _dragonStats = GetComponent<DragonCurrentStats>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


}
