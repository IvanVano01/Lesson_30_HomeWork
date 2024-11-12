using System;
using UnityEngine;

public class SwitcherGameConditions
{
    private GameController _gameController;
    private SpawnerEnemy _spawner;
    private Player _player;
    private MonoBehaviour _monoBehaviour;
    private int _surviveInTimeMax;
    private TimerView _timerView;
    private KillsEnemyView _killsEnemyView;

    public SwitcherGameConditions(GameController gameController, SpawnerEnemy spawner, Player player, MonoBehaviour monoBehaviour, int surviveInTimeMax, TimerView timerView, KillsEnemyView killsEnemyView)
    {
        _gameController = gameController;
        _spawner = spawner;
        _player = player;
        _monoBehaviour = monoBehaviour;
        _surviveInTimeMax = surviveInTimeMax;
        _timerView = timerView;
        _killsEnemyView = killsEnemyView;
    }

    public void SwitchGameConditions(GameVictory gameVictory, GameDefeat gameDefeat)
    {
        _gameController.SetConditionsVictory(GetConditionsVictory(gameVictory));
        _gameController.SetConditionsDefeat(GetConditionsDefeat(gameDefeat));
    }

    private IConditionsVictory GetConditionsVictory(GameVictory gameVictory)
    {
        switch (gameVictory)
        {
            case GameVictory.KillLimitedNumberEnemies:
                {
                    IConditionsVictory conditionsVictory = new GameVictoryKillEnemies(_gameController.NumberKilledEnemiesForVictory, _spawner, _player, _killsEnemyView);

                    return conditionsVictory;
                }

            case GameVictory.SurviveForLimitTime:
                {
                    IConditionsVictory conditionsVictory = new GameVictorySurviveForLimitTime(_monoBehaviour, _gameController, _surviveInTimeMax, _timerView, _player);

                    return conditionsVictory;
                }

            default:
                throw new ArgumentNullException(" Нет такого условия для победы! ", nameof(gameVictory));

        }
    }

    private IConditionsDefeat GetConditionsDefeat(GameDefeat gameDefeat)
    {
        switch (gameDefeat)
        {
            case GameDefeat.NumberEnymiesSpawnedInScence:
                {
                    IConditionsDefeat conditionsDefeat = new GameDefeatNumberEnymiesInScence(_gameController.NumberEnemiesInSceneForDefeat, _spawner);
                    return conditionsDefeat;
                }

            case GameDefeat.PlayerDied:
                {
                    IConditionsDefeat conditionsDefeat = new GameDefeatPlayerDied(_player);
                    return conditionsDefeat;
                }

            default:
                throw new ArgumentNullException(" Нет такого условия для поражения!", nameof(gameDefeat));

        }
    }
}
