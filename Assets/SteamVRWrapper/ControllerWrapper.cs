using UnityEngine;
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
    public enum EnumKolor { Green, Red, Blue };
    float colorVal;
    // Update is called once per frame
    protected override void Update () {
        base.Update();


        if(triggerHold)
        {
         //   if(mobject!=null) // uncomment to enable translation
         //   mobject.posToSet = (controller.transform.pos - savedControllerPosition);
        }

        if (mobject!=null && mobject.scalingmode)
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
        Debug.Log("update");
        // let's play:
        if (!is_left && isTouched) // while touched
        {
            //   by default, vector is printed with 1 floating point decimal, which is terribly incaurate.That's why "F2" is given in parameter.
            Vector2 touchPos = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
                Debug.Log("pad touched:" + touchPos.ToString("F2"));

            Debug.Log("pad touch begin" + initialTouchCoordinates.ToString("F2"));
            float deltaX = touchPos.x;
            float val = ((deltaX + 1) * 5.0f);

            Debug.LogError("pad debug val" + val);
            Debug.LogError("pad debug color" + transform.FindChild("SpawnableElementPlace").GetComponent<MeshRenderer>().material.color);
          //  transform.FindChild("SpawnableElementPlace").transform.localScale = new Vector3(val/10.0f, val / 10.0f, val / 10.0f);// = val;
            if (val<=2.5)
                transform.FindChild("SpawnableElementPlace").GetComponent<MeshRenderer>().material.color = Color.red; //SetColor("_Color", Color.red);
            else if(val<=5)
                transform.FindChild("SpawnableElementPlace").GetComponent<MeshRenderer>().material.color = Color.green; //SetColor("_Color", Color.red);
            else if(val<=7.5)
                transform.FindChild("SpawnableElementPlace").GetComponent<MeshRenderer>().material.color = Color.blue; //SetColor("_Color", Color.red);
            else if(val<=10)
                transform.FindChild("SpawnableElementPlace").GetComponent<MeshRenderer>().material.color = Color.white; //SetColor("_Color", Color.red);
            colorVal = val;
            //if (deltaX < 0)
            //    transform.FindChild("SpawnableElementPlace").GetComponent<MeshRenderer>().material.color = Color.red; //SetColor("_Color", Color.red);
            //else
            //    transform.FindChild("SpawnableElementPlace").GetComponent<MeshRenderer>().material.color = Color.green;// SetColor("_Color", Color.green);
            // rotate something of delta
        }
    }
    public bool triggerHold = false;
    public bool is_gripped;

    public Vector2 savedTouchCoordinates;

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
        //spawnedObject.
                        if (colorVal <= 2.5)
            spawnedObject.GetComponent<MeshRenderer>().material.color = Color.red; //SetColor("_Color", Color.red);
        else if (colorVal <= 5)
            spawnedObject.GetComponent<MeshRenderer>().material.color = Color.green; //SetColor("_Color", Color.red);
        else if (colorVal <= 7.5)
            spawnedObject.GetComponent<MeshRenderer>().material.color = Color.blue; //SetColor("_Color", Color.red);
        else if (colorVal <= 10)
            spawnedObject.GetComponent<MeshRenderer>().material.color = Color.white; //SetColor("_Color", Color.red);
    }

    public override void OnPadUnclicked(ClickedEventArgs e)
    {
        base.OnPadUnclicked(e);
        spawnedObject.transform.SetParent(GameObject.Find("TestSphere").transform);
    }
    bool isTouched;
    Vector2 initialTouchCoordinates;
    public override void OnPadTouched(ClickedEventArgs e) // when did touched (i.e. wasn't touched before and now is, it's not continuous)
    {
        Debug.LogError("!"+e.padX);
        base.OnPadTouched(e);
        isTouched = true;
        initialTouchCoordinates = new Vector2(e.padX, e.padY);
    }

    public override void OnPadUntouched(ClickedEventArgs e)
    {
        base.OnPadUntouched(e);
        isTouched = false;
        initialTouchCoordinates = Vector2.zero;
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
