using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MemoriesManager
{
    #region Singleton

    private static MemoriesManager instance;

    public static MemoriesManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MemoriesManager();
            }

            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    #endregion Singleton

    #region Public fields

    public List<Memory> MemoriesCollection;

    #endregion Public fields

    #region Constructor

    private MemoriesManager()
    {
        MemoriesCollection = new List<Memory>();

        string path = "Assets/Resources/memories.json";
        StreamReader reader = new StreamReader(path);
        Memory[] memories = JsonHelper.FromJson<Memory>(reader.ReadToEnd());
        reader.Close();
        MemoriesCollection = new List<Memory>(memories);
    }

    #endregion Constructor

    #region Public methods

    public Memory GetRandomStory()
    {
        int random = Random.Range(0, MemoriesCollection.Count);
        Memory memory = MemoriesCollection[random];
        MemoriesCollection.RemoveAt(random);
        return memory;
    }

    #endregion Public methods
}
