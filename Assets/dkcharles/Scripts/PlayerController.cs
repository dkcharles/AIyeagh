using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _Speed = 3;
    [SerializeField] Camera _Camera;
    private PlayerInput _Input;
    private Vector2 _Movement, _MousePos;
    Rigidbody2D _Rigidbody2D;
    private void Awake()
    {
        _Input = new PlayerInput();
        _Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _Input.Enable();

        _Input.Player.Movement.performed += OnMovement;
        _Input.Player.Movement.canceled += OnMovement;

        _Input.Player.MousePos.performed += OnMousePos;
        _Input.Player.MousePos.canceled += OnMousePos;
    }
    private void OnDisable()
    {
        _Input.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _Movement = context.ReadValue<Vector2>();
    }

    private void OnMousePos(InputAction.CallbackContext context)
    {
        _MousePos = _Camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        _Rigidbody2D.AddForce( _Movement * _Speed);

        Vector2 facingDirection = _MousePos - _Rigidbody2D.position;
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        _Rigidbody2D.MoveRotation(angle); 
    }
}
