using System.Collections;
using TMPro;
using UnityEngine;

public class SafeLetterCanvas : MonoBehaviour
{
    [SerializeField] private TMP_InputField safeInputField;
    [SerializeField] private GameObject incorrectInput;

    private string safe = "SAFE";
    private int safeIndex = 0;

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
        if (safeInputField.text == safe[safeIndex].ToString())
        {
            safeInputField.text = "";

            safeIndex++;
            if (safeIndex == safe.Length)
            {
                Debug.Log("Safe opened!");
            }
        }
        else
        {
            safeInputField.text = "";
            StartCoroutine(DisplayFailure());
        }
    }

    private IEnumerator DisplayFailure()
    {
        yield return new WaitForSeconds(1);

        incorrectInput.SetActive(true);

        yield return new WaitForSeconds(3);

        incorrectInput.SetActive(false);
    }
}