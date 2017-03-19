﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VrapperSizeTester : MonoBehaviour {

    ChaperoneInfo ci;
	// Use this for initialization
	void Start () {
        ci = ChaperoneInfo.instance; // causes some logs to appear (with accurate data on play area size...)
        ChaperoneInfo.Initialized.AddListener(new UnityEngine.Events.UnityAction(logInfoAboutSize));
        
    }

    // it's delayed
    void logInfoAboutSize() {
        Debug.Log("Size X: " + ci.playAreaSizeX);
        Debug.Log("Size Z: " + ci.playAreaSizeZ);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
