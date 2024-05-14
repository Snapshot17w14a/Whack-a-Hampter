using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Physics;
using GXPEngine.SceneManager;
using TiledMapParser;

namespace GXPEngine.Scenes
{
    internal class TiledTestScene : Scene
    {
        Player player;
        TiledLoader tiledLoader;  // TiledLoader instance

        public TiledTestScene()
        {
            tiledLoader = new TiledLoader("Maps/exporttest.tmx");  // Ensure correct path
            tiledLoader.LoadTileLayers();  // Load all tile layers
        }

        public override void OnLoad()
        {
            Game.main.OnBeforeStep += PhysicsManager.Step;

            LoadLevel();
            ColliderLoader.InstantiateColliders();
            PhysicsManager.PrintColliders();
        }

        public override void OnUnload()
        {
            Game.main.OnBeforeStep -= PhysicsManager.Step;
        }

        public override void UpdateObjects() { }

        private void LoadLevel()
        {
            CustomObjectLoader.Initialize(tiledLoader);
            tiledLoader.autoInstance = true;
            tiledLoader.AddManualType("WindCurrent", "Player");
            tiledLoader.LoadObjectGroups(0);
            CustomObjectLoader.Stop(tiledLoader);
        }

        // Method to get tile collision property
        public string GetTileCollisionProperty(float x, float y)
        {
            int tileX = (int)(x / GameData.TileWidth);
            int tileY = (int)(y / GameData.TileHeight);
            uint[,] tiles = tiledLoader.map.Layers[1].GetTileArrayRaw(); // Assuming tile layer index 0, adjust if needed

            if (tileX >= 0 && tileX < tiles.GetLength(0) && tileY >= 0 && tileY < tiles.GetLength(1))
            {
                uint gid = tiles[tileX, tileY];
                if (gid > 0)
                {
                    // Retrieve tile properties directly from the tile array
                    TileSet tileSet = tiledLoader.map.GetTileSet((int)gid);
                    Tile tile = tileSet?.GetTile(gid);
                    if (tile != null)
                    {
                        return tile.GetStringProperty("collider", null);
                    }
                }
            }
            return null;
        }


    }
}
