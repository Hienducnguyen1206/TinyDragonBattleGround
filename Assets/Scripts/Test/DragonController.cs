using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;
using Photon.Pun;
using JetBrains.Annotations;

[RequireComponent(typeof(Rigidbody), typeof(DragonCurrentStats))]
public class DragonController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] FixedJoystick _fixedJoystick;
    [SerializeField] DragonCurrentStats currentStats;


    [SerializeField] bool isRunning = false;
    [SerializeField] bool isFlying = false;


   

    [SerializeField] DragonHealth dragonHealth;

    [SerializeField] PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {



    }



    private void OnEnable()
    {


    }

    private void OnDisable()
    {


    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
             DragonControll();

            if (isFlying) {
               
                if(transform.position.y < 5f)
                {
                    _rigidbody.AddForce(Vector3.up*13.5f,ForceMode.Acceleration);

                }
                else
                {
                  
                    _rigidbody.useGravity = false;
                    _rigidbody.velocity = Vector3.zero;
                }
            }
            else
            {
                _rigidbody.useGravity = true;
            }

        }

    }

    public void SetJoystick(FixedJoystick joystick)
    {
        _fixedJoystick = joystick;
    }



    void DragonControll()
    {
       
          Vector3 velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed , _rigidbody.velocity.y, _fixedJoystick.Vertical * currentStats.currentMovementSpeed );
          Quaternion rotation = Quaternion.Euler(0, 45, 0);
          Vector3 rotatedVelocity = rotation * velocity;
          
          













        _rigidbody.velocity = rotatedVelocity;
        if (_rigidbody.velocity != Vector3.zero)
        {
            
            Vector3 direction = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            Quaternion targetRotation = Quaternion.LookRotation(direction);

           
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);
        }













    }


    public void FlyButtonPressed()
    {
        isFlying = !isFlying;
    }

    public void RunButtonPressed()
    {

    }
}