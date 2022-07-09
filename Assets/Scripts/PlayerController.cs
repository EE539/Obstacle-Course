using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Vector3 m_Position;

    private bool m_Running, m_LookAround, m_Dance, m_Finish;
    private float m_horizontalInput, waitTime = 19f, wait = 0f;
    private int choosenAnimation = 0;

    public InputActionAsset Map;
    InputActionMap gameplay;
    InputAction startGame, movePlayerHorizontal;
    private void Awake()
    {
        gameplay = Map.FindActionMap("Player");
        startGame = gameplay.FindAction("Start");
        movePlayerHorizontal = gameplay.FindAction("Move");

        startGame.performed += StartGame_performed;
        movePlayerHorizontal.performed += MovePlayerHorizontal_performed;
    }

    private void MovePlayerHorizontal_performed(InputAction.CallbackContext context)
    {
        m_horizontalInput = context.ReadValue<float>();
    }

    private void StartGame_performed(InputAction.CallbackContext context)
    {
        m_Running = true;
        m_Animator.SetBool("Running", m_Running);
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
        m_Dance = false;
        m_LookAround = false;
        m_Running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Running && !m_Finish) //E�er oyun ba�lamad�ysa bekleme animasyonlar�n� aktifle�tir
        {
            if(waitTime < wait && waitTime > 10)
            {
                choosenAnimation = Random.Range(0, 3);
                Debug.Log("Se�ile animasyon numaras� = " + choosenAnimation);
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
        else if(!m_Finish) //E�er kullan�c� oyunu ba�latt�ysa
        {

                transform.Translate(Vector3.right*m_horizontalInput*05f*Time.deltaTime);
        }
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_Running = false;
        m_Animator.SetBool("Running", m_Running);
        string winner = "win";
        if (collision.gameObject.tag.Equals("Finish"))
        {
            choosenAnimation = Random.Range(1, 6);
            GetComponent<Rigidbody>().freezeRotation = true;
            m_Finish = true;
            m_Animator.SetBool("Finished", m_Finish);
            winner = winner + choosenAnimation.ToString();
            m_Animator.SetTrigger(winner);
        }
    }
}
