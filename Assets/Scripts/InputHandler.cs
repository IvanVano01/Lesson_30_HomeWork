using UnityEngine;

public class InputHandler
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private float _horizontalAxisX;
    private float _horizontalAxisZ;

    public Vector3 InputDirection { get; private set; }

    public bool IsClickButtonShooting { get; private set; }

    public void Update()
    {
        _horizontalAxisX = Input.GetAxisRaw(Vertical);
        _horizontalAxisZ = Input.GetAxisRaw(Horizontal);

        InputDirection = new Vector3(-_horizontalAxisX, 0, _horizontalAxisZ);

        IsClickButtonShooting = Input.GetKeyDown(KeyCode.Space);
    }
}
