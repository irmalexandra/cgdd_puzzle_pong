﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPortalController : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision){
        SoundManagerScript.PlaySoundEffect("PortalSoundEffect");
        Destroy(collision.collider.gameObject); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
