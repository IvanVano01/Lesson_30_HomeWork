using UnityEngine;

public class Shoot : MonoBehaviour 
{
    [SerializeField] private Bullet _bulletPrefab;
    private float _timeLife = 2.5f;

    public void ToFire(Transform startPosition)
    {
        Bullet bullet = Instantiate(_bulletPrefab, startPosition.position, Quaternion.identity, null);
       
        bullet.transform.rotation = startPosition.rotation;

        Destroy(bullet.gameObject, _timeLife);
    }
}
