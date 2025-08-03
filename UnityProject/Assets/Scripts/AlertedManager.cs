using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertedManager : MonoBehaviour
{
    private TMP_Text _alertedText;
    public int _numAlerted = 0;
    void Start()
    {
        _alertedText = GameObject.Find("AlertedPeopleText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _alertedText.text = "Alerted People: " + _numAlerted;
    }
}
