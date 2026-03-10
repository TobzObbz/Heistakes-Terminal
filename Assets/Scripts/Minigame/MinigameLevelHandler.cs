using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLevelHandler : MonoBehaviour
{
    public static MinigameLevelHandler Instance;

    [SerializeField] private List<SceneAsset> levels;

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
        if (levels.Count == 0) return;

        if (_index < levels.Count)
        {
            levelIndex = _index;

            SceneManager.LoadScene(levels[levelIndex].name);
        }
        else
        {
            Debug.Log("ALL LEVELS COMPLETE");
        }
    }
}
