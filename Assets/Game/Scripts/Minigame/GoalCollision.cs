using System.Collections;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    private Coroutine successCoroutine;

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>() && MinigameHandler.Instance.GetCanMove())
        {
            successCoroutine = StartCoroutine(OnSuccess());
        }
    }

    private void OnTriggerStay2D(Collider2D _collider)
    {
        if (successCoroutine != null && !MinigameHandler.Instance.GetCanMove())
        {
            StopCoroutine(successCoroutine);
            successCoroutine = null;

            Animator anim = GetComponent<Animator>();
            anim.Play("Gate Move", 0, 0f);
            anim.Update(0f);
        }

        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>() && MinigameHandler.Instance.GetCanMove())
        {
            if (successCoroutine == null)
            {
                successCoroutine = StartCoroutine(OnSuccess());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _collider)
    {
        if (!gameObject.activeInHierarchy) return;

        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>() && MinigameHandler.Instance.GetPlayer().activeInHierarchy)
        {
            Animator anim = GetComponent<Animator>();
            anim.Play("Gate Move", 0, 0f);  
            anim.Update(0f);

            if (successCoroutine != null)
            {
                StopCoroutine(successCoroutine);
                successCoroutine = null;
            }

            anim.enabled = false;
        }
    }

    private IEnumerator OnSuccess()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("Gate Move", 0, 0f);

        yield return null;

        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            if (!MinigameHandler.Instance.GetCanMove())
            {
                anim.Play("Gate Move", 0, 0f);
                anim.Update(0f);
                yield break;
            }
            yield return null;
        }

        MinigameHandler.Instance.SetCanMove(false);
        MinigameHandler.Instance.GetPlayer().GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(MinigameCanvas.Instance.ShowOutcome(true));
    }
}