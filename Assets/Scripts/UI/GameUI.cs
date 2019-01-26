using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    #region Public fields

    public TMPro.TextMeshProUGUI MessageText;
    public KeyButton[] KeyButtons;
    public GameObject[] MessagePrefabs;
    public GameObject MessagePanel;
    public ScrollRect Scroll;

    #endregion Public fields

    #region Private fields

    private int CurrentKey;
    private List<int> Message;

    #endregion Private fields

    private void Start()
    {
        GameManager.Instance.OnStartState += CheckStartState;
        Message = new List<int>();
    }

    #region Public methods

    public void SendMessage()
    {
        // Process message
        GameManager.Instance.InputSymbols(Message.ToArray());
    }

    public void KeyboardButton(KeyButton button)
    {
        Message.Add(button.Icon);
        button.DisableButton();
        MessageText.text = MessageText.text + "<sprite=" + button.Icon + "> ";
    }

    public void KeyboardDelete()
    {
        if (Message.Count < 0)
        {
            return;
        }

        int symbol = Message[Message.Count - 1];
        Message.RemoveAt(Message.Count - 1);
        MessageText.text = string.Empty;
        // Refresh buttons
        foreach (KeyButton button in KeyButtons)
        {
            if (button.Icon == symbol)
            {
                button.EnableButton();
            }
        }
        // Rebuild message
        foreach (int icon in Message)
        {
            MessageText.text = MessageText.text + "<sprite=" + icon + "> ";
        }
    }

    #endregion Public methods

    #region Private methods

    private void CheckStartState(States state)
    {
        if (state == States.CHOOSE)
        {
            // TODO show symbols in UI
            CurrentKey = 0;
            int[] initial = GameManager.Instance.GetInitialSymbols();
            for (int i = 0; i < initial.Length; ++i)
            {
                KeyButtons[i].UpdateIcon(initial[i]);
                CurrentKey++;
            }

            int[] voting = GameManager.Instance.GetVotingSymbols();
            string message = string.Empty;
            for (int i = 0; i < voting.Length; ++i)
            {
                message = message + " " + i + "<sprite=" + voting[i] + "> ";
            }

            CreateMessage(false, message);

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

    private void CreateMessage(bool isPlayer, string message)
    {
        int i = isPlayer ? 0 : 1;
        GameObject msgPanel = GameObject.Instantiate(MessagePrefabs[0]);
        msgPanel.GetComponent<MessagePanel>().UpdateText(message);
        msgPanel.transform.parent = MessagePanel.transform;
        Scroll.normalizedPosition = Vector2.zero;
    }

    #endregion Private methods
}
