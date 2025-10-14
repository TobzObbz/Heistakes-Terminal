using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerminalManager : MonoBehaviour
{
    public static TerminalManager Instance;

    [SerializeField] private GameObject digitField;
    [SerializeField] private GameObject digitsParent;
    [SerializeField] private GameObject successCanvas;
    [SerializeField] private GameObject txtIncorrect;

    private List<GameObject> digitFields = new List<GameObject>();
    private List<bool> codeCorrect = new List<bool>();
    private string code = "1234";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach (char _digit in code)
        {
            GameObject go = Instantiate(digitField);
            go.transform.SetParent(digitsParent.transform);
            digitFields.Add(go);
            codeCorrect.Add(false);
        }

        StartCoroutine(FocusFirstFieldNextFrame());
    }

    private IEnumerator FocusFirstFieldNextFrame()
    {
        yield return new WaitForEndOfFrame();
        SetInputFieldFocused(digitFields[0].GetComponent<TMP_InputField>(), true);
    }

    public List<GameObject> GetDigitFields()
    {
        return digitFields;
    }

    public string GetCode()
    {
        return code;
    }

    public string GetCodeAt(int _index)
    {
        return code[_index].ToString();
    }

    public void SetBoolAt(int _index, bool _value)
    {
        codeCorrect[_index] = _value;
    }

    public void CheckCodeCorrect()
    {
        bool correct = true;

        foreach (bool b in codeCorrect)
        {
            if (!b)
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            gameObject.SetActive(false);
            successCanvas.SetActive(true);
        }
        else if (!correct)
        {
            StartCoroutine(DisplayIncorrect());
        }
    }

    public GameObject GetFocusedGameObject()
    {
        return EventSystem.current.currentSelectedGameObject;
    }

    public void SetInputFieldFocused(TMP_InputField _inputField, bool _focused)
    {
        if (_focused)
        {
            _inputField.Select();
            _inputField.ActivateInputField();
            EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
        }
        else if (!_focused)
        {
            _inputField.DeactivateInputField();
        }
    }

    private IEnumerator DisplayIncorrect()
    {
        txtIncorrect.SetActive(true);

        yield return new WaitForSeconds(1f);

        txtIncorrect.SetActive(false);

        SetInputFieldFocused(digitFields[0].GetComponent<TMP_InputField>(), true);

        for (int i = 0; i < digitFields.Count; i++)
        {
            digitFields[i].GetComponent<TMP_InputField>().text = "";
        }
    }
}