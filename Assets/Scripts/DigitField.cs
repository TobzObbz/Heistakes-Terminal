using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DigitField : MonoBehaviour
{
    private int index;
    private bool ignoreValueChange = false;

    //public void Update()
    //{
    //    if (Keyboard.current.backspaceKey.wasPressedThisFrame && TerminalManager.Instance.GetFocusedGameObject() == gameObject)
    //    {
    //        index = transform.GetSiblingIndex();
    //        ignoreValueChange = true;

    //        if (index > 0)
    //        {
    //            index--;
    //            TMP_InputField prevInputField = TerminalManager.Instance.GetDigitFields()[index].GetComponent<TMP_InputField>();
    //            prevInputField.text = "";
    //            TerminalManager.Instance.SetInputFieldFocused(prevInputField, true);
    //            TerminalManager.Instance.SetBoolAt(index, false);
    //            StartCoroutine(ResetIgnoreFlag());
    //        }
    //    }
    //}

    public void OnValueChanged()
    {
        if (ignoreValueChange) return;

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

    //private IEnumerator ResetIgnoreFlag()
    //{
    //    yield return null;
    //    ignoreValueChange = false;
    //}

    private IEnumerator ResetUI(TMP_InputField inputField)
    {
        TerminalManager.Instance.SetInputFieldFocused(inputField, false);

        yield return new WaitForSeconds(1f);

        TerminalManager.Instance.CheckCodeCorrect();
    }
}