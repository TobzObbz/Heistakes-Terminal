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
        public void SetTimerCoroutine(Coroutine _coroutine) => timerCoroutine = _coroutine;

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
    }
    
    public IEnumerator StartTimer()
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
