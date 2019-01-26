public class MemoryOption
{
    #region Public fields

    public int Id;
    public int[] Symbols;
    public Emotions Emotion;

    #endregion Public fields

    #region Constructor

    public MemoryOption(int id, int[] symbols, Emotions emotion)
    {
        Id = id;
        Symbols = symbols;
        Emotion = emotion;
    }

    #endregion Constructor
}
