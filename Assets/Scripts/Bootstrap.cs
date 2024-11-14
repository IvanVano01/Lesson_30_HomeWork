using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Bootstrap : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] GameDefeat _gameDefeat;
    [SerializeField] GameVictory _gameVictory;
    [SerializeField] private int _numberKilledEnemiesForVictory;
    [SerializeField] private int _numberEnemiesInSceneForDefeat;
    [SerializeField] private int _surviveInTimeMax;

    [Header("Links")]
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _pointSpawnPosition;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _targetCamera;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] List<Transform> pointsSpawnPosition;
    [SerializeField] private Transform _centrArenaPosition;

    [Header("UI")]
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private TimerView _timerView;
    [SerializeField] private KillsEnemyView _killsEnemyView;

    private GameController _gameController;
    private ConditionFactory _conditionFactory;

    private InputHandler _inputHandler;
    private SpawnerPlayer _spawnePlayer;
    private SpawnerEnemy _spawnerEnemy;

    private void Awake()
    {
        _inputHandler = new InputHandler();
        _spawnePlayer = new SpawnerPlayer(_playerPrefab, _inputHandler, _healthBar, _pointSpawnPosition.transform.position);
        _spawnerEnemy = new SpawnerEnemy(_enemyPrefab, _centrArenaPosition, this, pointsSpawnPosition);
        _killsEnemyView.Initialize(_spawnerEnemy);        

        _gameController = new GameController(_gameDefeat, _gameVictory, _conditionFactory, _numberKilledEnemiesForVictory, _numberEnemiesInSceneForDefeat, _surviveInTimeMax,
            _gameOverPanel, _timerView, _killsEnemyView, _spawnePlayer, _spawnerEnemy, this);

        _targetCamera.transform.position = new Vector3(_spawnePlayer.Player.transform.position.x, _targetCamera.position.y, _spawnePlayer.Player.transform.position.z);
        _targetCamera.transform.SetParent(_spawnePlayer.Player.transform);
    }

    private void Update()
    {
        _gameController.Update();
    }
}
