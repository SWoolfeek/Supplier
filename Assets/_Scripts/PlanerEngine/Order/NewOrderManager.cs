namespace PlanerEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif
    
    public class NewOrderManager : MonoBehaviour
    {

        [SerializeField] private StoreEngine.AllProducts allProducts;
        [SerializeField] private List<ProductInOrder> productsInOrder;

#if UNITY_EDITOR
        [Header("Editor Only")]
        [SerializeField] private GameObject prefabUiProductInOrder;
        [SerializeField] private Transform parentForUiProductsInOrder;
#endif

        public void Initialization()
        {
            UpdateAllProductsInOrder();
        }

        private void UpdateAllProductsInOrder()
        {
            foreach (ProductInOrder productInOrder in productsInOrder)
            {
                productInOrder.Initialize();
            }
        }


#if UNITY_EDITOR
        public void AddProductsInOrderCatalogue()
        {
            foreach (StoreEngine.Product product in allProducts.allProducts)
            {
                GameObject productInOrder = PrefabUtility.InstantiatePrefab(prefabUiProductInOrder) as GameObject;
                productInOrder.transform.parent = parentForUiProductsInOrder;
                productInOrder.transform.localScale = new Vector3(1, 1, 1);
                productInOrder.name = product.productName;
                productInOrder.GetComponent<ProductInOrder>().product = product;
                productInOrder.GetComponent<ProductInOrder>().Initialize();
            }

            productsInOrder = new List<ProductInOrder>();

            foreach (Transform child in parentForUiProductsInOrder)
            {
                productsInOrder.Add(child.GetComponent<ProductInOrder>());
            }
        }
#endif
    }
}
