using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(MouseLook))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private MouseLook _mouseLook;
    private PlayerMovement _move;
    private InteractionManager _interaction;

    private void Start()
    {
       
        _input = GetComponent<PlayerInput>();
        _mouseLook = GetComponent<MouseLook>();
        _move = GetComponent<PlayerMovement>();
        _interaction = GetComponent<InteractionManager>();
    }

    private void Update()
    {
        _mouseLook.UpdateMouseLook(_input.LookDirection);
        _move.UpdateMovement(_input.MoveDirection, _input.Dash, _input.Jump);
   
        _interaction.UpdateInteraction(_input.Interact);
    }
}