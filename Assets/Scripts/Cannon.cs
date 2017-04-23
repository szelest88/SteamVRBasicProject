using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallWorld
{
    public class Cannon : MonoBehaviour
    {

        [Tooltip("Cannon shooting interval (Const)")]
        public float ShootingIntervalInSec;

        [Tooltip("Amount of energy used per shoot (Const)")]
        public uint EnergyCostPerShoot;

        [Tooltip("Amount of mass used per shoot (Const)")]
        public uint MassCostPerShoot;

        [Tooltip("Mass storage (Cosnt)")]
        public MassStorage MassStorage;
        [Tooltip("Energy storage (Const)")]
        public EnergyStorage EnergyStorage;
        [Tooltip("Bullet prefab (Const)")]
        public Bullet BulletPrefab;

        [Tooltip("Cannon mesh (Cosnt)")]
        public GameObject CannonBody;
        [Tooltip("Cannon radar (Const)")]
        public CanonRadar CannonRadar;

		[Tooltip("Cannon ready (Var readonly)")]
        public bool CannonReady = true;

        public static void CreateAtPosition(Cannon cannonPrefab, Vector3 position, Transform parent, EnergySupplier energySupplier, MassSupplier massSupplier)
        {
            Cannon cannon = GameObject.Instantiate(
                cannonPrefab,
                position,
                Quaternion.identity,
                parent
            );

            energySupplier.Receivers.Add(cannon.GetComponent<EnergyStorage>());
            massSupplier.Receivers.Add(cannon.GetComponent<MassStorage>());
        }

        void Start()
        {
        }

        void Update()
        {
            if (CannonRadar.TargetCurrent)
            {
                var directionToTarget = (transform.position - CannonRadar.TargetCurrent.transform.position).normalized;
                CannonBody.transform.rotation = Quaternion.LookRotation(directionToTarget);
				ShootIfPossibleAndReload();
            }
        }

        void ShootIfPossibleAndReload()
        {
            var shootPossible = CannonReady && CannonRadar.TargetCurrent != null && EnergyStorage.PayLoadCurrent >= EnergyCostPerShoot && MassStorage.PayLoadCurrent >= MassCostPerShoot;

            if (shootPossible)
            {
                Bullet bullet = GameObject.Instantiate(
                    BulletPrefab,
                    transform.position,
                    Quaternion.identity,
                    transform
                );
                Physics.IgnoreCollision(GetComponent<Collider>(), CannonRadar.TargetCurrent.GetComponent<Collider>());

                EnergyStorage.PayLoadCurrent -= EnergyCostPerShoot;
                MassStorage.PayLoadCurrent -= MassCostPerShoot;

                bullet.SeekTarget(CannonRadar.TargetCurrent);

				CannonReady = false;
				StartCoroutine(Reload());
            }
        }

		IEnumerator Reload() {
			yield return new WaitForSeconds(ShootingIntervalInSec);
			CannonReady = true;
		}
    }
}