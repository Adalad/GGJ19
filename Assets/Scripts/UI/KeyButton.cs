using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class KeyButton : MonoBehaviour
{
    #region Public fields

    public int Icon;

    #endregion Public fields

    #region Private fields

    private Image ImageComponent;
    private Button ButtonComponent;

    #endregion Private fields

    #region MonoBehaviour methods

    private void Start()
    {
        ImageComponent = GetComponent<Image>();
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
        ImageComponent.sprite = Resources.LoadAll<Sprite>("Sprites/Atlas")[icon];
    }

    #endregion Public methods
}
