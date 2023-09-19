using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text levelText;
    

    public void NewScoreElement(string _username, int _level)
    {
        usernameText.text = _username;
        levelText.text = _level.ToString();
      
    }

}
