using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmallWorld
{
    public class Broadcaster : MonoBehaviour
    {
        public uint PayLoadMax;
        public uint PayLoadCurrent;

        public float BroadcastInterval;

        public List<Receiver> Receivers = new List<Receiver>();

        public Transporter TransporterPrefab;

        private uint numOfSentTransporters = 0;

        // private Dictionary<Receiver, List<Transporter>> ReceiversTransporters = new Dictionary<Receiver, List<Transporter>>();

        void Start()
        {
            StartCoroutine(Broadcast());
        }

        IEnumerator Broadcast()
        {
            while (true)
            {
                var maxPayLoadToBroadcastForEachReceiver = PayLoadCurrent / 3;

                if (maxPayLoadToBroadcastForEachReceiver > 0)
                {
                    foreach (Receiver receiver in Receivers)
                    {
                        if (receiver.PayLoadNeeded > 0)
                        {
                            uint payloadForReceiverInTransport = 0;
                            foreach (Transform childTransform in transform)
                            {
                                Transporter childTransporter = childTransform.GetComponent<Transporter>();
                                if (childTransporter != null && childTransporter.Receiver.Equals(receiver))
                                {
                                    payloadForReceiverInTransport += childTransporter.PayLoad;
                                }
                            }

                            var payLoadNeededForThisReceiver = Math.Max(receiver.PayLoadNeeded - payloadForReceiverInTransport, 0);

                            if (payLoadNeededForThisReceiver > 0)
                            {
                                Transporter transporter = UnityEngine.Object.Instantiate(
                                    TransporterPrefab,
                                    Vector3.zero,
                                    Quaternion.identity,
                                    transform);
                                transporter.name = string.Format("{0}#Transporter{1}", name, numOfSentTransporters);

                                PayLoadCurrent -= transporter.SendToReceiverWithPayLoad(
                                    receiver,
                                    Math.Min(maxPayLoadToBroadcastForEachReceiver, payLoadNeededForThisReceiver)
                                    );

                                numOfSentTransporters += 1;

                                Debug.LogFormat("Broadcaster: Sending transport. broadcaster: {0}, receiver: {1}, payLoad: {2}",
                                    name, receiver.name, transporter.PayLoad);
                            }
                        }
                    }
                }
                else
                {
                    // no payload to broadcast
                }

                yield return new WaitForSeconds(BroadcastInterval);
            }
        }
    }
}