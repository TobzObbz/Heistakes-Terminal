using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputUserIDHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField userIDInputField;
    [SerializeField] private GameObject incorrectInputScreen;

    [SerializeField] private float failureWaitTime = 3f;

    private string userID;

    private void Start()
    {
        string filePath = Application.streamingAssetsPath + "/UserID.txt";

        if (File.Exists(filePath))
        {
            userID = File.ReadAllText(filePath).Trim();
        }
        else
        {
            Debug.LogWarning("Code file not found, using default code.");
        }

        userIDInputField.onValidateInput += OnValidateInput;

        userIDInputField.ActivateInputField();
    }

    private char OnValidateInput(string _text, int _charIndex, char _addedChar)
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            EnterUserID();
            return '\0'; //Reject space character
        }

        return char.ToUpper(_addedChar);
    }

    public void EnterUserID()
    {
        if (userIDInputField.text == userID)
        {
            //Success logic goes here
            NavigationManager.Instance.SetNavigable(true);
            NavigationManager.Instance.CanvasNavigation(NavigationManager.Instance.GetCurrentCanvasIndex() + 1);
        }
        else
        {
            //Fail logic goes here
            StartCoroutine(DisplayFailure());
            userIDInputField.text = "";
        }
    }

    private IEnumerator DisplayFailure()
    {
        incorrectInputScreen.SetActive(true);

        yield return new WaitForSeconds(failureWaitTime);

        incorrectInputScreen.SetActive(false);
    }
}