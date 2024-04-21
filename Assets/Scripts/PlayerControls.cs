using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour {

    [Header("General Setup Settings")]
    [Tooltip("Input Action Asset for Player Controls.")] [SerializeField] InputActionAsset _playerActionsAsset;

    [Tooltip("Speed of ship for horizontal and vertical movement.")] [SerializeField] float _moveSpeed = 1f;

    // The GameObject this is attached to must be at (0;0) for coherence and camara centered
    [SerializeField] float _xAxisRange = 5f;
    [SerializeField] float _yAxisRange = 5f;

    [Tooltip("Add all player lasers here!")] [SerializeField] GameObject[] _lasers;

    InputActionMap _controlsActionMap;
    InputAction _moveAction;
    InputAction _fireAction;

    [Header("XY Position-based Tuning")]
    [SerializeField] float _pitchPositionFactor = 1f;
    [SerializeField] float _rollPositionFactor = 1f;
    [SerializeField] float _yawPositionFactor = 1f;

    [Header("Player Input-based Tuning")]
    [SerializeField] float _pitchThrowFactor = 1f;
    [SerializeField] float _rollThrowFactor = 1f;

    float _xThrow;
    float _yThrow;

    // Can also manage everything in script or every frame using Update()

    void Awake() {

        _controlsActionMap = _playerActionsAsset.FindActionMap("playerControls");
        _moveAction = _controlsActionMap["move"];
        _fireAction = _controlsActionMap["fire"];

    }

    void OnEnable() {

        // Enable all actions in this action map
        _controlsActionMap.Enable();

    }

    void OnDisable() {

        // Disable all actions in this action map
        _controlsActionMap.Disable();

    }

    void Update() {

        // Conditional removed for player visualization purposes
        // if (_moveAction.IsPressed())
        // {

            ProcessTranslation();
            ProcessRotation();
            ProcessFire();

        // }

    }

    private void ProcessRotation() {

        // Using clamped position values
        float pitchMovement = transform.localPosition.y * _pitchPositionFactor;
        float rollMovement = transform.localPosition.x * _rollPositionFactor;

        float pitchDirection = _yThrow * _pitchThrowFactor;
        float rollDirection = _xThrow * _rollThrowFactor;

        float pitch = -1 * (pitchMovement + pitchDirection);
        float roll = -1 * (rollMovement + rollDirection);
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

    private void ProcessFire() {

        if (_fireAction.IsPressed()) {

            SetLaserActive(true);
        }
        else {

            SetLaserActive(false);
        }
    }

    private void SetLaserActive(bool isActive) {

        // Play() and Stop() not used because after stopping it 
        // waits until particles dissappear to Play again.

        // Activating and deactivating GameObject makes existing
        // particles dissappear

        foreach (GameObject laser in _lasers) {

            var particleSystem = laser.GetComponent<ParticleSystem>();
            var emission = particleSystem.emission;

            emission.enabled = isActive;
        }
    }

}
