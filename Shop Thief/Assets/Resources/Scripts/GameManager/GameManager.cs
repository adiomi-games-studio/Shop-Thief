using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Functionalities
{
    #region variables
    private Timer Time;
    public int numberOfProductCart, numberOfProductInTruck;
    [SerializeField] private float roundTime = 150;

    public static GameManager Instance;
    [HideInInspector] public bool infinityTime;
    public int overallPrice, requireScore;
    [SerializeField] private int[] priceScore;
    #endregion

    private void Awake()
    {
        Instance = this;
        Time = GetComponent<Timer>();
    }

    public void StartGame()
    {
        infinityTime = false;

        Time.time = roundTime;
        StartCoroutine(Time.NextSecond());
        ManagerUI.Instance.windowMenu.SetActive(false);
        ManagerUI.Instance.windowInDuringGame.SetActive(true);
        ControlPlayer.Instante.allowMove = true;
        ManagerInteract.Instance.allowInteract = true;

        //require score price 
        requireScore = priceScore[Random.Range(0, priceScore.Length - 1)];
        ManagerUI.Instance.minScoreToWin.text = $"minimum value of products to win ${requireScore}";
    }

    public void FreeRide()
    {
        infinityTime = true;
        Time.NextSecond();
        ManagerUI.Instance.windowMenu.SetActive(false);
        ManagerUI.Instance.windowInDuringGame.SetActive(true);
        ControlPlayer.Instante.allowMove = true;
        ManagerInteract.Instance.allowInteract = true;
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("market");
    }

    public void GameOver()
    {
        ManagerUI.Instance.windowInDuringGame.SetActive(false);
        ManagerUI.Instance.windowGameOver.SetActive(true);
        ControlPlayer.Instante.allowMove = false;     
        ManagerInteract.Instance.allowInteract = false;
        ControlPlayer.Instante.SetAnimationAfterLosing();
    }

    public void GameWin()
    {
        ManagerUI.Instance.windowGameWin.SetActive(true);
        ManagerUI.Instance.windowInDuringGame.SetActive(false);
        ControlPlayer.Instante.allowMove = false;
        ManagerInteract.Instance.allowInteract = false;
    }

    public void UpdateNumberOfProductsUI()
    {
        ManagerUI.Instance.numberOfProductsInCart.text = $"{numberOfProductCart}/6";
        ManagerUI.Instance.numberOfProductsInTrucks.text = $"{numberOfProductInTruck}";
        ManagerUI.Instance.cashText.text = $"${overallPrice}";

        if (numberOfProductCart > 5)
            ManagerUI.Instance.numberOfProductsInCart.color = Color.red;
        else
            ManagerUI.Instance.numberOfProductsInCart.color = Color.green;


        var products = ControlPlayer.Instante.products;
        if (numberOfProductCart == 0)
        {
            foreach (var product in products)
                product.SetActive(false);
        }

        for (int i = 0; i < products.Length; i++)
        {
            if (i <= numberOfProductCart)
                products[i].SetActive(true);
        }
    }

    public void AddProductShoppingCart() => AddProduct();
}
