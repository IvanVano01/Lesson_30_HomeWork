using System;

public interface IConditions
{
    public event Action �onditionfulfilled;
    void OnEnable();
    void OnDisable();
    void Update();
}
