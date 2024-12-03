using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    public static MinimapCameraFollow Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
    }
   
    void LateUpdate()
    {
      Vector3 newPosition = playerTransform.position; 
      newPosition.y = transform.position.y;
      transform.position = newPosition;

    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
}
