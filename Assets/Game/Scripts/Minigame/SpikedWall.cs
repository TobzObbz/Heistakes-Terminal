using System.Collections;
using UnityEngine;

public class SpikedWall : MonoBehaviour
{
    [SerializeField] private float stunTime = 1.0f;

    [SerializeField] private AudioSource auSpikeHit;

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider == MinigameHandler.Instance.GetPlayer().GetComponent<BoxCollider2D>())
        {
            StartCoroutine(StunPlayer());
        }
    }

    private IEnumerator StunPlayer()
    {
        MinigameHandler.Instance.SetCanMove(false);
        MinigameHandler.Instance.SetAdjustedMove(Vector2.zero);

        MinigameHandler.Instance.GetPlayer().GetComponent<SpriteRenderer>().color = Color.red;

        auSpikeHit.Play();

        yield return new WaitForSeconds(stunTime);

        MinigameHandler.Instance.SetCanMove(true);

        MinigameHandler.Instance.GetPlayer().GetComponent<SpriteRenderer>().color = Color.white;
    }
}