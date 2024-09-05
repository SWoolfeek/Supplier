using PlanerEngine;

namespace CoreSystems
{
    using UnityEngine;
    using StoreEngine;

    public class GameManager : MonoBehaviour
    {
        [Header("Core")] [SerializeField] private TickManager tickManager;

        [Header("Managers")] 
        [SerializeField] private GameUiManager gameUiManager;
        [SerializeField] private StoreManager storeManager;
        [SerializeField] private PlanerManager planerManager;
        

        // Start is called before the first frame update
        void Start()
        {
            gameUiManager.Initialization();
            storeManager.StartStore();
            planerManager.StartPlaner();
            tickManager.StartTick();
        }
    }
}
