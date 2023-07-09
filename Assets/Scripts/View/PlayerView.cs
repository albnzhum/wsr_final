using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] public Animator playerAnimator;
    [SerializeField] private GameObject ball;
    //[SerializeField] 

    private static PlayerView _instance;
    public static PlayerView Instance
    {
        get 
        {
            if (_instance == null)
                _instance = new PlayerView();
            return _instance;
        }
    }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void PlayThrowAnimation(bool value)
    {
        playerAnimator.SetBool("IsThrowning", value);
    }

    public void PlayStartAnimation(bool value)
    {
        playerAnimator.SetBool("IsBallInHands", value);
    }

    public void PlayWaitingAnimation(bool value)
    {
        playerAnimator.SetBool("IsWaiting", value);
    }


}
