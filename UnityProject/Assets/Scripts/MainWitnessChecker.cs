using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainWitnessChecker : MonoBehaviour
{
    [SerializeField]
    private bool _allowGameToEnd;
    private NPCThoughts _mainWitnessThoughts;
    // Start is called before the first frame update
    void Start()
    {
        _mainWitnessThoughts = GetComponent<NPCThoughts>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_mainWitnessThoughts._gettingZapped == true && _allowGameToEnd == true) 
        {
            Debug.Log("Game Ended");
            Application.Quit();
        }
    }
}
