using System.Collections;
using TMPro;
using UnityEngine;

public class MinigameCanvas : MonoBehaviour
{
    public static MinigameCanvas Instance;

    [SerializeField] private TMP_Text txtTimer;

    [SerializeField] private TMP_Text txtOutcome;
        public void SetTxtOutcome(string _text) => txtOutcome.text = _text;

    [SerializeField] private float startTime;

    private float currentTime;

    private Coroutine timerCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        currentTime = startTime;
        txtTimer.text = currentTime.ToString("F2");
    }
    
    public void StartTimer()
    {
        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 10f)
            {
                txtTimer.text = "0" + currentTime.ToString("F2");
            }
            else
            {
                txtTimer.text = currentTime.ToString("F2");
            }

            yield return null;
        }

        MinigameHandler.Instance.SetCanMove(false);
        StartCoroutine(ShowOutcome(false));
    }

    public IEnumerator ShowOutcome(bool _win)
    {
        txtOutcome.gameObject.SetActive(true);

        if (_win)
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }

            int levelNumber = MinigameLevelHandler.Instance.GetLevelIndex() + 2; //Index starts at 0 and refer to future level
            txtOutcome.text = "Loading level " + levelNumber + "...";

            yield return new WaitForSeconds(3);

            if (MinigameLevelHandler.Instance != null)
            {
                MinigameLevelHandler.Instance.LoadLevel(MinigameLevelHandler.Instance.GetLevelIndex() + 1);
            }
        }
        else if (!_win)
        {
            txtOutcome.text = "TIMES UP! Restarting...";

            yield return new WaitForSeconds(3);

            if (MinigameLevelHandler.Instance != null)
            {
                MinigameLevelHandler.Instance.LoadLevel(MinigameLevelHandler.Instance.GetLevelIndex());
            }
        }

        yield return null;
    }
}
