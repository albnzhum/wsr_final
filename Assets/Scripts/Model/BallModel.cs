using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BallModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private static readonly GameManager gameManager;

    private static BallModel _instance;
    public static BallModel Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BallModel();
            return _instance;
        }
    }

    private bool isGrounded;

    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }

    private bool isHit;

    public bool IsHit
    {
        get => isHit;
        set => isHit = value;
    }

    public float ballLifeTime = 4f;

    protected virtual void OnPropertyChanged(string property)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

}
