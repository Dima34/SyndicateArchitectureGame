using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _following;
    [SerializeField] private float _rotationAngleX;
    [SerializeField] private float _distance;
    [SerializeField] private float _yOffset;

    private void LateUpdate()
    {
        if (_following == null)
            return;

        var rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
        var position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

        SetNewPostionAndRotation(rotation, position);
    }

    private void SetNewPostionAndRotation(Quaternion rotation, Vector3 position)
    {
        transform.rotation = rotation;
        transform.position = position;
    }

    private Vector3 FollowingPointPosition()
    {
        Vector3 followingPosition = _following.position;
        followingPosition.y = _yOffset; 
        
        return followingPosition;
    }

    public void Follow(GameObject objectToFollow)
    {
        _following = objectToFollow.transform;
    }
}
