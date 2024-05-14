using TiledMapParser;

namespace GXPEngine
{
    internal static class CustomObjectLoader
    {
        public static void Initialize(TiledLoader loader) => loader.OnObjectCreated += LoadCustomObjects;

        public static void Stop(TiledLoader loader) => loader.OnObjectCreated -= LoadCustomObjects;

        private static void LoadCustomObjects(Sprite spirte, TiledObject obj)
        {
            switch(obj.Type)
            {
                case "WindCurrent":
                    WindCurrent wind = new WindCurrent(obj.GetFloatProperty("strength", 0f), obj.GetIntProperty("angle", 0));
                    wind.SetXY(obj.X, obj.Y);
                    wind.width = (int)(obj.Width);
                    wind.height = (int)(obj.Height);
                    SceneManager.SceneManager.CurrentScene.AddChild(wind);
                    break;

                case "Player":
                    Player player = new Player(obj.X, obj.Y);
                    GameData.ActivePlayer = player;
                    SceneManager.SceneManager.CurrentScene.AddChild(player);
                    break;
            }
        }
    }
}
