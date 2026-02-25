using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SafeLetterCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField safeInputField;
    [SerializeField] private GameObject incorrectInput;

    [SerializeField] private char safeLetter;

    private void Start()
    {
        safeInputField.onValidateInput += OnValidateInput;
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

        if (safeInputField.text == safeLetter.ToString())
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