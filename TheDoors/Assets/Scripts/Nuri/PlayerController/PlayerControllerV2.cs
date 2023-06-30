using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheDoors.Manager;

namespace TheDoors.PlayerControl
{
    public class PlayerControllerV2 : MonoBehaviour
    {

        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _hasAnimator;
        private int _xVelHash;
        private int _yVelHash;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 6f;

        // Start is called before the first frame update
        void Start()
        {
            _inputManager = GetComponent<InputManager>();

            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();


            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");

        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targerSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
        }


    }
}

