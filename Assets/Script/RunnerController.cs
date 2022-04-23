using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    // Start is called before the first frame update

    bool isGrounded = false;
    Vector3 velocity=Vector3.zero;
    public LayerMask _groundLayer;

    void Start()
    {
        velocity.z = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), -transform.up, out hit, 1f, _groundLayer))
        {
            isGrounded = true;
            Debug.LogError("g");
        }
        else
        {
            isGrounded = false;
            //velocity.y = 0;
            Debug.LogError("ng");

        }
    }
    void Update()
    {


        
        //else
        {
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Debug.LogError("j");
                velocity.y = 5; // jump impulse force
                isGrounded = false;
            }
          //  else
            //    velocity.y = 0;
        }
        if (!isGrounded)
            velocity.y -= 10 * Time.deltaTime;
         else
            velocity.y = 0;

        transform.position += transform.up*velocity.y  * Time.deltaTime*10;
    }


    private void OnCollisionEnter(Collision collision)
    {
       // if(collision.gameObject.layer.Equals(_groundLayer))
         //   velocity.y=0;
    }
}
