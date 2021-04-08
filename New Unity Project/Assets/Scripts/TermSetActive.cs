using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using Photon.Pun;

public class TermSetActive : MonoBehaviour
{
	private bool isIncreasing = false;
    private SpriteRenderer m_SpriteRenderer;
    private TextMeshPro mText;
    public float value = 0;
    public string symbol = "TSLA";

    public GameSetupController gameSetup;

    private void Start()
    {
        // m_SpriteRenderer = GetComponent<SpriteRenderer>();
        mText = GetComponent<TextMeshPro>();
        gameSetup = GameObject.Find("GameSetup").GetComponent<GameSetupController>();
    }

    public void Update()
    {
        if (value > 0) {
            mText.text = "+"+value.ToString();
            mText.color = Color.green;
            // m_SpriteRenderer.color = Color.green;
        } else if (value < 0)
        {
            mText.text = value.ToString();
            mText.color = Color.red;
            // m_SpriteRenderer.color = Color.red;
        } else {
            mText.text = "----";
            mText.color = Color.white;
            // m_SpriteRenderer.color = Color.white;
        }
    }

    // [PunRPC]
    public void Increase()
    {
        // value += 100;
        gameSetup.Buy(symbol);
    }

    // [PunRPC]
    public void Decrease()
    {
        // value -= 100;
        gameSetup.Sell(symbol);
    }

    // public void ToggleColor() {
    // 	isIncreasing = !isIncreasing;
    // 	if (isIncreasing) {
    // 		Increase();
    // 	} else 
    // 	{
    // 		Decrease();
    // 	}
    // }

    public void SetValue(float price) 
    {
        value = price;
    } 
}
