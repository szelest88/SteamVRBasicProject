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

    private ManipulableObject mobject;

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
    public enum ObjectType { Digger, Shooter};
    ObjectType spawnedObjectType;
    Color colorToSpawn;
    float chosenPadPositionX;
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
            parentObject.transform.localScale = new Vector3(distance, distance, distance);
            Debug.LogError(distance);
        }

        //Debug.LogError("triggerhold" + ((is_left ? "left" : "right") + ":" + triggerHold));

        //Debug.Log("trigger state: "+controller.GetState().rAxis1.x); // gets the trigger state (analog, 0-1)
        //if (is_left)
        //{
        //    Debug.Log("position:" + transform.position.ToString("F4")); // if we want controller position (in global (?) coordinates)
        //}
        // let's play:
        if (!is_left && isTouched) // while touched
        {
            //   by default, vector is printed with 1 floating point decimal, which is terribly incaurate.That's why "F2" is given in parameter.
            Vector2 touchPos = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
            float deltaX = touchPos.x;
            float padPositionXNormalizedTo10 = ((deltaX + 1) * 5.0f); // 0..10

            //  transform.FindChild("SpawnableElementPlace").transform.localScale = new Vector3(val/10.0f, val / 10.0f, val / 10.0f);// = val;
           // Material spawnableElementPlaceMaterial = transform.FindChild("SpawnableElementPlace").GetComponent<MeshRenderer>().material;
            if (padPositionXNormalizedTo10<=2.5)
                colorToSpawn = Color.yellow; //SetColor("_Color", Color.red);
            else if(padPositionXNormalizedTo10<=5)
                colorToSpawn = Color.green; //SetColor("_Color", Color.red);
            else if(padPositionXNormalizedTo10<=7.5)
                colorToSpawn = Color.blue; //SetColor("_Color", Color.red);
            else if(padPositionXNormalizedTo10<=10)
                colorToSpawn = Color.white; //SetColor("_Color", Color.red);
            chosenPadPositionX = padPositionXNormalizedTo10;

            transform.FindChild("SpawnableElementPlace").GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = colorToSpawn;

            transform.FindChild("SpawnableElementPlace").GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = colorToSpawn;
            //  colorToSpawn = spawnableElementPlaceMaterial.color;
            if (padPositionXNormalizedTo10 < 5 && spawnedObjectType!=ObjectType.Digger)
            {
                spawnedObjectType = ObjectType.Digger; // show it

                transform.FindChild("SpawnableElementPlace").GetChild(0).gameObject.SetActive(true);

                transform.FindChild("SpawnableElementPlace").GetChild(1).gameObject.SetActive(false);


            }
            if(padPositionXNormalizedTo10 >= 5 && spawnedObjectType!=ObjectType.Shooter)
            {
                spawnedObjectType = ObjectType.Shooter;
                transform.FindChild("SpawnableElementPlace").GetChild(0).gameObject.SetActive(false);

                transform.FindChild("SpawnableElementPlace").GetChild(1).gameObject.SetActive(true);


            }

        }
    }
    public bool triggerHold = false;
    public bool is_gripped;

    private Vector2 savedTouchCoordinates;

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
    }
    bool isInCollision = false;
    public void unsetCollision(Collider collision)
    {
        isInCollision = false;
        //collidingGameObject = null;
    }


    public override void OnTriggerUnclicked(ClickedEventArgs e)
    {
        base.OnTriggerUnclicked(e);
        triggerHold = false;
         //   mobject.posToSet = (controller.transform.pos - savedControllerPosition); //  BAD
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
    public GameObject spawnableElementCannon, spawnableElementMassMiner;
    public GameObject parentObject;

    public SmallWorld.GameController gameController;

    public override void OnPadClicked(ClickedEventArgs e)
    {
        base.OnPadClicked(e);

        //TODO: replace with gameController::createEntity


        
        if (spawnedObjectType == ObjectType.Digger)
            gameController.CreateEntity(spawnableElementMassMiner, transform.position);
        if (spawnedObjectType == ObjectType.Shooter)
            gameController.CreateEntity(spawnableElementCannon, transform.position);

    }

    public override void OnPadUnclicked(ClickedEventArgs e)
    {
        base.OnPadUnclicked(e);
    }
    bool isTouched;
    Vector2 initialTouchCoordinates;
    public override void OnPadTouched(ClickedEventArgs e) // when did touched (i.e. wasn't touched before and now is, it's not continuous)
    {
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

    public void Awake()
    {
        if(!this.is_left)
        mobject = parentObject.GetComponent<ManipulableObject>();
    }

    public void OnHealthChanged(uint val)
    {
        Debug.LogError("HEALTH CHANGED!");
        if (!is_left)
            text.text = ""+val;
    }
    public TextMesh text;
}
