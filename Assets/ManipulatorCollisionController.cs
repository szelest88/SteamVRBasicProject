using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulatorCollisionController : MonoBehaviour {

    public GameObject collidingObject;

    private void OnTriggerEnter(Collider collision)
    {
        collidingObject = collision.gameObject;
        GetComponentInParent<ControllerWrapper>().setCollision(collision);
    }

    private void OnTriggerExit(Collider collision)
    {

        collidingObject = null;

        GetComponentInParent<ControllerWrapper>().unsetCollision(collision);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnCollisionExit(Collision collision)
    {
}
}
