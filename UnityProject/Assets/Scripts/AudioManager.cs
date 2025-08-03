using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _sfxAudioSource;

    [SerializeField]
    private AudioClip _npcScream;
    [SerializeField]
    private AudioClip _laserBeam;
    // Start is called before the first frame update
    void Start()
    {
        _sfxAudioSource = GetComponent<AudioSource>();
    }

    public void PlayNPCScream() 
    {
        _sfxAudioSource.PlayOneShot(_npcScream);
    }
    public void PlayLaserBeam()
    {
        _sfxAudioSource.PlayOneShot(_laserBeam);
    }
}
