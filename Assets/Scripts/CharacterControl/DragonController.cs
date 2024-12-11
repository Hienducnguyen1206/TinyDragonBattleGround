
using UnityEngine;
using Photon.Pun;



[RequireComponent(typeof(Rigidbody), typeof(DragonCurrentStats))]
public class DragonController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private DragonHealthController _dragonHealth;
    private DragonCurrentStats _currentStats;


    private bool runButtonPressed = false;
    private bool flyButtonPressed = false;
    private float rotationSpeed = 3f;

    [SerializeField] FixedJoystick _fixedJoystick;

    [SerializeField] GameObject firePoint;


    public PhotonView photonView;

    private IMagicStrategy magicStrategy;


    private enum DragonState { IDLE, WALK, RUN, FLY }
    private DragonState currentState = DragonState.IDLE;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _dragonHealth = GetComponent<DragonHealthController>();
        _currentStats = GetComponent<DragonCurrentStats>();
    }
    void Start()
    {   
      

        magicStrategy = new EarthBallMegaStrategy();

    }
    private void OnEnable()
    {    
        DragonHealthController.ZeroStaminaAction += ZeroStaminaReset;
    }
       

    private void OnDisable()
    {   
        DragonHealthController.ZeroStaminaAction -= ZeroStaminaReset;
        
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
        if (photonView.IsMine)
        {
            if (!flyButtonPressed)
            {
                _dragonHealth.ToggleStaminaChange();
            }
            runButtonPressed = !runButtonPressed;
        }
    }
     
    public void FlyButtonPressed()
    {
        if (photonView.IsMine)
        {
            if (!runButtonPressed)
            {
                _dragonHealth.ToggleStaminaChange();
            }
            flyButtonPressed = !flyButtonPressed;
            _rigidbody.useGravity = !_rigidbody.useGravity;

        }
    }


    public void ZeroStaminaReset()
    {   if (photonView.IsMine)
        {
            flyButtonPressed = false;
            runButtonPressed = false;
            _rigidbody.useGravity = true;
            _dragonHealth.ToggleStaminaChange();
        }
    }

    public void Walk()
    {
        Move(_currentStats.currentWalkSpeed);
    }

    public void Run()
    {
        Move(_currentStats.currentRunSpeed);
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


        Move(_currentStats.currentFlySpeed);
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
        if (photonView.IsMine)
        {
            this.magicStrategy = newMagicStrategy;
        }
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