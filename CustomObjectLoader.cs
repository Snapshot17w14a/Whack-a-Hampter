using TiledMapParser;
using GXPEngine.Physics;
using GXPEngine.Physics.PhysicsObjects;
using GXPEngine.SceneManagement;

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
                    wind.width = (int)obj.Width;
                    wind.height = (int)obj.Height;
                    PhysicsObjectManager.AddWindCurrent(wind);
                    SceneManager.CurrentScene.AddChild(wind);
                    break;

                case "Windmill":
                    Windmill windmill = new Windmill();
                    windmill.SetXY(obj.X, obj.Y);
                    windmill.width = (int)obj.Width;
                    windmill.height = (int)obj.Height;
                    SceneManager.CurrentScene.AddChild(windmill);
                    break;

                case "Player":
                    Player player = new Player(obj.X, obj.Y + GameData.PlayerSpawnYOffset);
                    player.Collider.IsActive = false;
                    GameData.ActivePlayer = player;
                    SceneManager.CurrentScene.AddChild(player);
                    break;

                case "Fire":
                    Fire fire = new Fire();
                    fire.SetXY(obj.X, obj.Y);
                    fire.width = (int)obj.Width;
                    fire.height = (int)obj.Height;
                    PhysicsObjectManager.AddFire(fire);
                    SceneManager.CurrentScene.AddChild(fire);
                    break;
            }
        }
    }
}
