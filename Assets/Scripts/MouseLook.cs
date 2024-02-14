using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 20f;
    [SerializeField] [Space(5)] private Transform playerCamera = null;
    [SerializeField] private float pitchConstraint = 90f;
    private float _cameraPitch;
    private bool _cursorLockValue;
    

    private bool LockCursor
    {
        get => _cursorLockValue;
        set
        {
            if (_cursorLockValue == value) return;
            if (value)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            _cursorLockValue = value;
        }
    }

    private void Start()
    {
        LockCursor = true;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            LockCursor = !LockCursor;
        }
    }

    public void UpdateMouseLook(Vector2 mouseDelta)
    {
        if (mouseDelta == Vector2.zero) return;
        
        transform.Rotate(Vector3.up * (mouseDelta.x * mouseSensitivity));
        
        _cameraPitch -= mouseDelta.y * mouseSensitivity;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -pitchConstraint, pitchConstraint);

        var cameraEulerAngles = playerCamera.eulerAngles;
        cameraEulerAngles.x = _cameraPitch;
        playerCamera.eulerAngles = cameraEulerAngles;

    }
}