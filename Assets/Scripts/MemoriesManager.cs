using UnityEngine;
using System.Collections.Generic;

public class MemoriesManager : MonoBehaviour
{
    #region Singleton

    private static MemoriesManager instance;

    public static MemoriesManager Instance
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
            Destroy(this.gameObject);
        }
    }

    #endregion Singleton

    #region Public fields

    public List<Memory> MemoriesCollection;

    #endregion Public fields

    #region MonoBehaviour methods

    private void Start()
    {
        MemoriesCollection = new List<Memory>();

        // ADD MEMORIES
        Memory memory = new Memory(new int[] { 0, 1, 2 }, new int[] { 0, 1, 2, 3 });
        MemoryOption option = new MemoryOption(new int[] { 0, 1, 0 }, Emotions.JOY);
        memory.Options.Add(option);
        option = new MemoryOption(new int[] { 0, 1, 0 }, Emotions.JOY);
        memory.Options.Add(option);
        MemoriesCollection.Add(memory);
    }

    #endregion MonoBehaviour methods

    #region Public methods

    public Memory GetRandomStory()
    {
        int random = Random.Range(0, MemoriesCollection.Count);
        Memory memory =  MemoriesCollection[random];
        MemoriesCollection.RemoveAt(random);
        return memory;
    }

    #endregion Public methods
}
