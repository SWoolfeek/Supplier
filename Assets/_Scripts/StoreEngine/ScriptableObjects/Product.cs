using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace StoreEngine
{
    public class Product : ScriptableObject
    {
        // Product description.
        [BoxGroup("Properties")]
        [HideLabel, PreviewField(60)]
        public Texture icon;
        
        [BoxGroup("Properties")] [DisableInPlayMode]
        public string productName;
        [BoxGroup("Properties")] [TextArea(4, 14)]
        public string Description;
        [BoxGroup("Properties")] [DisableInPlayMode]
        public string description;
        [BoxGroup("Properties")] [ReadOnly]
        public ProductType productType;
        [BoxGroup("Properties")] [Range(0,10)] [DisableInPlayMode]
        public int happiness;
        
        // Store data.
        [BoxGroup("Store data")]
        [MinValue(0)]
        public int amount;
        [BoxGroup("Store data")]
        [MinValue(0f)]
        public float outputPerTick;
        [BoxGroup("Store data")]
        [Range(0,200)] [ReadOnly]
        public int outputMultiplier = 100; // real.
        [BoxGroup("Store data")]
        [MinValue(0f)]
        public float space;

        // Data for calculation;
        [SerializeField] [HideInInspector]
        public float outputResidual;
        [HideInInspector]
        public int startOutputMultiplier;
        [HideInInspector]
        public int targetOutputMultiplier;
        [HideInInspector]
        public int ticksDone;
        [HideInInspector]
        public int ticksTarget;
        [HideInInspector]
        public bool outputMultiplierRecalculation;

        private int _perTick;
        private int _minTick;

        public void LoadData(int inputAmount, float inputOutputPerTick, int inputOutputMultiplier, float inputOutputResidual, bool inputOutputMultiplierRecalculation, int inputStartOutputMultiplier = 0, int inputTargetOutputMultiplier = 0, int inputTicksDone = 0, int inputTicksTarget = 0, int inputMinTick = 0)
        {
            amount = inputAmount;
            outputPerTick = inputOutputPerTick;
            outputMultiplier = inputOutputMultiplier;
            outputResidual = inputOutputResidual;
            
            if (inputOutputMultiplierRecalculation)
            {
                outputMultiplierRecalculation = inputOutputMultiplierRecalculation;
                startOutputMultiplier = inputStartOutputMultiplier;
                targetOutputMultiplier = inputTargetOutputMultiplier;
                ticksDone = inputTicksDone;
                ticksTarget = inputTicksTarget;
                CalculatePerTick(inputMinTick);
            }
        }
        
        public void TickProduction()
        {
            if (outputPerTick * outputMultiplier + outputResidual > 1)
            {
                int newProduct = (int)(outputPerTick * outputMultiplier + outputResidual);
                amount += newProduct;
                outputResidual = (outputPerTick * outputMultiplier + outputResidual) - newProduct;
            }
            else
            {
                outputResidual += outputPerTick * outputMultiplier;
            }

            if (outputMultiplierRecalculation)
            {
                RecalculateProduction();
            }
        }

        public void StartProductionRecalculation(int target, int ticksMax, int minTick)
        {
            outputMultiplierRecalculation = true;
            targetOutputMultiplier = target;
            startOutputMultiplier = outputMultiplier;
            ticksTarget = ticksMax;
            ticksDone = 0;
            CalculatePerTick(minTick);
        }

        private void CalculatePerTick(int minTick)
        {
            int absDifference = Mathf.Abs(targetOutputMultiplier - startOutputMultiplier);
            int difference = targetOutputMultiplier - startOutputMultiplier;
            _minTick = minTick;
            
            if (absDifference < minTick)
            {
                ticksTarget = minTick;
                _perTick = difference;
            }
            else if (absDifference < ticksTarget)
            {
                ticksTarget = difference;
                _perTick = (int)(1 * Mathf.Sign(difference));
            }
            else
            {
                _perTick = difference / ticksTarget;
            }
        }

        private void RecalculateProduction()
        {
            if(ticksTarget - ticksDone <= _minTick)
            {
                ticksDone++;
                if (ticksTarget == ticksDone)
                {
                    outputMultiplier = targetOutputMultiplier;
                    outputMultiplierRecalculation = false;
                }
            }
            else
            {
                outputMultiplier += _perTick;
                ticksDone++;
            }
        }
    }

    public enum ProductType
    {
        Food = 0,
        Weapon = 1,
        Сlothes = 2,
        Transport = 3,
        Materials = 4,
        Others = 10
    }
}

