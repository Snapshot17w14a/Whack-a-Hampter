using System.Collections.Generic;
using System;

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
            if(!scenes.TryGetValue(sceneName, out Scene scene)) throw new Exception($"Scene with name {sceneName} not found.");
            CurrentScene?.OnUnload();
            Load(scene);
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
            }
            CurrentScene = scene;
            CurrentScene?.OnLoad();
            game.AddChild(CurrentScene);
        }

        /// <summary>Creates an instance of a scene with the given name and returns it.</summary>
        public static Scene CreateScene(Type toInstantiate, params object[] args)
        {
            return (Scene)Activator.CreateInstance(toInstantiate, args: args);
        }

        /// <summary>Load the scene named "InitialScene"</summary>
        public static void LoadInitialScene() => Load(scenes["InitialScene"]);
    }
}
