using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPortalController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision){

        Destroy(collision.collider.gameObject);
        Application.LoadLevel("EndLevel");
    }
}
