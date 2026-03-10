using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>())
        {
            StartCoroutine(MinigameCanvas.Instance.ShowOutcome(true));
        }
    }
}