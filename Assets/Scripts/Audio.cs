using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        EventCenter.AddListener<AudioClip>(EventType.PlayAudio, Play);
        EventCenter.AddListener<bool>(EventType.SetAudio, SetAudio);
    }

    private void OnDestroy() {
        EventCenter.RemoveListener<AudioClip>(EventType.PlayAudio, Play);
        EventCenter.RemoveListener<bool>(EventType.SetAudio, SetAudio);
    }

    private void Start() {
        SetAudio(GameManager.Instance.Data.IsMusicOn);
    }

    private void SetAudio(bool isOpen) {
        _audioSource.mute = !isOpen;
    }

    private void Play(AudioClip clip) {
        _audioSource.PlayOneShot(clip);
    }
}