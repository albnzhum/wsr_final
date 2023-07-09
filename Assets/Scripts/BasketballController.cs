using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketballController : MonoBehaviour
{
    public int scorePerHit = 1; 

    private bool hasScored = false;
    private Transform startPosition;

    [SerializeField] private GameObject prefab;

    private void Start()
    {
        startPosition = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            ScoreManager.Instance.AddScore(scorePerHit); 
            hasScored = true; 
        }
    }

    private float ballLifetime = 4f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // если это касание с землей, то устанавливаем время жизни мяча
            StartCoroutine(DestroyBallDelayed(ballLifetime));
        }
    }

    private IEnumerator DestroyBallDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        SpawnBall();
    }

    private void SpawnBall()
    {
        Instantiate(prefab, startPosition);
    }
}
