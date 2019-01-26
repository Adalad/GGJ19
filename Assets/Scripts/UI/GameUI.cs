using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    #region Public fields

    public GameObject MessageText;
    public KeyButton[] KeyButtons;
    public GameObject[] MessagePrefabs;
    public GameObject MessagePanel;
    public ScrollRect Scroll;
    public GameObject ImagePrefab;
    public GameObject SendButton;

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
        if (Message.Count <= 0)
        {
            return;
        }

        // Process message
        SendButton.GetComponent<Button>().interactable = false;
        GameManager.Instance.InputSymbols(Message.ToArray());

    }

    public void KeyboardButton(KeyButton button)
    {
        Message.Add(button.Icon);
        button.DisableButton();
        GameObject image = LoadSprite(button.Icon);
        image.transform.SetParent(MessageText.transform);
        image.transform.localEulerAngles = Vector3.zero;
        image.transform.localScale = new Vector3(1, 1, 1);
    }

    public void KeyboardDelete()
    {
        if (Message.Count <= 0)
        {
            return;
        }

        int symbol = Message[Message.Count - 1];
        Message.RemoveAt(Message.Count - 1);
        Destroy(MessageText.transform.GetChild(MessageText.transform.childCount - 1).gameObject);
        // Refresh buttons
        foreach (KeyButton button in KeyButtons)
        {
            if (button.Icon == symbol)
            {
                button.EnableButton();
            }
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
            CreateMessage(false, voting);

            // Notify manager
            GameManager.Instance.ChooseFinished();
        }
        else if (state == States.WRITE)
        {
            SendButton.GetComponent<Button>().interactable = true;
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

    private void CreateMessage(bool isPlayer, int[] icons)
    {
        int p = isPlayer ? 0 : 1;
        GameObject msgPanel = GameObject.Instantiate(MessagePrefabs[p]);
        for (int i = 0; i < icons.Length; ++i)
        {
            GameObject go = LoadSprite(icons[i]);
            go.transform.SetParent(msgPanel.transform);
        }
        msgPanel.transform.SetParent(MessagePanel.transform);
        msgPanel.transform.localEulerAngles = Vector3.zero;
        msgPanel.transform.localScale = new Vector3(1, 1, 1);
        Scroll.normalizedPosition = Vector2.zero;
    }

    private GameObject LoadSprite(int sprite)
    {
        Sprite mySprite = Resources.LoadAll<Sprite>("Sprites/Atlas")[sprite];
        GameObject go = GameObject.Instantiate(ImagePrefab);
        go.GetComponent<Image>().sprite = mySprite;

        return go;
    }

    #endregion Private methods
}
