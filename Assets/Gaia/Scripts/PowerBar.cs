using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {

    public Image bar;
    public RectTransform btn;

    public float _power;

	// Use this for initialization
	void Start () {
		
	}

    void powerChange (float power) {

        float amount = (power / 100.0f) * 180.0f/360;
        bar.fillAmount = amount;
        float btnAngle = amount * 360;
        btn.localEulerAngles = new Vector3(0,0,-btnAngle);
    }
	
	// Update is called once per frame
	void Update () {

        powerChange(_power);
	}
}
