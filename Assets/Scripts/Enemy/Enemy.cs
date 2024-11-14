using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour, IDamageable
{
    private float _speed = 8;
    private float _speedRotation = 900f;
    private float _delayTimeMax = 2f;

    private float _radiusArenaMax = 15f;
    private Vector3 _centrArenaPosition;
    private Vector3 _moveDirection;
    private Coroutine _delayToChangeMoveDirection;

    private CharacterController _characterController;
    private Mover _mover;
    private Rotator _rotator;
    private Health _health;
    private GameController _gameController;
    private SpawnerEnemy _spawner;

    public int DamageAplly { get; private set; }

    public void Initialize(Vector3 centrArenaPosition, Health health, SpawnerEnemy spawner, GameController gameController)
    {
        _characterController = GetComponent<CharacterController>();
        _mover = new Mover();
        _rotator = new Rotator(this.transform);
        _health = health;
        _gameController = gameController;
        _spawner = spawner;

        DamageAplly = 2;
        _centrArenaPosition = centrArenaPosition;
        _moveDirection = RandomDirect();
        _delayToChangeMoveDirection = StartCoroutine(StartTimerForSwitchMoveDirection());

        _health.ChangedHealth += OnChangedHealth;
    }

    private void OnDestroy()
    {
        _health.ChangedHealth -= OnChangedHealth;
    }

    private void Update()
    {
        if (_gameController.IsRunningGame == false)
            return;

        if (_delayToChangeMoveDirection == null)
        {
            _delayToChangeMoveDirection = StartCoroutine(StartTimerForSwitchMoveDirection());
        }

        ToMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponentInParent<Bullet>();

        if (bullet != null)
        {
            Destroy(bullet.gameObject);

            TakeDamage(bullet.DamageAplly);
        }
    }

    private void OnDrawGizmos()
    {
        if (IsOutBoundsOfArena())
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawLine(_centrArenaPosition, this.transform.position);
    }

    public void TakeDamage(int damage)
    {
        _health.Reduce(damage);
    }

    private void OnChangedHealth(int obj)
    {
        if (_health.CurrentHealth <= 0)
            ToDie();
    }

    private void ToMove()
    {
        _mover.MoveTo(_speed, _moveDirection, _characterController);
        _rotator.RotateTo(_speedRotation, _moveDirection);
    }

    private IEnumerator StartTimerForSwitchMoveDirection()
    {
        float _currenTimer = 0f;

        while (_currenTimer < _delayTimeMax)
        {
            _currenTimer += Time.deltaTime;
            yield return null;
        }

        SwitchMoveDirection();
        _delayToChangeMoveDirection = null;
    }

    private void SwitchMoveDirection()
    {
        _moveDirection = RandomDirect();
    }

    private Vector3 RandomDirect()
    {
        Vector2 centerArena = new(_centrArenaPosition.x, _centrArenaPosition.y);
        Vector2 randomPoint = (UnityEngine.Random.insideUnitCircle * _radiusArenaMax) + centerArena;
        Vector3 pointPosition = new Vector3(randomPoint.y, 0, randomPoint.x);

        Vector3 randomDirect = pointPosition - transform.position;

        if (IsOutBoundsOfArena())
            return DirectToCenterArena();

        return randomDirect;
    }

    private bool IsOutBoundsOfArena()
    {
        Vector3 currentDistanceOfArensCenter = _centrArenaPosition - transform.position;

        if (currentDistanceOfArensCenter.magnitude > _radiusArenaMax)
            return true;

        return false;
    }

    private Vector3 DirectToCenterArena()
    {
        Vector3 directToArensCenter = _centrArenaPosition - transform.position;

        return directToArensCenter;
    }

    private void ToDie()
    {
        StopCoroutine(_delayToChangeMoveDirection);
        _delayToChangeMoveDirection = null;
        _spawner.ToReportEnemyKilled();
        Destroy(this.gameObject);
    }
}
