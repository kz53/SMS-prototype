using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class PriceController : MonoBehaviour
{
	private PhotonView myPV;
	public Dictionary<string, float> priceDict = 
                    new Dictionary<string, float>();
    public Dictionary<int, float> playerFundsDict = 
                    new Dictionary<int, float>();
    public Dictionary<string, float> playerSharesDict = 
                    new Dictionary<string, float>();
	float player1Funds = 100000f;

    void Start()
    {
        Debug.Log("trauiry"+PhotonNetwork.LocalPlayer.UserId);

        if (GlobalControl.Instance.priceDict == null)
        {
			priceDict.Add("AAPL", 100f);
			priceDict.Add("AMZN", 100f);
			priceDict.Add("BA", 100f);
			priceDict.Add("FB", 100f);
			priceDict.Add("GME", 100f);
			priceDict.Add("GOOG", 100f);
			priceDict.Add("MSFT", 100f);
			priceDict.Add("SHOP", 100f);
			priceDict.Add("TSLA", 700f);
			priceDict.Add("U", 100f);
    	}
		else
		{
			priceDict = GlobalControl.Instance.priceDict; 
		}

		GameObject obj = this.transform.gameObject;
        myPV = obj.GetComponent<PhotonView>();

    }

    void InitPlayer(int playerID)
    {
        playerFundsDict.Add(playerID, 30000f);

        // playerSharesDict.Add(playerID, emptyShares);
    }

    public void PlaceOrder(string orderType, string symbol, string playerID="player1")
    {
    	// if (orderType == "BUY" && CanBuy("player", symbol))
    	// {
    	// 	player1Funds -= GetPrice(symbol);
    	// 	float newPrice = CalculateNewPrice(symbol);
    	// 	SetPrice(symbol, newPrice);
    	// 	myPV.RPC("SetPrice", RpcTarget.Master, symbol, newPrice);
    	// 	return;
    	// } 
    	// if (orderType == "SELL" && CanSell("player", symbol))
    	// {
    	// 	player1Funds -= GetPrice(symbol);
    	// 	float newPrice = CalculateNewPrice(symbol);
    	// 	// SetPrice(symbol, newPrice);
    	// 	myPV.RPC("SetPrice", RpcTarget.Master, symbol, newPrice); 
    	// 	return;
    	// } 
        myPV.RPC("HandleOrder", RpcTarget.MasterClient, orderType, symbol, playerID); 
    }

    [PunRPC]
    public void HandleOrder(string orderType, string symbol, string playerID="player1")
    {
        if (orderType == "BUY" && CanBuy("player", symbol))
        {
            player1Funds -= GetPrice(symbol);
            float newPrice = CalculateNewPrice(symbol);
            SetPrice(symbol, newPrice);
            myPV.RPC("SetPrice", RpcTarget.All, symbol, newPrice);
            return;
        } 
        if (orderType == "SELL" && CanSell("player", symbol))
        {
            player1Funds -= GetPrice(symbol);
            float newPrice = CalculateNewPrice(symbol);
            // SetPrice(symbol, newPrice);
            myPV.RPC("SetPrice", RpcTarget.All, symbol, newPrice);
            return;
        } 
    }

    float GetPrice(string symbol)
    {
    	return priceDict[symbol];
    }

    [PunRPC]
    public void SetPrice(string symbol, float price)
    {
    	priceDict[symbol] = price;
    }

	float CalculateNewPrice(string symbol)
	{
		return priceDict[symbol] + 10f;
	}

    public Dictionary<string, float> GetAllPrices() 
    {
    	return priceDict;
    }

    bool CanBuy(string playerID, string symbol)
    {
    	return true;
    }

    bool CanSell(string playerID, string symbol)
    {
    	return true;
    }

    public void SaveData()
    {
    	GlobalControl.Instance.priceDict = this.priceDict;
    }

}
