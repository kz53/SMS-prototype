// master game object 

//voting system
    // button for all players
    // master client controller
//stock tracker and display

// price models
// w

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameSetupController : MonoBehaviour
{
    private float funds = 20000f;
    private float max_bar = 10000f;
    public HealthBar healthBar;
    // private HealthBar
    public Tuple<string, float>[] priceTuples;
    public PriceController priceController;
    private TermSetActive[] termObjectArr;
    Dictionary<string, float> priceDict; 
    Dictionary<string, int> shareDict = 
                    new Dictionary<string, int>(); 
    Dictionary<string, HealthBar> stockBarDict = 
                    new Dictionary<string, HealthBar>(); 
    public bool roundFinished = false;
    public GameObject finishRoundButton;

    public PlayerStatistics localPlayerData = new PlayerStatistics();
    

    public HealthBar stockBar0;
    public HealthBar stockBar1;
    public HealthBar stockBar2;
    public HealthBar stockBar3;
    public HealthBar stockBar4;
    public HealthBar stockBar5;
    public HealthBar stockBar6;
    public HealthBar stockBar7;
    public HealthBar stockBar8; 
    public HealthBar stockBar9;

    void Start()
    {

        CreatePlayer();
        LoadPlayer();
        InitializeShareDict();
        funds = localPlayerData.funds;
        shareDict = localPlayerData.shares;
        InitializeStockBarDict();
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

        float price = priceDict[symbol]; 
        if (funds-price >= 0)
        {
            funds -= price;
            float ratio = (funds / max_bar);
            healthBar.SetSize(ratio);
            shareDict[symbol] = shareDict[symbol] + 1;
            stockBarDict[symbol].SetSize(shareDict[symbol]/10f);
            priceController.PlaceOrder("BUY", symbol);
            Debug.Log(shareDict[symbol]);
            //decrease stock by 1
        } 
    }

    public void Sell(string symbol)
    {
        // funds += value;
        // float ratio = (funds / max_bar);
        // healthBar.SetSize(ratio);
        float price = priceDict[symbol]; 
        if (shareDict[symbol] > 0)
        {
            funds += price;
            float ratio = (funds / max_bar);
            healthBar.SetSize(ratio);
            shareDict[symbol] = shareDict[symbol] - 1;
            stockBarDict[symbol].SetSize(shareDict[symbol]/10f);
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
        localPlayerData.shares = shareDict;
        // localPlayerData.shareDict = 
        GlobalControl.Instance.savedPlayerData = localPlayerData;
        priceController.SaveData();
        Debug.Log(shareDict["AAPL"]);
        Debug.Log(GlobalControl.Instance.savedPlayerData.shares["AAPL"]);
    }

    void LoadPlayer () 
    {   
        localPlayerData = GlobalControl.Instance.savedPlayerData;
    }

    void InitializeShareDict()
    {
        
    }

    void InitializeStockBarDict()
    {
        stockBarDict.Add("AAPL", stockBar0);
        stockBarDict.Add("AMZN", stockBar1);
        stockBarDict.Add("BA", stockBar2);
        stockBarDict.Add("FB", stockBar3);
        stockBarDict.Add("GME", stockBar4);
        stockBarDict.Add("GOOG", stockBar5);
        stockBarDict.Add("MSFT", stockBar6);
        stockBarDict.Add("SHOP", stockBar7);
        stockBarDict.Add("TSLA", stockBar8);
        stockBarDict.Add("U", stockBar9);
        foreach(KeyValuePair<string, HealthBar> entry in stockBarDict) 
        {
            entry.Value.SetSize(shareDict[entry.Key]/10f);
        }
    }
}
