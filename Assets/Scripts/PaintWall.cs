using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PaintWall : MonoBehaviour
{
    Vector3 wallPosition;
    public float wallSpeed = 10;

    public InputActionAsset Map;
    InputActionMap gameplay;
    InputAction paintWallController;

    GameObject player;
    private void Awake()
    {
        gameplay = Map.FindActionMap("Painting");
        paintWallController = gameplay.FindAction("Paint");

        paintWallController.performed += PaintWallController_performed;
    }

    private void PaintWallController_performed(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
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
    }

    // Update is called once per frame
    void Update()
    {
        if(wallPosition.y > transform.position.y)
        {
            transform.Translate(Vector3.up * wallSpeed * Time.deltaTime);
        }
    }
}
