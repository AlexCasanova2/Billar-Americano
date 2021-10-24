using UnityEngine;

public class Ball : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("StripedBalls") || collision.collider.CompareTag("SmoothBall"))
        {
            audioSource.Play();
        }
    }
}
