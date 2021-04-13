using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Move : MonoBehaviour
{

	public float speed = 0.25f;
    private PhotonView myPV;
    public GameSetupController gameSetup;
    // public HealthBar healthBar; 
    
    Vector2 velocity;
	Rigidbody2D rb; 

	private void Awake() 
	{
		rb = GetComponent<Rigidbody2D>();
		velocity = Vector2.zero;
	}

    // Start is called before the first frame update
    void Start()
    {
        gameSetup = GameObject.Find("GameSetup").GetComponent<GameSetupController>();
        myPV = GetComponent<PhotonView>();
        if (myPV.IsMine)
        {
            Camera.main.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal"); 
        velocity.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        if (myPV.IsMine && gameSetup.roundFinished == false)
        {
    	   rb.MovePosition(rb.position+velocity*speed*Time.fixedDeltaTime);
        }
    }
}
