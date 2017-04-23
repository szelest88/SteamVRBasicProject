using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmallWorld
{
    public class GameController : MonoBehaviour
    {
        [Tooltip("Main Base (Const)")]
        public Health Base;

        [Tooltip("Receives messages (Const)")]
        public GameObject Listener;

        public bool CreateEntity(GameObject gameObjectPrefab, Vector3 position)
        {
            bool success;

            if (gameObjectPrefab.GetComponent<Cannon>() != null)
            {
                var nearestEnergySupplier = FindNearestEnergySupplierToPosition(position);
                var nearestMassSupplier = FindNearestMassSupplierToPosition(position);

                if (nearestEnergySupplier != null && nearestMassSupplier != null)
                {
                    Cannon.CreateAtPosition(gameObjectPrefab.GetComponent<Cannon>(), position, transform, nearestEnergySupplier, nearestMassSupplier);
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            else if (gameObjectPrefab.GetComponent<MassMiner>() != null)
            {
                var nearestEnergySupplier = FindNearestEnergySupplierToPosition(position);

                if (nearestEnergySupplier != null)
                {
                    MassMiner.CreateAtPosition(gameObjectPrefab.GetComponent<MassMiner>(), position, transform, nearestEnergySupplier);
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            else
            {
                throw new System.ArgumentException(string.Format("Unsupported game object prefab. gameObjectPrefab: {0}", gameObjectPrefab));
            }

            return success;
        }

        EnergySupplier FindNearestEnergySupplierToPosition(Vector3 position)
        {
            EnergySupplier nearestEnergySupplier = null;
            float smallestDistance = float.MaxValue;

            foreach (Transform childTransform in transform)
            {
                var energySupplier = childTransform.GetComponent<EnergySupplier>();
                if (energySupplier != null)
                {
                    var distance = (childTransform.position - position).magnitude;
                    if (distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        nearestEnergySupplier = energySupplier;
                    }
                }
            }

            return nearestEnergySupplier;
        }

        MassSupplier FindNearestMassSupplierToPosition(Vector3 position)
        {
            MassSupplier nearestMassSupplier = null;
            float smallestDistance = float.MaxValue;

            foreach (Transform childTransform in transform)
            {
                var massSupplier = childTransform.GetComponent<MassSupplier>();
                if (massSupplier != null)
                {
                    var distance = (childTransform.position - position).magnitude;
                    if (distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        nearestMassSupplier = massSupplier;
                    }
                }
            }

            return nearestMassSupplier;
        }

        void Start()
        {

        }

        void Update()
        {
            if (Base)
            {
                if (Listener != null)
                {
                    Listener.SendMessage("OnHealthChanged", Base.HealthCurrent, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}