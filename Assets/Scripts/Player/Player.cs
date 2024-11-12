using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IDamageable
{
    public event Action DeidPlayer;

    [SerializeField] private float _speed;
    [SerializeField] private Transform _startBulletPosition;
    private float _speedRotation = 900f;

    private CharacterController _characterController;
    private Shoot _shoot;
    private InputHandler _inputHandler;
    private Mover _mover;
    private Rotator _rotator;
    private Health _health;

    private HealthBar _healthBar;    

    private bool _isDead;
    private bool _isShooting;

    public void Initialize(InputHandler inputHandler, Health health, HealthBar healthBar)
    {
        _characterController = GetComponent<CharacterController>();
        _shoot = GetComponent<Shoot>();

        _inputHandler = inputHandler;
        _mover = new Mover();
        _rotator = new Rotator(this.transform);
        _health = health;
        _healthBar = healthBar;
       

        _health.ChangedHealth += OnChangedHealth;
    }

    private void OnDestroy()
    {
        _health.ChangedHealth -= OnChangedHealth;
    }   

    private void Update()
    {
        if (_isDead)
            return;

        _inputHandler.Update();
        _mover.MoveTo(_speed, _inputHandler.InputDirection, _characterController);
        _rotator.RotateTo(_speedRotation, _inputHandler.InputDirection);

        if (_isShooting)
        {
            if (_inputHandler.IsClickButtonShooting)
                _shoot.ToFire(_startBulletPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)          
            TakeDamage(enemy.DamageAplly);        
    }

    public void TakeDamage(int damage)
    {
        _health.Reduce(damage); 
    }

    public void ToDie()
    {
        _healthBar.Hide();
        _isDead = true;
        gameObject.SetActive(false);
    }
    
    public void ActivateHealthBar() => _healthBar.Show();    
    public void SetShoot(bool shoot) => _isShooting = shoot;

    private void OnChangedHealth(int currenthealth)
    {
        if (_health.CurrentHealth <= 0)
        {
            DeidPlayer?.Invoke();
            ToDie();
        }
    }

}
