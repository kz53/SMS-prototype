// master game object 

//voting system
    // button for all players
    // master client controller
    //button to reload floor scene
//stock tracker and display

// price models

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameSetupController : MonoBehaviour
{
    private float funds;
    private float max_bar = 10000f;
    public HealthBar healthBar;
    public Tuple<string, float>[] priceTuples;
    public PriceController priceController;
    private TermSetActive[] termObjectArr;
    Dictionary<string, float> priceDict; 
    Dictionary<string, int> shareDict; 
    public bool roundFinished = false;
    public GameObject finishRoundButton;

    public PlayerStatistics localPlayerData = new PlayerStatistics();

    void Start()
    {

        CreatePlayer();
        LoadPlayer();
        funds = localPlayerData.funds;
        healthBar.SetSize(funds / max_bar);
        termObjectArr = FindObjectsOfType<TermSetActive>();

        
        priceTuples = new Tuple<string, float>[termObjectArr.Length];
        for (int i = 0; i < termObjectArr.Length; i++) 
        {
            Tuple<string, float> entry = new Tuple<string, float>("TSLA",100f);
            priceTuples[i] = entry;
            termObjectArr[i].SetValue(500f);
        }

        priceController = FindObjectOfType<PriceController>();

        GetPriceDict();
    }

    private void CreatePlayer()
    {
    	Debug.Log("Creating Player");
    	Debug.Log(Path.Combine("PhotonPrefabs", "PhotonPlayer"));
    	PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity);
    }

    public void Buy(string symbol)
    {
        // if ()
        // { 

        // }
        // if (priceDict != null){
        //     foreach(KeyValuePair<string,float> element in priceDict)
        //     {
        //     }
        // }

        float price = priceDict[symbol]; 
        if (funds-price >= 0)
        {
            funds -= price;
            float ratio = (funds / max_bar);
            healthBar.SetSize(ratio);
            priceController.PlaceOrder("BUY", symbol);
            // GetPrice(symbol);
        } 
    }

    public void Sell(string symbol)
    {
        // funds += value;
        // float ratio = (funds / max_bar);
        // healthBar.SetSize(ratio);
        float price = priceDict[symbol]; 
        if (funds-price >= 0)
        {
            funds += price;
            float ratio = (funds / max_bar);
            healthBar.SetSize(ratio);
            priceController.PlaceOrder("SELL", symbol);
            // GetPrice(symbol);
        } 
    } 

    public float GetPrice(string symbol){
        for (int i = 0; i < priceTuples.Length; i++)
        {
            if (symbol == priceTuples[i].Item1){
                return priceTuples[i].Item2;
            }
        }
        return -1; 
    }

    void FixedUpdate(){
        GetPriceDict();
        PushPricesToTerminals();
    }

    private void GetPriceDict(){
        priceDict = priceController.GetAllPrices();
    }

    private void PushPricesToTerminals()
    {
        foreach(TermSetActive element in termObjectArr) 
        {
            float newPrice = priceDict[element.symbol];
            element.SetValue(newPrice);
        }
    }

    public void MakeFinishButtonActive()
    {
        finishRoundButton.SetActive(true);
    }

    public void LoadVotingScene()
    {
        SavePlayer();
        SceneManager.LoadScene("VotingScene");
    }

    public void SavePlayer()
    {
        localPlayerData.funds = funds;
        GlobalControl.Instance.savedPlayerData = localPlayerData;
        priceController.SaveData();
    }

    void LoadPlayer () 
    {   
        localPlayerData = GlobalControl.Instance.savedPlayerData;
    }
}
