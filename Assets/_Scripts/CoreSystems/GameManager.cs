using PlanerEngine;
using RoadEngine;

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
        [SerializeField] private RoadManager roadManager;
        [SerializeField] private PlanerManager planerManager;
        

        // Start is called before the first frame update
        void Start()
        {
            gameUiManager.Initialization();
            storeManager.StartStore();
            roadManager.StartRoad();
            planerManager.StartPlaner();
            tickManager.StartTick();
        }
    }
}
