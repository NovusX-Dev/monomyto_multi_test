using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction _movementAction;

    [Header ("Parameters")]
    [SerializeField] float _movementSpeed = 5f;
    [SerializeField] float _rotateSpeed = 2f;

    private Vector2 _moveDir;
    private Vector3 _mousePos;

    Rigidbody2D _rb2D;
    Camera _camera;

    private void OnEnable()
    {
        _movementAction.Enable();
    }

    private void OnDisable()
    {
        _movementAction.Disable();
    }

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    void Start()
    {
        
    }

    void Update()
    {
        CalculateMoveDirection();
        RotateTowardsMouse();
    }

    private void CalculateMoveDirection()
    {
        _moveDir.x = _movementAction.ReadValue<Vector2>().x;
        _moveDir.y = _movementAction.ReadValue<Vector2>().y;
        _moveDir *= _movementSpeed;
    }

    private void RotateTowardsMouse()
    {
        _mousePos = _camera.ScreenToViewportPoint(Input.mousePosition);
        var _lookDirection = _mousePos - transform.position;
        var angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
        var _rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, _rotateSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _rb2D.MovePosition(_rb2D.position + _moveDir * Time.fixedDeltaTime);
    }
}
