using UnityEngine;
using System.Collections;

public class ControllerWrapper : SteamVR_TrackedController {

    public bool is_left;
    public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)controllerIndex); } }
    public Vector3 velocity { get { return controller.velocity; } }
    public Vector3 angularVelocity { get { return controller.angularVelocity; } }


    // Use this for initialization
    protected override void Start () {
        base.Start();
        // TODO: implement
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        Debug.Log("trigger state: "+controller.GetState().rAxis1.x); // gets the trigger state (analog, 0-1)

        if (padTouched) // while touched
        {
            
                Debug.Log("pad touched:" + controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad));
        }
	}

    public override void OnTriggerClicked(ClickedEventArgs e)
    {
        base.OnTriggerClicked(e);
    }

    public override void OnTriggerUnclicked(ClickedEventArgs e)
    {
        base.OnTriggerUnclicked(e);
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

    public override void OnPadClicked(ClickedEventArgs e)
    {
        base.OnPadClicked(e);
    }

    public override void OnPadUnclicked(ClickedEventArgs e)
    {
        base.OnPadUnclicked(e);
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
        base.OnGripped(e);
    }

    public override void OnUngripped(ClickedEventArgs e)
    {
        base.OnUngripped(e);
    }
}
