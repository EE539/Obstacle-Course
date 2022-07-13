using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PaintWall : MonoBehaviour
{
    Collider m_Collider;
    Vector3 wallPosition, pointerPosition = Vector3.zero;
    Color redColor = Color.red;
    public float wallSpeed = 10;
    public int brushSize = 10;
    Texture2D wallTexture;

    public InputActionAsset Map;
    InputActionMap gameplay;
    InputAction paintWallController;

    GameObject player;
    private bool paintStart = false;
    private void Awake()
    {
        gameplay = Map.FindActionMap("Painting");
        paintWallController = gameplay.FindAction("Paint");

        paintWallController.performed += PaintWallController_performed;
    }

    private void PaintWallController_performed(InputAction.CallbackContext context)
    {
        pointerPosition = context.ReadValue<Vector2>();
        float distance = Vector3.Dot(transform.position - Camera.main.transform.position, Camera.main.transform.forward);
        pointerPosition.z = distance;
        pointerPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
        paintStart = true;
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
        m_Collider = GetComponent<Collider>();
        wallPosition = new Vector3(transform.position.x , 0.6f, transform.position.z);
        wallTexture = new Texture2D(128, 128);
        GetComponent<Renderer>().material.mainTexture = wallTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if(wallPosition.y > transform.position.y)
        {
            transform.Translate(Vector3.up * wallSpeed * Time.deltaTime);
        }
        if (paintStart)
        {
            float minX = m_Collider.bounds.min.x; //minimum point in x axis of the paint canvas
            float minY = m_Collider.bounds.min.y; //minimum point in y axis of the paint canvas
            float maxX = m_Collider.bounds.max.x; //maximum point in x axis of the paint canvas
            float maxY = m_Collider.bounds.max.y; //maximum point in y axis of the paint canvas
            if((pointerPosition.x >= minX && pointerPosition.x <= maxX) && (pointerPosition.y >= minY && pointerPosition.y <= maxY))
            {
                Color color = Color.red;
                int pointerPositionX = Mathf.FloorToInt(pointerPosition.x - minX), pointerPositionY = Mathf.FloorToInt(pointerPosition.y - minY);
                wallTexture.SetPixel(pointerPositionX, pointerPositionY, color);
                wallTexture.Apply();
            }
            /*for (int y = 0; y < wallTexture.height; y++)
            {
                for (int x = 0; x < wallTexture.width; x++)
                {
                    Color color = Color.red;
                    wallTexture.SetPixel(x, y, color);
                }
            }*/
            
        }
    }
}
