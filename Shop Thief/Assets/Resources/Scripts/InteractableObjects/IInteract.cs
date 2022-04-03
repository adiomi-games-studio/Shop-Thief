using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    zoom,
    dropProduct
}

public interface IInteract
{
    void ActionOnObject(Action action);

    void ResetObject();
}
