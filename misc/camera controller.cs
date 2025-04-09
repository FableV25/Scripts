using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : singleton<cameraController>
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public void setPlayerCameraFollow()
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = playerController.Instance.transform;
    }
}
