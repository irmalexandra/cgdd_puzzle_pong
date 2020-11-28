using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    private static AudioClip _portalSound;
    private static AudioSource _audioSource;
    void Start()
    {
        _portalSound = Resources.Load<AudioClip>("PortalSounds/PortalSound");
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlayPortalSound()
    {
        _audioSource.PlayOneShot(_portalSound);
    }

}
