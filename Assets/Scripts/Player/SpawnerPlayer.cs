using UnityEngine;

public class SpawnerPlayer
{
    private GameController _gameController;

    private Player _playerPrefab;
    private InputHandler _inputHandler;
    private Health _playerHealth;
    private HealthBar _healthBar;
    private Vector3 _pointSpawnPosition;
    private Player _player;
    private int _amountHelth = 10;

    public Player Player => _player;

    public SpawnerPlayer(Player playerPrefab, InputHandler inputHandler, HealthBar healthBar, Vector3 playerSpawnPosition, GameController gameController)
    {
        _playerPrefab = playerPrefab;
        _inputHandler = inputHandler;

        _playerHealth = new Health(_amountHelth);
        _healthBar = healthBar;
        _healthBar.Initialize(_playerHealth);

        _pointSpawnPosition = playerSpawnPosition;
        _gameController = gameController;
        ToSpawn();
    }

    private void ToSpawn()
    {
        Player player = GameObject.Instantiate(_playerPrefab, _pointSpawnPosition, Quaternion.identity, null);
        player.Initialize(_inputHandler, _playerHealth, _healthBar, _gameController);
        _player = player;
    }
}
