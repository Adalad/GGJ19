using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
[RequireComponent(typeof(Button))]
public class KeyButton : MonoBehaviour
{
    #region Public fields

    public int Icon;

    #endregion Public fields

    #region Private fields

    private TMPro.TextMeshProUGUI TextComponent;
    private Button ButtonComponent;

    #endregion Private fields

    #region MonoBehaviour methods

    private void Start()
    {
        TextComponent = GetComponent<TMPro.TextMeshProUGUI>();
        ButtonComponent = GetComponent<Button>();
    }

    #endregion MonoBehaviour methods

    #region Public methods

    public void EnableButton()
    {
        ButtonComponent.interactable = true;
    }

    public void DisableButton()
    {
        ButtonComponent.interactable = false;
    }

    public void UpdateIcon(int icon)
    {
        if (icon < 0)
        {
            return;
        }

        Icon = icon;
        TextComponent.text = "<sprite=" + icon + ">";
    }

    #endregion Public methods
}
