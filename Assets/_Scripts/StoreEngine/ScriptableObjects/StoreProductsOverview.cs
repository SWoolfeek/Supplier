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
        public Product[] products;
        
        [ReadOnly] [TableList(AlwaysExpanded = true)]
        [SerializeField] private List<StoreProductDataOverview> data;


        [Button(ButtonSizes.Medium), PropertyOrder(-1)]
        public void UpdateCharacterOverview()
        {
            // Finds and assigns all scriptable objects of type Character
            this.products = AssetDatabase.FindAssets("", new[] {"Assets/Data/Store Engine/Products/Weapons", "Assets/Data/Store Engine/Products/Food"})
                .Select(guid => AssetDatabase.LoadAssetAtPath<Product>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();

            data = new List<StoreProductDataOverview>();
            
            foreach (Product sProduct in products)
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
        public ProductType type;
        public int amount;
        public float outputPerTick;
        public float outputMultiplier;
        public float space;

        public StoreProductDataOverview(Product product)
        {
            icon = product.icon;
            name = product.productName;
            type = product.productType;
            amount = product.amount;
            outputPerTick = product.outputPerTick;
            outputMultiplier = product.outputMultiplier;
            space = product.space;
        }
    }
#endif
}
