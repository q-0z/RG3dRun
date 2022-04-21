using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rigidBody;
    Animator _animator;

    bool _isRunning = false;
    public bool _isJumping = false;
    public bool _isInAir = false;

    public int _jumpCount = 2;

    public LayerMask _groundLayer;

    float _animBlendRatio=0.5f;
    float _walkSpeed=400f, _runSpeed=1500f,_currentSpeed,_jumpSpeed=800f;
    float mouseX, mouseY, sens = 10;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float forward = Input.GetAxis("Vertical") > _animBlendRatio || Input.GetAxis("Vertical") < -_animBlendRatio ? Input.GetAxis("Vertical") : 0;
        float right = Input.GetAxis("Horizontal") > _animBlendRatio || Input.GetAxis("Horizontal") < -_animBlendRatio ? Input.GetAxis("Horizontal") : 0;

        RaycastHit hit;


        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), -transform.up, out hit, 1f, _groundLayer))
        {
            _isInAir = false;
            _isJumping = false;
        }
        else
        {
            _isInAir = true;
            _isJumping = true;
        }
        _animator.SetFloat("dirY", Input.GetAxis("Vertical")> _animBlendRatio || Input.GetAxis("Vertical") < -_animBlendRatio ? Input.GetAxis("Vertical"):0);
        _animator.SetFloat("dirX", Input.GetAxis("Horizontal")> _animBlendRatio || Input.GetAxis("Horizontal") <-_animBlendRatio ? Input.GetAxis("Horizontal"):0);

        if (Input.GetAxis("Vertical") > 0  && Input.GetKey(KeyCode.LeftShift))
        {
            _animator.SetFloat("dirY", 2);

            _currentSpeed = _runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            _currentSpeed = _walkSpeed;
        else
            _currentSpeed = _walkSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount>0)
        {
            _jumpCount--;
            _isJumping = true;
            _rigidBody.AddForce(transform.up * _jumpSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        _animator.SetBool("isJumping", _isJumping);

        Vector3 velocity = (forward * transform.forward + right*transform.right)  .normalized * Time.deltaTime * _currentSpeed;
      //  Vector3 velocity = new Vector3(right*transform.forward.x, 0, forward*transform.forward.z).normalized * Time.deltaTime * _currentSpeed;
        velocity.y = _rigidBody.velocity.y;
        _rigidBody.velocity = velocity;


         mouseX += Input.GetAxis("Mouse X") * sens;
        transform.transform.rotation = Quaternion.Euler(0, mouseX, 0);

         Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), -transform.up * 1f,Color.red);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("ground"))
        {

            _jumpCount = 2;
            //_isJumping = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("ground"))
        {
           // _isJumping = false;
        }
    }
}
