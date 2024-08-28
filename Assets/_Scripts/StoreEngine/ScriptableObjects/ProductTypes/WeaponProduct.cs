namespace StoreEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;


    [CreateAssetMenu (menuName = "ScriptableObjects/Store Engine/Products/Weapon")]
    public class WeaponProduct : Product
    {
        [BoxGroup("Weapon")] [DisableInPlayMode]
        public float damage;

        private WeaponProduct()
        {
            productType = ProductType.Weapon;
        }
    }
}
