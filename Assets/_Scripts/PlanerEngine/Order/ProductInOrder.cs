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
        [ReadOnly]
        public StoreEngine.Product product;

        [Header("Elements")] 
        [SerializeField] private RawImage icon;
        [SerializeField] private TMP_Text productName;
        [SerializeField] private TMP_Text totalAmount;
        [SerializeField] private TMP_Text chosenAmount;
        [SerializeField] private TMP_Text calculatedWeight;
        [SerializeField] private Slider slider;
        
        public void Initialize()
        {
            icon.texture = product.icon;
            productName.text = product.productName;
            totalAmount.text = product.amount.ToString();
            slider.maxValue = product.amount;
        }
    }
}
