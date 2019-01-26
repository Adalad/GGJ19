using UnityEngine;

public class AlbumManager : MonoBehaviour
{
    #region Public fields

    public GameObject PhotoPrefab;
    public GameObject SpawnPoint;

    #endregion Public fields

    #region Private fields

    private int CurrentPhoto;

    #endregion Private fields

    private void Start()
    {
        GameManager.Instance.OnStartState += CheckStartState;
    }

    #region Public methods


    #endregion Public methods

    #region Private methods

    private void CheckStartState(States state)
    {
        if (state == States.REVEAL)
        {
            // TODO show memory result in UI
            GameObject go = LoadPhoto(GameManager.Instance.GetRevealedMemory());
            Vector3 pos = SpawnPoint.transform.position;
            go.transform.position = new Vector3(pos.x + Random.Range(-1, 1), pos.y + Random.Range(-1, 1), pos.z);
            go.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-15, 15));
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

    private GameObject LoadPhoto(int id)
    {
        string photo = "Sprites/Photo" + id;
        if (id < 0)
        {
            photo = "Sprites/PhotoBlack";
        }

        Sprite mySprite = Resources.Load<Sprite>(photo);
        GameObject go = GameObject.Instantiate(PhotoPrefab);
        go.GetComponent<SpriteRenderer>().sprite = mySprite;
        CurrentPhoto++;
        go.GetComponent<SpriteRenderer>().sortingOrder = CurrentPhoto;

        return go;
    }

    #endregion Private methods
}
