using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{
    [Header("General")]

    public UserData userData;

    public Slider levelBar;
    public TextMeshProUGUI Leveltxt;
    public TextMeshProUGUI NextLeveltxt;
    public TextMeshProUGUI Gemstxt;
   
    public int Level;
    public int Gems;
    public int lvlprogress;
    public int Ritemscount;
    public int Witemscount;
    public int Mitemscount;

    public bool flag = false;

    [Header("World")]
    public int WorldProg;

    public GameObject Earth0;
    public GameObject Earth1;
    public GameObject Earth2;
    public GameObject Earth3;
    public GameObject Earth4;
    public GameObject Earth5;

    [Header("Animals")]

    public int ChickenProg;
    public GameObject Chicken1;
    public GameObject Chicken2;
    public GameObject Chicken3;
    public GameObject Chicken4;
    public GameObject Chicken5;

    public int BuffProg;
    public GameObject Buff1;
    public GameObject Buff2;
    public GameObject Buff3;

    public int LizardProg;
    public GameObject Lizard;

    public int MonkeyProg;
    public GameObject Monkey1;
    public GameObject Monkey2;
    public GameObject Monkey3;




    void Start()
    {
        levelBar.maxValue = 3;
        // set Level Bar value
    }

    // Update is called once per frame
    void Update()
    {
        if (userData.flag)
        {
            // if data is received in UserData
            LoadData();
        }
        if (flag)
        {
            // if data is received in this script
            Leveltxt.text = Level.ToString();
            NextLeveltxt.text = (Level + 1).ToString();
            Gemstxt.text = Gems.ToString();
            levelBar.value = userData.GetLevelBarProg();

            WorldChecker();

        }
    }

    public void LoadData()
    {
        Level = userData.GetLevel();
        Gems = userData.GetGems();

        WorldProg = userData.GetWorldProg();
        MonkeyProg = userData.GetMonkeyProg();
        ChickenProg = userData.GetChickenProg();
        LizardProg = userData.GetLizardProg();
        BuffProg = userData.GetBuffProg();

        flag = true;
    }

    public void WorldChecker()
    {
        // This method displays the UI elements of the virtual world
        if(WorldProg == 1)
        {
            Earth1.SetActive(true);

            Earth0.SetActive(false);
            Earth2.SetActive(false);
            Earth3.SetActive(false);
            Earth4.SetActive(false);
            Earth5.SetActive(false);

        }else if(WorldProg == 2)
        {
            Earth2.SetActive(true);

            Earth0.SetActive(false);
            Earth1.SetActive(false);
            Earth3.SetActive(false);
            Earth4.SetActive(false);
            Earth5.SetActive(false);
        }
        else if (WorldProg == 3)
        {
            Earth3.SetActive(true);

            Earth0.SetActive(false);
            Earth1.SetActive(false);
            Earth2.SetActive(false);
            Earth4.SetActive(false);
            Earth5.SetActive(false);
        }
        else if (WorldProg == 4)
        {
            Earth4.SetActive(true);

            Earth0.SetActive(false);
            Earth1.SetActive(false);
            Earth3.SetActive(false);
            Earth2.SetActive(false);
            Earth5.SetActive(false);
        }
        else if (WorldProg == 5)
        {
            Earth5.SetActive(true);

            Earth0.SetActive(false);
            Earth1.SetActive(false);
            Earth3.SetActive(false);
            Earth4.SetActive(false);
            Earth2.SetActive(false);
        }

        if(ChickenProg == 1)
        {
            Chicken1.SetActive(true);

        }else if(ChickenProg == 2)
        {
            Chicken1.SetActive(true);
            Chicken2.SetActive(true);

        }
        else if (ChickenProg == 3)
        {
            Chicken1.SetActive(true);
            Chicken2.SetActive(true);
            Chicken3.SetActive(true);

        }
        else if (ChickenProg == 4)
        {
            Chicken1.SetActive(true);
            Chicken2.SetActive(true);
            Chicken3.SetActive(true);
            Chicken4.SetActive(true);

        }
        else if (ChickenProg == 5)
        {
            Chicken1.SetActive(true);
            Chicken2.SetActive(true);
            Chicken3.SetActive(true);
            Chicken4.SetActive(true);
            Chicken5.SetActive(true);

        }

        if (BuffProg == 1)
        {
            Buff1.SetActive(true);

        }
        else if (BuffProg == 2)
        {
            Buff1.SetActive(true);
            Buff2.SetActive(true);

        }
        else if (BuffProg == 3)
        {
            Buff1.SetActive(true);
            Buff2.SetActive(true);
            Buff3.SetActive(true);

        }


        if (MonkeyProg == 1)
        {
            Monkey1.SetActive(true);

        }
        else if (MonkeyProg == 2)
        {
            Monkey1.SetActive(true);
            Monkey2.SetActive(true);

        }
        else if (MonkeyProg == 3)
        {
            Monkey1.SetActive(true);
            Monkey2.SetActive(true);
            Monkey3.SetActive(true);

        }

        if (LizardProg == 1)
        {
            Lizard.SetActive(true);

        }
    }
}
