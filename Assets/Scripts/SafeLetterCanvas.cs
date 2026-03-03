using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class SafeLetterCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField safeInputField;
    [SerializeField] private GameObject incorrectInput;

    [SerializeField] private int safeLetterIndex;

    private string safeLetters;

    private void Start()
    {
        string filePath = Application.streamingAssetsPath + "/SafeLetters.txt";

        if (File.Exists(filePath))
        {
            safeLetters = File.ReadAllText(filePath).Trim();
        }
        else
        {
            Debug.LogWarning("Code file not found, using default code.");
        }

        safeInputField.onValidateInput += OnValidateInput;

        safeInputField.ActivateInputField();
    }

    private char OnValidateInput(string _text, int _charIndex, char _addedChar)
    {
        if (char.IsLetter(_addedChar))
        {
            return char.ToUpper(_addedChar);
        }

        return '\0'; // Reject invalid input
    }

    public void OnValueChanged()
    {
        if (safeInputField.text == "")
        {
            return;
        }

        if (safeInputField.text == safeLetters[safeLetterIndex].ToString())
        {
            StartCoroutine(DisplaySuccess());
        }
        else
        {
            StartCoroutine(DisplayFailure());
        }
    }

    private IEnumerator DisplaySuccess()
    {
        yield return new WaitForSeconds(0.5f);

        safeInputField.text = "";

        NavigationManager.Instance.SetNavigable(true);
        NavigationManager.Instance.CanvasNavigation(NavigationManager.Instance.GetCurrentCanvasIndex() + 1);
    }

    private IEnumerator DisplayFailure()
    {
        yield return new WaitForSeconds(1);

        incorrectInput.SetActive(true);
        safeInputField.text = "";

        yield return new WaitForSeconds(3);

        incorrectInput.SetActive(false);
    }
}