namespace StoreEngine
{
#if UNITY_EDITOR
    using System.Linq;
    using UnityEditor;  
#endif
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    
    public class AllProducts : ScriptableObject
    {
        [ReadOnly]
        public List<Product> allProducts;

        [HideInInspector]
        public string directory = "Saves";
        
#if UNITY_EDITOR
        [Button(ButtonSizes.Medium), PropertyOrder(-1)]
        public void UpdateList()
        {
            Product[] products = AssetDatabase.FindAssets("", new[] {"Assets/Data/Store Engine/Products/Weapons", "Assets/Data/Store Engine/Products/Food"})
                .Select(guid => AssetDatabase.LoadAssetAtPath<Product>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToArray();

            allProducts = new List<Product>();
            
            foreach (Product product in products)
            {
                allProducts.Add(product);
            }
            
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
