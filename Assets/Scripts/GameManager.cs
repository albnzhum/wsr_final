using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    [Header("Player MVC")]
    [SerializeField] private GameObject player;
    private PlayerView _playerView;
    private PlayerModel _playerModel;
    private PlayerController _playerController;

    [Header("Ball MVC")]
    [SerializeField] private GameObject ballPrefab;
    private GameObject currentBall;
    private BallView _ballView;
    private BallModel _ballModel;
    private BallController _ballController;

    [SerializeField] private GameObject ring;
    [SerializeField] private ParticleSystem fireParticle;
    [SerializeField] private TMP_Text timerText;

    private bool isMusicEnabled;
    private bool isSoundEnabled;
    private bool isBallInHands;
    private bool isWaiting;
    private bool isThrowing;
    private bool isBallFlying = false;

    private bool isWin = false;
    private bool isGameOver = false;

    private float timePerLevel = 30f;
    private Coroutine timerCoroutine;

    private float T;

    public bool IsMusicEnabled
    {
        get => isMusicEnabled;
        set
        {
            if (isMusicEnabled != value) isMusicEnabled = value;
        }
    }

    public bool IsSoundEnabled
    {
        get => isSoundEnabled;
        set
        {
            if (isSoundEnabled != value) isSoundEnabled = value;
        }
    }

    public bool IsBallInHands
    {
        get => isBallInHands;
        set
        {
            isBallInHands = value;
            _playerView.PlayStartAnimation(value);
        }
    }

    public bool IsWaiting
    {
        get => isWaiting;
        set
        {
            isWaiting = value;
            _playerView.PlayWaitingAnimation(value);
        }
    }

    public bool IsThrowing
    {
        get => isThrowing;
        set
        {
            isThrowing = value;
            _playerView.PlayThrowAnimation(value);
            _ballView.PlayThrowAnimation();
        }
    }

    private void Start()
    {
        
        _playerModel = PlayerModel.Instance ;
        _playerView = player.GetComponent<PlayerView>();
        _playerController = new PlayerController( _playerModel, _playerView);
        
        _ballModel = BallModel.Instance;
        
        _ballController = new BallController(_ballModel, _ballView);
        CreateBall();
        IsBallInHands = true;

        StartTimer();
    }

    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            IsWaiting = true;
            isBallInHands = false;
        }

        else if (UnityEngine.Input.GetMouseButtonUp(0) && IsWaiting)
        { 
            IsWaiting = false;
            isBallFlying = true;
            //isBallInHands = false;
            PlayerModel.Instance.IsBallThrown = true;
            T = 0;
        }
        if (isBallFlying)
        {
            IsThrowing = true;
            T += Time.deltaTime;
            float t01 = T / 0.5f;
            Vector3 A = player.transform.position;
            Vector3 B = ring.transform.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);
            Vector3 arc = Vector3.up * 1f * Mathf.Sin(t01 * Mathf.PI);
            currentBall.transform.position = pos + arc;

            if (t01 >= 1)
            {
                isBallFlying = false;
                IsThrowing = false;
            }
        }

        if (BallModel.Instance.IsHit)
        {
            fireParticle.Play();
            StartCoroutine(StopParticleSystemAfterDelay(fireParticle.main.duration));
            BallModel.Instance.IsHit = false;
            //fireParticle.Stop();
        }

        if (PlayerModel.Instance.IsBallThrown)
        {
            if (!_playerModel.IsBallInHands)
            {
                CreateBall();
                _playerModel.IsBallThrown = false;
            }
        }
    }

    public void CreateBall()
    {
        if (currentBall != null)
            Destroy(currentBall);
        currentBall = Instantiate(ballPrefab);
        PlayerModel.Instance.IsBallInHands = true;

        // Запуск вращения мяча после активации
        _ballView = currentBall.GetComponent<BallView>();
        if (_ballView != null)
        {
            _ballView.StartRotation();
        }
    }

    private IEnumerator StopParticleSystemAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fireParticle.Stop();
    }

    private void StartTimer()
    {
        if (timerCoroutine != null)
        {
            StopTimer();
        }
        timerCoroutine = StartCoroutine(timerTicking());
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }

    private IEnumerator timerTicking()
    {
        float timeRemaining = timePerLevel;
        while (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            int seconds = Mathf.RoundToInt(timeRemaining);
            timerText.text = FormatTime(seconds);
            yield return null;
        }
       timerText.text = "Time is up!";
    }

    private string FormatTime(int seconds)
    {
        seconds %= 60;
        return string.Format("{0:00}", seconds);
    }
}

