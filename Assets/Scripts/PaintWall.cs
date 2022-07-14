using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class PaintWall : MonoBehaviour
{
    public Slider canvasPercantage;
    public Text canvasPercantageText;
    Collider m_Collider;
    Vector3 wallPosition, pointerPosition = Vector3.zero;
    Color redColor = Color.red;
    public float wallSpeed = 10;
    public int brushSize = 10, width = 100, height = 100;
    Texture2D wallTexture;
    private float paintedPixelCount = 0f;
    private float totalPixel;

    public InputActionAsset Map;
    InputActionMap gameplay;
    InputAction paintWallController;

    GameObject player;
    private bool paintStart = false;
    private void Awake()
    {
        gameplay = Map.FindActionMap("Painting");
        paintWallController = gameplay.FindAction("Paint");

    }

    private void PaintWallController_canceled(InputAction.CallbackContext obj)
    {
        paintStart = false;
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
        StartCoroutine(WaitABit());
        m_Collider = GetComponent<Collider>();
        wallPosition = new Vector3(transform.position.x, 0.6f, transform.position.z);
        wallTexture = new Texture2D(width, height);
        totalPixel = width * height;
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
                int pointerPositionX = Mathf.FloorToInt(((pointerPosition.x - minX) * -100) ); //Where is the point in x axis according to the cube
                int pointerPositionY = Mathf.FloorToInt(((pointerPosition.y - minY) * -100) ); //Where is the point in y axis according to the cube
                
                //Brush settings
                int xLimit = Mathf.Min(pointerPositionX + brushSize + 1, 100);
                int yLimit = Mathf.Min(pointerPositionY + brushSize + 1, 100);

                for (int y = pointerPositionY; y < yLimit; y++)
                {
                    for (int x = pointerPositionX; x < xLimit; x++)
                    {
                        if (!wallTexture.GetPixel(x, y).Equals(Color.red))
                        {
                            wallTexture.SetPixel(x, y, Color.red);
                            paintedPixelCount += 1.0f;
                        }
                    }
                }
                float value = paintedPixelCount / totalPixel;
                canvasPercantage.value = value;
                canvasPercantageText.text = (value * 100).ToString("F2") + "% has been completed";
                if (Mathf.Approximately(paintedPixelCount/10000f, 1f))
                {
                    Debug.Log("Yay");
                }
               
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

    IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(2f);
        canvasPercantage.gameObject.SetActive(true);
        canvasPercantageText.gameObject.SetActive(true);
        paintWallController.performed += PaintWallController_performed;
        paintWallController.canceled += PaintWallController_canceled;

    }
}
