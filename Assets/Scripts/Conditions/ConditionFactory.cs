using System;
using UnityEngine;

public class ConditionFactory
{
    private GameController _gameController;
    private SpawnerEnemy _spawnerEnemy;
    private Player _player;
    private MonoBehaviour _monoBehaviour;
    private int _surviveInTimeMax;
    private int _numberKilledEnemiesForVictory;
    private int _numberEnemiesInSceneForDefeat;
    private TimerView _timerView;
    private KillsEnemyView _killsEnemyView;

    public ConditionFactory(GameController gameController, SpawnerEnemy spawnerEnemy, Player player, MonoBehaviour monoBehaviour, int surviveInTimeMax, int numberKilledEnemiesForVictory,
        int numberEnemiesInSceneForDefeat, TimerView timerView, KillsEnemyView killsEnemyView)
    {
        _gameController = gameController;
        _spawnerEnemy = spawnerEnemy;
        _player = player;
        _monoBehaviour = monoBehaviour;
        _surviveInTimeMax = surviveInTimeMax;
        _timerView = timerView;
        _killsEnemyView = killsEnemyView;
        _numberKilledEnemiesForVictory = numberKilledEnemiesForVictory;
        _numberEnemiesInSceneForDefeat = numberEnemiesInSceneForDefeat;
    }

    public IConditions GetConditionsVictory(GameVictory gameVictory)
    {
        switch (gameVictory)
        {
            case GameVictory.SurviveForLimitTime:
                {
                    IConditions conditions = new SurviveForLimitTime(_monoBehaviour, _gameController, _surviveInTimeMax, _timerView, _player);

                    return conditions;
                }
            case GameVictory.KillLimitedNumberEnemies:
                {
                    IConditions conditions = new KillEnemies(_numberKilledEnemiesForVictory, _spawnerEnemy, _player, _killsEnemyView);
                    return conditions;
                }

            default:
                throw new ArgumentNullException(" Нет такого условия для победы! ", nameof(gameVictory));
        }
    }

    public IConditions GetConditionsDefeat(GameDefeat gamedefeat)
    {
        switch (gamedefeat)
        {
            case GameDefeat.NumberEnymiesSpawnedInScence:
                {
                    IConditions condition = new NumberEnymiesInScence(_numberEnemiesInSceneForDefeat, _spawnerEnemy, _player);
                    return condition;
                }
            case GameDefeat.PlayerDied:
                {
                    IConditions condition = new PlayerDied(_player);
                    return condition;
                }
            default:
                throw new ArgumentNullException(" Нет такого условия для поражения!", nameof(gamedefeat));
        }
    }
}
