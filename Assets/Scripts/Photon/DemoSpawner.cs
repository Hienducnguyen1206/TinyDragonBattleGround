<<<<<<< Updated upstream
=======
using Photon.Pun;
>>>>>>> Stashed changes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSpawner : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
=======
    [SerializeField] GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("DragonSD_29", Vector3.zero, Quaternion.identity);
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
