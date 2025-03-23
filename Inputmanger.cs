using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Inputmanger : MonoBehaviour
{   
    private Playerinput playerInput;
    private Playerinput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook Look;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new Playerinput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        onFoot.Jump.performed += ctx => motor.Jump();
        Look = GetComponent<PlayerLook>();
        onFoot.Sprint.performed += ctx => motor.Sprint(true); 
        onFoot.Sprint.canceled += ctx => motor.Sprint(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        Look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
