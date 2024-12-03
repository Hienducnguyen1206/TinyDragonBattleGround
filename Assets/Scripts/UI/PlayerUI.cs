using UnityEngine;
using Cinemachine;

public class PlayerUI : MonoBehaviour
{
    
    private void OnEnable()
    {      
        CinemachineCore.CameraUpdatedEvent.AddListener(MyHandler);
    }

    private void OnDisable()
    {     
        CinemachineCore.CameraUpdatedEvent.RemoveListener(MyHandler);
    }

    private void MyHandler(CinemachineBrain brain)
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);   
    }

 
}
