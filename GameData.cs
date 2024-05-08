namespace GXPEngine
{
    internal static class GameData
    {
        public static readonly Vec2 PlayerSpawnPosition = new Vec2(Game.main.width / 2, Game.main.height / 2); // The spawn position of the player
        public static readonly float PlayerMaxVelocity = 10f; // The maximum velocity the player can have
        public static readonly int PlayerRadius = 32; // The radius of the player

        public static readonly int GlobalArrowSize = 30; // The size of the arrows
    }
}
