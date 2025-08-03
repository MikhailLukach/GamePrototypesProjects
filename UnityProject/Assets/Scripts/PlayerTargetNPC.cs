using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetNPC : MonoBehaviour
{
    private GameObject[] _NPCArray;
    [SerializeField]
    private Transform _closestNPC;
    public Vector3 _closestNPCPosition = Vector3.zero;
    public bool _enemyTargetted;
    public NPCThoughts _enemyTargettedThoughts = null;
    public EnemyPatrolBehaviour _enemyTargettedPatrolBehaviour = null;
    public bool _allowTargetting;
    void Start()
    {
        _closestNPC = null;
        _enemyTargetted = false;
        _allowTargetting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_closestNPC == null) 
        {
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("NPCs")) 
        {
            if (_closestNPC != null) 
            {
                _closestNPC.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            }
            if (_allowTargetting == true) 
            {
                _closestNPC = GetClosestNPCPos();
                _closestNPC.gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, 0.7f, 0, 1);
                _enemyTargetted = true;
                _enemyTargettedThoughts = _closestNPC.gameObject.GetComponent<NPCThoughts>();
                _enemyTargettedPatrolBehaviour = _closestNPC.gameObject.GetComponent<EnemyPatrolBehaviour>();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("NPCs"))
        {
            if (_closestNPC == null) 
            {
                return;
            }
            _enemyTargetted = false;
            _closestNPC.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            _closestNPCPosition = Vector3.zero;
            _enemyTargettedThoughts = null;
            _enemyTargettedPatrolBehaviour = null;
        }
    }

    public Transform GetClosestNPCPos()
    {
        _NPCArray = GameObject.FindGameObjectsWithTag("NPCs");
        float closestDistance = Mathf.Infinity;
        Transform npcTransform = null;
        foreach (GameObject _npc in _NPCArray)
        {

            float currentDistanceBetween;
            currentDistanceBetween = Vector3.Distance(transform.position, _npc.transform.position);
            if (currentDistanceBetween < closestDistance)
            {
                 closestDistance = currentDistanceBetween;
                 npcTransform = _npc.transform;
                 _closestNPCPosition = npcTransform.position;
            }
        }
        return npcTransform;
    }
}
