using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
	private Transform bar;
    private int multiple = 0;
    private TextMeshPro mText;
    public GameObject mult;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(mult);
        Debug.Log("aaa");
        mText = mult.GetComponent<TextMeshPro>();
        
        bar = transform.Find("Bar");
        
        // bar.localScale = new Vector3(.4f, 1f);
    }

    public void SetSize(float sizeNormalized)
    { 
    	if (bar == null) 
        {
            bar = transform.Find("Bar");
        }
        if (mText == null)
        {
           mText = mult.GetComponent<TextMeshPro>(); 
        }
        multiple = (int)sizeNormalized;
        
        mText.text = "x"+multiple.ToString();
        bar = transform.Find("Bar");
    	bar.localScale = new Vector3(sizeNormalized%1, 1f);
    }
}
