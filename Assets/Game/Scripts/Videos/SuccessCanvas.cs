using UnityEngine;
using UnityEngine.UI;

public class SuccessCanvas : MonoBehaviour
{
    [SerializeField] private Image background;

    void Start()
    {
        background.sprite = TerminalManager.Instance.LoadImageFromPath("Success");
    }
}