using System.Collections.Generic;
using GXPEngine.Physics.Colliders;
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
        public static readonly float PlayerIsZeroThreshold = 0.1f; // Threshold for checking if a vector is zero

        public static readonly int PlayerSpawnYOffset = -200; // The initial y offset of the player, this is needed for the start animation
        public static readonly int PlayerStartAnimSpeed = 3; // The speed of the start animation in units per frame

        public static readonly float FireSpeed = 0.1f; // The speed of the fire animation in units per frame

        public static Player ActivePlayer;

        //Scene data
        public static readonly float UIScale = 0.5f; // The scale of the UI
        public static readonly float FadeSpeed = 4f; // The speed of the fade in/out effect
        public static readonly float TransitionWaitTime = 2f; // The time to wait before transitioning to the next scene in seconds

        //Dynamic scene data - dont change this
        public static string NextScene = "Level1"; // The next scene to load

        //Level data
        public static readonly int[] MaxHits = { 4, 6, 15 }; // The maximum amount of hits for each level

        //Dynamic level data - dont change this
        public static uint[,] TileValues;
        public static Dictionary<uint, float> TileSlowdownValues = new Dictionary<uint, float>();

        //General data
        public static readonly string SceneToLoad = "Title"; // The scene to load when the game starts

        public static readonly bool ShowMouse = false; // Show the mouse in the scene
        public static readonly bool ShowColliders = false; // Show colliders in the scene
        public static readonly uint ColliderColor = 0xFFFF0000; // Color of the colliders

        public static readonly int TileHeight = 64;
        public static readonly int TileWidth = 64;

        public static readonly float BushRequiredVelocity = 20f; // The required velocity to pass a bush

        public static readonly GameSoundHandler SoundHandler = new GameSoundHandler();

        //Physics objects
        //public static readonly List<LineSegmentCollider> WindmillBlades = new List<LineSegmentCollider>();
        public static readonly float windmillSpinSpeed = 90f;
        public static readonly float windmillForceMagnitude = 0.2f;

        public static void Initialize() //This method is called once when the game starts, use it to create scenes and add them to the SceneManager
        {
            SceneManager.AddScene("Title", SceneManager.CreateScene(typeof(TitleScene)));
            SceneManager.AddScene("Level1", SceneManager.CreateScene(typeof(TiledScene), "Maps/Level1.tmx"));
            SceneManager.AddScene("Level2", SceneManager.CreateScene(typeof(TiledScene), "Maps/Level2.tmx"));
            SceneManager.AddScene("Level3", SceneManager.CreateScene(typeof(TiledScene), "Maps/Level3.tmx", "Title"));
            SceneManager.AddScene("TransitionLevel1", SceneManager.CreateScene(typeof(TransitionScene), "Level2"));
            SceneManager.AddScene("TransitionLevel2", SceneManager.CreateScene(typeof(TransitionScene), "Level3"));
            SceneManager.AddScene("TransitionLevel3", SceneManager.CreateScene(typeof(TransitionScene), "Title"));
            SceneManager.PrintScenes();
        }

        public static void ResetLevelData()
        {
            TileValues = null;
            TileSlowdownValues.Clear();
        }
    }
}