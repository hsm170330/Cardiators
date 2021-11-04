using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedPause = delegate { };
    public event Action PressedLeft = delegate { };
    public event Action PressedRight = delegate { };

    void Update()
    {
        DetectConfirm();
        DetectPause();
        DetectLeft();
        DetectRight();
    }

    private void DetectRight()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PressedRight?.Invoke();
        }
    }

    private void DetectLeft()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressedLeft?.Invoke();
        }
    }

    private void DetectPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedPause?.Invoke();
        }
    }

    private void DetectConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressedConfirm?.Invoke();
        }
    }
}
