using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _pointSpawnPosition;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _targetCamera;

    [SerializeField] private Enemy _enemyPrefab;    
    [SerializeField] List<Transform> pointsSpawnPosition;
    [SerializeField] private Transform _centrArenaPosition;

    private InputHandler _inputHandler;   
    private SpawnerPlayer _spawnePlayer;
    private SpawnerEnemy _spawnerEnemy;

    private void Awake()
    {
        _inputHandler = new InputHandler();       
        _spawnePlayer = new SpawnerPlayer(_playerPrefab, _inputHandler, _healthBar, _pointSpawnPosition.transform.position);       

        _spawnerEnemy = new SpawnerEnemy(_enemyPrefab, _centrArenaPosition, this, pointsSpawnPosition, _gameController);
        _gameController.Initialize(_spawnerEnemy, _spawnePlayer);

        _targetCamera.transform.position = new Vector3(_spawnePlayer.Player.transform.position.x, _targetCamera.position.y, _spawnePlayer.Player.transform.position.z);
        _targetCamera.transform.SetParent(_spawnePlayer.Player.transform);
    }
}
