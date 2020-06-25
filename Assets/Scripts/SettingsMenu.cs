using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    //All of the buttons
    [SerializeField]
    private GameObject OpenSettingsButton;
    [SerializeField]
    private GameObject SettingMenu;
    [SerializeField]
    private GameObject PlayButton;
    [SerializeField]
    private GameObject BallCounter;
    [SerializeField]
    private GameObject CounterHolder;

    //Script
    public SliderToText2 _sliderToText;

    public void Start()
    {
        //Assigning script
        _sliderToText = GetComponent<SliderToText2>();
    }



    // Opening Settings Menu
    //deactivating settings button and activating the settings menu
    public void OpenSettingsMenu()
    {

        OpenSettingsButton.SetActive(false);
        PlayButton.SetActive(false);
        SettingMenu.SetActive(true);
        BallCounter.SetActive(false);
        CounterHolder.SetActive(false);
        
        //Finding objects with these tags
        var Ball = GameObject.FindGameObjectsWithTag("Ball");
        var Peg = GameObject.FindGameObjectsWithTag("Peg");
        var Rack = GameObject.FindGameObjectsWithTag("Rack");

        _sliderToText.StopAllCoroutines();
        _sliderToText.UnpressButton();

        //Destroying all objects on the scene (scene resets)
        foreach (var item in Ball)
        {
            Destroy(item);
        }

        foreach (var item in Peg)
        {
            Destroy(item);
        }

        foreach (var item in Rack)
        {
            Destroy(item);
        }
    }

    //Deactivating all settings menu UI and reactivating all simulation UI
    public void CloseSettingsMenu()
    {

        OpenSettingsButton.SetActive(true);
        PlayButton.SetActive(true);
        SettingMenu.SetActive(false);

        BallCounter.SetActive(true);
        CounterHolder.SetActive(true);

        _sliderToText.BeginSpawningCloseSettings();

    }
}
