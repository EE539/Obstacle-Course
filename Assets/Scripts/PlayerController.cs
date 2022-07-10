using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    /*To do
     Change the way movement works (wasd)
     Make rotating platform work*/
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Vector3 m_Position;

    private bool m_Running, m_LookAround, m_Dance, m_Finish, m_Fail, start;
    private float m_horizontalInput, m_verticalInput, waitTime = 19f, wait = 0f;
    private int choosenAnimation = 0;

    [SerializeField]public float horizontalMovement = 0f, verticalMovement = 0f, speed = 0.5f, horizontalSpeed = 100;
    
    public InputActionAsset Map;
    InputActionMap gameplay;
    InputAction startGame, movePlayerHorizontal, movePlayerVertical;
    private void Awake()
    {
        gameplay = Map.FindActionMap("Player");
        startGame = gameplay.FindAction("Start");
        movePlayerHorizontal = gameplay.FindAction("Move Horizontal");
        movePlayerVertical = gameplay.FindAction("Move Vertical");

        startGame.performed += StartGame_performed;
        movePlayerHorizontal.performed += MovePlayerHorizontal_performed;
        movePlayerVertical.performed += MovePlayerVertical_performed;

        movePlayerHorizontal.canceled += MovePlayerHorizontal_canceled;
    }

    private void MovePlayerHorizontal_canceled(InputAction.CallbackContext obj)
    {
        waitTime = 13.933f;
        horizontalMovement = 0f;
        m_Running = false;
        m_Animator.SetBool("Running", m_Running);
    }

    private void MovePlayerVertical_performed(InputAction.CallbackContext context)
    {
        m_verticalInput = context.ReadValue<float>();
        m_Running = true;
        m_Animator.SetBool("Running", m_Running);
    }

    private void MovePlayerHorizontal_performed(InputAction.CallbackContext context)
    {
        m_horizontalInput = context.ReadValue<float>();
        m_Running = true;
    }

    private void StartGame_performed(InputAction.CallbackContext context)
    {
        start = true;
    }

    private void OnEnable()
    {
        gameplay.Enable();
    }
    private void OnDisable()
    {
        gameplay.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Position = transform.position;

        m_Finish = false;
        m_Fail = false;
        m_Dance = false;
        m_LookAround = false;
        m_Running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Running && !m_Finish) //Eðer oyun baþlamadýysa bekleme animasyonlarýný aktifleþtir
        {
            if (waitTime < wait && waitTime > 10)
            {
                choosenAnimation = Random.Range(0, 3);
                Debug.Log("Seçile animasyon numarasý = " + choosenAnimation);
                if (choosenAnimation != 0)
                {
                    if (choosenAnimation == 1)
                    {
                        m_LookAround = true;
                        m_Animator.SetBool("Look Around", m_LookAround);
                        waitTime = 6.333f;
                    }
                    else if (choosenAnimation == 2)
                    {
                        m_Dance = true;
                        m_Animator.SetBool("Start Dance", m_Dance);
                        waitTime = 3.633f;
                    }
                }
                else if (choosenAnimation == 0)
                {
                    waitTime = 13.933f;
                }
                wait = 0;
            }
            else if (waitTime < wait)
            {
                waitTime = 13.933f;
                choosenAnimation = 0;
                m_LookAround = false;
                m_Animator.SetBool("Look Around", m_LookAround);
                m_Dance = false;
                m_Animator.SetBool("Start Dance", m_Dance);
                wait = 0;
            }
            wait += Time.deltaTime;
        }
        else if (start && !m_Finish) //Eðer kullanýcý oyunu baþlattýysa
        {
            horizontalMovement = m_horizontalInput * horizontalSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * horizontalMovement, Space.Self);
            //transform.transform.rotation = Quaternion.LookRotation(Vector3.right * m_horizontalInput);

            if(m_verticalInput == 0)
            {
                m_Running = false;
                m_Animator.SetBool("Running", m_Running);
            }   
            verticalMovement = m_verticalInput * speed * Time.deltaTime;
            transform.Translate(Vector3.forward * verticalMovement);

        }

        if (m_Fail)
        {
            StartEndAnimation(1, 3, "fail", "Fail");
        }
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag.Equals("Finish"))
        {
            Debug.Log("Entered");
            StartEndAnimation(1, 6, "win", "Finished");
        }
    }

    void StartEndAnimation(int begin, int endPlusOne, string winOrFail, string whichAnimator)
    {
        m_Running = false;
        m_Animator.SetBool("Running", m_Running);

        choosenAnimation = Random.Range(begin, endPlusOne);
        GetComponent<Rigidbody>().freezeRotation = true;
        m_Animator.SetBool(whichAnimator, true);
        winOrFail = winOrFail + choosenAnimation.ToString();
        m_Animator.SetTrigger(winOrFail); 
    }
}
