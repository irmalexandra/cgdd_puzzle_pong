﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private static AudioClip _portalSound;
    private static AudioClip _portalSound2;
    private static AudioClip _slowMotion1;
    private static AudioClip _slowMotion2;

    private static AudioClip _positiveFeedback;
    
    private static AudioClip _buttonClick;
    
    private static List<AudioClip> ballHitSounds = new List<AudioClip>();
    private static List<AudioClip> paddleHitSounds = new List<AudioClip>();

    // private static GameObject[] allButtons = new GameObject[];
    
    private static AudioClip _abilityCooldown;
    private static AudioClip _thrustReady;

    private static int _ballHitSound = 4;
    
    
    private static AudioSource _audioSource;
    void Start()
    {
        _portalSound = Resources.Load<AudioClip>("PortalSounds/PortalSound");
        _portalSound2 = Resources.Load<AudioClip>("PortalSounds/PortalSound2");
        _slowMotion1 = Resources.Load<AudioClip>("Development/SlowMotion1");
        _slowMotion2 = Resources.Load<AudioClip>("Development/SlowMotion2");
        _abilityCooldown = Resources.Load<AudioClip>("Development/cooldown1");
        _thrustReady = Resources.Load<AudioClip>("Development/thrustReady");
        _positiveFeedback = Resources.Load<AudioClip>("Development/positiveFeedback");

        _buttonClick = Resources.Load<AudioClip>("Development/clickButtonEffect");

        for (var i = 1; i <= 5; i++)
        {
           ballHitSounds.Add(Resources.Load<AudioClip>("Development/BallHit/BallHit"+i));
           paddleHitSounds.Add(Resources.Load<AudioClip>("Development/Paddle/PaddleHit"+i));
        }

        AddAudioSourceToButtons();
        
        _audioSource = GetComponent<AudioSource>();
    }

    private static void AddAudioSourceToButtons()
    {
        var allButtons = GameObject.FindGameObjectsWithTag("MenuButton");
        
        foreach (var buttonGameObject in allButtons )
        {
            var source = buttonGameObject.AddComponent<AudioSource>();
            var buttonComponent = buttonGameObject.GetComponent<Button>();
            source.clip = _buttonClick;
            source.playOnAwake = false;
            buttonComponent.onClick.AddListener(()=> PlayMenuButtonSoundEffect(source));
        }
    }
    
    private static void PlayMenuButtonSoundEffect(AudioSource source)
    {
        source.PlayOneShot(_buttonClick);
    }
    
    public static void PlaySoundEffect(string SoundEffectName)
    {
        switch (SoundEffectName)
        {
            case "PortalSoundEffect":
                _audioSource.PlayOneShot(_portalSound2);
                break;
            case "SlowMotion":
                _audioSource.PlayOneShot(_slowMotion2);
                break;
            case "BallHit":
                _audioSource.PlayOneShot(ballHitSounds[_ballHitSound]);
                break;
            case "PaddleHit":
                _audioSource.PlayOneShot(paddleHitSounds[Random.Range(0, paddleHitSounds.Count)]);
                break;
            case "Cooldown":
                _audioSource.PlayOneShot(_abilityCooldown);
                break;
            case "ThrustReady":
                _audioSource.PlayOneShot(_thrustReady);
                break;
            case "PositiveFeedback":
                _audioSource.PlayOneShot(_positiveFeedback);
                break;
        }
    }

}
