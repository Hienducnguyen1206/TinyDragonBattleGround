using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;
using Photon.Pun;
using JetBrains.Annotations;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody), typeof(DragonCurrentStats))]
public class DragonController : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] FixedJoystick _fixedJoystick;
    [SerializeField] DragonCurrentStats currentStats;
    [SerializeField] bool runButtonPressed = false;
    [SerializeField] bool flyButtonPressed = false;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] DragonHealth dragonHealth;
  
    [SerializeField] GameObject firePoint;
    [SerializeField] ParticleSystem fireBall;

    public PhotonView photonView;

    [SerializeField] IMagicStrategy magicStrategy;


    private enum DragonState { IDLE, WALK, RUN, FLY }
    private DragonState currentState = DragonState.IDLE;

    private void Awake()
    {      
    }
    void Start()
    {   
        dragonHealth = GetComponent<DragonHealth>();
        magicStrategy = new EarthBallMegaStrategy();
       
    }
    private void OnEnable()
    {
       
        DragonHealth.ZeroStaminaAction += ZeroStaminaReset;
    }
       

    private void OnDisable()
    {   
        DragonHealth.ZeroStaminaAction -= ZeroStaminaReset;
        
    }

 

    public void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetMagicStrategy(new FireBallTinyStrategy());
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
        _fixedJoystick.SnapX = true;
        _fixedJoystick.SnapY = true;
    }



    void DragonControll()
    {
        if (flyButtonPressed  ) { 
            currentState = DragonState.FLY;
            return;
        }
       
            if ((_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0))
            {
                if (runButtonPressed)
                {
                    currentState = DragonState.RUN;
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
        Move(currentStats.currentWalkSpeed);
    }

    public void Run()
    {
        Move(currentStats.currentRunSpeed);
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


        Move(currentStats.currentFlySpeed);
    }


    private void Move(float speed)
    {
        Vector3 velocity = new Vector3(_fixedJoystick.Horizontal, 0, _fixedJoystick.Vertical) * speed;
        velocity.y = _rigidbody.velocity.y;  

       
        Quaternion rotation = Quaternion.Euler(0, 45, 0); 
        Vector3 rotatedVelocity = rotation * velocity;
        _rigidbody.velocity = rotatedVelocity;

       
        if (_rigidbody.velocity.x != 0 || _rigidbody.velocity.z != 0)
        {
            Vector3 direction = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); 
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

    }


    public void SetMagicStrategy(IMagicStrategy newMagicStrategy) { 
        this.magicStrategy = newMagicStrategy;
    }

    [PunRPC]
    public void Fire()
    { 
        
        if (photonView.IsMine)
        {
            _animator.SetTrigger("Fire");
            magicStrategy.Firing(firePoint.transform, 20, 5f);
        }
        
    }

    public void FireBreathe()
    {

    }
}