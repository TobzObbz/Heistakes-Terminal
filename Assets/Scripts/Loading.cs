using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("HeistakesTerminal");
    }
}