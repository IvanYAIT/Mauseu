using Services.Input;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        //"Movement Parameters"
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravity;

        //"Look Parameters"
        [SerializeField, Range(1, 10)] private float _lookSpeedX;
        [SerializeField, Range(1, 10)] private float _lookSpeedY;
        [SerializeField, Range(1, 90)] private float _upperLookLimit;
        [SerializeField, Range(1, 90)] private float _lowerLookLimit;
    
        [field: SerializeField] public bool CanMove { get; private set; }
        [field: SerializeField] public bool CanSprint { get; private set; }
        [field: SerializeField] public bool CanJump { get; private set; }

        
        private bool IsSprinting => CanSprint && Input.GetKey(SprintKey);
        private bool IsJumpting => _characterController.isGrounded && Input.GetKey(JumpKey);
        
        private const KeyCode SprintKey = KeyCode.LeftShift;
        private const KeyCode JumpKey = KeyCode.Space;

        private Camera _playerCamera;
        private UnityEngine.CharacterController _characterController;

        private Vector3 _moveDirection;
        private Vector3 _currentInput;
        private float _rotationX;
        private IInputService _inputService;
        
        private void Awake()
        {
            _playerCamera = GetComponentInChildren<Camera>();
            _characterController = GetComponent<UnityEngine.CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private void Update()
        {
            if (!CanMove)
                return;

            HandleMovementInput();
            HandleMouseLock();
        
            if(CanJump)
                HandleJump();
        
            ApplyFinalMovements();
        }

        private void HandleMovementInput()
        {
            var currentSpeed = IsSprinting ? _sprintSpeed : _walkSpeed;
            var horizontal = currentSpeed * _inputService.Horizontal;
            var vertical = currentSpeed * _inputService.Vertical;

            var test = currentSpeed * _inputService.Axis;

            _currentInput = test;//new Vector2(vertical, horizontal);

            var moveDirectionY = _moveDirection.y;
            _moveDirection = transform.TransformDirection(Vector3.forward) * _currentInput.x
                             + transform.TransformDirection(Vector3.right) * _currentInput.y;

            _moveDirection.y = moveDirectionY;
        }

        private void HandleJump()
        {
            if (!IsJumpting)
                return;

            _moveDirection.y = _jumpForce;
        }
    
        private void HandleMouseLock()
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _lookSpeedY;
            _rotationX = Mathf.Clamp(_rotationX, -_upperLookLimit, _lowerLookLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeedX, 0);
        }

        private void ApplyFinalMovements()
        {
            if (!_characterController.isGrounded)
                _moveDirection.y -= _gravity * Time.deltaTime;

            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
}