using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLevelHandler : MonoBehaviour
{
    public static MinigameLevelHandler Instance;

    [SerializeField] private List<string> levelNames;

    private int levelIndex = 0;
        public int GetLevelIndex() => levelIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);

        LoadLevel(0);
    }

    public void LoadLevel(int _index)
    {
        if (levelNames.Count == 0) return;

        if (_index < levelNames.Count)
        {
            levelIndex = _index;

            SceneManager.LoadScene(levelNames[levelIndex]);
        }
        else
        {
            Debug.Log("ALL LEVELS COMPLETE");
        }
    }
}
