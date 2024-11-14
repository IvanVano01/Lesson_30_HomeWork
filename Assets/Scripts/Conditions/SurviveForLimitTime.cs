using System;
using System.Collections;
using UnityEngine;

public class SurviveForLimitTime : IConditions
{
    public event Action Ñonditionfulfilled;

    private MonoBehaviour _monoBehaviour;
    private Coroutine _timerCoroutine;
    private int _surviveInTimeMax;
    private float _currentTime;

    private GameController _gameController;
    private Player _player;
    private TimerView _timerView;

    public SurviveForLimitTime(MonoBehaviour monoBehaviour, GameController gameController, int surviveInTimeMax, TimerView timerView, Player player)
    {
        _monoBehaviour = monoBehaviour;
        _gameController = gameController;
        _player = player;        
        _surviveInTimeMax = surviveInTimeMax;
        _timerView = timerView;         
    }   

    public void OnDisable()
    {
        if (_timerCoroutine != null)
        _monoBehaviour.StopCoroutine(_timerCoroutine);

        _timerCoroutine = null;
    }

    public void OnEnable()
    {
        _player.ActivateHealthBar();
        _timerView.Show();
        _timerCoroutine = _monoBehaviour.StartCoroutine(StartTimer());
    }

    public void Update()
    {
        _timerView.Hide();
        Ñonditionfulfilled?.Invoke();
    }

    private IEnumerator StartTimer()
    {
        _currentTime = 0;

        while (_currentTime <= _surviveInTimeMax)
        {
            if (_gameController.IsRunningGame == false)
                _currentTime = 0;

            _currentTime += Time.deltaTime;
            _timerView.SetTimerView((float)Math.Round(_currentTime, 0));

            yield return null;
        }

        _timerCoroutine = null;
        Update();
    }
}
