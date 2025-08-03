using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTargetting : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layersToHit;
    public bool _isTargeting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isTargeting == true)
        {
            float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 direction = -transform.up;
            RaycastHit2D npcHit = Physics2D.Raycast(transform.position, direction, 50f, _layersToHit);
            if (npcHit.collider == null)
            {
                transform.localScale = new Vector3(transform.localScale.x, 50f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, npcHit.distance, 1f);
            }
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 0f, 1f);
        }
    }
}
