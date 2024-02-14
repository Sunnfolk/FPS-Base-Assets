using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }
    public Vector2 LookDirection { get; private set; }
    public bool Jump { get; private set; }
    public bool Dash { get; private set; }
    public bool Interact { get; private set; }
    private PlayerInputAsset _inputAsset;
    
    private void Awake() { _inputAsset = new PlayerInputAsset(); }
    private void OnEnable() { _inputAsset.Enable(); }
    private void OnDisable() { _inputAsset.Disable(); }

    private void Update()
    {
        MoveDirection = _inputAsset.Player.Move.ReadValue<Vector2>();
        LookDirection = _inputAsset.Player.Look.ReadValue<Vector2>();
        Jump = _inputAsset.Player.Jump.triggered;
        Dash = _inputAsset.Player.Dash.triggered;
        Interact = _inputAsset.Player.Interact.triggered;
    }
}