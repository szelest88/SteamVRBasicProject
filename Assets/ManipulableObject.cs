using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulableObject : MonoBehaviour {
    public ControllerWrapper left;
    public ControllerWrapper right;
    public Vector3 posToSet;
    public Vector3 initialPosition;
	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
       // left.gameObject.transform.
    }
	
	// Update is called once per frame
	void Update () {

        //        Debug.LogError("state: " + left.triggerHold + ", " + right.triggerHold);

        //    transform.localScale = new Vector3(left.triggerHold ? 2 : initialScale.x, right.triggerHold?2: initialScale.y, transform.localScale.z);
        // transform.position = left.posToSet;
        if (left.triggerHold && right.triggerHold)
        {
             transform.localRotation=Quaternion.LookRotation((left.transform.position-right.transform.position).normalized);

            Debug.LogError("rotation:" + transform.localRotation);
        } else if(right.triggerHold)

        if (posToSet != null &&  (! (posToSet.x==0 && posToSet.y==0 && posToSet.z==0)))
            transform.position = initialPosition+posToSet;
        
    }
}
