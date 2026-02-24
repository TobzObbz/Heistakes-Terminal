using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClueHandler : MonoBehaviour
{
    public static ClueHandler Instance;

    [SerializeField] private List<ClueSO> clues;
    [SerializeField] private TMP_Text displayText;

    private ClueSO currentClue = null;

    private Coroutine displayCoroutine = null;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach (ClueSO clue in clues)
        {
            clue.GetActivationKey().action.performed += ToggleClue;
            clue.GetActivationKey().action.Enable();
        }

        gameObject.SetActive(false);
    }

    private void ToggleClue(InputAction.CallbackContext context)
    {
        ClueSO clue = clues.Find(x => x.GetActivationKey().action == context.action);

        if (clue != currentClue)
        {
            currentClue = clue;

            //Stop coroutine if it is already running
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }

            displayText.text = "";

            gameObject.SetActive(true);
            displayCoroutine = StartCoroutine(DisplayClue(clue));
        }
        else if (clue == currentClue)
        {
            currentClue = null;
            gameObject.SetActive(false);
        }
    }

    public IEnumerator DisplayClue(ClueSO _clue)
    {
        //Play audio if there is any with dialogue
        if (_clue.GetAudioSource() != null)
        {
            _clue.GetAudioSource().Play();
        }

        string clueText = _clue.GetClueText();

        //Display clue one character at a time
        for (int i = 0; i < clueText.Length; i++)
        {
            displayText.text += clueText[i].ToString();

            yield return new WaitForSeconds(_clue.GetCharInterval());
        }

        if (_clue.GetAudioSource() != null)
        {
            _clue.GetAudioSource().Stop();
        }

        displayCoroutine = null;
    }
}
