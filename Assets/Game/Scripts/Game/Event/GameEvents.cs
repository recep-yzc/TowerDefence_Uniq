namespace Game.Event
{
    public class GameEvents
    {
        #region game

        public const string GameInit = nameof(GameInit);

        public const string RestartWave = nameof(RestartWave);
        public const string CreateWave = nameof(CreateWave);
        public const string InitWave = nameof(InitWave);
        public const string FinishWave = nameof(FinishWave);
        public const string BtnClickPlay = nameof(BtnClickPlay);

        #endregion

        #region Player

        public const string SendPlayerComponentActor = nameof(SendPlayerComponentActor);
        public const string SendPlayerJoystickValue = nameof(SendPlayerJoystickValue);
        public const string SendCameraFollowTransform = nameof(SendCameraFollowTransform);

        #endregion

        #region Ui

        public const string UpdateMoney = nameof(UpdateMoney);
        public const string UpdateWaveLevel = nameof(UpdateWaveLevel);

        #endregion

        #region Merge

        public const string ClickDownSlot = nameof(ClickDownSlot);
        public const string ClickUpSlot = nameof(ClickUpSlot);

        public const string UpdateTower = nameof(UpdateTower);

        #endregion

        #region Tower

        public const string SendTowerComponentActor = nameof(SendTowerComponentActor);

        public const string MergeMenu = nameof(MergeMenu);

        #endregion

        #region Castle

        public const string CastleDead = nameof(CastleDead);
        public const string UpdateCastleHealth = nameof(UpdateCastleHealth);

        #endregion

        #region Enemy

        public const string SendEnemyTargetTransform = nameof(SendEnemyTargetTransform);
        public const string SendEnemySpawnTransform = nameof(SendEnemySpawnTransform);
        public const string CreateEnemy = nameof(CreateEnemy);

        #endregion
    }
}