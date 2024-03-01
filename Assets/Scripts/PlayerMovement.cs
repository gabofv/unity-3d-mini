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

        if (_moveAction.IsPressed()) {
            ProcessTranslation();
            ProcessRotation();
        }

    }

    private void ProcessRotation() {

    }

    private void ProcessTranslation() {

        float horizontalThrow = _moveAction.ReadValue<Vector2>().x * Time.deltaTime * _moveSpeed;
        float verticalThrow = _moveAction.ReadValue<Vector2>().y * Time.deltaTime * _moveSpeed;

        float rawXPos = horizontalThrow + transform.localPosition.x;
        float rawYPos = verticalThrow + transform.localPosition.y;

        float clampedXPos = Mathf.Clamp(rawXPos, -_xAxisRange, _xAxisRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -_yAxisRange, _yAxisRange);

        float currentZPos = transform.localPosition.z;

        ApplyTranslation(clampedXPos, clampedYPos, currentZPos);

    }

    private void ApplyTranslation(float clampedXPos, float clampedYPos, float currentZPos) {

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, currentZPos);

    }
}
