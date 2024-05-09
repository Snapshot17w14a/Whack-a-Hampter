using GXPEngine.SceneManager;
using TiledMapParser;

namespace GXPEngine.Scenes
{
    internal class TiledTestScene : Scene
    {
        public TiledTestScene() {  }

        public override void OnLoad() => LoadLevel();

        public override void OnUnload() { }

        public override void UpdateObjects() { }

        private void LoadLevel()
        {
            TiledLoader loader = new TiledLoader("Maps/exporttest.tmx");
            loader.LoadTileLayers(0);
        }
    }
}
