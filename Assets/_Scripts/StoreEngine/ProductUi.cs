namespace StoreEngine
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class ProductUi : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private Product product;
        [SerializeField] private StoreProduction manager;
        
        [Header("Elements")]
        [SerializeField] private RawImage icon;
        [SerializeField] private TMP_Text productName;
        [SerializeField] private TMP_Text amount;
        [SerializeField] private TMP_Text productionPerTick;
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text multiplierValue;
        [SerializeField] private GameObject acceptChangesButton;

        private int _currentSliderValue;
        private bool _changes;

        public void Initialize()
        {
            icon.texture = product.icon;
            productName.text = product.productName;
            amount.text = product.amount.ToString();
            productionPerTick.text = (product.outputPerTick * product.outputMultiplier / 100).ToString();
            slider.value = product.outputMultiplier;
            multiplierValue.text = product.outputMultiplier + "%";
            
            _currentSliderValue = (int)slider.value;
        }

        public void MoveSlider()
        {
            if ((int)slider.value - _currentSliderValue > manager.tempFreeProduction)
            {
                slider.value = _currentSliderValue;
            }
            else if (slider.value != _currentSliderValue)
            {
                manager.tempFreeProduction -= (int)slider.value - _currentSliderValue;
                _changes = true;
                acceptChangesButton.SetActive(true);
            }
        }

        public void AcceptChanges()
        {
            _changes = false;
            acceptChangesButton.SetActive(false);

            product.outputMultiplier = (int)slider.value;
            productionPerTick.text = (product.outputPerTick * product.outputMultiplier / 100).ToString();
            
            manager.ChangedOutputMultiplier(product);
        }
        
#if UNITY_EDITOR
        public void EditorInstantiate(Product inputProduct, StoreProduction inputManager)
        {
            manager = inputManager;
            product = inputProduct;
            name = inputProduct.productName;
            Initialize();
        }
#endif
        
    }
}
