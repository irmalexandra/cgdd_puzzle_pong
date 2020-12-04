using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuController : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
