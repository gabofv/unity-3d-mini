using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] InputActionAsset _playerActionsAsset;

    [SerializeField] float _moveSpeed = 1f;

    // The GameObject this is attached to must be at (0;0) for coherence and camara centered
    [SerializeField] float _xAxisRange = 5f;
    [SerializeField] float _yAxisRange = 5f;

    InputActionMap _movementActionMap;
    InputAction _moveAction;

    [SerializeField] float _pitchPositionFactor = 1f;
    [SerializeField] float _rollPositionFactor = 1f;
    [SerializeField] float _yawPositionFactor = 1f;

    [SerializeField] float _pitchThrowFactor = 1f;
    [SerializeField] float _rollThrowFactor = 1f;

    float _xThrow;
    float _yThrow;

    // Can also manage everything in script or every frame using Update()

    void Awake() {

        _movementActionMap = _playerActionsAsset.FindActionMap("movement");
        _moveAction = _movementActionMap["move"];

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

        if (_moveAction.IsPressed())
        {

            ProcessTranslation();
            ProcessRotation();

        }

    }

    private void ProcessRotation() {

        // Using clamped position values
        float pitch = -1 * (transform.localPosition.y * _pitchPositionFactor + _yThrow * _pitchThrowFactor);
        float roll = -1 * (transform.localPosition.x * _rollPositionFactor + _xThrow * _rollThrowFactor);
        float yaw = transform.localPosition.x * _yawPositionFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    private void ProcessTranslation() {

        _xThrow = _moveAction.ReadValue<Vector2>().x;
        _yThrow = _moveAction.ReadValue<Vector2>().y;
        
        float xOffset = _xThrow * Time.deltaTime * _moveSpeed;
        float yOffset = _yThrow * Time.deltaTime * _moveSpeed;

        float rawXPos = xOffset + transform.localPosition.x;
        float rawYPos = yOffset + transform.localPosition.y;

        float clampedXPos = Mathf.Clamp(rawXPos, -_xAxisRange, _xAxisRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -_yAxisRange, _yAxisRange);

        float currentZPos = transform.localPosition.z;

        ApplyTranslation(clampedXPos, clampedYPos, currentZPos);

    }

    private void ApplyTranslation(float clampedXPos, float clampedYPos, float currentZPos) {

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, currentZPos);

    }
}
