namespace StoreEngine
{
#if UNITY_EDITOR
    using UnityEditor;
#endif
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;

    public class StoreProduction : MonoBehaviour
    {
        [SerializeField] private AllProducts allProducts;
        [SerializeField] private List<ProductUi> allUiProducts;
        
#if UNITY_EDITOR
        [SerializeField] private GameObject prefabUiProducts;
        [SerializeField] private Transform parentForUiProducts;
#endif
        [ReadOnly]
        [SerializeField] private int freeProduction;
        [HideInInspector]
        public int tempFreeProduction;
        private int maxProduction;
        
        private Product[] _products;
        private SaveProducts _saveProducts;

        public void Initialization(Product[] products, SaveProducts saveProducts)
        {
            _products = products;
            _saveProducts = saveProducts;

            maxProduction = allUiProducts.Count * 100;
            freeProduction = maxProduction;

            foreach (Product product in allProducts.allProducts)
            {
                freeProduction -= product.outputMultiplier;
            }

            tempFreeProduction = freeProduction;

            foreach (ProductUi productUi in allUiProducts)
            {
                productUi.Initialize();
            }
        }
        
        public void StartProduction()
        {
            //Debug.Log("Start production.");
            float time = Time.time;
            foreach (Product product in _products)
            {
                product.TickProduction();
                _saveProducts.AddModifiedProduct(product.productName,product.amount,product.outputMultiplier,product.outputResidual, false);
            }
            
            _saveProducts.SaveData();
            time = Time.time - time;
            Debug.Log("End production. " + time);
        }

        public void ChangedOutputMultiplier(Product product)
        {
            freeProduction = tempFreeProduction;
            _saveProducts.AddModifiedProduct(product.productName,product.amount,product.outputMultiplier,product.outputResidual);
        }
        
        
#if UNITY_EDITOR
        [Button]
        public void AddUiProducts()
        {
            allProducts.UpdateList();
            
            foreach (Product product in allProducts.allProducts)
            {
                //GameObject newProductUi = Instantiate(prefabUiProducts, parentForUiProducts);
                GameObject newProductUi = PrefabUtility.InstantiatePrefab(prefabUiProducts) as GameObject;
                newProductUi.transform.parent = parentForUiProducts;
                newProductUi.transform.localScale = new Vector3(1,1,1);
                newProductUi.GetComponent<ProductUi>().EditorInstantiate(product, this);
            }

            allUiProducts = new List<ProductUi>();
            
            foreach (Transform child in parentForUiProducts)
            {
                allUiProducts.Add(child.GetComponent<ProductUi>());
            }
        }
#endif
    }
}
