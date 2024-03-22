using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int BlendVelocityHash = Animator.StringToHash("blendVelocity");
        
        private Animator _animator;
        private CharacterController _characterController;
        private float _animatorVelocity;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_characterController.velocity.magnitude == 0f)
                _animatorVelocity = 0f;
            else
                _animatorVelocity += Time.deltaTime * 0.7f;

            _animator.SetFloat(BlendVelocityHash, _animatorVelocity, 0.01f, Time.deltaTime);
        }
    }
}