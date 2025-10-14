using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DigitField : MonoBehaviour
{
    private int index;

    public void OnValueChanged()
    {
        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            int index = TerminalManager.Instance.GetFocusedGameObject().transform.GetSiblingIndex();

            if (index > 0)
            {
                TMP_InputField prevInputField = TerminalManager.Instance.GetDigitFields()[index - 1].GetComponent<TMP_InputField>();
                TerminalManager.Instance.SetBoolAt(index - 1, false);
                prevInputField.text = "";
                TerminalManager.Instance.SetInputFieldFocused(prevInputField, true);
            }
        }
        else
        {
            index = transform.GetSiblingIndex();

            TMP_InputField inputField = GetComponent<TMP_InputField>();

            if (inputField.text.ToString() == TerminalManager.Instance.GetCodeAt(index))
            {
                TerminalManager.Instance.SetBoolAt(index, true);
            }
            else
            {
                TerminalManager.Instance.SetBoolAt(index, false);
            }

            if (TerminalManager.Instance.GetCode().Length - 1 > index)
            {
                TMP_InputField nextInputField = TerminalManager.Instance.GetDigitFields()[index + 1].GetComponent<TMP_InputField>();
                TerminalManager.Instance.SetInputFieldFocused(nextInputField, true);
            }
            else
            {
                StartCoroutine(ResetUI(inputField));
            }
        }
    }

    private IEnumerator ResetUI(TMP_InputField inputField)
    {
        TerminalManager.Instance.SetInputFieldFocused(inputField, false);

        yield return new WaitForSeconds(1f);

        TerminalManager.Instance.CheckCodeCorrect();
    }
}