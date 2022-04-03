using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ManagerInteract))]
public class ManagerUI : Functionalities
{
    #region variables
    public GameObject windowMenu, windowGameOver, windowGameWin, windowInDuringGame;
    public GameObject menuButtons, buttonResume;
    public TMP_Text cashText, numberOfProductsInCart, numberOfProductsInTrucks, minScoreToWin;
    private bool sound;
    [SerializeField] private AudioSource[] audio;
    [SerializeField] private Image soundButton;
    [SerializeField] private Sprite[] soundOnOff;

    public static new ManagerUI Instance;
    #endregion

    private void Awake()
    {
        Instance = this;
        sound = false;
        windowMenu.GetComponent<Animator>().Play("openMenuUI");

        if (PlayerPrefs.GetInt("sound") == 1)
            SoundOnOff();
    }

    public void Restart()
    {
        GameManager.Instance.BackToHome();
    }

    public void OpenMenuInDuringGameUI()
    {
        windowMenu.SetActive(!windowMenu.activeInHierarchy);
        ManagerInteract.Instance.ResetObject();
        buttonResume.SetActive(true);
        menuButtons.SetActive(false);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void FreeRide()
    {
        GameManager.Instance.FreeRide();
    }

    public void SoundOnOff()
    {
        sound = !sound;

        float vol = sound ? 0f : 1f;
        foreach (var s in audio)
            s.volume = vol;

        if (sound)
        {
            soundButton.sprite = soundOnOff[1];
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            soundButton.sprite = soundOnOff[0];
            PlayerPrefs.SetInt("sound", 0);
        }
    }

    public void ExitGame() => CloseApp();
}
