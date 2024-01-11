using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraSetting : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    public Transform followTarget;

    private void Awake()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void FixedUpdate()
    {
        followTarget = GameManager.Instance.currentplayer.transform;

        virtualCam.Follow = followTarget;
    }
}
