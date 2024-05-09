using GXPEngine.SceneManager;
using TiledMapParser;

namespace GXPEngine.Scenes
{
    internal class TiledTestScene : Scene
    {
        public TiledTestScene() {  }

        private void LoadLevel()
        {
            TiledLoader loader = new TiledLoader("test.tmx");
            loader.LoadTileLayers(0);
        }
    }
}
