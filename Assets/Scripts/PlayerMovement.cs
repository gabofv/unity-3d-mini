using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] InputActionAsset _playerActionsAsset;
    [SerializeField] float _moveSpeed = 1.0f;

    InputActionMap _movementActionMap;
    InputAction _moveKeys;

    // Can also manage everything in script or every frame using Update()
     
    void Awake() {

        _movementActionMap = _playerActionsAsset.FindActionMap("movement");
        _moveKeys = _movementActionMap["move"];

    }

    void OnEnable() {

        // Enable all actions in this action map
        _movementActionMap.Enable();

    }

    void OnDisable() {

        // Disable all actions in this action map
        _movementActionMap.Disable();

    }

    void Update() {

        float horizontalThrow = 0f;
        float verticalThrow = 0f;

        if (_moveKeys.IsPressed()) {
            
            horizontalThrow = _moveKeys.ReadValue<Vector2>().x * Time.deltaTime * _moveSpeed;            
            verticalThrow = _moveKeys.ReadValue<Vector2>().y * Time.deltaTime * _moveSpeed;

        }

        float xPos = horizontalThrow + transform.localPosition.x;
        float yPos = verticalThrow + transform.localPosition.y;
        
        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
    
}
