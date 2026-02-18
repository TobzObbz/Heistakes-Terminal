using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private InputActionReference nextCanvasAction;
    [SerializeField] private InputActionReference previousCanvasAction;

    [SerializeField] private List<Canvas> canvases;
    private int currentCanvasIndex = 0;

    private List<bool> canvasFinished;

    private void Start()
    {
        canvasFinished = new List<bool>(canvases.Count);

        canvases[currentCanvasIndex].gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        nextCanvasAction.action.performed += NextCanvas;
        previousCanvasAction.action.performed += PreviousCanvas;

        nextCanvasAction.action.Enable();
        previousCanvasAction.action.Enable();
    }

    private void NextCanvas(InputAction.CallbackContext context)
    {
        CanvasNavigation(currentCanvasIndex++);
    }

    private void PreviousCanvas(InputAction.CallbackContext context)
    {
        CanvasNavigation(currentCanvasIndex--);
    }

    private void CanvasNavigation(int _index)
    {
        if (!canvasFinished[currentCanvasIndex])
        {
            return;
        }

        canvases[currentCanvasIndex].gameObject.SetActive(false);

        if (_index < 0 || _index >= canvases.Count)
        {
            return;
        }

        currentCanvasIndex = _index;

        canvases[currentCanvasIndex].gameObject.SetActive(true);
    }
}
