using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IConditions 
{
    public event Action Ņonditionfulfilled;
    void OnEnable();
    void OnDisable();
    void Update();
}
