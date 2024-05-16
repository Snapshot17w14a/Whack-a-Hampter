using System.Collections.Generic;
using GXPEngine.SceneManagement;
using GXPEngine.Scenes;

namespace GXPEngine
{
    internal static class GameData
    {
        //Player data
        public static readonly uint ArrowStartColor = 0xFF00FF00; // color of the arrow at the start
        public static readonly uint ArrowMedianColor = 0xFFFFFF00; // color of the arrow in the middle
        public static readonly uint ArrowEndColor = 0xFFFF0000; // color of the arrow at the end

        public static readonly float PlayerMaxHitStrength = 50f; // Maximum hit strength
        public static readonly float PlayerMouseMaxStrengthThreshold = 300f; // Maximum strength when the mouse is at this distance
        public static readonly float PlayerIsZeroThreshold = 1.0f; // Threshold for checking if a vector is zero

        public static readonly int PlayerSpawnYOffset = -200; // The initial y offset of the player, this is needed for the start animation
        public static readonly int PlayerStartAnimSpeed = 3; // The speed of the start animation in units per frame

        public static readonly float FireSpeed = 0.1f; // The speed of the fire animation in units per frame

        public static Player ActivePlayer;

        //Scene data
        public static readonly float UIScale = 0.5f; // The scale of the UI

        //Level data
        public static readonly int[] MaxHits = { 15, 0, 0 }; // The maximum amount of hits for each level

        //Dynamic level data - dont change this
        public static uint[,] TileValues;
        public static Dictionary<uint, float> TileSlowdownValues = new Dictionary<uint, float>();

        //General data
        public static readonly string SceneToLoad = "Level1"; // The scene to load when the game starts

        public static readonly bool ShowColliders = true; // Show colliders in the scene
        public static readonly uint ColliderColor = 0xFFFF0000; // Color of the colliders
        public static readonly bool ShowMouse = true; // Show the mouse in the scene

        public static readonly int TileHeight = 64;
        public static readonly int TileWidth = 64;

        public static readonly GameSoundHandler SoundHandler = new GameSoundHandler();

        //Physics objects

        public static readonly float windmillSpinSpeed = 90f;
        public static readonly float windmillForceMagnitude = 1f;

        public static void Initialize() //This method is called once when the game starts, use it to create scenes and add them to the SceneManager
        {
            SceneManager.AddScene("Level1", SceneManager.CreateScene(typeof(TiledScene), "Maps/Level1.tmx"));
            SceneManager.AddScene("Level2", SceneManager.CreateScene(typeof(TiledScene), "Maps/Level1.tmx"));
            SceneManager.AddScene("Level3", SceneManager.CreateScene(typeof(TiledScene), "Maps/Level1.tmx"));
            SceneManager.PrintScenes();
        }

        public static void ResetLevelData()
        {
            TileValues = null;
            TileSlowdownValues.Clear();
        }
    }
}
