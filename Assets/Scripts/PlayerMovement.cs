using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float gravity = -13f;
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] [Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    
    private float _velocityY = 0.0f;
    private Vector2 _currentDirection = Vector2.zero;
    private Vector2 _currentDirectionVelocity = Vector2.zero;
    private CharacterController _characterController = null;
    #endregion
    
    private void Start()
    { _characterController = GetComponent<CharacterController>(); }

    public void UpdateMovement(Vector2 moveDirection, bool dash, bool jump)
    {
        if (_characterController.isGrounded)
        { _velocityY = -2f; }
        
        _currentDirection = Vector2.SmoothDamp(_currentDirection, moveDirection, ref _currentDirectionVelocity, moveSmoothTime);
        
        if (dash) { _currentDirection.y = dashSpeed; }
        
        if (_characterController.isGrounded && jump)
        {
            _velocityY = Mathf.Sqrt(jumpSpeed * -2 * gravity);
        }
        
        _velocityY += gravity * Time.deltaTime;
        
        var velocity = (transform.forward * _currentDirection.y +
                        transform.right * _currentDirection.x) *
                        moveSpeed + Vector3.up * _velocityY;
        _characterController.Move(velocity * Time.deltaTime);
    }
}