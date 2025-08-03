using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyPatrolBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPoint;
    [SerializeField]
    private GameObject _endPoint;
    [SerializeField]
    private GameObject _npcViewCone;

    private AlertedManager _alertedManager;
    private Rigidbody2D _rigidBodyWitness;
    private Transform _currentLocation;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRendererViewCone;
    private PolygonCollider2D _polygonColliderViewCone;
    private Vector3 _initLookUpPos;
    private float _speed = 2f;
    private float _elapsedSec = 0f;
    private float _elapsedTimeStun = 0f;
    //private float _lookUpTime = 3;
    private float _lookUpWC;
    private float _stunnedWC = 5f;
    public bool _allowLookUp;
    public bool _isStunned;
    public bool _isPanicked;
    [SerializeField]
    private bool _goesLeft;
    [SerializeField] private AnimationCurve _currentAnimationCurve;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBodyWitness = GetComponent<Rigidbody2D>();
        _currentLocation = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRendererViewCone = _npcViewCone.GetComponent<SpriteRenderer>();
        _alertedManager = GameObject.Find("Canvas").GetComponent<AlertedManager>();
        _currentLocation = _endPoint.transform;
        _lookUpWC = Random.Range(5f, 10f);
        _allowLookUp = true;
        _isStunned = false;
        _initLookUpPos = _npcViewCone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoving();
        HandleTurning();
        if (_allowLookUp == true)
        {
            HandleLookingUp();
        }
        if(_isStunned == true)
        {
            HandleStunLookUp();
        }
        if(_isPanicked == true)
        {
            HandleBeingPanicked();
        }
    }

    private void HandleMoving()
    {
        if (_spriteRenderer == null)
        {
            return;
        }
        if (_rigidBodyWitness == null)
        {
            return;
        }
        if (_currentLocation == _endPoint.transform)
        {
            if (_goesLeft == false) 
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                _rigidBodyWitness.velocity = new Vector2(_speed, 0);
            }
            else 
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                _rigidBodyWitness.velocity = new Vector2(-_speed, 0);
            }
            //previous flip solution, just in case
            /*_spriteRenderer.flipX = false;
            _npcViewCone.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            _currentRotation = _npcViewCone.transform.rotation;
            _rigidBodyWitness.velocity = new Vector2(_speed, 0);*/
        }
        else
        {
            /*_spriteRenderer.flipX = true;
            _npcViewCone.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            _currentRotation = _npcViewCone.transform.rotation;
            _rigidBodyWitness.velocity = new Vector2(-_speed, 0);*/
            if (_goesLeft == false) 
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                _rigidBodyWitness.velocity = new Vector2(-_speed, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                _rigidBodyWitness.velocity = new Vector2(_speed, 0);
            }
        }
    }

    private void HandleTurning()
    {
        if (Vector2.Distance(transform.position, _currentLocation.position) < 0.5f && _currentLocation == _endPoint.transform)
        {
            if(_isPanicked == true)
            {
                Debug.Log("Restart Game");
                SceneManager.LoadScene(4);
            }
            else 
            {
                _currentLocation = _startPoint.transform;
            }
        }
        if (Vector2.Distance(transform.position, _currentLocation.position) < 0.5f && _currentLocation == _startPoint.transform)
        {
            _currentLocation = _endPoint.transform;
        }
    }

    private void HandleLookingUp()
    {
        _elapsedSec += Time.deltaTime;
        float lookupPercentage = _elapsedSec /_lookUpWC;
        _npcViewCone.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 60f), 
            _currentAnimationCurve.Evaluate( lookupPercentage));
    }

    private void HandleStunLookUp()
    {
        _elapsedTimeStun += Time.deltaTime;
        if (_elapsedTimeStun >= _stunnedWC)
        {
            _isStunned = false;
            _speed = 2f;
            _allowLookUp = true;
            _npcViewCone.SetActive(true);
            _elapsedTimeStun -= _stunnedWC;
        }
        else
        {
            _speed = 0f;
            _allowLookUp = false;
            _npcViewCone.SetActive(false);
            if(_isPanicked == true)
            {
                _alertedManager._numAlerted--;
            }
            _isPanicked = false;
        }
    }

    private void HandleBeingPanicked() //mood
    {
        _speed = 4f;
        _npcViewCone.SetActive(false);
    }
}
