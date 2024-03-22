using System;
using Player.InputService;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 7f;
      
        private IInputService _inputService;
        private CharacterController _characterController;
        private Camera _camera;

        private void Awake()
        {
            _inputService = new KeyboardInputService();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > 0.001f)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(_moveSpeed * movementVector * Time.deltaTime);

        }
    }
}