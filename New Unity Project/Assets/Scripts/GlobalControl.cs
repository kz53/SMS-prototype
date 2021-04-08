using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour 
{
    public static GlobalControl Instance;
    public int test = 9;

    public PlayerStatistics savedPlayerData = new PlayerStatistics();
	public Dictionary<string, float> priceDict;

    void Awake ()   
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }
}