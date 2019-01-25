public class MemoryOption
{
    #region Public fields

    public int[] Symbols;
    public Emotions Emotion;

    #endregion Public fields

    #region Constructor

    public MemoryOption(int[] symbols, Emotions emotion)
    {
        Symbols = symbols;
        Emotion = emotion;
    }

    #endregion Constructor
}
