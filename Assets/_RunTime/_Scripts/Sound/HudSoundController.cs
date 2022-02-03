using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _hudAudioSource;
    [SerializeField] private AudioClip _buttonAudioClip;

    public void OnButtonPress()
    {
        _hudAudioSource.PlayOneShot(_buttonAudioClip);
    }
}
