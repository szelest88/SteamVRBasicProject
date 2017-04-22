using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulableObject : MonoBehaviour {
    public ControllerWrapper left;
    public ControllerWrapper right;

    public Vector3 initialPosition;
	// Use this for initialization
	void Start () {
        initialPosition = transform.localPosition;
       // left.gameObject.transform.
    }
	
	// Update is called once per frame
	void Update () {

        Debug.LogError("state: " + left.triggerHold + ", " + right.triggerHold);

        //    transform.localScale = new Vector3(left.triggerHold ? 2 : initialScale.x, right.triggerHold?2: initialScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(left.scaleToSet, left.scaleToSet, left.scaleToSet);
        if(!left.triggerHold && !right.triggerHold)
        {
           // transform.localScale = initialScale;
        }
    }
}
