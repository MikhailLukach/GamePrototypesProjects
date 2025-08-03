using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    public float _movementSpeed = 5.0f;
    private Vector3 _desiredMovementDirection = Vector3.zero;
    private Vector3 _desiredLookatDirection = Vector3.zero;

    private Rigidbody2D _rigidBody;
    //public PlayerTargetNPC _playerTargetNPC;
    [SerializeField]
    private GameObject _memoryWipeRay;
    [SerializeField]
    private GameObject _rayLaser;
    private AudioManager _audioManager;
    private AudioSource _audioSource;
    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _audioManager = GameObject.Find("AudioSource").GetComponent<AudioManager>();
        _audioSource = GetComponent<AudioSource>();
    }
    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }
    public Vector3 DesiredLookAtDirection
    {
        get { return _desiredLookatDirection; }
        set { _desiredLookatDirection = value; }
    }
    private void Update()
    {
        HandleMovement();
        HandleLookAt();
    }
    private void HandleMovement()
    {
        if (_rigidBody == null) 
        {
            return;
        }
        Vector3 movement = _desiredMovementDirection.normalized;
        movement *= _movementSpeed;
        movement.y = _rigidBody.velocity.y;
        _rigidBody.velocity = movement;
        if(_rigidBody.velocity.x != 0) 
        {
            _audioSource.enabled = true;
        }
        else
        {
            _audioSource.enabled = false;
        }
    }

    private void HandleLookAt()
    {
        if(_memoryWipeRay == null)
        {
            return;
        }
        if(_desiredLookatDirection != Vector3.zero)
        {
            Vector3 LookAt = transform.InverseTransformPoint(_desiredLookatDirection);
            float lookAtAngle = Mathf.Atan2(LookAt.y, LookAt.x) * Mathf.Rad2Deg;
            _memoryWipeRay.transform.rotation = Quaternion.Euler(0, 0, lookAtAngle);
            //_memoryWipeRay.transform.position;
            _rayLaser.transform.rotation = Quaternion.Euler(0, 0, lookAtAngle + 90f);
        }
    }

}
