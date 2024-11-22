using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody), typeof(DragonCurrentStats))]
public class DragonController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] FixedJoystick _fixedJoystick;
    [SerializeField] DragonCurrentStats currentStats;


    [SerializeField] bool isRunning = false;
    [SerializeField] bool isFlying = false;
    [SerializeField] bool isGround = true;
    [SerializeField] bool isMaxHeight = false;

    [SerializeField] DragonHealth dragonHealth;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        


    }

    

    private void OnEnable()
    {
        DragonHealth.ZeroStrength += ResetDragonMoveState;
       
    }

    private void OnDisable()
    {
        DragonHealth.ZeroStrength -= ResetDragonMoveState;
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        DragonControll();
    }




    void DragonControll()
    {



        if (isRunning)
        {
            _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed * 3f, 0, _fixedJoystick.Vertical * currentStats.currentMovementSpeed*3f);
            if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
            {
                _animator.SetBool("Running", true);
                Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);

            }
            else
            {
                _animator.SetBool("Running", false);
            }
        }else if (isFlying)
        {   
            
            _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed * 4f, 0, _fixedJoystick.Vertical * currentStats.currentMovementSpeed * 4f);
            
            if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
            {
                _animator.SetBool("Flying", true);
                Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);

            }
            
        }
        else 
        {
            _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed, 0, _fixedJoystick.Vertical * currentStats.currentMovementSpeed);
            if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
            {
                _animator.SetBool("Running", false);
                _animator.SetBool("Flying",false);
                _animator.SetBool("Walking", true);
                Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);


            }
            else
            {
                _animator.SetBool("Walking", false);
            }
        }
        


        





    }

    public void RunButtonPressed()
    {   
        isFlying = false;
        isRunning  =  !isRunning;
        

        if (isRunning)
        {
            dragonHealth.StartDecreaseStrength();
        }
        else
        {
            dragonHealth.StartIncreaseStrength();
        }
        
    } 

    public void FlyButtonPressed() 
    {
        
        isFlying = !isFlying;
        isRunning = false;
       
        if (isFlying)
        {
            dragonHealth.StartDecreaseStrength();
           
        }
        else
        {
            dragonHealth.StartIncreaseStrength();
        }

    }


    public void ResetDragonMoveState()
    {
        isFlying = false;
        isRunning = false;
        dragonHealth.StartIncreaseStrength();
    }

}
