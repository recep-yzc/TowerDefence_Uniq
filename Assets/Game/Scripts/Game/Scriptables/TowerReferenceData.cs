using Characters.Tower.Ui;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "TowerReferenceData", menuName = "Data/TowerReferenceData")]
    public class TowerReferenceData : ScriptableObject
    {
        [Header("Properties")] 
        public float CostPerCard = 75;

        [Header("References")] 
        public TowerSlotCardActor TowerSlotCardActorPrefab;
        public List<TowerCharacterCard> TowerCharacterCards = new();
        public List<TowerCharacterModel> TowerCharacterModels = new();
    }
}