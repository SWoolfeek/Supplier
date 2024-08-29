

namespace CoreSystems
{
    using UnityEngine;
    using StoreEngine;

    public class GameManager : MonoBehaviour
    {
        [Header("Core")] [SerializeField] private TickManager tickManager;

        [Header("Store Engine")] 
        
        [SerializeField] private StoreManager storeManager;
        

        // Start is called before the first frame update
        void Start()
        {
            storeManager.StartStore();
            tickManager.StartTick();
        }
    }
}
