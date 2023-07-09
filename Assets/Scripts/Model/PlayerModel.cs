using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerModel : INotifyPropertyChanged
{
    private readonly GameManager gameManager;
    public event PropertyChangedEventHandler PropertyChanged;

    // Паттерн Одиночка
    private static PlayerModel _instance;
    public static PlayerModel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerModel();
            }
            return _instance;
        }
    }

    private PlayerModel()
    {
        gameManager = GameManager.Instance;
    }

    protected virtual void OnPropertyChanged(string property)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    // мяч в руках
    private bool isBallInHands;
    public bool IsBallInHands
    {
        get => isBallInHands;

        set 
        {
            OnPropertyChanged(nameof(IsBallInHands));
            gameManager.IsBallInHands = value;
        }    
    }

    // ожидание броска
    private bool isWaiting;
    public bool IsWaiting
    {
        get => isWaiting;
        set
        {
            OnPropertyChanged(nameof(IsWaiting));
            gameManager.IsWaiting = value;
        }
    }

    // совершен бросок
    private bool isThrowing;
    public bool IsThrowing
    {
        get => isThrowing;
        set
        {
            OnPropertyChanged(nameof(IsThrowing));
            gameManager.IsThrowing = value;
        }
    }

    private bool isBallThrown;
    public bool IsBallThrown
    {
        get => isBallThrown;
        set
        {
            OnPropertyChanged(nameof(IsBallThrown));
            isBallThrown = value;
        }
    }
}
