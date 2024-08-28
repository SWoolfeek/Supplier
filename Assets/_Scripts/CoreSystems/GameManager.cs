namespace CoreSystems
{
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [Header("Core")] [SerializeField] private TickManager tickManager;

        // Start is called before the first frame update
        void Start()
        {
            tickManager.StartTick();
        }
    }
}
