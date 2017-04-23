using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmallWorld
{
    public class PayLoadSupplier<PayLoadStorageType> : MonoBehaviour
        where PayLoadStorageType : PayLoadStorage
    {
        public float BroadcastInterval;

        public PayLoadStorageType PayLoadSource;

        public List<PayLoadStorageType> Receivers = new List<PayLoadStorageType>();

        public Transporter TransporterPrefab;

        private uint numOfSentTransporters = 0;

        void Start()
        {
            StartCoroutine(Broadcast());
        }

        IEnumerator Broadcast()
        {
            while (true)
            {
                var maxPayLoadToBroadcastForEachReceiver = PayLoadSource.PayLoadCurrent / 3;

                if (maxPayLoadToBroadcastForEachReceiver > 0)
                {
                    uint[] receiversRemainingPayLoadNeed = new uint[Receivers.Count];

                    for (int i = 0; i < Receivers.Count; ++i)
                    {
                        PayLoadStorage receiver = Receivers[i];

                        uint receiverRemainingPayLoadNeed;
                        if (receiver.PayLoadNeeded > 0)
                        {
                            uint payloadForReceiverInTransport = 0;
                            foreach (Transform childTransform in transform)
                            {
                                Transporter childTransporter = childTransform.GetComponent<Transporter>();
                                if (childTransporter != null && childTransporter.Receiver.Equals(receiver))
                                {
                                    payloadForReceiverInTransport += childTransporter.PayLoadCapacity;
                                }
                            }

                            // FIXME: Fix uint problem !!!
                            receiverRemainingPayLoadNeed = Math.Max(receiver.PayLoadNeeded - payloadForReceiverInTransport, 0);
                        }
                        else
                        {
                            receiverRemainingPayLoadNeed = 0;
                        }

                        receiversRemainingPayLoadNeed[i] = receiverRemainingPayLoadNeed;
                    }

                    List<PayLoadStorageType> receiversSortedByPayLoadNeed = new List<PayLoadStorageType>(Receivers);
                    receiversSortedByPayLoadNeed.Sort((r1, r2) =>
                    {
                        var r1RemainingPayLoadNeed = receiversRemainingPayLoadNeed[Receivers.IndexOf(r1)];
                        var r2RemainingPayLoadNeed = receiversRemainingPayLoadNeed[Receivers.IndexOf(r2)];
                        return r1RemainingPayLoadNeed.CompareTo(r2RemainingPayLoadNeed) * -1;
                    });

                    foreach (PayLoadStorageType receiver in receiversSortedByPayLoadNeed)
                    {
                        if (receiver.PayLoadNeeded > 0)
                        {
                            var remainingPayLoadNeededForThisReceiver = receiversRemainingPayLoadNeed[Receivers.IndexOf(receiver)];

                            bool receiverNeedsPayLoad = remainingPayLoadNeededForThisReceiver > 0;
                            bool payLoadSourceHasRequiredAmountOfPayLoad = PayLoadSource.PayLoadCurrent >= TransporterPrefab.PayLoadCapacity;
                            if (receiverNeedsPayLoad && payLoadSourceHasRequiredAmountOfPayLoad)
                            {
                                Transporter transporter = UnityEngine.Object.Instantiate(
                                    TransporterPrefab,
                                    transform.position,
                                    Quaternion.identity,
                                    transform);
                                transporter.name = string.Format("{0}#Transporter{1}", name, numOfSentTransporters);

                                transporter.SendToReceiverWithPayLoad(receiver);
                                PayLoadSource.PayLoadCurrent -= transporter.PayLoadCapacity;

                                numOfSentTransporters += 1;

                                Debug.LogFormat("Broadcaster: Sending transport. broadcaster: {0}, receiver: {1}, payLoad: {2}",
                                    name, receiver.name, TransporterPrefab.PayLoadCapacity);
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