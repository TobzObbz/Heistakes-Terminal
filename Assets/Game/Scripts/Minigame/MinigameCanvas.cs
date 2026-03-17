using System.Collections;
using TMPro;
using UnityEngine;

public class MinigameCanvas : MonoBehaviour
{
    public static MinigameCanvas Instance;

    [SerializeField] private TMP_Text txtTimer;

    [SerializeField] private TMP_Text txtOutcome;

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
        txtTimer.text = currentTime.ToString();

        timerCoroutine = StartCoroutine(StartTimer());
    }
    
    private IEnumerator StartTimer()
    {
        while (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            txtTimer.text = currentTime.ToString("F2");

            yield return null;
        }

        txtTimer.text = "Time expired";
        StartCoroutine(ShowOutcome(false));
    }

    public IEnumerator ShowOutcome(bool _win)
    {
        txtOutcome.gameObject.SetActive(true);

        if (_win)
        {
            StopCoroutine(timerCoroutine);

            txtOutcome.text = "YOU WIN";

            yield return new WaitForSeconds(3);

            if (MinigameLevelHandler.Instance != null)
            {
                MinigameLevelHandler.Instance.LoadLevel(MinigameLevelHandler.Instance.GetLevelIndex() + 1);
            }
        }
        else if (!_win)
        {
            txtOutcome.text = "YOU LOSE";

            yield return new WaitForSeconds(3);

            if (MinigameLevelHandler.Instance != null)
            {
                MinigameLevelHandler.Instance.LoadLevel(MinigameLevelHandler.Instance.GetLevelIndex());
            }
        }

        yield return null;
    }
}
