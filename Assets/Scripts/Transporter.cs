using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class Transporter : MonoBehaviour
    {
        public uint PayLoadMax;
        public float Speed;

        public uint PayLoad { get; private set; }
        public Receiver Receiver { get; private set; }

        public uint SendToReceiverWithPayLoad(Receiver receiver, uint payLoad)
        {
            Receiver = receiver;
            uint amountOfPayLoadLoaded;
            if (payLoad > PayLoadMax)
            {
                PayLoad = PayLoadMax;
                amountOfPayLoadLoaded = PayLoadMax;
            }
            else
            {
                PayLoad = payLoad;
                amountOfPayLoadLoaded = payLoad;
            }


            var directionToTarget = (receiver.transform.position - this.transform.position).normalized;
            Debug.LogFormat("Receiver pos: {0}, transporter pos: {1}", receiver.transform.position, this.transform.position);

            transform.rotation = Quaternion.LookRotation(directionToTarget);
            GetComponent<Rigidbody>().velocity = Speed * directionToTarget;

            return amountOfPayLoadLoaded;
        }

        void Start()
        {
            // GetComponent<Rigidbody>().velocity = new Vector3(20, 20, 20);
        }

        void Update()
        {
            // Debug.LogFormat("Transporter {0} velocity: {1}", name, GetComponent<Rigidbody>().velocity);
        }

        void OnCollisionEnter(Collision collision)
        {
            bool collidedWithReceiver = (collision.gameObject.GetInstanceID() == Receiver.gameObject.GetInstanceID());
            if (collidedWithReceiver)
            {
                Receiver.ReceivePayLoad(this, PayLoad);
                PayLoad = 0;
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarningFormat("Transporter encountered unexpected collision. transporter: {0}, other: {1}", name, collision.gameObject.name);
            }
        }
    }
}
