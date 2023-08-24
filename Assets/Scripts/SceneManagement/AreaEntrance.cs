using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    
    private void Start()
    {
        if (transitionName == SceneManagment.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position=this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
         
    }

}
