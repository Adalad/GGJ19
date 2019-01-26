using System;
using System.Collections.Generic;

[Serializable]
public class Memory
{
    #region Public fields

    public int[] InitialSymbols;
    public List<MemoryOption> Options;
    public int[] VotingSymbols;

    #endregion Public fields

    #region Constructor

    public Memory(int[] initialSymbols, int[] votingSymbols)
    {
        InitialSymbols = initialSymbols;
        Options = new List<MemoryOption>();
        VotingSymbols = votingSymbols;
    }

    #endregion Constructor
}
