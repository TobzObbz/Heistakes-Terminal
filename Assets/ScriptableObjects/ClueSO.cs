using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[CreateAssetMenu(fileName = "ClueSO", menuName = "Scriptable Objects/ClueSO")]
public class ClueSO : ScriptableObject
{
    [SerializeField] private InputActionReference activationKey;
        public InputActionReference GetActivationKey() => activationKey;

    [SerializeField] private string clueText;
        public string GetClueText() => clueText;

    [SerializeField] private float charInterval;
        public float GetCharInterval() => charInterval;

    [SerializeField] private AudioSource audioSource;
        public AudioSource GetAudioSource() => audioSource;
}