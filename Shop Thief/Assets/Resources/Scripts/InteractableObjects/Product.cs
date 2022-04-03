using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Product : Functionalities
{
    #region variables
    [Header("Properties of product")]
    public string name;
    public int price;
    public bool isAnim;
    private Animator anim;
    [SerializeField] private TMP_Text numberText;

    [Header("Other")]
    [SerializeField] private Transform[] posOfProduct;
    [SerializeField] private GameObject prefabProduct;
    public List<GameObject> listOfProducts;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        SpawnProductInShopShelf();
    }

    private void SpawnProductInShopShelf()
    {
        for (int i = 0; i < posOfProduct.Length; i++)
        {
            var product = Instantiate(prefabProduct, posOfProduct[i]);
            listOfProducts.Add(product);
        }
        numberText.text = listOfProducts.Count.ToString();
    }

    public void GainProduct()
    {
        if(isAnim) anim.Play("gain");

        if (listOfProducts.Count > 0)
        {
            Destroy(listOfProducts[listOfProducts.Count - 1]);
            listOfProducts.RemoveAt(listOfProducts.Count - 1);
        }        
        else
        {
            Destroy(listOfProducts[0]);
            listOfProducts.Clear();
            this.GetComponent<Product>().enabled = false;
        }
        numberText.text = listOfProducts.Count.ToString();
    }

    public bool ProductIsAvailable(Transform other) => IsCloseToObject(transform, other);
}