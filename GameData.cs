﻿namespace GXPEngine
{
    internal static class GameData
    {
        //Player data
        public static readonly uint ArrowStartColor = 0xFF00FF00; // color of the arrow at the start
        public static readonly uint ArrowMedianColor = 0xFFFFFF00; // color of the arrow in the middle
        public static readonly uint ArrowEndColor = 0xFFFF0000; // color of the arrow at the end

        public static readonly float PlayerMaxHitStrength = 100f; // Maximum hit strength
        public static readonly float PlayerMouseMaxStrengthThreshold = 300f; // Maximum strength when the mouse is at this distance
        public static readonly float PlayerIsZeroThreshold = 0.01f; // Threshold for checking if a vector is zero

        //General data
        public static readonly string SceneToLoad = "TiledScene"; // The scene to load when the game starts
        public static readonly string TiledSceneMap = "Maps/exporttest.tmx"; // The map to load in the TiledScene

        public static readonly bool ShowColliders = false; // Show colliders in the scene
        public static readonly uint ColliderColor = 0xFFFF0000; // Color of the colliders

        public static Player ActivePlayer;
    }
}
