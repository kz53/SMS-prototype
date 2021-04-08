using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TermButton : MonoBehaviour
{
	// public TermSetActive term; 
    public Transform playerTransform;
    private float interactRadius = 1f;
    private GameSetupController gameSetup;
    private PhotonView myPV;

    void Start()
    {
        // playerTransform = this.parent.transform;
        gameSetup = GameObject.Find("GameSetup").GetComponent<GameSetupController>();
        GameObject obj = this.transform.parent.gameObject;
        myPV = obj.GetComponent<PhotonView>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (myPV.IsMine && gameSetup.roundFinished == false)
        {
            if (Input.GetKeyDown(KeyCode.J)) 
            {
            	// term.Decrease();
                Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(playerTransform.position, interactRadius);
                foreach (Collider2D collider2D in collider2DArray)
                {
                    TermSetActive term = collider2D.GetComponent<TermSetActive>();
                    
                    if(term != null)
                    {
                        // term.Decrease();
                        gameSetup.Sell(term.symbol);
                    }
                    // PhotonView pView = collider2D.GetComponent<PhotonView>();
                    // if (pView != null && term != null) 
                    // {
                    //     pView.RPC("Decrease", RpcTarget.All);
                    // }
                }
            }
            if (Input.GetKeyDown(KeyCode.I)) 
            {
            	// term.Increase();
                Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(playerTransform.position, interactRadius);
                foreach (Collider2D collider2D in collider2DArray)
                {
                    TermSetActive term = collider2D.GetComponent<TermSetActive>();
                    if(term != null)
                    {
                        // term.Increase();
                        gameSetup.Buy(term.symbol);
                    }

                    // PhotonView pView = collider2D.GetComponent<PhotonView>();
                    // if (pView != null && term != null) 
                    // {
                    //     pView.RPC("Increase", RpcTarget.All);
                    // }
                }
            }
        }
    }
}
