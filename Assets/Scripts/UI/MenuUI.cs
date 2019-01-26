using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void GoToGame()
    {
        SceneManager.LoadScene("InitialScene");
    }
}
