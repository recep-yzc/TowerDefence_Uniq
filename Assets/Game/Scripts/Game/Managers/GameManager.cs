using Game.Event;
using Game.Interface;
using UnityEngine;

namespace Game.Manager
{
    [ScriptOrder(-9995)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        #region private

        private IGameEventSystem gameEventSystem = new GameEventSystem();

        #endregion

        private void Awake()
        {
            #region singleton

            if (!Instance)
            {
                DontDestroyOnLoad(this);
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            #endregion
        }

        private void Start()
        {
            gameEventSystem.Publish(GameEvents.GameInit);
        }
    }
}