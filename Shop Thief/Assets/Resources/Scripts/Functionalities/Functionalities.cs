using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Functionalities : MonoBehaviour
{
    public bool IsCloseToObject(Transform thisTransform, Transform other)
    {
        return Vector3.Distance(transform.position, other.position) < 1.8f ? true : false;
    }

    public float SubstractSecond(float actualTime)
    {
        if (actualTime > 0)
            actualTime--;
        return actualTime;
    }

    public void DropProducts()
    {
        GameManager.Instance.numberOfProductInTruck += GameManager.Instance.numberOfProductCart;
        GameManager.Instance.numberOfProductCart = 0;
        GameManager.Instance.UpdateNumberOfProductsUI();
    }

    public void AddProduct()
    {
        var products = ControlPlayer.Instante.products;
        if (GameManager.Instance.numberOfProductCart < products.Length)
            GameManager.Instance.numberOfProductCart++;

        GameManager.Instance.UpdateNumberOfProductsUI();
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
