namespace PlanerEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class CouriersPlanerManager : MonoBehaviour
    {
        [SerializeField] private GameObject courierEditWindow;
        [SerializeField] private TMP_Text orderSizeText;
        [SerializeField] private TMP_Text daysText;
        [SerializeField] private TMP_Text couriersAmountText;

        private PlanerManager _manager;
        private int _couriers;
        private float _length;
        private float _size;
        private int _days;

        private readonly float _negativeMultiplier = 4;
        
        // Need to read from global parameter!!!
        private readonly int _maxCouriers = 100000; 

        public void Initialization(PlanerManager manager)
        {
            _manager = manager;
        }

        public void ModifyCouriers(float size, float length, int inputCouriers = 0)
        {
            _length = length;
            _size = size;
            orderSizeText.text = size.ToString();
            _couriers = CountCouriers(size,inputCouriers, _maxCouriers);
            couriersAmountText.text = _couriers.ToString();
            _days = CalculateDays(_length, _size, _couriers);
            daysText.text = _days.ToString();
            
            courierEditWindow.SetActive(true);
        }

        public void ChangeCouriersAmount(int value)
        {
            if (value > 0)
            {
                _couriers = Mathf.Min(_maxCouriers, _couriers + value);
            }
            else
            {
                _couriers = Mathf.Max(1, _couriers + value);
            }
            
            couriersAmountText.text = _couriers.ToString();
            _days = CalculateDays(_length, _size, _couriers);
            daysText.text = _days.ToString();
        }

        public void ApplyChanges()
        {
            _manager.AddCouriers(_couriers, _days);
            courierEditWindow.SetActive(false);
        }

        private int CountCouriers(float size, int inputCouriers, int couriersExist)
        {
            if (inputCouriers > 0)
            {
                return inputCouriers;
            }

            return Mathf.Min(couriersExist, (int)Mathf.Ceil(size / _manager.gameParameters.courierAcceptableWeight));

        }

        private int CalculateDays(float length, float size, int couriers)
        {
            float couriersStrength = couriers * _manager.gameParameters.courierAcceptableWeight;
            float delta = Mathf.Abs(couriersStrength - size) / size;
            
            if (size < couriersStrength)
            {
                if (delta < 0.02f)
                {
                    return (int)Mathf.Ceil(length);
                }

                return (int)Mathf.Ceil(length * (1 - Mathf.Min(0.25f, delta)));
            }
            
            if (delta < 0.02f)
            {
                return (int)Mathf.Ceil(length);
            }
            
            return (int)Mathf.Ceil(length * (1 + (_negativeMultiplier * delta)));
        }
    }
}
