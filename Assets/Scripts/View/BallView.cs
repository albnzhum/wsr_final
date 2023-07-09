using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{
    private static GameManager gameManager;
    private static BallModel _ballModel;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rotationSpeed = 5f;

    private Coroutine rotationCoroutine;


    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Ring").transform;
        _ballModel = BallModel.Instance;
        gameManager = GameManager.Instance;
        animator.applyRootMotion = false;
    }

    public void PlayThrowAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("IsThrowning", true);
            animator.applyRootMotion = true;
        }
    }

    public void StartRotation()
    {
        // ��������� �������� ��� �������� ����
        rotationCoroutine = StartCoroutine(RotateBall());
    }

    public void StopRotation()
    {
        // ������������� �������� �������� ����
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            BallController.Instance.IsBallGrounded(true);
            StartCoroutine(DestroyBallDelayed(_ballModel.ballLifeTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ring"))
        {
            BallController.Instance.IsBallHitting(true);
            Score score = other.gameObject.GetComponent<Score>();
            score.CollectScore();
            Debug.Log("comapred");
        }
    }

    private IEnumerator DestroyBallDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
        gameManager.CreateBall();
        
    }

    private IEnumerator RotateBall()
    {
        while (true)
        {
            // ������� ��� ������ ��� Y (����� �������� �� ������ ��� �� ������ �������)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
