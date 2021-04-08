using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using Photon.Pun;

public class VotingController : MonoBehaviour
{
     
	// playerList = [];
	// playerToCountDict = ;
    // Start is called before the first frame update
    public Dictionary<int, int> votesDict = new Dictionary<int, int>(); 
    private int localID;
    private int majorityCount=PhotonNetwork.CurrentRoom.PlayerCount/2+1;
    public int votesIn = 0;

    void Start()
    {
        localID = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("qweuhrlkhfkdjf" + localID);
        votesDict.Add(1, 0);
        votesDict.Add(2, 0);
        votesDict.Add(3, 0);
        votesDict.Add(4, 0);
        votesDict.Add(5, 0);
        votesDict.Add(6, 0);
        votesDict.Add(7, 0);
        votesDict.Add(8, 0);
    }
 
    // for player in playerList send out options
    // view sends out vote
    // once all votes in count and find max
    // set palyer to cannot BUY SELL
    // reveal funds?
    // add funds lof remaining players to pool ?
    // change button

    public void LoadFloorScene()
    {
    	SceneManager.LoadScene("GameScene");
    }

    void findMaxCountPlayerID()
	{

	}

	// [PunRPC]
	public void CastVote(int id)
	{
        votesDict[id] = votesDict[id]+1;
        if(1==1)
        {
            Debug.Log("botes for "+votesDict[id]);
        }
	}
}
