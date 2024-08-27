namespace ProductMenu
{
    using System;
    using UnityEngine;
#if UNITY_EDITOR
    using System.Collections.Generic;
    using UnityEditor;
    using Sirenix.OdinInspector;
    using System.Linq;
    using StoreEngine;
#endif

    [CreateAssetMenu (menuName = "ScriptableObjects/Product Menu/Store Products")]
    public class StoreProductsOverview : ScriptableObject
    {
#if UNITY_EDITOR
        [ListDrawerSettings(ShowFoldout = true)] [HideInInspector]
        public StoreProduct[] storeProducts;
        
        [ReadOnly] [TableList(AlwaysExpanded = true)]
        [SerializeField] private List<StoreProductDataOverview> data;


        [Button(ButtonSizes.Medium), PropertyOrder(-1)]
        public void UpdateCharacterOverview()
        {
            // Finds and assigns all scriptable objects of type Character
            this.storeProducts = AssetDatabase.FindAssets("", new[] {"Assets/Data/Store Engine/Products/Store Product"})
                .Select(guid => AssetDatabase.LoadAssetAtPath<StoreProduct>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();

            data = new List<StoreProductDataOverview>();
            
            foreach (StoreProduct sProduct in storeProducts)
            {
                data.Add(new StoreProductDataOverview(sProduct));
            }
        }
#endif
    }
 
#if UNITY_EDITOR
    [Serializable]
    public class StoreProductDataOverview
    {
        [HideLabel, PreviewField(60)]
        public Texture icon;
        public string name;
        public int amount;
        public float outputPerTick;
        public float outputMultiplier;
        public float space;

        public StoreProductDataOverview(StoreProduct storeProduct)
        {
            icon = storeProduct.product.icon;
            name = storeProduct.product.name;
            amount = storeProduct.amount;
            outputPerTick = storeProduct.outputPerTick;
            outputMultiplier = storeProduct.outputMultiplier;
            space = storeProduct.space;
        }
    }
#endif
}
