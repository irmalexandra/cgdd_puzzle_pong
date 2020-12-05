using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsPaused()) {return;}
        if (!other.gameObject.CompareTag("Ball")) return;
        TimeManager.Instance.DoSlowmotion();
    }
}
