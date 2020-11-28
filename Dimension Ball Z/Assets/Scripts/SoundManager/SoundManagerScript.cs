using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    private static AudioClip _portalSound;
    private static AudioClip _portalSound2;
    private static AudioSource _audioSource;
    void Start()
    {
        _portalSound = Resources.Load<AudioClip>("PortalSounds/PortalSound");
        _portalSound2 = Resources.Load<AudioClip>("PortalSounds/PortalSound2");
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlayPortalSound()
    {
        _audioSource.PlayOneShot(_portalSound);
    }
    public static void PlayPortalSound2()
    {
        _audioSource.PlayOneShot(_portalSound2);
    }
}
