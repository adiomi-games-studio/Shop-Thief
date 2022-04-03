using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : Functionalities, IInteract
{
    public Action action;

    public void ActionOnObject(Action action)
    {
        if (action == Action.zoom)
        {
            if (ManagerInteract.Instance.cam.TryGetComponent(out camFollow cam))
                cam.enabled = false;
            ManagerInteract.Instance.cam.localPosition = new Vector3(-5.2f, 0.1f, -17.3f);
            ManagerInteract.Instance.cam.rotation = Quaternion.Euler(0f, 0f, 0f);
            //disabled move player and interact objects
            ManagerInteract.Instance.allowInteract = false;
            ControlPlayer.Instante.allowMove = false;
        }
        else if (action == Action.dropProduct)
            DropProducts();
    }

    public void ResetObject()
    {
        //feature...
        throw new System.NotImplementedException();
    }
}