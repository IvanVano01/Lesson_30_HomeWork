using UnityEngine;

public class Mover 
{ 
    public void MoveTo(float speed, Vector3 direction, CharacterController characterController)
    {
        Vector3 directionNormalized = direction.normalized;

        characterController.Move(directionNormalized * speed * Time.deltaTime);
    }
}
