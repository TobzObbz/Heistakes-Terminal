using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SafeLetterCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField safeInputField;
    [SerializeField] private GameObject incorrectInput;

    [SerializeField] private TMP_Text lockerNumber;
    [SerializeField] private TMP_Text lockerLocation;

    private string safe = "SAFE";
    private int safeIndex = 0;

    private List<string> safeLocations = new List<string>
    {
        "A15",
        "A32",
        "A6",
        "A26"
    };

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

        if (safeInputField.text == safe[safeIndex].ToString())
        {
            StartCoroutine(DisplaySuccess());

            safeIndex++;

            if (safeIndex == safe.Length)
            {
                //Success logic
                NavigationManager.Instance.SetNavigable(true);
                NavigationManager.Instance.CanvasNavigation(NavigationManager.Instance.GetCurrentCanvasIndex() + 1);
            }
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
        lockerNumber.text = (safeIndex + 1).ToString();
        lockerLocation.text = safeLocations[safeIndex];
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