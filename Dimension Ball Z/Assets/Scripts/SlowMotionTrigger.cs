using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{

    public bool disableSlowmotion;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (disableSlowmotion) return;
        if (GameManager.Instance.IsPaused()) {return;}
        if (!other.gameObject.CompareTag("Ball")) return;
        TimeManager.Instance.DoSlowmotion();
    }
}
