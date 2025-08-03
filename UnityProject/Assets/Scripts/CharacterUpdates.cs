using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpdates : MonoBehaviour
{
    protected MovementBehaviour _movementBehav;
    
    protected virtual void Awake() 
    {
        _movementBehav = GetComponent<MovementBehaviour>();
    }
}
