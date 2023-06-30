using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CinemachinePOVExtension : CinemachineExtension
{

    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float clampAngle = 80f;

    private InputManager inputManager;
    private Vector3 startinRotation;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startinRotation == null) startinRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startinRotation.x += deltaInput.x * horizontalSpeed * Time.deltaTime;
                startinRotation.y += deltaInput.y * verticalSpeed * Time.deltaTime;
                startinRotation.y = Mathf.Clamp(startinRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = quaternion.Euler(-startinRotation.y, startinRotation.x, 0f);
            }
        }
    }
}
