using System;
using _Project.Logic.Infrastructure;
using _Project.Logic.Services.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Logic.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        private IInputService _inputService;

        private Camera _camera;

        [SerializeField] private float _movementSpeed;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;

            CameraFollow();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.EPSILON)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0f;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }

        private void CameraFollow() => _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}