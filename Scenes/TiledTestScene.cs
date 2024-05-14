using GXPEngine.Physics;
using GXPEngine.SceneManager;
using TiledMapParser;

namespace GXPEngine.Scenes
{
    internal class TiledTestScene : Scene
    {
        Player player;

        public TiledTestScene() {  }

        public override void OnLoad()
        {
            Game.main.OnBeforeStep += PhysicsManager.Step;
            
            LoadLevel();
            //ColliderLoader.InstantiateColliders();
            PhysicsManager.PrintColliders();
        }

        public override void OnUnload() { Game.main.OnBeforeStep -= PhysicsManager.Step; }

        public override void UpdateObjects() { }

        private void LoadLevel()
        {
            TiledLoader loader = new TiledLoader(GameData.TiledSceneMap, defaultOriginX: 0, defaultOriginY: 0);
            CustomObjectLoader.Initialize(loader);
            loader.LoadTileLayers(0);
            loader.autoInstance = true;
            loader.AddManualType("WindCurrent");
            loader.AddManualType("Player");
            loader.LoadObjectGroups(0);
            CustomObjectLoader.Stop(loader);
        }
    }
}
