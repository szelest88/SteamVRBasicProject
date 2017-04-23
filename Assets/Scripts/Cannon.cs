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

		[Tooltip("Cannon radar (Const)")]
		public CanonRadar CannonRadar;

        void Start()
        {
        }

        void OnEnemyActivityDected(Enemy enemy)
        {
            var directionToEnemy = (enemy.transform.position - this.transform.position).normalized;
            GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(directionToEnemy);
        }

        void OnEnemyActivityCeased(Enemy enemy)
        {

        }

        IEnumerator Shoot()
        {
            while (true)
            {
                var shootPossible = CannonRadar.TargetCurrent != null && EnergyStorage.PayLoadCurrent >= EnergyCostPerShoot && MassStorage.PayLoadCurrent >= MassCostPerShoot;

                if (shootPossible)
                {
                    Bullet bullet = GameObject.Instantiate(
                        BulletPrefab,
                        transform.position,
                        Quaternion.identity,
                        transform
                    );

                    EnergyStorage.PayLoadCurrent -= EnergyCostPerShoot;
                    MassStorage.PayLoadCurrent -= MassCostPerShoot;

					bullet.SeekTarget(CannonRadar.TargetCurrent);
                }

                yield return new WaitForSeconds(ShootingIntervalInSec);
            }
        }
    }
}