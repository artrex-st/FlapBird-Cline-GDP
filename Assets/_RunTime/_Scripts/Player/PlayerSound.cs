using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _flap, _pipeHit;
    
    public void OnFlapSound()
    {
        _playerAudioSource.PlayOneShot(_flap);
    }
    public void OnSoundHitCall(Collider2D hit)
    {
        if (hit.TryGetComponent(out Obstacles hitAudioSource))
        {
            hitAudioSource.HitSound();
            return;
        }
        _playerAudioSource.PlayOneShot(_pipeHit);
        Debug.Log("Player");
    }
    private void Awake()
    {
        _playerAudioSource = _playerAudioSource != null ? _playerAudioSource : GetComponent<AudioSource>();
    }
}
