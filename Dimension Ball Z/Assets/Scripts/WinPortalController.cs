using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPortalController : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision){

        Destroy(collision.collider.gameObject);
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
    }
}
