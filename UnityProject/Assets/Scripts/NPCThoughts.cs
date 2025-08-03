using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCThoughts : MonoBehaviour
{
    [SerializeField]
    private GameObject _thought1;
    [SerializeField]
    private GameObject _thought2;
    [SerializeField]
    private GameObject _thought3;
    [SerializeField]
    private GameObject _thought4;
    [SerializeField]
    private GameObject _panickedThought;
    private GameObject _currentThought = null;
    [SerializeField]
    private bool _BeginWithAlienThought;
    public bool _gettingZapped;
    private float _elapsedTime = 0f;
    private const float _reappearWC = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _thought1.SetActive(false);
        _thought2.SetActive(false);
        _thought3.SetActive(false);
        _thought4.SetActive(false);
        _panickedThought.SetActive(false);
        int digit = Random.Range(0, 10);
        if(_BeginWithAlienThought != true)
        {
            if (digit <= 4)
            {
                _currentThought = _thought1;
                _currentThought.SetActive(true);
            }
            else if (digit > 4 && digit <= 7)
            {
                _currentThought = _thought2;
                _currentThought.SetActive(true);
            }
            else
            {
                _currentThought = _thought3;
                _currentThought.SetActive(true);
            }
        }
        else
        {
            _currentThought = _thought3 ;
            _currentThought.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gettingZapped == true) 
        {
            HandleMemporyZap();
        }
    }

    private void HandleMemporyZap()
    {
        _elapsedTime += Time.deltaTime;
        if(_currentThought == _thought3) 
        {
            HighScoreManager.instance.AddScore();
        }
        _currentThought.SetActive(false);
        if(_elapsedTime > _reappearWC) 
        {
            _currentThought.SetActive(false);
            int digit = Random.Range(0, 10);
            if (digit <= 5) 
            {
                _currentThought = _thought1;
                _currentThought.SetActive(true);
            }
            else
            {
                _currentThought = _thought2;
                _currentThought.SetActive(true);
            }
            _gettingZapped = false;
            _elapsedTime -= _reappearWC;
            return;
        }
        _currentThought = _thought4;
        _currentThought.SetActive(true);
    }

    public void BecomePanicked()
    {
        _currentThought.SetActive(false);
        _currentThought = _panickedThought;
        _currentThought.SetActive(true);
    }
}
