using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    [Header("General")]
    public Boolean flag = false;
    public int Level;
    public int LevelBarProg;
    public int Gems;

    [Header("Mission")]
    //Water
    public int Wprog;
    public int Wgoal;
    //Recycle
    public int Rprog;
    public int Rgoal;
    //Meat
    public int Mprog;
    public int Mgoal;

    [Header("Store")]
    public int ChickenProg;
    public int BuffProg;
    public int LizardProg;
    public int MonkeyProg;
    public int WorldProg;

  

    public void SetChickenProg(int c)
    {
        ChickenProg = c;
    }
    public int GetChickenProg()
    {
        return ChickenProg;
    }

    public void SetBuffProg(int b)
    {
        BuffProg = b;
    }
    public int GetBuffProg()
    {
        return BuffProg;
    }

    public void SetLizardProg(int l)
    {
        LizardProg = l;
    }
    public int GetLizardProg()
    {
        return LizardProg;
    }

    public void SetMonkeyProg(int m)
    {
        MonkeyProg = m;
    }
    public int GetMonkeyProg()
    {
        return MonkeyProg;
    }

    public void SetWorldProg(int w)
    {
        WorldProg = w;
    }
    public int GetWorldProg()
    {
        return WorldProg;
    }

    public void SetLevel(int lvl)
    {
        Level = lvl;
    }
    public int GetLevel()
    {
        return Level;
    }

    public void SetLevelBarProg(int prog)
    {
        LevelBarProg = prog;
    }
    public int GetLevelBarProg()
    {
        return LevelBarProg;
    }

    public void SetGems(int g)
    {
        Gems = g;
    }
    public int GetGems()
    {
        return Gems;
    }


    public void SetWprog(int g)
    {
        Wprog = g;
    }
    public int GetWprog()
    {
        return Wprog;
    }

    public void SetRprog(int g)
    {
        Rprog = g;
    }
    public int GetRprog()
    {
        return Rprog;
    }

    public void SetMprog(int g)
    {
        Mprog = g;
    }
    public int GetMprog()
    {
        return Mprog;
    }

    public void SetWgoal(int g)
    {
        Wgoal = g;
    }
    public int GetWgoal()
    {
        return Wgoal;
    }

    public void SetRgoal(int g)
    {
        Rgoal = g;
    }
    public int GetRgoal()
    {
        return Rgoal;
    }

    public void SetMgoal(int g)
    {
        Mgoal = g;
    }
    public int GetMgoal()
    {
        return Mgoal;
    }


}

