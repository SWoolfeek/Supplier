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
        
        [Header("Elements")]
        [SerializeField] private RawImage icon;
        [SerializeField] private TMP_Text productName;
        [SerializeField] private TMP_Text amount;
        [SerializeField] private TMP_Text productionPerTick;
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text multiplierValue;

        public void Initialize()
        {
            icon.texture = product.icon;
            productName.text = product.productName;
            amount.text = product.amount.ToString();
            productionPerTick.text = (product.outputPerTick * product.outputMultiplier / 100).ToString();
            slider.value = product.outputMultiplier;
            multiplierValue.text = product.outputMultiplier + "%";
        }
        
#if UNITY_EDITOR
        public void EditorInstantiate(Product inputProduct)
        {
            product = inputProduct;
            name = inputProduct.productName;
            Initialize();
        }
#endif
        
    }
}
