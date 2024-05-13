namespace GXPEngine.SceneManager
{
    internal abstract class Scene : GameObject
    {
        public Scene() { }

        /// <summary>Is called after the update cycle is finished</summary>
        public abstract void UpdateObjects();

        /// <summary>Is called when the scene is loaded</summary>
        public abstract void OnLoad();

        /// <summary>Is called when the scene is unloaded</summary>
        public abstract void OnUnload();
    }
}
