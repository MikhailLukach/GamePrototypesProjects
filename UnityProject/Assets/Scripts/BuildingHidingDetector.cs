using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHidingDetector : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private GameObject[] _NPCArray;
    private PlayerMovement _playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        if(_player.GetComponent<PlayerMovement>() == null)
        {
            return;
        }
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _NPCArray = GameObject.FindGameObjectsWithTag("NPCs");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            if(_player == null)
            {
                return;
            }
            _playerMovement._allowedToShoot = false;
            foreach (GameObject npc in _NPCArray) 
            {
                if(npc.GetComponentInChildren<ViewConePlayerDetection>() == null)
                {
                    return;
                }
                npc.GetComponentInChildren<ViewConePlayerDetection>()._allowDetection = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            if (_player == null)
            {
                return;
            }
            _playerMovement._allowedToShoot = true;
            foreach (GameObject npc in _NPCArray)
            {
                if(npc == null) 
                {
                    return;
                }
                if (npc.GetComponentInChildren<ViewConePlayerDetection>() == null)
                {
                    return;
                }
                npc.GetComponentInChildren<ViewConePlayerDetection>()._allowDetection = true;
            }
        }
    }
}
