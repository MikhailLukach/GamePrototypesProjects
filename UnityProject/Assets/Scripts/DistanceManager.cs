using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    //[SerializeField]
    private TMP_Text _distanceText;
    private int _distance = 0;
    private Vector3 _alienPosition;
    private Vector3 _mainWitnessPosition;
    // Start is called before the first frame update
    void Start()
    {
        _distanceText = GameObject.Find("AwayFromTargetText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == null) 
        {
            return;
        }
        if (GameObject.Find("MainWitness") == null)
        {
            return;
        }
        _alienPosition = GameObject.Find("Player").GetComponent<Transform>().position;
        _mainWitnessPosition = GameObject.Find("MainWitness").GetComponent<Transform>().position;
        //Debug.Log("update");
        _distance = (int)Vector3.Distance(_alienPosition, _mainWitnessPosition) - 7;
        _distanceText.text = "Distance: " + _distance;
    }
}
