using Game.Event;
using Game.Interface;
using Game.Scriptable;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerComponentActor : MonoBehaviour
    {
        [Header("Properties")] 
        public PlayerProperty Property;

        [Header("References")] 
        public PlayerMovementActor PlayerMovementActor;
        public PlayerCollisionActor PlayerCollisionActor;
        public PlayerStackActor PlayerStackActor;

        public IGameEventSystem GameEventSystem = new GameEventSystem();
        public MovementStates MovementState { get; set; }

        private void Start()
        {
            CreateData();
            GameEventSystem.Publish(GameEvents.SendPlayerComponentActor, this);
        }

        private void CreateData()
        {
            Property = Property.Clone() as PlayerProperty;
        }
    }
}