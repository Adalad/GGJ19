using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI MessageText;

    private void Start()
    {
        GameManager.Instance.OnStartState += CheckStartState;
    }

    public void SendMessage()
    {
        string message = MessageText.text;

        // TODO process message
        GameManager.Instance.InputSymbols(new int[] { });
    }

    public void KeyboardButton(Button button)
    {
        button.gameObject.SetActive(false);
        MessageText.text = "AAAA";
    }

    private void CheckStartState(States state)
    {
        if (state == States.CHOOSE)
        {
            // TODO show symbols in UI
            GameManager.Instance.GetInitialSymbols();
            GameManager.Instance.GetVotingSymbols();
            // Notify manager
            GameManager.Instance.ChooseFinished();
        }
        else if (state == States.REVEAL)
        {
            // TODO show memory result in UI
            // Notify manager
            GameManager.Instance.RevealFinished();
        }
        else if (state == States.RESULT)
        {
            // TODO show feedback result in UI
            GameManager.Instance.GetFeedbackResult();
            // Notify manager
            GameManager.Instance.ResultFinished();
        }
    }
}
