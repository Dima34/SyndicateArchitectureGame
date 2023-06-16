using System;
using UnityEngine;

namespace UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Start() =>
            _camera = Camera.main;

        private void FixedUpdate()
        {
            Quaternion cameraRotation = _camera.transform.rotation;
            transform.LookAt(transform.position + cameraRotation * Vector3.back, cameraRotation * Vector3.up);
        }
    }
}