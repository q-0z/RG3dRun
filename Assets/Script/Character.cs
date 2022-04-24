using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3 velocity;
    public Animator _animator;

    float side = 0;
    [SerializeField]private float _runSpeed = 1;
    public bool _isJumping = false;
    public bool _isInAir = false;
    public LayerMask _groundLayer;


  
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), -transform.up, out hit, 1f, _groundLayer))
        {
            _isInAir = true;
            _isJumping = true;

        }
        else
        {
            _isInAir = false;
            _isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -3)
        {
            side += -3;
            StartCoroutine(SLerp(side));
            _animator.SetInteger("right", (int)-1);

            // velocity.x =- 3;
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 3)
        {
            side += 3;
            StartCoroutine(SLerp(side));
            _animator.SetInteger("right", (int)1);
        }
        if (Input.GetKeyDown(KeyCode.W) && _isJumping)
        {
            _isJumping = false;
            velocity.y = 4;
                _animator.SetInteger("forward", 1);
        }
        if (!_isJumping)
            velocity.y -= 10 * Time.deltaTime;
        else
        {
            velocity.y = 0;

                _animator.SetInteger("forward", 0);
        }
        if(Input.GetKeyDown(KeyCode.S) && _isJumping)
        {
            _animator.SetInteger("forward", -1);

        }
        transform.position += new Vector3(0, velocity.y, _runSpeed) * Time.deltaTime * 10;
        transform.position = new Vector3(velocity.x, transform.position.y, transform.position.z);
    }


    IEnumerator SLerp(float end)
    {
        {
            while (velocity.x > end)
            {
                velocity.x -= .5f;
                yield return null;
                // await System.Threading.Tasks.Task.Delay(1);
                // yield return new WaitForEndOfFrame();
               // yield return new WaitForSeconds(Time.deltaTime / 10000);

            }
            while (velocity.x < end)
            {
                velocity.x += .5f;
                yield return null;

                //  yield return new WaitForEndOfFrame();

                // await System.Threading.Tasks.Task.Delay(1);
               // yield return new WaitForSeconds(Time.deltaTime / 10000);

            }
            velocity.x = end;

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="NextTrackCollider")
        {
            EnvManager.Instance.Trigg();
        }
    }
    public void OnAnimEventCallback(string state)
    {
      //  Debug.LogError(state);
        if(state.Equals("right"))
            _animator.SetInteger("right", 0);
        if (state.Equals("forward"))
            _animator.SetInteger("forward", 0);

    }
}
