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
        [SerializeField] private StoreEventManager eventManager;
        [SerializeField] private StoreParameters storeParameters;
        
        [Header("Elements")]
        [SerializeField] private RawImage icon;
        [SerializeField] private TMP_Text productName;
        [SerializeField] private TMP_Text amount;
        [SerializeField] private TMP_Text productionPerTick;
        [SerializeField] private Slider slider;
        [SerializeField] private Slider realSlider;
        [SerializeField] private TMP_Text multiplierValue;
        
        [Header("Elements")]
        [SerializeField] private Color surplusColor;
        [SerializeField] private Color deficitColor;
        private Color _regularColor;

        private int _beforeChangesSliderValue;
        private int _currentSliderValue;
        private bool _changes;
        private bool _initialized;

        public void Initialize()
        {
            _regularColor = productionPerTick.color;
            
            eventManager.OnChangesAccepted.AddListener(AcceptChanges);
            eventManager.OnUpdateUiValues.AddListener(UpdateUiValues);
            
            icon.texture = product.icon;
            productName.text = product.productName;
            amount.text = product.amount.ToString();
            productionPerTick.text = (product.outputPerTick * product.outputMultiplier / 100).ToString();
            
            if (product.outputMultiplierRecalculation)
            {
                slider.value = product.targetOutputMultiplier;
            }
            else
            {
                slider.value = product.outputMultiplier;;
            }
            
            realSlider.value = product.outputMultiplier;
            multiplierValue.text = product.outputMultiplier + "%";
            
            _currentSliderValue = (int)slider.value;
            _beforeChangesSliderValue = _currentSliderValue;
            _initialized = true;
        }

        public void MoveSlider()
        {
            if (_initialized)
            {
                int realValue = (int)slider.value;
            
                if (realValue - _currentSliderValue > manager.tempFreeProduction)
                {
                    slider.value = _currentSliderValue;
                }
                else if (slider.value != _currentSliderValue)
                {
                    if (!_changes)
                    {
                        _beforeChangesSliderValue = _currentSliderValue;
                    }
                
                    Debug.Log("Slider name - " + name + " Current - " + _currentSliderValue + " Real - " + realValue);
                    manager.ChangeProduction(_currentSliderValue - realValue);
                    _currentSliderValue = realValue;
                    multiplierValue.text = _currentSliderValue + "%";
                    _changes = true;
                    UpdateUiValues();
                }
            }
        }

        private void UpdateUiValues()
        {
            amount.text = product.amount.ToString();
            multiplierValue.text = _currentSliderValue + "%";
            productionPerTick.text = (product.outputPerTick * _currentSliderValue / 100).ToString();
            realSlider.value = product.outputMultiplier;

            ColorizeData();
        }

        private void ColorizeData()
        {
            if (_currentSliderValue < _beforeChangesSliderValue)
            {
                multiplierValue.color = deficitColor;
                productionPerTick.color = deficitColor;
            }
            else if (_currentSliderValue > _beforeChangesSliderValue)
            {
                multiplierValue.color = surplusColor;
                productionPerTick.color = surplusColor;
            }
            else
            {
                multiplierValue.color = _regularColor;
                productionPerTick.color = _regularColor;
            }
        }

        private void AcceptChanges(bool state)
        {
            if (_changes)
            {
                _changes = false;
                
                if (state)
                {
                    _beforeChangesSliderValue = _currentSliderValue;
                    product.StartProductionRecalculation(_currentSliderValue, storeParameters.productionRecoveryTime,storeParameters.minProductionRecoveryTime);
                    manager.ChangedOutputMultiplier(product);
                }
                else
                {
                    _initialized = false;
                    _currentSliderValue = _beforeChangesSliderValue;
                    slider.value = _beforeChangesSliderValue;
                    _initialized = true;
                }
                
                UpdateUiValues();
            }
        }
        
#if UNITY_EDITOR
        public void EditorInstantiate(Product inputProduct, StoreEventManager inputEventManager, StoreProduction inputManager)
        {
            manager = inputManager;
            eventManager = inputEventManager;
            product = inputProduct;
            name = inputProduct.productName;
            Initialize();
        }
#endif
        
        private void OnDisable()
        {
            eventManager.OnChangesAccepted.RemoveListener(AcceptChanges);
            eventManager.OnUpdateUiValues.RemoveListener(UpdateUiValues);
        }
        
    }
}
