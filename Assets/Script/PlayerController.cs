using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidBody;
    Animator _animator;

    float _animBlendRatio = 0.5f;
    float _runSpeed = 10;
    public float updatedX = 0;
    Vector3 velocity;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        velocity = transform.position;
    }

    void Update()
    {
        //  float forward = Input.GetAxisRaw("Vertical") > _animBlendRatio || Input.GetAxisRaw("Vertical") < -_animBlendRatio ? Input.GetAxisRaw("Vertical") : 0;
        // float right = Input.GetAxisRaw("Horizontal") > _animBlendRatio || Input.GetAxisRaw("Horizontal") < -_animBlendRatio ? Input.GetAxisRaw("Horizontal") : 0;

        // _animator.SetFloat("vertical", Input.GetAxis("Vertical"));
        // _animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));


        //_animator.

        float forward = Input.GetAxisRaw("Vertical");
        float right = Input.GetAxisRaw("Horizontal");
        _animator.SetInteger("forward", (int)forward);


        //velocity=transform.position+ transform.forward;

       // if(transform.position.x < 4 && transform.position.x>-4)
        {

            if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -3)
            {
                 velocity.x += -3;
                // updatedX=velocity.x + -3;
                Debug.LogError(transform.position);
               // transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
                _animator.SetInteger("right", (int)right);
            }
            else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 3)
            {
                velocity.x += 3;

                // updatedX = velocity.x + 3;
            //    transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);

                _animator.SetInteger("right", (int)right);

            }
            else
            {
                velocity.x = 0;
                _animator.SetInteger("right", (int)right);
            }
        }

        //if(velocity.x<=updatedX)
        //  velocity.x  =Mathf.Lerp(updatedX,velocity.x  , Time.deltaTime * 10);
        velocity.z = 1;
        transform.position += velocity * Time.deltaTime * _runSpeed;

        //_rigidBody.centerOfMass = Vector3.zero;
        //_rigidBody.velocity = Vector3.zero;



        //  Vector3 velocity = (forward * transform.forward + right * transform.right).normalized * Time.deltaTime * _currentSpeed;

    }

    IEnumerator Movement()
    {
        
        yield return new WaitForEndOfFrame();
    }

}
