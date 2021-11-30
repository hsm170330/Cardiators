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
    public event Action MouseMoved = delegate { };

    void Update()
    {
        DetectConfirm();
        DetectPause();
        DetectLeft();
        DetectRight();
        DetectMouse();
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            PressedConfirm?.Invoke();
        }
    }

    private void DetectMouse()
    {
        if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        {
            MouseMoved?.Invoke();
        }
    }
}
