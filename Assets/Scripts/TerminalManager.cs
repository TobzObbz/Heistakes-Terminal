using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{
    public static TerminalManager Instance;

    [SerializeField] private GameObject digitField;
    [SerializeField] private GameObject digitsParent;

    [SerializeField] private Image imgLogo;

    [SerializeField] private GameObject successCanvas;
    [SerializeField] private GameObject txtIncorrect;

    private List<GameObject> digitFields = new List<GameObject>();
    private List<bool> codeCorrect = new List<bool>();

    private string code;

    private bool resetting = false;

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

    public bool GetResetting()
    {
        return resetting;
    }

    public void SetResetting(bool _value)
    {
        resetting = _value;
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
            _inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
        else if (!_focused)
        {
            _inputField.DeactivateInputField();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        imgLogo.gameObject.SetActive(true);
        imgLogo.sprite = LoadImageFromPath("Logo");
        imgLogo.preserveAspect = true;

        string filePath = Application.streamingAssetsPath + "/Code.txt";

        if (File.Exists(filePath))
        {
            code = File.ReadAllText(filePath).Trim();
        }
        else
        {
            Debug.LogWarning("Code file not found, using default code.");
        }

        for (int i = 0; i < code.Length; i++)
        {
            GameObject go = Instantiate(digitField);
            go.transform.SetParent(digitsParent.transform);
            digitFields.Add(go);
            codeCorrect.Add(false);
        }

        StartCoroutine(FocusFirstFieldNextFrame());
    }

    public Sprite LoadImageFromPath(string _imgName)
    {
        string[] matches = Directory.GetFiles(Application.streamingAssetsPath, _imgName + ".*");

        if (matches.Length > 0)
        {
            string filePath = matches[0];

            byte[] fileData = File.ReadAllBytes(filePath);

            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            return sprite;
        }
        else
        {
            Debug.LogWarning("Code file not found, using default code.");
            return null;
        }
    }

    private IEnumerator FocusFirstFieldNextFrame()
    {
        yield return null;
        yield return null;
        SetInputFieldFocused(digitFields[0].GetComponent<TMP_InputField>(), true);
    }

    public bool CheckCodeCorrect()
    {
        foreach (bool b in codeCorrect)
        {
            if (!b)
            {
                return false;
            }
        }

        return true;
    }

    public void DisplaySuccess()
    {
        Instantiate(successCanvas);
        gameObject.SetActive(false);
    }

    public IEnumerator DisplayIncorrect()
    {
        txtIncorrect.SetActive(true);

        ShakeAllDigitFields();

        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1f);

        txtIncorrect.SetActive(false);

        SetInputFieldFocused(digitFields[0].GetComponent<TMP_InputField>(), true);

        for (int i = 0; i < digitFields.Count; i++)
        {
            digitFields[i].GetComponent<TMP_InputField>().text = "";
        }

        resetting = false;
    }

    private void ShakeAllDigitFields()
    {
        foreach (DigitField df in digitsParent.GetComponentsInChildren<DigitField>())
        {
            StartCoroutine(df.Shake());
        }
    }
}