using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class CanvasEntry
{
    public Canvas canvas;
    public bool navigable;
}

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager Instance;

    [SerializeField] private InputActionReference nextCanvasAction;
    [SerializeField] private InputActionReference previousCanvasAction;

    [SerializeField] private List<CanvasEntry> canvases;
        public List<CanvasEntry> GetCanvases() => canvases;

    private int currentCanvasIndex = 0;
        public int GetCurrentCanvasIndex() => currentCanvasIndex;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        canvases[currentCanvasIndex].canvas.gameObject.SetActive(true);
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
        CanvasNavigation(currentCanvasIndex + 1);
    }

    private void PreviousCanvas(InputAction.CallbackContext context)
    {
        CanvasNavigation(currentCanvasIndex - 1);
    }

    public void CanvasNavigation(int _index)
    {
        if (_index > currentCanvasIndex)
        {
            if (!canvases[currentCanvasIndex].navigable)
            {
                return;
            }
        }

        if (_index < 0 || _index >= canvases.Count)
        {
            return;
        }

        canvases[currentCanvasIndex].canvas.gameObject.SetActive(false);

        currentCanvasIndex = _index;

        canvases[currentCanvasIndex].canvas.gameObject.SetActive(true);
    }

    public void SetNavigable(bool _navigable)
    {
        canvases[currentCanvasIndex].navigable = _navigable;
    }
}