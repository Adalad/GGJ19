using System.Collections.Generic;

public class Memory
{
    public int[] InitialSymbols;
    public List<MemoryOption> Options;
    public int[] VotingSymbols;

    public Memory(int[] initialSymbols, int[] votingSymbols)
    {
        InitialSymbols = initialSymbols;
        Options = new List<MemoryOption>();
        VotingSymbols = votingSymbols;
    }
}
