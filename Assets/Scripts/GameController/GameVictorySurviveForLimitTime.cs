using System;
using System.Collections;
using UnityEngine;

public class GameVictorySurviveForLimitTime : IConditionsVictory, IDisposable
{
    public event Action ÑonditionVictoryfulfilled;    

    private MonoBehaviour _monoBehaviour;
    private Coroutine _timerCoroutine;
    private int _surviveInTimeMax;
    private float _currentTime;
   
    private GameController _gameController;
    private Player _player;
    private TimerView _timerView;

    public float CurrentTime => _currentTime;

    public GameVictorySurviveForLimitTime(MonoBehaviour monoBehaviour, GameController gameController, int surviveInTimeMax, TimerView timerView, Player player)
    {
        _monoBehaviour = monoBehaviour;        
        _gameController = gameController;
        _player = player;
        _player.ActivateHealthBar();
        _surviveInTimeMax = surviveInTimeMax;
        _timerView = timerView;
        _timerView.Show();
        _timerCoroutine = _monoBehaviour.StartCoroutine(StartTimer());
        
        _player.DeidPlayer += OnDeidPlayer;
    }

    public void Dispose()
    {
        _player.DeidPlayer -= OnDeidPlayer;
    }


    public void ProcessingWinCondition()
    {
        _timerView.Hide();
        ÑonditionVictoryfulfilled?.Invoke();
    }

    public void ProcessingDefeatCondition()
    {        
        _player.ToDie();
       //ÑonditionDefeatfulfilled?.Invoke();
    }

    private IEnumerator StartTimer()
    {
        _currentTime = 0;

        while(_currentTime <= _surviveInTimeMax)
        {
            if (_gameController.IsRunningGame == false)
                _currentTime = 0;

            _currentTime += Time.deltaTime;            
            _timerView.SetTimerView((float)Math.Round(_currentTime, 0));           

            yield return null;
        }

        _timerCoroutine = null;
        ProcessingWinCondition();
    }
    
    private void OnDeidPlayer()
    {
       _monoBehaviour.StopCoroutine( _timerCoroutine );
        _timerCoroutine = null;
    }
}
