using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject CreditsPanel;

    public void GoToGame()
    {
        SceneController.Instance.FadeAndLoadScene("GamePlay");
    }

    public void CreditsToggle()
    {
        if (CreditsPanel == null)
        {
            return;
        }

        CreditsPanel.SetActive(!CreditsPanel.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
