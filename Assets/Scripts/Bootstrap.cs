using System.Collections.Generic;
using UnityEngine;

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
    private ConditionSwitcher _conditionSwitcher;

    private InputHandler _inputHandler;
    private SpawnerPlayer _spawnePlayer;
    private SpawnerEnemy _spawnerEnemy;

    private void Awake()
    {
        _gameController = new GameController();
        _inputHandler = new InputHandler();

        _spawnePlayer = new SpawnerPlayer(_playerPrefab, _inputHandler, _healthBar, _pointSpawnPosition.transform.position, _gameController);
        Player player =_spawnePlayer.Player;

        _spawnerEnemy = new SpawnerEnemy(_enemyPrefab, _centrArenaPosition,_gameController, this, pointsSpawnPosition);
        _killsEnemyView.Initialize(_spawnerEnemy);

        _conditionFactory = new ConditionFactory(_gameController, _spawnerEnemy, player, this, _surviveInTimeMax, _numberKilledEnemiesForVictory,
            _numberEnemiesInSceneForDefeat, _timerView, _killsEnemyView);

        _conditionSwitcher = new ConditionSwitcher(_gameDefeat, _gameVictory, _gameController, _conditionFactory);
        _gameOverPanel.Initialize(_gameController);

        _targetCamera.transform.position = new UnityEngine.Vector3(_spawnePlayer.Player.transform.position.x, _targetCamera.position.y, _spawnePlayer.Player.transform.position.z);
        _targetCamera.transform.SetParent(_spawnePlayer.Player.transform);
    }    

    private void Update()
    {
        _gameController.Update();
        _spawnerEnemy.Update();
    }
}
