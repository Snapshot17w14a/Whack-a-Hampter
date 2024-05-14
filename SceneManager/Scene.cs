namespace GXPEngine.SceneManager
{
    internal abstract class Scene : GameObject
    {
        public Scene() { }

        /// <summary>Is called after the update cycle is finished. Useful for updating and drawing UI elements that need to be redrawn per frame.</summary>
        public abstract void UpdateObjects();

        /// <summary>Is called when the scene is loaded. Use this to initialize objects or play start animations.</summary>
        public abstract void OnLoad();

        /// <summary>Is called when the scene is unloaded. Useful for unsubscribing from events to allow garbage collection.</summary>
        public abstract void OnUnload();
    }
}
