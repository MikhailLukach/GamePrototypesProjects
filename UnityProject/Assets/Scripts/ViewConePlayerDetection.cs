using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewConePlayerDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerSprite;
    [SerializeField]
    private GameObject _npc;
    public bool _allowDetection = true;
    private EnemyPatrolBehaviour _enemyPatrolBehav;
    private NPCThoughts _npcThoughts;
    private AlertedManager _alertedManager;
    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _enemyPatrolBehav = _npc.GetComponent<EnemyPatrolBehaviour>();
        _npcThoughts = _npc.GetComponent<NPCThoughts>();
        _alertedManager = GameObject.Find("Canvas").GetComponent<AlertedManager>();
        _audioManager = GameObject.Find("AudioSource").GetComponent<AudioManager>();
        _allowDetection = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player") && _allowDetection == true)
        {
            _playerSprite.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            _enemyPatrolBehav._isPanicked = true;
            _alertedManager._numAlerted++;
            _npcThoughts.BecomePanicked();
            _audioManager.PlayNPCScream();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            _playerSprite.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}
