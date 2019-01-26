using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum States
    {
        NONE, CHOOSE, VOTE, WRITE, CHECK, REVEAL, FEEDBACK, RESULT
    }

    #region Singleton

    private static GameManager instance;

    public static GameManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion Singleton

    #region Public fields

    public float VotingTime = 15;
    public float FeedbackTime = 15;

    #endregion Public fields

    #region Private fields

    private States CurrentState;
    private States NextState;
    private int[] Votings = new int[4];
    private Memory CurrentMemory;
    private int[] WrittenSymbols;
    private int RevealedMemory;

    #endregion Private fields

    #region Private events

    private delegate void StateEvent();
    private event StateEvent StartStateDelegate;
    private event StateEvent UpdateStateDelegate;
    private event StateEvent FinishStateDelegate;

    #endregion Private events

    #region MonoBehaviour methods

    private void Start()
    {
        CurrentState = States.NONE;
        NextState = States.CHOOSE;

        MixerInteractive.GoInteractive();
        MixerInteractive.SetCurrentScene("default");
    }

    private void Update()
    {
        if (CurrentState != NextState)
        {
            UpdateStateDelegate = null;
            FinishStateDelegate?.Invoke();
            FinishStateDelegate = null;

            CurrentState = NextState;

            StartEvents();

            StartStateDelegate?.Invoke();
            StartStateDelegate = null;

            return;
        }

        UpdateStateDelegate?.Invoke();
    }

    #endregion MonoBehaviour methods

    #region Public methods

    public void InputSymbols(int[] symbols)
    {
        if (CurrentState != States.WRITE)
        {
            return;
        }

        WrittenSymbols = symbols;
        NextState = States.CHECK;
    }

    public int[] GetInitialSymbols()
    {
        return CurrentMemory.InitialSymbols;
    }

    public int[] GetVotingSymbols()
    {
        return CurrentMemory.VotingSymbols;
    }

    public void ChooseFinished()
    {
        if (CurrentState != States.CHOOSE)
        {
            return;
        }

        NextState = States.VOTE;
    }

    public int GetRevealedMemory()
    {
        return RevealedMemory;
    }

    public void RevealFinished()
    {
        if (CurrentState != States.REVEAL)
        {
            return;
        }

        NextState = States.FEEDBACK;
    }

    public int[] GetFeedbackResult()
    {
        return new int[] { Votings[0], Votings[1] };
    }

    public void ResultFinished()
    {
        if (CurrentState != States.RESULT)
        {
            return;
        }

        NextState = States.CHOOSE;
    }

    #endregion Public methods

    #region Private methods

    private void StartEvents()
    {
        switch (CurrentState)
        {
            case States.CHOOSE:
                StartStateDelegate = StartChooseState;
                break;
            case States.VOTE:
                StartStateDelegate = StartVoteState;
                break;
            case States.CHECK:
                StartStateDelegate = StartCheckState;
                break;
            case States.FEEDBACK:
                StartStateDelegate = StartFeedbackState;
                break;
        }
    }

    private void StartChooseState()
    {
        CurrentMemory = MemoriesManager.Instance.GetRandomStory();
    }

    private void StartVoteState()
    {
        for (int i = 0; i < Votings.Length; ++i)
        {
            Votings[i] = 0;
        }

        MixerInteractive.SetCurrentScene("voting");
        StartCoroutine(VotingCountDown());

        UpdateStateDelegate = UpdateVoteState;
        FinishStateDelegate = FinishVoteState;
    }

    private void UpdateVoteState()
    {
        if (MixerInteractive.GetButton("1"))
        {
            Votings[0]++;
        }
        if (MixerInteractive.GetButton("2"))
        {
            Votings[1]++;
        }
        if (MixerInteractive.GetButton("3"))
        {
            Votings[2]++;
        }
        if (MixerInteractive.GetButton("4"))
        {
            Votings[3]++;
        }
    }

    private void FinishVoteState()
    {
        MixerInteractive.SetCurrentScene("default");
    }

    private IEnumerator VotingCountDown()
    {
        yield return new WaitForSeconds(VotingTime);

        NextState = States.WRITE;
    }

    private void StartCheckState()
    {
        // TODO check written with current options
        RevealedMemory = 0;

        NextState = States.REVEAL;
    }

    private void StartFeedbackState()
    {
        for (int i = 0; i < Votings.Length; ++i)
        {
            Votings[i] = 0;
        }

        MixerInteractive.SetCurrentScene("feedback");
        StartCoroutine(FeedbackCountDown());
        UpdateStateDelegate = UpdateFeedbackState;
        FinishStateDelegate = FinishFeedbackState;
    }

    private void UpdateFeedbackState()
    {
        if (MixerInteractive.GetButton("Good"))
        {
            Votings[0]++;
        }
        if (MixerInteractive.GetButton("Bad"))
        {
            Votings[1]++;
        }
    }

    private void FinishFeedbackState()
    {
        MixerInteractive.SetCurrentScene("default");
    }

    private IEnumerator FeedbackCountDown()
    {
        yield return new WaitForSeconds(FeedbackTime);

        NextState = States.RESULT;
    }

    #endregion Private methods
}
