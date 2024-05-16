using GXPEngine.SceneManagement;

namespace GXPEngine.Scenes
{
    internal class TransitionScene : Scene
    {
        private readonly UI _ui = new UI(Game.main.width, Game.main.height);
        private readonly string _toLoad;

        private float _elapsedTime = 0;

        public TransitionScene(string toLoad) { _toLoad = toLoad; _ui.SetBackground("transition.png"); }

        public override void OnLoad()
        {
            AddChild(_ui);
        }

        public override void OnUnload()
        {
            
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            if(_elapsedTime >= GameData.TransitionWaitTime * 1000) SceneManager.LoadScene(_toLoad);
        }
    }
}
