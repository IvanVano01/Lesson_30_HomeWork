using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public event Action<bool, bool> GameFinished;

    [Header("Options")]
    [SerializeField] GameDefeat _gameDefeat;
    [SerializeField] GameVictory _gameVictory;

    [Header("Links")]
    [SerializeField] private int _numberKilledEnemiesForVictory;
    [SerializeField] private int _numberEnemiesInSceneForDefeat;
    [SerializeField] private int _surviveInTimeMax;

    [Header("UI")]
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private KillsEnemyView _killsEnemyView;

    private SpawnerPlayer _spawnerPlayer;
    private Player _player;
    private SpawnerEnemy _spawner;
    private SwitcherGameConditions _switcherGameConditions;
    private IConditionsDefeat _conditionsDefeat;
    private IConditionsVictory _conditionsVictory;

    public bool IsRunningGame { get; private set; }

    public GameDefeat GameDefeat => _gameDefeat;
    public GameVictory GameVictory => _gameVictory;

    public int NumberKilledEnemiesForVictory => _numberKilledEnemiesForVictory;
    public int NumberEnemiesInSceneForDefeat => _numberEnemiesInSceneForDefeat;

    public void Initialize(SpawnerEnemy spawner, SpawnerPlayer spawnerPlayer)
    {
        _spawner = spawner;
        _killsEnemyView.Initialize(_spawner);
        _spawnerPlayer = spawnerPlayer;
        _player = _spawnerPlayer.Player;
        _switcherGameConditions = new SwitcherGameConditions(this, _spawner, _player, this, _surviveInTimeMax, _timerView, _killsEnemyView);
        _gameOverPanel.Initialize(this);

        _player.DeidPlayer += OnDeidPlayer;
        StartGame();
    }

    public void Update()
    {
        if (IsRunningGame == false)
            return;

        _spawner.Update();
    }

    private void OnDestroy()
    {
        _conditionsVictory.—onditionVictoryfulfilled -= On—onditionVictoryfulfilled;
        _conditionsDefeat.—onditionDefeatfulfilled -= On—onditionDefeatfulfilled;
    }

    private void StartGame()
    {
        _switcherGameConditions.SwitchGameConditions(_gameVictory, _gameDefeat);        

        IsRunningGame = true;
    }

    private void WinGame()
    {
        GameOver(true, false);
    }

    private void DefeatGame()
    {
        GameOver(false, true);
    }

    public void SetConditionsVictory(IConditionsVictory conditionsVictory)
    {
        if (conditionsVictory == null)
            throw new ArgumentNullException(nameof(conditionsVictory));

        _conditionsVictory = conditionsVictory;
        _conditionsVictory.—onditionVictoryfulfilled += On—onditionVictoryfulfilled;
    }

    public void SetConditionsDefeat(IConditionsDefeat conditionsDefeat)
    {
        if (conditionsDefeat == null)
            throw new ArgumentNullException(nameof(conditionsDefeat));

        _conditionsDefeat = conditionsDefeat;
        _conditionsDefeat.—onditionDefeatfulfilled += On—onditionDefeatfulfilled;
    }

    private void OnDeidPlayer()
    {
        DefeatGame();
    }

    private void On—onditionDefeatfulfilled()
    {
        DefeatGame();
    }

    private void On—onditionVictoryfulfilled()
    {
        WinGame();
    }

    private void GameOver(bool win, bool lost)
    {
        GameFinished?.Invoke(win, lost);
       
        _timerView.Hide();

        IsRunningGame = false;
    }
}
