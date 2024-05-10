using GXPEngine.Physics;
using GXPEngine.SceneManager;
using TiledMapParser;

namespace GXPEngine.Scenes
{
    internal class TiledTestScene : Scene
    {
        Player player;
        Line line;

        public TiledTestScene() {  }

        public override void OnLoad()
        {
            Game.main.OnBeforeStep += PhysicsManager.Step;
            LoadLevel();
            ColliderLoader.InstantiateColliders();
            PhysicsManager.PrintColliders();
        }

        public override void OnUnload() { Game.main.OnBeforeStep -= PhysicsManager.Step; }

        public override void UpdateObjects() { }

        private void LoadLevel()
        {
            TiledLoader loader = new TiledLoader("Maps/exporttest.tmx");
            loader.LoadTileLayers(0);
            player = new Player();
            AddChild(player);
        }
    }
}
