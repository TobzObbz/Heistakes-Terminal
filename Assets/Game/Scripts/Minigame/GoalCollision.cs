using System.Collections;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    private Coroutine successCoroutine;

    private bool wasAbleToMove = false;

    private bool levelCompleted = false;

    private void Start()
    {
        levelCompleted = false;
    }

    private void OnTriggerStay2D(Collider2D _collider)
    {
        if (_collider != MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>() || levelCompleted) return;

        if (!MinigameHandler.Instance.GetCanMove() && wasAbleToMove)
        {
            if (successCoroutine != null)
            {
                StopCoroutine(successCoroutine);
                successCoroutine = null;
            }

            Animator anim = GetComponent<Animator>();
            anim.Play("Gate Move", 0, 0f);
            anim.Update(0f);
            anim.enabled = false;
        }

        if (MinigameHandler.Instance.GetCanMove() && !wasAbleToMove)
        {
            if (successCoroutine == null)
            {
                successCoroutine = StartCoroutine(OnSuccess());
            }
        }

        wasAbleToMove = MinigameHandler.Instance.GetCanMove();
    }

    private void OnTriggerExit2D(Collider2D _collider)
    {
        if (!gameObject.activeInHierarchy || !MinigameHandler.Instance.GetPlayer().activeInHierarchy) return;

        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>())
        {
            Animator anim = GetComponent<Animator>();
            anim.Play("Gate Move", 0, 0f);  
            anim.Update(0f);
            anim.enabled = false;

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

        levelCompleted = true;
        MinigameHandler.Instance.SetCanMove(false);
        MinigameHandler.Instance.GetPlayer().GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(MinigameCanvas.Instance.ShowOutcome(true));
    }
}