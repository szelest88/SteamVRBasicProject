using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class TestCase1 : MonoBehaviour
    {
        public GameController GameController;

        public Cannon CannonPrefab;

        public MassMiner MassMinerPrefab;

        // Use this for initialization
        void Start()
        {
            Vector3[] coords = new Vector3[] {
                new Vector3( 1, 1, 1 ),
                new Vector3( 1, 1, -1 ),
                new Vector3( 1, -1, 1 ),
                new Vector3( 1, -1, -1 ),
                new Vector3( -1, 1, 1 ),
                new Vector3( -1, 1, -1),
                new Vector3( -1, -1, 1),
                new Vector3( -1, -1, -1),
            };

            foreach (var coord in coords) {
				GameController.CreateEntity(MassMinerPrefab.gameObject, 3 * coord);
			}
			foreach (var coord in coords) {
				GameController.CreateEntity(CannonPrefab.gameObject, 5 * coord);
			}

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}