using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    private Coroutine successCoroutine;

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>())
        {
            successCoroutine = StartCoroutine(OnSuccess());
        }
    }

    private void OnTriggerExit2D(Collider2D _collider)
    {
        if (!gameObject.activeInHierarchy) return;

        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>())
        {
            Animator anim = GetComponent<Animator>();
            anim.Play("Gate Move", 0, 0f);  
            anim.Update(0f);

            StopCoroutine(successCoroutine);

            anim.enabled = false;
        }
    }

    private IEnumerator OnSuccess()
    {
        GetComponent<Animator>().enabled = true;
        AnimatorClipInfo[] animatorClipInfo = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        yield return new WaitForSeconds(animatorClipInfo[0].clip.length);
        MinigameHandler.Instance.SetCanMove(false);
        StartCoroutine(MinigameCanvas.Instance.ShowOutcome(true));
    }
}