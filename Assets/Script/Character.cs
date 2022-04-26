using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour,IPawn
{

    
    [SerializeField] private float _runSpeed = 1;
    [SerializeField] private float _obstacleGenInterval = 0.5f;
    [SerializeField] private float _jumpSpeed = 4;
    [SerializeField] private float _smoothRatio = 10;
    [SerializeField] private int _healthPointMax = 3;
    [SerializeField] private int _coinCount = 0;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _groundLayer;

    private float _xSlide = 0;
    private float _swipeMaxInXY = .5f;
    private float _xDirLerp = .5f;
    private float _maxRaycastInYdir = 0.2f;
    private float _xDirSliding = 3;
    private int _healthPointCurr = 0;
    private bool _isInAir = false;
    private bool _isLeft, _isRight, _isDown, _isUp;
    private Vector2 firstPressPos, secondPressPos;
    private Vector3 currentSwipe;
    private Vector3 _velocity;

    private void Start()
    {
        StartCoroutine(ObstacleGenerator());
        _healthPointCurr = _healthPointMax;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        RaycastHit hit;
        if (Physics.Raycast(
            new Vector3(transform.position.x, transform.position.y + _maxRaycastInYdir, transform.position.z),
            -transform.up, out hit, 1f, _groundLayer))
        {
            _isInAir = false;

        }
        else
        {
            _isInAir = true;
        }
        MovementKey();
        if (_isLeft && transform.position.x > -_xDirSliding)
        {
            _xSlide += -_xDirSliding;
            StartCoroutine(SLerp(_xSlide));
            _animator.SetInteger("right",-1);
            _isLeft = false;
            _isRight = false;
            _isDown = false;
            _isUp = false;
        }
        else if (_isRight && transform.position.x < _xDirSliding)
        {
            _xSlide += _xDirSliding;
            StartCoroutine(SLerp(_xSlide));
            _animator.SetInteger("right", 1);
            _isLeft = false;
            _isRight = false;
            _isDown = false;
            _isUp = false;
        }
        if (_isUp && !_isInAir)
        {
            _isInAir = true;
            _velocity.y = _jumpSpeed;
                _animator.SetInteger("forward", 1);
            _isLeft = false;
            _isRight = false;
            _isDown = false;
            _isUp = false;
        }
        if (_isInAir)
            _velocity.y -= _smoothRatio * Time.deltaTime;
 
        if(_isDown && !_isInAir)
        {
            _animator.SetInteger("forward", -1);
            _isLeft = false;
            _isRight = false;
            _isDown = false;
            _isUp = false;

        }
        transform.position += new Vector3(0, _velocity.y, _runSpeed) * Time.deltaTime * _smoothRatio;
        transform.position = new Vector3(_velocity.x, transform.position.y, transform.position.z);



    }

    void MovementKey()
    {

#if UNITY_EDITOR
        _isLeft = Input.GetKeyDown(KeyCode.A);
        _isRight = Input.GetKeyDown(KeyCode.D);
        _isDown = Input.GetKeyDown(KeyCode.S);
        _isUp = Input.GetKeyDown(KeyCode.W);

#elif UNITY_ANDROID || UNITY_IOS

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (currentSwipe.y > 0 && currentSwipe.x > -_swipeMaxInXY && currentSwipe.x < _swipeMaxInXY)
                {
                    _isLeft = false;
                    _isRight = false;
                    _isDown = false;
                    _isUp = true;
                }
                if (currentSwipe.y < 0 && currentSwipe.x > -_swipeMaxInXY && currentSwipe.x < _swipeMaxInXY)
                {
                    _isLeft = false;
                    _isRight = false;
                    _isDown = true;
                    _isUp = false;
                }
                if (currentSwipe.x < 0 && currentSwipe.y > -_swipeMaxInXY && currentSwipe.y < _swipeMaxInXY)
                {
                    _isLeft = true;
                    _isRight = false;
                    _isDown = false;
                    _isUp = false;
                }
                if (currentSwipe.x > 0 && currentSwipe.y > -_swipeMaxInXY && currentSwipe.y < _swipeMaxInXY)
                {
                    _isLeft = false;
                    _isRight = true;
                    _isDown = false;
                    _isUp = false;
                }
            }
        }
#endif
    }

    IEnumerator SLerp(float end)
    {
        {
            while (_velocity.x > end)
            {
                _velocity.x -= _xDirLerp;
                yield return null;

            }
            while (_velocity.x < end)
            {
                _velocity.x += _xDirLerp;
                yield return null;

            }
            _velocity.x = end;

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("NextTrackCollider"))
        {
            EnvManager.Instance.OnTriggerCallback();
        }

        if (other.GetComponent<IInteractable>()!=null)
        {
            other.GetComponent<IInteractable>().OnCollide(this);
            //_healthPointCurr--;
        }
       
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            _isInAir = false;
            _velocity.y = 0;
            _animator.SetInteger("forward", 0);
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            _isInAir = false;

            _velocity.y = 0;
            _animator.SetInteger("forward", 0);
        }
    }
    public void OnAnimEventCallback(string state)
    {
      //  Debug.LogError(state);
        if(state.Equals("right"))
            _animator.SetInteger("right", 0);
        if (state.Equals("forward"))
        {
           // _velocity.y = 0;
            _animator.SetInteger("forward", 0);
        }

    }

    IEnumerator ObstacleGenerator()
    {
        while (true)
        {
            var obj=ObstacleManager.Instance.getRandomObstacle();
            yield return new WaitForSeconds(_obstacleGenInterval);
        }
    }
    public void ReduceHealth()
    {
           _healthPointCurr--;
        UIManager.Instance.SetLife(_healthPointCurr);

        if (_healthPointCurr<=0)
        {
            Time.timeScale = 0;
            UIManager.Instance.ShowGameOverPanel();
        }
    }
    public void AddCoin()
    {
        _coinCount++;
        UIManager.Instance.SetCoin(_coinCount);

    }
}
