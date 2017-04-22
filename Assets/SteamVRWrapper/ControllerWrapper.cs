﻿using UnityEngine;
using System.Collections;

public class ControllerWrapper : SteamVR_TrackedController {

    public bool is_left;
    public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)controllerIndex); } }
    public Vector3 velocity { get { return controller.velocity; } }
    public Vector3 angularVelocity { get { return controller.angularVelocity; } }


    /// position remembered when trigger pulled
    /// </summary>
    Vector3 savedControllerPosition;
    Vector3 savedControllersDistance;

    public ManipulableObject mobject;

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
        yield return null;
    }
}
    /// <summary>
    /// Triggers the haptic feedback for the selected controller
    /// </summary>
    /// <param name="seconds">duration in seconds</param>
    /// <param name="force">force from 0 to 1</param>
    /// 
    public void VrapperTriggerHaptics(float seconds, float force)
    {
        if(gameObject.activeSelf) // marudził bez tego
            StartCoroutine(LongVibration(seconds, force));
    }
    // Use this for initialization
    protected override void Start () {
        base.Start();
        // TODO: implement
        posToSet = new Vector3();
	}
    public Vector3 posToSet;
    public bool scalingMode = false;
    public float scale = 1;
	// Update is called once per frame
	protected override void Update () {
        base.Update();


        if(triggerHold)
        {
         //   if(mobject!=null) // uncomment to enable translation
         //   mobject.posToSet = (controller.transform.pos - savedControllerPosition);
        }

        if (mobject.scalingmode)
        {
            float distance = (mobject.left.transform.position - mobject.right.transform.position).magnitude;
            mobject.transform.localScale = new Vector3(distance, distance, distance);
        }

        //Debug.LogError("triggerhold" + ((is_left ? "left" : "right") + ":" + triggerHold));
   
        //Debug.Log("trigger state: "+controller.GetState().rAxis1.x); // gets the trigger state (analog, 0-1)
        //if (is_left)
        //{
        //    Debug.Log("position:" + transform.position.ToString("F4")); // if we want controller position (in global (?) coordinates)
        //}
        //if (padTouched) // while touched
        //{
                // by default, vector is printed with 1 floating point decimal, which is terribly incaurate. That's why "F2" is given in parameter.
                //Debug.Log("pad touched:" + controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).ToString("F2"));
        //}
	}
    public bool triggerHold = false;
    public bool is_gripped;

    public override void OnTriggerClicked(ClickedEventArgs e)
    {

        base.OnTriggerClicked(e);
      //  Debug.Log("trigger clicked!"); // to be specific, when trigger in very high position - not the "click" itself
        VrapperTriggerHaptics(0.1f,0.5f);
     
            triggerHold = true;
        if(mobject!=null)
        savedControllerPosition = controller.transform.pos;
        Debug.LogError("saved position:" + savedControllerPosition.ToString("F4"));
    }

    public void setCollision(Collider collision)
    {
        GameObject collidingGameObject = collision.gameObject;
        isInCollision = true;
        mobject = collidingGameObject.GetComponent<ManipulableObject>();
        Debug.LogError("ENTER");
    }
    bool isInCollision = false;
    public void unsetCollision(Collider collision)
    {
        isInCollision = false;
        //collidingGameObject = null;
        Debug.LogError("EXIT");
    }


    public override void OnTriggerUnclicked(ClickedEventArgs e)
    {
        base.OnTriggerUnclicked(e);
        triggerHold = false;
      //  if (isInCollision)
            mobject.posToSet = (controller.transform.pos - savedControllerPosition);
    //    mobject.initialPosition = mobject.transform.position;//local?
    }

    public override void OnMenuClicked(ClickedEventArgs e)
    {
        base.OnMenuClicked(e);
    }

    public override void OnMenuUnclicked(ClickedEventArgs e)
    {
        base.OnMenuUnclicked(e);
    }

    public override void OnSteamClicked(ClickedEventArgs e)
    {
        base.OnSteamClicked(e);
    }
    public GameObject spawnableElement1;
    public GameObject parentObject;
    GameObject spawnedObject;
    public override void OnPadClicked(ClickedEventArgs e)
    {
        base.OnPadClicked(e);
        
        spawnedObject = Instantiate(spawnableElement1, transform.position, Quaternion.identity, transform.transform);
    }

    public override void OnPadUnclicked(ClickedEventArgs e)
    {
        base.OnPadUnclicked(e);
        spawnedObject.transform.SetParent(GameObject.Find("TestSphere").transform);
    }

    public override void OnPadTouched(ClickedEventArgs e) // when did touched (i.e. wasn't touched before and now is, it's not continuous)
    {
        base.OnPadTouched(e);
    }

    public override void OnPadUntouched(ClickedEventArgs e)
    {
        base.OnPadUntouched(e);
    }

    public override void OnGripped(ClickedEventArgs e)
    {
        is_gripped = true;
        base.OnGripped(e);
    }

    public override void OnUngripped(ClickedEventArgs e)
    {
        is_gripped = false;
        base.OnUngripped(e);
    }
}
