using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class MassMiner : MonoBehaviour
    {

		public static void CreateAtPosition(MassMiner massMinerPrefab, Vector3 position, Transform parent, EnergySupplier energySupplier) {
			MassMiner massMiner = GameObject.Instantiate(
				massMinerPrefab,
				position,
				Quaternion.identity,
				parent
			);

			energySupplier.Receivers.Add(massMiner.GetComponent<EnergyStorage>());
		}

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}