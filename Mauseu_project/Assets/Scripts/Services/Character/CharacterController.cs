using Photon.Pun;
using Services.Input;
using UnityEngine;

namespace Services.Character
{
    public class CharacterController : MonoBehaviourPun
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

        [SerializeField] private Camera playerCamera;

        public bool IsOwner = false;
        public bool IsStealth => IsCrouching;

        private bool IsSprinting => CanSprint && UnityEngine.Input.GetKey(SprintKey) && !IsCrouching;
        private bool IsJumpting => _characterController.isGrounded && UnityEngine.Input.GetKey(JumpKey);
        private bool IsCrouching => UnityEngine.Input.GetKey(KeyCode.LeftControl);


        private const KeyCode SprintKey = KeyCode.LeftShift;
        private const KeyCode JumpKey = KeyCode.Space;

        private UnityEngine.CharacterController _characterController;

        private Vector3 _moveDirection;
        private Vector3 _currentInput;
        private float _rotationX;
        private IInputService _inputService;
        private float _speedMultiplier = 1;
        
        private void Awake()
        {
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
            if (!IsOwner) return;

            if (!CanMove)
                return;

            HandleMovementInput();
            if(playerCamera != null)
                HandleMouseLock();
        
            if(CanJump)
                HandleJump();
        
            ApplyFinalMovements();
        }

        public void ChangeMovementSpeed(bool value)
        {
            if (value)
                _speedMultiplier = 0.75f;
            else
                _speedMultiplier = 1;
        }

        private void HandleMovementInput()
        {
            var currentSpeed = IsSprinting ? _sprintSpeed : _walkSpeed;
            if (IsCrouching) ChangeMovementSpeed(true); else ChangeMovementSpeed(false);
            currentSpeed *= _speedMultiplier;
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
            if (!Cursor.visible)
            {
                _rotationX -= UnityEngine.Input.GetAxis("Mouse Y") * _lookSpeedY;
                _rotationX = Mathf.Clamp(_rotationX, -_upperLookLimit, _lowerLookLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, UnityEngine.Input.GetAxis("Mouse X") * _lookSpeedX, 0);
            }
        }

        private void ApplyFinalMovements()
        {
            if (!_characterController.isGrounded)
                _moveDirection.y -= _gravity * Time.deltaTime;

            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }
}