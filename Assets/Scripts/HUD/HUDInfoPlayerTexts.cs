using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDInfoPlayerTexts : MonoBehaviour
{
    /*public TMP_Text damagePlayer;
    public TMP_Text autoclickDamage;
    public TMP_Text intervalAutoclick;
    public TMP_Text criticalProb;
    public TMP_Text criticalDamage;
    public TMP_Text chestChance;
    public TMP_Text strongthRecoveryChance;
    public TMP_Text strongthRecoveryAmount;*/

    //public List<InfoPlayer> infos;

    public InfoPlayer[] infos;

    [Serializable]
    public class InfoPlayer
    {
        public string info;
        public TMP_Text text;
    }

    private void OnEnable()
    {
        infos[0].text.text = "";
    }
}
