using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragonController : MonoBehaviour
{   
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] FixedJoystick _fixedJoystick;
    [SerializeField] float _moveSpeed;

    // Start is called before the first frame update
     void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jumping");
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        DragonControll();
    }
  

    

    void DragonControll()
    {   

        _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * _moveSpeed,0, _fixedJoystick.Vertical * _moveSpeed);




        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            _animator.SetBool("Walking", true);
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);

        }
        else {
            _animator.SetBool("Walking", false);
        }


        

    }
}
