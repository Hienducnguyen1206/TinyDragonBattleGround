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


    private Coroutine flyUpCoroutine;
    private Coroutine flyDownCoroutine;

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
                if (_rigidbody.velocity.magnitude > 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);
                }

            }
            else
            {
                _animator.SetBool("Running", false);
            }
        }else if (isFlying)
        {   
            
            _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed * 3f, 0, _fixedJoystick.Vertical * currentStats.currentMovementSpeed * 3f);
            
            if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
            {
                _animator.SetBool("Flying", true);

                if (_rigidbody.velocity.magnitude > 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);
                }

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

                if (_rigidbody.velocity.magnitude > 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);
                }

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

        if (isFlying )
        {
            dragonHealth.StartDecreaseStrength();
            flyUpCoroutine = StartCoroutine(FlyUp());
        }
        else 
        {
            _animator.SetBool("Flying", false);
            if (flyUpCoroutine != null)
                {
                    StopCoroutine(flyUpCoroutine);
                   
                }
            flyDownCoroutine = StartCoroutine(FlyDown());
            dragonHealth.StartIncreaseStrength();
            
        }

    }


    public void ResetDragonMoveState()
    {
        if (isFlying)
        {   
            StartCoroutine(FlyDown());
            isFlying = false;
            _animator.SetBool("Flying",false);
        }

        isRunning = false;
        dragonHealth.StartIncreaseStrength();
       
    }

    IEnumerator FlyUp()
    {
        while(transform.position.y  < 5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f * Time.deltaTime, transform.position.z);
            yield return null;
        }
      
        flyUpCoroutine = null;
    }

    IEnumerator FlyDown()
    {
        while (transform.position.y > 0f)
        {
            if (transform.position.y < 0.02f)
            {
                break;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y -  1.5f * Time.deltaTime, transform.position.z);
            yield return null;
        }
        flyDownCoroutine = null;

        

    }
}
