using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerStatistics
{
    public float HP;
    public float Ammo;
    public float XP;
    public float funds = 15000;
    public Dictionary<string,int> shares = new Dictionary<string,int>()
    {
    	{"AAPL", 0},
        {"AMZN", 0},
        {"BA", 0},
        {"FB", 0},
        {"GME", 0},
        {"GOOG", 0},
        {"MSFT", 0},
        {"SHOP", 0},
        {"TSLA", 0},
        {"U", 0},
    };
}