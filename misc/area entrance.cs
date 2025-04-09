using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaentrance : MonoBehaviour
{
    
    [SerializeField] private string transitionName;

    void Start()
    {
     if (transitionName == scenemanagment.Instance.SceneTransitionName)
     {
        playerController.Instance.transform.position = this.transform.position;
        cameraController.Instance.setPlayerCameraFollow();
        UIfade.Instance.FadeToClear();
     }   
    }
}
