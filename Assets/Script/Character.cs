using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3 velocity;
    public Animator _animator;

    float side = 0;
    // Start is called before the first frame update
    void Start()
    {
       // velocity.z = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float right = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -3)
        {
            side += -3;
            StartCoroutine(SLerp(side));
            _animator.SetInteger("right", (int)right);

            // velocity.x =- 3;
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 3)
        {
            side += 3;
            StartCoroutine(SLerp(side));
            _animator.SetInteger("right", (int)right);


            //velocity.x = 3;
        }
        else
        {
            _animator.SetInteger("right", (int)right);
            // side = 0;

            // velocity.x = 0;
        }

        // velocity.x = Mathf.Lerp(velocity.x, side, Time.deltaTime * 12);
        // velocity.x = Mathf.Lerp(velocity.x, side, Time.deltaTime * 12);

        transform.position = new Vector3(velocity.x, velocity.y, transform.position.z+1);
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
}
