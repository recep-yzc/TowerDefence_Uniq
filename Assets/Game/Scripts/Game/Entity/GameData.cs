using System.Collections.Generic;

namespace Game.Entity
{
    public class GameData : Entity<GameData>
    {
        #region Variables

        public float Money = 2500;
        public int Wave = 0;

        public List<TowerData> TowerDatas = new()
        {
            new(),
            new(),
            new(),
            new()
        };

        #endregion

        protected override bool Init() => true;
    }
}