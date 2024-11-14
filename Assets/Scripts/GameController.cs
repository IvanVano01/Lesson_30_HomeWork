using System;
using UnityEngine;

public class GameController 
{
    public event Action<bool> GameFinished;

    private GameDefeat _gameDefeat;
    private GameVictory _gameVictory;

    private int _numberKilledEnemiesForVictory;
    private int _numberEnemiesInSceneForDefeat;
    private int _surviveInTimeMax;

    private GameOverPanel _gameOverPanel;
    private TimerView _timerView;
    private KillsEnemyView _killsEnemyView;

    private SpawnerPlayer _spawnerPlayer;
    //private Player _player;
    private SpawnerEnemy _spawnerEnemy;
    private MonoBehaviour _monoBehaviour;

    private ConditionFactory _conditionFactory;
    private IConditions _currentConditionVictory;
    private IConditions _currentConditionDefeat;

    public GameController(GameDefeat gameDefeat, GameVictory gameVictory, ConditionFactory conditionFactory, int numberKilledEnemiesForVictory, int numberEnemiesInSceneForDefeat,
        int surviveInTimeMax, GameOverPanel gameOverPanel, TimerView timerView, KillsEnemyView killsEnemyView, SpawnerPlayer spawnerPlayer, SpawnerEnemy spawnerEnemy,
        MonoBehaviour monoBehaviour)
    {
        _gameDefeat = gameDefeat;
        _gameVictory = gameVictory;
        _conditionFactory = conditionFactory;

        _numberKilledEnemiesForVictory = numberKilledEnemiesForVictory;
        _numberEnemiesInSceneForDefeat = numberEnemiesInSceneForDefeat;
        _surviveInTimeMax = surviveInTimeMax;

        _gameOverPanel = gameOverPanel;
        _timerView = timerView;
        _killsEnemyView = killsEnemyView;

        _spawnerPlayer = spawnerPlayer;
        Player player = _spawnerPlayer.Player;
       
        _spawnerEnemy = spawnerEnemy;
        _monoBehaviour = monoBehaviour;

        _conditionFactory = new ConditionFactory(this, _spawnerEnemy, player, _monoBehaviour, _surviveInTimeMax, _timerView, _killsEnemyView);
        _gameOverPanel.Initialize(this);

        player.DeidPlayer += OnDeidPlayer;
        StartGame();
    }

    public bool IsRunningGame { get; private set; }
    public int NumberKilledEnemiesForVictory => _numberKilledEnemiesForVictory;
    public int NumberEnemiesInSceneForDefeat => _numberEnemiesInSceneForDefeat;

    public void Update()
    {
        if (IsRunningGame == false)
            return;

        _spawnerEnemy.Update();
    }

    public void StartGame()
    {
        SetConditionVictory(_conditionFactory.GetConditionsVictory(_gameVictory));
        SetConditionDefaet(_conditionFactory.GetConditionsDefeat(_gameDefeat));

        IsRunningGame = true;
    }

    private void WinGame()
    {
        GameOver(true);
    }

    private void DefeatGame()
    {
        GameOver(false);
    }

    public void SetConditionVictory(IConditions conditionVictory)
    {
        if (conditionVictory == null)
            throw new ArgumentNullException(nameof(conditionVictory));

        if (_currentConditionVictory != null)
        {
            _currentConditionVictory.—onditionfulfilled -= On—onditionfulfilledVictory;
            _currentConditionVictory.OnDisable();
        }

        _currentConditionVictory = conditionVictory;
        _currentConditionVictory.OnEnable();
        _currentConditionVictory.—onditionfulfilled += On—onditionfulfilledVictory;
    }

    public void SetConditionDefaet(IConditions conditionDefeat)
    {
        if (conditionDefeat == null)
            throw new ArgumentNullException(nameof(conditionDefeat));

        if (_currentConditionDefeat != null)
        {
            _currentConditionDefeat.—onditionfulfilled -= On—onditionfulfilledDefeat;
            _currentConditionDefeat.OnDisable();
        }

        _currentConditionDefeat = conditionDefeat;
        _currentConditionDefeat.OnEnable();
        _currentConditionDefeat.—onditionfulfilled += On—onditionfulfilledDefeat;
    }

    private void On—onditionfulfilledVictory()
    {
        WinGame();
    }

    private void On—onditionfulfilledDefeat()
    {
        DefeatGame();
    }

    private void OnDeidPlayer()
    {
        DefeatGame();
    }

    private void GameOver(bool isWin)
    {
        GameFinished?.Invoke(isWin);

        _timerView.Hide();

        IsRunningGame = false;
    }

    //void IDisposable.Dispose()
    //{
    //    _player.DeidPlayer -= OnDeidPlayer;
    //}
}
