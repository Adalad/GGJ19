using UnityEngine;

public class MessagePanel : MonoBehaviour
{
    #region Public fields

    public TMPro.TextMeshProUGUI Text;

    #endregion Public fields
    
    #region Public methods

    public void UpdateText(string text)
    {
        Text.text = text;
    }

    #endregion Public methods
}
