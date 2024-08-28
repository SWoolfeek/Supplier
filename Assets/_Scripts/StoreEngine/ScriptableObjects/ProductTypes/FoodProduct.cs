namespace StoreEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;


    [CreateAssetMenu (menuName = "ScriptableObjects/Store Engine/Products/Food")]
    public class FoodProduct : Product
    {
        [BoxGroup("Food")] [DisableInPlayMode]
        public float energyValue;
        
        private FoodProduct()
        {
            productType = ProductType.Weapon;
        }
    }
}