using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : CharacterUpdates
{
    [SerializeField]
    private InputActionAsset _inputAsset;
    [SerializeField]
    private InputActionReference _movementAction;
    private InputAction _shootAction;
    private AudioManager _audioManager;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private PlayerTargetNPC _playerTargetNPC;
    [SerializeField]
    private LaserTargetting _laserTargetting;
    [SerializeField]
    private EnemyPatrolBehaviour _enemyPatrolBehaviour = null;
    public bool _allowedToShoot = true;
    private int _timesGotZapped = 0;
    private bool _isZapping = false;
    private float _elapsedSec = 0f;
    private const float _allowedToMoveWC = 1f;
    protected override void Awake()
    {
        base.Awake();
        if (_inputAsset == null) 
        {
            return;
        }
        _shootAction = _inputAsset.FindActionMap("Gameplay").FindAction("Shoot");
        _audioManager = GameObject.Find("AudioSource").GetComponent<AudioManager>();
        _allowedToShoot = true;
    }
    private void OnEnable()
    {
        if (_inputAsset == null)
        {
            return;
        }
        _inputAsset.Enable();
    }
    private void OnDisable()
    {
        if (_inputAsset == null)
        {
            return;
        }
        _inputAsset.Disable();
    }
    private void Update()
    {
        HandleMovementInput();
        HandleAiming();
        if(_allowedToShoot == true)
        {
            HandleShootInput();
        }
    }
    void HandleMovementInput()
    {
        if (_movementBehav == null ||
        _movementAction == null)
        {
            return;
        }
        //movement
        float movementInput = _movementAction.action.ReadValue<float>();
        if(movementInput > 0) 
        {
            _spriteRenderer.flipX = true;
        }
        else if(movementInput < 0)
        {   
            _spriteRenderer.flipX = false;
        }
        Vector3 movement = movementInput * Vector3.right;
        _movementBehav.DesiredMovementDirection = movement;
    }
     private void HandleAiming()
    {
        _movementBehav.DesiredLookAtDirection = _playerTargetNPC._closestNPCPosition;
    }

    private void HandleShootInput()
    {
        if (_shootAction == null)
        {
            return;
        }
        if(_isZapping == false)
        {
            if (_shootAction.IsPressed() && _playerTargetNPC._enemyTargetted == true)
            {
                _laserTargetting._isTargeting = true;
                _isZapping = true;
                _movementBehav._movementSpeed = 0f;
                _spriteRenderer.material.color = new Color(0,0.5f,1,1);
                _audioManager.PlayLaserBeam();
                _enemyPatrolBehaviour = _playerTargetNPC._enemyTargettedPatrolBehaviour;
                _playerTargetNPC._allowTargetting = false;
                if (_timesGotZapped < 1)
                {
                    _playerTargetNPC._enemyTargettedThoughts._gettingZapped = true;
                    _enemyPatrolBehaviour._isStunned = true;
                    _timesGotZapped++;
                }
            }
        }
        else
        {
            _elapsedSec += Time.deltaTime;
            if (_isZapping == true && _elapsedSec >= _allowedToMoveWC)
            {
                _laserTargetting._isTargeting = false;
                _movementBehav._movementSpeed = 5f;
                _spriteRenderer.material.color = Color.white;
                _timesGotZapped--;
                _elapsedSec -= _allowedToMoveWC;
                if (_shootAction.IsPressed() == false)
                {
                    _isZapping = false;
                    _playerTargetNPC._allowTargetting = true;
                }
            }
        }
    }

}
