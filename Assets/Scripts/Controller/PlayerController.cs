using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController 
{
    private static PlayerView _playerView;
    private static  PlayerModel _playerModel;

    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerController(_playerModel, _playerView);
            return _instance;
        }
    }

    public PlayerController(PlayerModel playerModel, PlayerView playerView)
    {
        _playerModel = playerModel;
        _playerView = playerView;
    }

    public void TakingBall()
    {
        _playerModel.IsBallInHands = true;
    }

    public void OnWaiting()
    {
        _playerModel.IsWaiting = true;
    }

    public void OnThrowing()
    {
        _playerModel.IsBallInHands = false;
        _playerModel.IsWaiting = false;
        _playerModel.IsThrowing = true;
        _playerModel.IsBallThrown = true;
    }
}
