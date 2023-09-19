using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    //Screen object variables
    [Header("Objects")]
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject mainUI;
    public GameObject missionUI;
    public GameObject storeUI;
    public GameObject UpgradeWorldUI;
    public GameObject AnimalsUI;
    public GameObject HelpUI;
    public GameObject ScoreBoardUI;
    public GameObject authman;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);

        // set others off
        registerUI.SetActive(false);
        mainUI.SetActive(false);
        missionUI.SetActive(false);
        storeUI.SetActive(false);
        UpgradeWorldUI.SetActive(false);
        AnimalsUI.SetActive(false);


        authman.GetComponent<AuthManager>().ClearLoginFeilds();

        
    }
    public void RegisterScreen() // Register button
    {
        registerUI.SetActive(true);

        // set others off
        loginUI.SetActive(false);
        mainUI.SetActive(false);
        missionUI.SetActive(false);
        storeUI.SetActive(false);
        UpgradeWorldUI.SetActive(false);
        AnimalsUI.SetActive(false);

        authman.GetComponent<AuthManager>().ClearRegisterFeilds();

    }

    public void MainScreen()
    {
        mainUI.SetActive(true);

        loginUI.SetActive(false);
        registerUI.SetActive(false);
        missionUI.SetActive(false);
        storeUI.SetActive(false);
        UpgradeWorldUI.SetActive(false);
        AnimalsUI.SetActive(false);
        HelpUI.SetActive(false);
        ScoreBoardUI.SetActive(false);
    }

    public void MissionScreen()
    {
        missionUI.SetActive(true);

        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainUI.SetActive(false);
        storeUI.SetActive(false);
        UpgradeWorldUI.SetActive(false);
        AnimalsUI.SetActive(false);
    }

    public void StoreScreen()
    {
        storeUI.SetActive(true);

        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainUI.SetActive(false);
        missionUI.SetActive(false);
        UpgradeWorldUI.SetActive(false);
        AnimalsUI.SetActive(false);
    }

    public void WorldScreen()
    {
        UpgradeWorldUI.SetActive(true);

        AnimalsUI.SetActive(false);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainUI.SetActive(false);
        missionUI.SetActive(false);
        storeUI.SetActive(false);
    }

    public void AnimalScreen()
    {
        AnimalsUI.SetActive(true);

        UpgradeWorldUI.SetActive(false);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainUI.SetActive(false);
        missionUI.SetActive(false);
        storeUI.SetActive(false);
    }

    public void HelpScreen()
    {
        HelpUI.SetActive(true);

        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainUI.SetActive(false);
        missionUI.SetActive(false);
        storeUI.SetActive(false);
        UpgradeWorldUI.SetActive(false);
        AnimalsUI.SetActive(false);
    }

    public void BoardScreen()
    {
        ScoreBoardUI.SetActive(true);

        loginUI.SetActive(false);
        registerUI.SetActive(false);
        mainUI.SetActive(false);
        missionUI.SetActive(false);
        storeUI.SetActive(false);
        UpgradeWorldUI.SetActive(false);
        AnimalsUI.SetActive(false);
        HelpUI.SetActive(false);
    }


}