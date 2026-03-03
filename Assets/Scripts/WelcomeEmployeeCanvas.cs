using UnityEngine;
using UnityEngine.InputSystem;

public class WelcomeEmployeeCanvas : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            NavigationManager.Instance.SetNavigable(true);
            NavigationManager.Instance.CanvasNavigation(NavigationManager.Instance.GetCurrentCanvasIndex() + 1);
        }
    }
}