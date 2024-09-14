using TMPro;

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
        private OrderManager _manager;
        
        [SerializeField] private StoreEngine.AllProducts allProducts;
        [SerializeField] private List<ProductInOrder> productsInOrder;

#if UNITY_EDITOR
        [Header("Editor Only")]
        [SerializeField] private GameObject prefabUiProductInOrder;
        [SerializeField] private Transform parentForUiProductsInOrder;
#endif

        [Header("Ui elements")] 
        [SerializeField] private TMP_Text orderSizeText;
        [SerializeField] private GameObject applyButton;

        private Dictionary<string, int> _indexesInOrder;
        private float _orderSize;
        private Order _order;

        public void Initialization(OrderManager manager)
        {
            _manager = manager;
            InitializeAllProductsInOrder();
        }

        public void ApplyOrder()
        {
            _manager.OrderCompleted(_order);
        }

        private void InitializeAllProductsInOrder()
        {
            foreach (ProductInOrder productInOrder in productsInOrder)
            {
                productInOrder.Initialize(this);
            }
        }
        
        public void UpdateAllProductsInOrder()
        {
            foreach (ProductInOrder productInOrder in productsInOrder)
            {
                productInOrder.UpdateValues();
            }
            NewOrder();
        }
        
        
        private void NewOrder()
        {
            _order = new Order();
            _orderSize = 0;
            orderSizeText.text = _orderSize.ToString();
            _indexesInOrder = new Dictionary<string, int>();
        }

        public void AddProduct(Product product)
        {
            _order.products.Add(product);
            _indexesInOrder.Add(product.productName, _order.products.Count - 1);
            ChangeTotalSize(product.size);
            applyButton.SetActive(true);
        }

        public void RemoveProduct(Product product)
        {
            if (_indexesInOrder.ContainsKey(product.productName))
            {
                int productIndex = _indexesInOrder[product.productName];
                
                if (productIndex < _order.products.Count - 1)
                {
                    for (int i = productIndex + 1; i < _order.products.Count; i++)
                    {
                        _indexesInOrder[_order.products[i].productName] = i - 1;
                    }
                }

                ChangeTotalSize(-product.size);
                _order.products.RemoveAt(productIndex);
                _indexesInOrder.Remove(product.productName);

                if (_order.products.Count > 0)
                {
                    applyButton.SetActive(true);
                }
                else
                {
                    applyButton.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning("Such product is not present in Order!");
            }
        }

        private void ChangeTotalSize(float input)
        {
            _orderSize += input;
            _order.totalSize = _orderSize;
            orderSizeText.text = _orderSize.ToString();
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
                productInOrder.GetComponent<ProductInOrder>().Initialize(this);
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
