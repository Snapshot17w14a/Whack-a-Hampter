namespace GXPEngine.SceneManagement
{
    internal abstract class Scene : GameObject
    {
        public Scene() { }

        /// <summary>Is called when the scene is loaded. Use this to initialize objects or play start animations.</summary>
        public abstract void OnLoad();

        /// <summary>Is called when the scene is unloaded. Useful for unsubscribing from events to allow garbage collection.</summary>
        public abstract void OnUnload();
    }
}
