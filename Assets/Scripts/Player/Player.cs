using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isPaused;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rollSpeed;

    private Rigidbody2D Rigidbody2D;
    private PlayerItemsController playerItemsController;

    private float speed;
    private bool _isRolling;
    private bool _isRunning;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;
    private Vector2 _direction;

    private int handlingObject;

    #region Properties

    public bool isRolling { get { return _isRolling; } set { _isRolling = value; } }
    public bool isRunning{ get { return _isRunning; } set { _isRunning = value; } }
    public bool isCutting{ get { return _isCutting; } set { _isCutting = value; } }
    public bool isDigging{ get { return _isDigging; } set { _isDigging= value; } }
    public bool isWatering { get { return _isWatering; } set { _isWatering = value; } }
    public Vector2 direction { get { return _direction; } set {  _direction = value; } }

    public int HandlingObject { get => handlingObject; set => handlingObject = value; }

    #endregion

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        playerItemsController = GetComponent<PlayerItemsController>();
        speed = initialSpeed;
    }

    private void Update()
    {
        if (!isPaused)
        {
            //try "else if"
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HandlingObject = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HandlingObject = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HandlingObject = 2;
            }
            OnInput();
            OnRun();
            OnRolling();
            //Need to fix changing tools problem
            switch (HandlingObject)
            {
                case 0:
                    OnCutting();
                    if (HandlingObject != 0)
                    {
                        isCutting = false;
                    }
                    break;
                case 1:
                    OnDigging();
                    if (HandlingObject != 1)
                    {
                        isDigging = false;
                    }
                    break;
                case 2:
                    OnWatering();
                    if (HandlingObject != 2)
                    {
                        isWatering = false;
                    }
                    break;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SceneManager.LoadScene("test");
        //}
    }

    private void FixedUpdate()
    {
        if(!isPaused)
        {
            OnMove();
        }
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

    #region Actions

    void OnCutting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCutting = true;
            speed = 0.5f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isCutting = false;
            speed = initialSpeed;
        }
    } 
    
    void OnDigging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDigging = true;
            speed = 0.5f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDigging = false;
            speed = initialSpeed;
        }
        
    }
    
    void OnWatering()
    {
        if (Input.GetMouseButtonDown(0) && playerItemsController.CurrentWater > 0)
        {
            isWatering = true;
            speed = 0.5f;
        }
        if (Input.GetMouseButtonUp(0) || playerItemsController.CurrentWater <= 0)
        {
            isWatering = false;
            speed = initialSpeed;
        }
        if (isWatering)
        {
            playerItemsController.CurrentWater -= 0.01f;
        }
        
    }


    #endregion
}
