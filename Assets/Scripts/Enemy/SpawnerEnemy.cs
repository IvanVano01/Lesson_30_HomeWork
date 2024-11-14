using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerEnemy
{
    public event Action KilledEnemy;
    public event Action SpawnedNewEnemy;

    private MonoBehaviour _bevaviorCoroutiners;
    private GameController _gameController;
    private Queue<Vector3> _pointsSpawnPosition;
    private Enemy _spawnPrefab;
    private Transform _centrArenaPosition;
    private float _timeDelayToSpawn = 1.5f;
    private int _maxCountSpawn = 45;
    private Coroutine _delayToSpawnCoroutine;

    private int _healthEnemy = 4;
    private List<Enemy> _enemyArrayInScense;
    private Vector3 _currentPointSpawnPosition;

    public SpawnerEnemy(Enemy spawnPrefab, Transform centrArenaPosition, GameController gameController, MonoBehaviour bevaviorCoroutiners, List<Transform> pointsSpawn)
    {
        _spawnPrefab = spawnPrefab;

        _centrArenaPosition = centrArenaPosition;
        _bevaviorCoroutiners = bevaviorCoroutiners;
        _enemyArrayInScense = new List<Enemy>();

        _pointsSpawnPosition = new Queue<Vector3>(pointsSpawn.Select(pointSpawn => pointSpawn.position));
        _currentPointSpawnPosition = _pointsSpawnPosition.Dequeue();
        _delayToSpawnCoroutine = _bevaviorCoroutiners.StartCoroutine(StartTimerToSpawn());
        _gameController = gameController;
    }

    public void Update()
    {
        if (_gameController.IsRunningGame == false)
            return;

        if (_delayToSpawnCoroutine == null && _enemyArrayInScense.Count < _maxCountSpawn)
        {
            _delayToSpawnCoroutine = _bevaviorCoroutiners.StartCoroutine(StartTimerToSpawn());
        }
    }

    private IEnumerator StartTimerToSpawn()
    {
        float timer = 0;

        while (timer < _timeDelayToSpawn)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        ToSpawn();
        _delayToSpawnCoroutine = null;
    }

    private void ToSpawn()
    {
        var enemy = GameObject.Instantiate(_spawnPrefab, _currentPointSpawnPosition, Quaternion.identity, null);

        if (enemy.TryGetComponent(out Enemy newEnemy))
        {
            Health health = new Health(_healthEnemy);
            newEnemy.Initialize(_centrArenaPosition.position, health, this, _gameController);
            _enemyArrayInScense.Add(newEnemy);
            SwitchPointSpawn();

            SpawnedNewEnemy?.Invoke();
        }
        else
        {
            throw new ArgumentException($" У созданного объекта, отстутствует компонент Enemy");
        }
    }

    private void SwitchPointSpawn()
    {
        _pointsSpawnPosition.Enqueue(_currentPointSpawnPosition);
        _currentPointSpawnPosition = _pointsSpawnPosition.Dequeue();
    }

    public void ToReportEnemyKilled()
    {
        KilledEnemy?.Invoke();
    }
}
