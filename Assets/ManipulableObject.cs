using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulableObject : MonoBehaviour {
    public ControllerWrapper left;
    public ControllerWrapper right;
    public Vector3 posToSet;
    public Vector3 initialPosition;
    public bool scalingmode;
	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        scalingmode = false;
       // left.gameObject.transform.
    }
   public  float initialDistance;
	// Update is called once per frame
	void Update () {

        //        Debug.LogError("state: " + left.triggerHold + ", " + right.triggerHold);

        //    transform.localScale = new Vector3(left.triggerHold ? 2 : initialScale.x, right.triggerHold?2: initialScale.y, transform.localScale.z);
        // transform.position = left.posToSet;
        if (left.triggerHold && right.triggerHold)
        {
            transform.localRotation = Quaternion.LookRotation((left.transform.position - right.transform.position).normalized);

     //       Debug.LogError("rotation:" + transform.localRotation);
        }
        else if (right.triggerHold)
        {

            if (posToSet != null && (!(posToSet.x == 0 && posToSet.y == 0 && posToSet.z == 0)))
                transform.position = initialPosition + posToSet;
        }

        if (left.is_gripped && right.is_gripped)
        {

            if (!this.scalingmode)
            {
                // began scaling

                initialDistance = (right.transform.position - left.transform.position).magnitude/transform.localScale.x;
                Debug.LogError("initialDistance set to " + initialDistance);
            }
            this.scalingmode = true;
        }
        else
            this.scalingmode = false;
    }
}
