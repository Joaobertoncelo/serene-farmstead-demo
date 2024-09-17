using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float initialSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rollSpeed;

    private Rigidbody2D Rigidbody2D;

    private bool _isRolling;
    public bool isRolling { get { return _isRolling; } set { _isRolling = value; } }
    private bool _isRunning;
    public bool isRunning{ get { return _isRunning; } set { _isRunning = value; } }
    private float speed; 
    private Vector2 _direction;
    public Vector2 direction { get { return _direction; } set {  _direction = value; } }

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        speed = initialSpeed;
    }

    private void Update()
    {
        OnInput();
        OnRun();
        OnRolling();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {

        Rigidbody2D.MovePosition(Rigidbody2D.position + _direction * speed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    void OnRolling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            speed = rollSpeed;
            _isRolling = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            speed = initialSpeed;
        }
    }

    #endregion
}
