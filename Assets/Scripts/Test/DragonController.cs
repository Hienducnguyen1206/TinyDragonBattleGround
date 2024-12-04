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
    [SerializeField] bool runButtonPressed = false;
    [SerializeField] bool flyButtonPressed = false;
    [SerializeField] DragonHealth dragonHealth;
    [SerializeField] PhotonView photonView;
    [SerializeField] GameObject firePoint;
    [SerializeField] ParticleSystem fireBall; 

    private enum DragonState { IDLE, WALK, RUN, FLY }
    private DragonState currentState = DragonState.IDLE;


    void Start()
    {
        dragonHealth = GetComponent<DragonHealth>();
        _fixedJoystick.SnapX = true;
        _fixedJoystick.SnapY = true;
    }
    private void OnEnable()
    {
        dragonHealth.RunZeroStamina += ZeroStaminaReset;
    }
       

    private void OnDisable()
    {
        dragonHealth.RunZeroStamina -= ZeroStaminaReset;
        
    }

    public void Update()
    {
        if (photonView.IsMine)
        {
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Firee");
                Fire();
            }


        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
             DragonControll();
             HandleState();

          
           

        }

    }

    public void SetJoystick(FixedJoystick joystick)
    {
        _fixedJoystick = joystick;
    }



    void DragonControll()
    {

        if ((_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0) )
        {
            if (runButtonPressed)
            {
                currentState = DragonState.RUN;
            }
            else if (flyButtonPressed)
            {
                currentState = DragonState.FLY;

            }
            else
            {
                currentState = DragonState.WALK;
            }

        }
        else 
        {
            if (!flyButtonPressed)
            {
                currentState = DragonState.IDLE;
            }
           
        }
               
    }


    private void HandleState()
    {

        switch (currentState)
        {
            case DragonState.IDLE:
                _animator.SetBool("Walking", false);
                _animator.SetBool("Running", false);
                _animator.SetBool("Flying", false);
                break;
            case DragonState.WALK:
                   _animator.SetBool("Running", false);
                _animator.SetBool("Flying", false);
                _animator.SetBool("Walking",true);

                Walk();
                
                break;
            case DragonState.RUN:
                _animator.SetBool("Running", true);
                Run();
                break;
            case DragonState.FLY:
                _animator.SetBool("Walking", false);
                _animator.SetBool("Running", false);
                _animator.SetBool("Flying",true );
                Fly();
                break;

        }
    }

    public void RunButtonPressed()
    {
        if (!flyButtonPressed)
        {
            dragonHealth.ToggleStaminaChange();
        }
        runButtonPressed = !runButtonPressed;
       
    }
     
    public void FlyButtonPressed()
    {
        if (!runButtonPressed)
        {
            dragonHealth.ToggleStaminaChange();
        }
        runButtonPressed = false;
        flyButtonPressed = !flyButtonPressed;
        _rigidbody.useGravity = !_rigidbody.useGravity;
       
      
    }


    public void ZeroStaminaReset()
    {
        flyButtonPressed = false;
        runButtonPressed = false;
        _rigidbody.useGravity = true;
        dragonHealth.ToggleStaminaChange();
    }

    public void Walk()
    {
        Vector3 velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed, _rigidbody.velocity.y, _fixedJoystick.Vertical * currentStats.currentMovementSpeed);
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

    public void Run()
    {
        Vector3 velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed * 3f, _rigidbody.velocity.y, _fixedJoystick.Vertical * currentStats.currentMovementSpeed * 3f);
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

    public void Fly()
    {
    
            if (transform.position.y < 5f)
            {
                _rigidbody.AddForce(Vector3.up * 5f, ForceMode.Force);

            }
            else
            {

                
                _rigidbody.velocity = Vector3.zero;
            }
               

        Vector3 velocity = new Vector3(_fixedJoystick.Horizontal * currentStats.currentMovementSpeed * 4f, _rigidbody.velocity.y, _fixedJoystick.Vertical * currentStats.currentMovementSpeed * 4f);
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

    public void Fire()
    {
        ParticleSystem fireball = GameObject.Instantiate(fireBall, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody _rb = fireball.GetComponent<Rigidbody>();
        _rb.AddForce(firePoint.transform.forward *5f,ForceMode.Impulse);
        _animator.SetTrigger("Fire");
    }
}