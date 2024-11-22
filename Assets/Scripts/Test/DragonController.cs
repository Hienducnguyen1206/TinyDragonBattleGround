using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        


    }

    private void OnEnable()
    {
        DragonHealth.ZeroStrength += RunButtonPressed;
    }

    private void OnDisable()
    {
        DragonHealth.ZeroStrength -= RunButtonPressed;
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
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);

            }
            else
            {
                _animator.SetBool("Running", false);
            }
        }
        else
        {
            _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed, 0, _fixedJoystick.Vertical * currentStats.currentMovementSpeed);
            if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
            {
                _animator.SetBool("Running", false);
                _animator.SetBool("Walking", true);
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);

            }
            else
            {
                _animator.SetBool("Walking", false);
            }
        }
        


        





    }

    public void RunButtonPressed()
    {
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


}
