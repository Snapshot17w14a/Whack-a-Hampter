using System.Collections.Generic;
using GXPEngine.Scenes;

namespace GXPEngine.SceneManager
{
    internal static class SceneManager
    {
        /// <summary>The dictionary that holds all the scenes.</summary>
        public static IDictionary<string, Scene> scenes = new Dictionary<string, Scene>();

        /// <summary>The current scene that is loaded</summary>
        public static Scene CurrentScene { get; private set; }

        /// <summary>Load the scene with the given name</summary>
        /// <param name="sceneName">The name of the scene that was given during instantiation</param>
        public static void LoadScene(string sceneName)
        {
            CurrentScene?.OnUnload();
            Load(scenes[sceneName]);
        }

        /// <summary>Adds the given scene to the dictionary with the given name</summary>
        /// <param name="sceneName">The name of the scene, this will be used to access the scene</param>
        /// <param name="scene">The scene to add</param>
        public static void AddScene(string sceneName, Scene scene) => scenes.Add(sceneName, scene);

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

        /// <summary>Creates an instance of a scene with the given name and returns it.</summary>
        public static Scene CreateScene(string sceneName)
        {
            Scene createdScene;
            switch(sceneName)
            {
                case "GameScene":
                    createdScene = new GameScene();
                    break;
                case "TiledScene":
                    createdScene = new TiledTestScene();
                    break;
                default:
                    throw new System.Exception("Scene name incorrect or does not exist");
            }
            return createdScene;
        }

        /// <summary>Load the scene named "InitialScene"</summary>
        public static void LoadInitialScene() => Load(scenes["InitialScene"]);
    }
}
