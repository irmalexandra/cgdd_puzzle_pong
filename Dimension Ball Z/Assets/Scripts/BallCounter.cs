using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI ballCounter;

    public void Update()
    {
        ballCounter.text = "Balls: " + GameManager.Instance.extraBalls.ToString();
    }
}
