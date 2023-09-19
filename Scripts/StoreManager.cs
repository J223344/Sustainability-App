using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [Header("General Store")]
    public UserData userData;
    public int level;
    public int LevelBarProg;
    public int LevelProg;
    public TextMeshProUGUI leveltxt;
    public TextMeshProUGUI nextleveltxt;
    public Slider LevelBar;

    public bool flag = false;

    public TextMeshProUGUI Gemstxt;
    public int Gems;

    public int WorldProg;
    public int ChickenProg;
    public int MonkeyProg;
    public int LizardProg;
    public int BuffProg;



    [Header("World")]
    // W_ refers to World UI of Store
    public TextMeshProUGUI WLevel;
    public TextMeshProUGUI WNextLevel;
    public Slider WLevelBar;
    public TextMeshProUGUI WGems;

    public Button WorldUpgrade1;
    public Button WorldUpgrade2;
    public Button WorldUpgrade3;
    public Button WorldUpgrade4;
    public Button WorldUpgrade5;

    public TextMeshProUGUI status1;
    public TextMeshProUGUI status2;
    public TextMeshProUGUI status3;
    public TextMeshProUGUI status4;
    public TextMeshProUGUI status5;

    [Header("Animal")]
    // A_ refers to Animal UI of Store
    public TextMeshProUGUI ALevel;
    public TextMeshProUGUI ANextLevel;
    public Slider ALevelBar;
    public TextMeshProUGUI AGems;

    public Button ChickenButton;
    public Button MonkeyButton;
    public Button BuffButton;
    public Button LizardButton;

    public TextMeshProUGUI CickenButtonText;
    public TextMeshProUGUI MonkeyButtonText;
    public TextMeshProUGUI BuffaloButtonText;
    public TextMeshProUGUI LizardButtonText;

    public TextMeshProUGUI CickenStatus;
    public TextMeshProUGUI MonkeyStatus;
    public TextMeshProUGUI BuffaloStatus;
    public TextMeshProUGUI LizardStatus;


    // Start is called before the first frame update
    void Start()
    {
        // Set bar value of store, animal, and world screen
        LevelBar.maxValue = 3;
        ALevelBar.maxValue = 3;
        WLevelBar.maxValue = 3;

    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            // If script has recieved data, update each texts
            Gemstxt.text = Gems.ToString();
            AGems.text = Gems.ToString();
            WGems.text = Gems.ToString();

            leveltxt.text = level.ToString();
            ALevel.text = level.ToString();
            WLevel.text = level.ToString();

            nextleveltxt.text = (level + 1).ToString();
            ANextLevel.text = (level + 1).ToString();
            WNextLevel.text = (level + 1).ToString();

            ALevelBar.value = LevelBarProg;
            WLevelBar.value = LevelBarProg;
            LevelBar.value = LevelBarProg;




        }

        // Set Status text and see if World level is available to get
        Isinteractable(WorldUpgrade1, 1, 3, 1);
        StatusText(WorldUpgrade1, 1, 3, 1, status1);

        Isinteractable(WorldUpgrade2, 3, 6, 2);
        StatusText(WorldUpgrade2, 3, 6, 2, status2);

        Isinteractable(WorldUpgrade3, 5, 10, 3);
        StatusText(WorldUpgrade3, 5, 10, 3, status3);

        Isinteractable(WorldUpgrade4, 6, 15, 4);
        StatusText(WorldUpgrade4, 6, 15, 4, status4);

        Isinteractable(WorldUpgrade5, 8, 20, 5);
        StatusText(WorldUpgrade5, 8, 20, 5, status5);

        // Same for animal items
        AnimalInteractable(ChickenButton, 4, 5, 2, ChickenProg, CickenButtonText, CickenStatus);
        AnimalInteractable(BuffButton, 8, 3, 3, BuffProg, BuffaloButtonText, BuffaloStatus);
        AnimalInteractable(LizardButton, 18, 1, 4, LizardProg, LizardButtonText, LizardStatus);
        AnimalInteractable(MonkeyButton, 30, 3, 5, MonkeyProg, MonkeyButtonText, MonkeyStatus);



    }



    public void Isinteractable(Button but, int neededLevel, int neededgems, int max)
    {
        if (Gems >= neededgems && level >= neededLevel && WorldProg<max && WorldProg == max-1)
        {
            but.interactable = true;
        }
        else
        {
            but.interactable = false;
        }
    }

    public void StatusText(Button but, int neededLevel, int neededgems, int max, TextMeshProUGUI txt)
    {
        if (but.interactable == false && neededLevel <= level && WorldProg < max || but.interactable == true)
        {
            txt.text = "Gems required: " + neededgems.ToString();
        }
        else if (but.interactable == false && neededLevel > level)
        {
            txt.text = "Unlocks at Level " + neededLevel.ToString();

        }
        else if (max == WorldProg)
        {
            txt.text = "Owned";

        }
    }

    public void AnimalInteractable(Button but, int neededgems, int max, int neededprog, int animaltype,
        TextMeshProUGUI buttontext, TextMeshProUGUI statustxt)
    {
        buttontext.text = animaltype.ToString() + "/" + max.ToString();

        if (Gems >= neededgems && neededprog <= WorldProg && animaltype <max)
        {
            but.interactable = true;
            statustxt.text = "Gems Required: " + neededgems.ToString();
        }
        else
        {
            but.interactable = false;
        }

        if (but.interactable == false && neededprog <= WorldProg && animaltype < max)
        {
            statustxt.text = "Gems Required: " + neededgems.ToString();

        }
        else if (but.interactable == false && neededprog > WorldProg && animaltype <= max)
        {
            statustxt.text = "Upgrade World to Level " + neededprog.ToString();
        }else if (but.interactable == false && animaltype == max)
        {
            statustxt.text = "Fully Upgraded";
        }

    }

    public void LoadData()
    {
        level = userData.GetLevel();
        Debug.Log(level.ToString());
        leveltxt.text = level.ToString();
        nextleveltxt.text = (level + 1).ToString();

        Gems = userData.GetGems();

        WorldProg = userData.GetWorldProg();

        ChickenProg = userData.GetChickenProg();
        BuffProg = userData.GetBuffProg();
        LizardProg = userData.GetLizardProg();
        MonkeyProg = userData.GetMonkeyProg();

        LevelBarProg = userData.GetLevelBarProg();


        flag = true;
    }

    public void WorldButtonPressed()
    {
        WorldProg++;
        userData.SetWorldProg(WorldProg);

        switch (WorldProg)
        {
            case 1:
                Gems -= 3;
                userData.SetGems(Gems);
                break;
            case 2:
                Gems -= 6;
                userData.SetGems(Gems);
                break;
            case 3:
                Gems -= 10;
                userData.SetGems(Gems);
                break;
            case 4:
                Gems -= 15;
                userData.SetGems(Gems);
                break;
            case 5:
                Gems -= 20;
                userData.SetGems(Gems);
                break;
            default:
                break;
        }
    }

    public void ChickenButtonPressed()
    {
        ChickenProg++;
        userData.SetChickenProg(ChickenProg);
        Gems -= 4;
        userData.SetGems(Gems);
    }

    public void BuffButtonPressed()
    {
        BuffProg++;
        userData.SetBuffProg(BuffProg);
        Gems -= 8;
        userData.SetGems(Gems);
    }

    public void LizardButtonPressed()
    {
        LizardProg++;
        userData.SetLizardProg(LizardProg);
        Gems -= 18;
        userData.SetGems(Gems);
    }

    public void MonkeyButtonPressed()
    {
        MonkeyProg++;
        userData.SetMonkeyProg(MonkeyProg);
        Gems -= 30;
        userData.SetGems(Gems);
    }

}
