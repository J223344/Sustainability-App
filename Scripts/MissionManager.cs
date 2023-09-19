using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MissionManager : MonoBehaviour
{

    [Header("Scripts and UI")]

    public GameObject LoginUI;
    public GameObject RegUI;
    public UserData userData;
    public bool flag = false;

    [Header("Gems")]

    public int Gems;
    public TextMeshProUGUI gemsTxt;
    

    [Header("Recycle Collection")]
    public TextMeshProUGUI RecycleStatement;
    public TextMeshProUGUI RecycleRewardtxt;
   


    public int RecycleGoal;
    public int RecycleReward;
    public int RecycleProgress;

    [Header("Water Collection")]
    public TextMeshProUGUI WaterStatement;
    public TextMeshProUGUI WaterRewardtxt;



    public int WaterGoal;
    public int WaterReward;
    public int WaterProgress;

    [Header("Meat Collection")]
    public TextMeshProUGUI MeatStatement;
    public TextMeshProUGUI MeatRewardtxt;



    public int MeatGoal;
    public int MeatReward;
    public int MeatProgress;


    [Header("General")]
    public int level;
    public float levelprogress;
    public TextMeshProUGUI leveltxt;
    public TextMeshProUGUI nextleveltxt;
    public Slider levelBar;

    public TextMeshProUGUI Gemstxt;
   



    void Start()
    {
        levelBar.maxValue = 3;
    }

        void Update()
    {
        if (flag)
        {
            // If data is received in this script
            RecycleReward = RecycleGoal;
            MeatReward = MeatGoal;
            WaterReward = WaterGoal;

            // Set texts
            RecycleStatement.text = RecycleProgress.ToString() + "/" + RecycleGoal.ToString();
            RecycleRewardtxt.text = RecycleReward.ToString();

            WaterStatement.text = WaterProgress.ToString() + "/" + WaterGoal.ToString();
            WaterRewardtxt.text = WaterReward.ToString();

            MeatStatement.text = MeatProgress.ToString() + "/" + MeatGoal.ToString();
            MeatRewardtxt.text = MeatReward.ToString();

          

            gemsTxt.text = Gems.ToString();



            if (RecycleGoal == RecycleProgress)
            {
                //If goal is achieved
                RecycleProgress = 0;
                userData.SetRprog(RecycleProgress);
                RecycleGoal += 1;
                userData.SetRgoal(RecycleGoal);

                // update gem balance;
                Gems += RecycleReward;
                userData.SetGems(Gems);

                RecycleReward++;
                levelBar.value += 1;
                userData.SetLevelBarProg((int)levelBar.value);

            }


            if (WaterGoal == WaterProgress)
            {
                //If goal is achieved
                WaterProgress = 0;
                userData.SetWprog(WaterProgress);
                WaterGoal = 2;
                userData.SetWgoal(WaterGoal);

                // update gem balance;
                Gems += WaterReward;
                userData.SetGems(Gems);

                WaterReward++;
                levelBar.value += 1;
                userData.SetLevelBarProg((int)levelBar.value);

            }


            if (MeatGoal == MeatProgress)
            {
                //If goal is achieved
                MeatProgress = 0;
                userData.SetMprog(MeatProgress);
                MeatGoal = 2;
                userData.SetMgoal(MeatGoal);
                // update gem balance;

                Gems += MeatReward;
                userData.SetGems(Gems);

                MeatReward++;
                levelBar.value += 1;
                userData.SetLevelBarProg((int)levelBar.value);

            }


            leveltxt.text = level.ToString();
            nextleveltxt.text = (level + 1).ToString();


            if (levelBar.value == 3)
            {
                //Level Up
                level++;
                userData.SetLevel(level);
                levelBar.value = 0;
                userData.SetLevelBarProg((int)levelBar.value);

            }
        }
    }

    // Log Buttons
    public void RecycleProg()
    {
        RecycleProgress++;
        userData.SetRprog(RecycleProgress);
    }

    public void WaterProg()
    {
        WaterProgress++;
        userData.SetWprog(WaterProgress);

    }

    public void MeatProg()
    {
        MeatProgress++;
        userData.SetMprog(MeatProgress);

    }

    public void LoadData()
    {
        level = userData.GetLevel();
        Debug.Log(level.ToString());
        nextleveltxt.text = (level + 1).ToString();

        Gems = userData.GetGems();
       

        RecycleProgress = userData.GetRprog();
        RecycleGoal = userData.GetRgoal();
        
        RecycleReward = (RecycleGoal - 2) * 7;

        WaterGoal = userData.GetWgoal();
        WaterReward = WaterGoal * 7;
        WaterProgress = userData.GetWprog();

        MeatGoal = userData.GetMgoal();
        MeatReward = (MeatGoal - 1) * 7;
        MeatProgress = userData.GetMprog();

        levelBar.value = userData.GetLevelBarProg();

        flag = true;
    }

 


}
