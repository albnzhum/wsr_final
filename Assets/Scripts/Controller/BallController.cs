using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController
{
    private static BallView _ballView;
    private static BallModel _ballModel;

    private static BallController _instance;

    public static BallController Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BallController( _ballModel, _ballView);
            return _instance;
        }
    }

    public BallController(BallModel ballModel, BallView ballView)
    {
        _ballModel = ballModel;
        _ballView = ballView;
    }

    public void IsBallGrounded(bool value)
    {
        _ballModel.IsGrounded = value;
        _ballModel.IsHit = false;
    }

    public void IsBallHitting(bool value)
    {
        _ballModel.IsHit = value;
    }
}
