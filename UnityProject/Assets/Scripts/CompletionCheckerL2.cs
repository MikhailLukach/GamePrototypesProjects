using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionCheckerL2 : MonoBehaviour
{
    [SerializeField]
    private GameObject _witness;
    [SerializeField]
    private GameObject _portal;
    private NPCThoughts _witnessThoughts;
    // Start is called before the first frame update
    void Start()
    {
        _witnessThoughts = _witness.GetComponent<NPCThoughts>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_witnessThoughts._gettingZapped == true)
        {
            _portal.SetActive(true);
        }
    }
}
