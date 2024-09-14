namespace PlanerEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using TMPro;
    using UnityEngine.UI;


    public class ProductInOrder : MonoBehaviour
    {
        private NewOrderManager _manager;
        
        [ReadOnly]
        public StoreEngine.Product product;

        [Header("Elements")] 
        [SerializeField] private RawImage icon;
        [SerializeField] private TMP_Text productName;
        [SerializeField] private TMP_Text totalAmount;
        [SerializeField] private TMP_Text chosenAmount;
        [SerializeField] private TMP_Text calculatedSize;
        [SerializeField] private Slider slider;
        [SerializeField] private GameObject approvalButton;
        [SerializeField] private GameObject rejectButton;

        private bool _approvalActive;
        private Product _orderProduct;
        
        
        public void Initialize(NewOrderManager newOrderManager)
        {
            _manager = newOrderManager;
            UpdateValues();
        }

        private void UpdateValues()
        {
            icon.texture = product.icon;
            productName.text = product.productName;
            totalAmount.text = product.amount.ToString();
            slider.maxValue = product.amount;
        }

        public void RestartWindow()
        {
            UpdateValues();
            
            _orderProduct = new Product();
            _orderProduct.productName = product.productName;

            slider.interactable = true;
            slider.value = 0;
            rejectButton.SetActive(false);
            ActivateApproval(false);
        }

        public void OnSliderChanged()
        {
            chosenAmount.text = slider.value.ToString();
            calculatedSize.text = (product.space * slider.value).ToString();

            if (slider.value > 0)
            {
                if (!_approvalActive)
                {
                    ActivateApproval(true);
                }
            }
            else
            {
                ActivateApproval(false);
            }
        }

        public void Approve()
        {
            ActivateApproval(false);
            slider.interactable = false;
            rejectButton.SetActive(true);

            SetProductValue();
            _manager.AddProduct(_orderProduct);
        }

        public void Reject()
        {
            rejectButton.SetActive(false);
            
            _manager.RemoveProduct(_orderProduct);
            
            ActivateApproval(true);
            slider.interactable = true;
        }

        private void ActivateApproval(bool state)
        {
            approvalButton.SetActive(state);
            _approvalActive = state;
        }

        private void SetProductValue()
        {
            _orderProduct.amount = (int)slider.value;
            _orderProduct.size = product.space * _orderProduct.amount;
        }
        
    }
}
