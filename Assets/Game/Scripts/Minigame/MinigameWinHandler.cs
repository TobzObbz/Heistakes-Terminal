using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MinigameWinHandler : MonoBehaviour
{
    public static MinigameWinHandler Instance;

    [SerializeField] private GameObject minigameCanvas;
    [SerializeField] private GameObject globalLight2D;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        gameObject.SetActive(false);
    }

    public void WinGame()
    {
        gameObject.SetActive(true);
        minigameCanvas.SetActive(false);

        globalLight2D.GetComponent<Light2D>().intensity = 1f;
    }
}
