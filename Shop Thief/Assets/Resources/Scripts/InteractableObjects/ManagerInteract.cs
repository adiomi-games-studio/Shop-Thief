using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInteract : MonoBehaviour, IInteract
{
    #region variables
    [HideInInspector] 
    private Vector3 startPosCam;
    private Quaternion startRotCam;
    [HideInInspector]
    public bool allowInteract;

    //[HideInInspector]
    public Transform cam, camLose;
    private RaycastHit hit;

    public static ManagerInteract Instance;
    #endregion

    private void Awake()
    {
        startPosCam = cam.transform.position;
        startRotCam = cam.transform.rotation;
        Instance = this;
        cam = GameObject.Find("Cam").transform;
    }

    public void ActionOnObject(Action action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 20f))
        {
            if (hit.transform.TryGetComponent(out ObjectInteract _object))
            {
                var actionObject = _object.action;
                _object.ActionOnObject(actionObject);
            }
            else if(hit.transform.TryGetComponent(out Product product))
            {
                if (product.ProductIsAvailable(ControlPlayer.Instante.transform))
                    StartCoroutine(ControlPlayer.Instante.GainProdcuct(product));
            }
        }
    }

    private void LateUpdate()
    {
        if (allowInteract)
        {
            if(Input.GetMouseButtonDown(0))
                ActionOnObject(0);
        }            
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !ManagerUI.Instance.menuButtons.activeInHierarchy)
                ResetObject();
        }
    }

    public void ResetObject()
    {
        if (cam.position != startPosCam)
        {
            cam.position = startPosCam;
            cam.rotation = startRotCam;
        }

        ManagerUI.Instance.buttonResume.SetActive(false);
        ManagerUI.Instance.menuButtons.SetActive(true);

        allowInteract = true;
        ControlPlayer.Instante.allowMove = true;

        if (cam.TryGetComponent(out camFollow camera))
            camera.enabled = true;
    }
}
