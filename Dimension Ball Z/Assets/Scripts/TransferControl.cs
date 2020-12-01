
using UnityEngine;

public class TransferControl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D ball)
    {
        if (ball.gameObject.tag == "Ball")
        {
            transform.parent.gameObject.GetComponentInChildren<PaddleController>().active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D ball)
    {
        if (ball.gameObject.tag == "Ball")
        {
            transform.parent.gameObject.GetComponentInChildren<PaddleController>().active = false;
        }
    }
}
