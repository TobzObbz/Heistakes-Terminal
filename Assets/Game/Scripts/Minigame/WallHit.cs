using UnityEngine;

public class WallHit : MonoBehaviour
{
    [SerializeField] private AudioSource auWallHit;

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        auWallHit.Play();
    }
}
