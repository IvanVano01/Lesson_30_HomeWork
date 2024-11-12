using UnityEngine;

public class Rotator
{
    private float _deadZone = 0.01f;

    private Transform _characterTransform;

    public Rotator(Transform characterTransform)
    {
        _characterTransform = characterTransform;
    }

    public void RotateTo(float speedRotation, Vector3 direction)
    {
        if (direction.magnitude > _deadZone)
        {
            Quaternion loockRotation = Quaternion.LookRotation(direction.normalized);

            _characterTransform.rotation = Quaternion.RotateTowards(_characterTransform.rotation, loockRotation, speedRotation * Time.deltaTime);
        }
    }

}
