using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _velocity = 20f;
    private int _damageAplly = 4;

    public int DamageAplly => _damageAplly;

    private void Update()
    {
        Vector3 direction = Vector3.forward;
        transform.Translate(direction * _velocity * Time.deltaTime);
    }
}
