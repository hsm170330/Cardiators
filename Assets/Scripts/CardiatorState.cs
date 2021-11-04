using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardiatorSM))]
public class CardiatorState : State
{
    protected CardiatorSM StateMachine { get; private set; }

    void Awake()
    {
        StateMachine = GetComponent<CardiatorSM>();
    }
}
