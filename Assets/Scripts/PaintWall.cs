using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PaintWall : MonoBehaviour
{
    Vector3 wallPosition, pointerPosition = Vector3.zero;
    Color redColor = Color.red;
    public float wallSpeed = 10;
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
        pointerPosition.z = 10;
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
            for (int y = 0; y < wallTexture.height; y++)
            {
                for (int x = 0; x < wallTexture.width; x++)
                {
                    Color color = Color.red;
                    wallTexture.SetPixel(x, y, color);
                }
            }
            wallTexture.Apply();
        }
    }
}
