using Characters.Enemy;
using Characters.Tower;
using Game.Manager;
using Game.Scriptable;
using System.Linq;
using UnityEngine;

namespace Characters.TowerPlayer
{
    public class TowerCharacterComponentActor : MonoBehaviour
    {
        [Header("References")]
        public TowerCharacterAttackActor TowerCharacterAttackActor;
        public TowerCharacterRotateActor TowerCharacterRotateActor;
        public TowerComponentActor towerComponentActor;
        [SerializeField] private Transform characterTransform;

        public TowerCharacterPropertyData TowerCharacterPropertyData => towerCharacterModelActor.TowerCharacterPropertyData;
        public EnemyComponentActor CurrentEnemyComponentActor { get; private set; }
        public TowerCharacterModelActor towerCharacterModelActor { get; private set; }

        public void SetEnemyComponentActor(EnemyComponentActor enemyComponentActor)
        {
            CurrentEnemyComponentActor = enemyComponentActor;
        }

        public void UpdateCharacter()
        {
            TowerReferenceData towerReferenceDataTemp = TowerManager.Instance.TowerReferenceData;
            TowerCharacter towerCharacterTemp = towerComponentActor.TowerData.TowerCharacters.Where(x => x.SlotIndex == 0).FirstOrDefault();

            if (towerCharacterModelActor)
            {
                Destroy(towerCharacterModelActor.gameObject);
            }

            if (towerCharacterTemp != null)
            {
                int level = towerCharacterTemp.Level;

                TowerCharacterModel towerCharacterModelTemp = towerReferenceDataTemp.TowerCharacterModels.Where(x => x.CharacterLevel == level).FirstOrDefault();
                TowerCharacterModelActor towerCharacterModelActorTemp = Instantiate(towerCharacterModelTemp.TowerCharacterModelActor, characterTransform);

                towerCharacterModelActor = towerCharacterModelActorTemp;

                TowerCharacterAttackActor.SetMuzzleTransform(towerCharacterModelActor.GetMuzzleTransform());
            }
        }
    }
}