using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    private static AudioClip _portalSound;
    private static AudioClip _portalSound2;
    private static AudioClip _slowMotion1;
    private static AudioClip _slowMotion2;
    private static AudioSource _audioSource;
    void Start()
    {
        _portalSound = Resources.Load<AudioClip>("PortalSounds/PortalSound");
        _portalSound2 = Resources.Load<AudioClip>("PortalSounds/PortalSound2");
        _slowMotion1 = Resources.Load<AudioClip>("Development/SlowMotion1");
        _slowMotion2 = Resources.Load<AudioClip>("Development/SlowMotion2");
        
        _audioSource = GetComponent<AudioSource>();
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
        }
    }

}
