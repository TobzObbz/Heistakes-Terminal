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

    [SerializeField] private AudioSource auTimesUp;
    [SerializeField] private AudioSource auWin;

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

        auTimesUp.Play();
        MinigameHandler.Instance.SetCanMove(false);
        StartCoroutine(ShowOutcome(false));
    }

    public IEnumerator ShowOutcome(bool _win)
    {
        txtOutcome.gameObject.SetActive(true);

        txtOutcome.text = "";

        if (_win)
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }

            auWin.Play();

            int levelNumber = MinigameLevelHandler.Instance.GetLevelIndex() + 2; //Index starts at 0 and refer to future level

            if (MinigameLevelHandler.Instance.GetLevelNames().Count >= levelNumber)
            {
                yield return StartCoroutine(TextDotsAppear("Loading level " + levelNumber));
            }

            if (MinigameLevelHandler.Instance != null)
            {
                MinigameLevelHandler.Instance.LoadLevel(MinigameLevelHandler.Instance.GetLevelIndex() + 1);
            }
        }
        else if (!_win)
        {
            auTimesUp.Play();

            yield return StartCoroutine(TextDotsAppear("TIMES UP! Restarting"));

            if (MinigameLevelHandler.Instance != null)
            {
                MinigameLevelHandler.Instance.LoadLevel(MinigameLevelHandler.Instance.GetLevelIndex());
            }
        }

        yield return null;
    }

    private IEnumerator TextDotsAppear(string _baseText)
    {
        txtOutcome.text = _baseText;

        int elapsedTime = 0;

        //Add dots to the loading text for 3 seconds
        while (elapsedTime < 3f)
        {
            yield return new WaitForSeconds(1);
            txtOutcome.text += ".";
            elapsedTime += 1;
        }
    }
}
