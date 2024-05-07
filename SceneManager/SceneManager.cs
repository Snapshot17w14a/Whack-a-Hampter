using System.Collections.Generic;

namespace GXPEngine.SceneManager
{
    internal static class SceneManager
    {
        public static IDictionary<string, Scene> scenes = new Dictionary<string, Scene>();
        private static Scene _sceneToLoad;

        public static Scene CurrentScene { get; private set; }

        /// <summary>Load the scene with the given name</summary>
        /// <param name="sceneName">The name of the scene that was given during instantiation</param>
        public static void LoadScene(string sceneName)
        {
            if (_sceneToLoad != null) _sceneToLoad = null;
            CurrentScene?.OnUnload();
            Load(scenes[sceneName]);
        }

        /// <summary>Create a new scene with the given name and return it</summary>
        public static Scene CreateScene(string sceneName)
        {
            var create = new Scene();
            scenes.Add(sceneName, create);
            return create;
        }

        /// <summary>Loads the given scene</summary>
        private static void Load(Scene scene)
        {
            var game = Game.main;
            if (CurrentScene != null)
            {
                game.RemoveChild(CurrentScene);
                game.OnAfterStep -= CurrentScene.UpdateObjects;
            }
            CurrentScene = scene;
            CurrentScene?.OnLoad();
            game.AddChild(CurrentScene);
            game.OnAfterStep += CurrentScene.UpdateObjects;
        }

        /// <summary>Load the scene named "InitialScene"</summary>
        public static void LoadInitialScene() => Load(scenes["InitialScene"]);

    }
}
