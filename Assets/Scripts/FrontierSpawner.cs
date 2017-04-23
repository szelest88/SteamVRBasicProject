using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class FrontierSpawner : MonoBehaviour
    {
        [Tooltip("Time between spawnings (Const)")]
        public float SpawningIntervalInSec;

        [Tooltip("Spawning sphere radius")]
        public float SpawningRadius;

        [Tooltip("Spawned object prefab (Const)")]
        public Enemy SpawnedObjectPrefab;

        [Tooltip("Initial target of spawned object (Const)")]
        public Health InitialTarget;

        void Start()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                if (InitialTarget)
                {
                    var spawningPosition = SpawningRadius * UnityEngine.Random.onUnitSphere;

                    Enemy spawnedObject = GameObject.Instantiate(
                        SpawnedObjectPrefab,
                        spawningPosition,
                        Quaternion.identity,
                        transform
                    );
                    spawnedObject.SeekTarget(InitialTarget);

                    yield return new WaitForSeconds(SpawningIntervalInSec);
                }
				else
				{
					// disable spawning
					break;
				}
            }
        }
    }
}