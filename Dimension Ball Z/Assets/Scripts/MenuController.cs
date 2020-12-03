using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    

    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BallHell()
    {
        SceneManager.LoadScene()
    }

    public void PortalInsanity()
    {
        SceneManager.LoadScene()
    }
}

