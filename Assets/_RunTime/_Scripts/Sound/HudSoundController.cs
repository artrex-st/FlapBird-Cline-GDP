using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _hudAudioSource;
    [SerializeField] private AudioClip _buttonAudioClip, _scoreUpSound;

    public void OnButtonPress()
    {
        _hudAudioSource.PlayOneShot(_buttonAudioClip);
    }
    public void OnScoreUp()
    {
        _hudAudioSource.PlayOneShot(_scoreUpSound);
    }
}
