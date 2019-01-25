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

    #region Private fields

    private States CurrentState;
    private States NextState;
    private int[] Votings = new int[4];
    private Memory CurrentMemory;

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

    public int InputSymbols(int[] symbols)
    {
        if (CurrentState != States.WRITE)
        {
            return -1;
        }

        return -1;
    }

    public int[] GetInitialSymbols()
    {
        return CurrentMemory.InitialSymbols;
    }

    public int[] GetVotingSymbols()
    {
        return CurrentMemory.VotingSymbols;
    }

    public void SetupFinished()
    {
        NextState = States.VOTE;
    }

    #endregion Public methods

    #region Private methods

    private void StartEvents()
    {
        switch(CurrentState)
        {
            case States.CHOOSE:
                StartStateDelegate = StartChooseState;
                break;
            case States.VOTE:
                StartStateDelegate = StartVoteState;
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

    #endregion Private methods
}
