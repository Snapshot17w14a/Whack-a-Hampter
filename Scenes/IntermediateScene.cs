using GXPEngine.SceneManagement;

namespace GXPEngine.Scenes
{
    internal class IntermediateScene : Scene
    {
        private UI _ui = new UI(Game.main.width, Game.main.height);

        public IntermediateScene()
        {
            AddChild(_ui);
            _ui.SetBackground("title_background.png");
            _ui.Canvas.TextAlign(CenterMode.Center, CenterMode.Max);
            _ui.Canvas.TextSize(24);
            _ui.Canvas.Fill(255);
        }

        public override void OnLoad()
        {

        }

        public override void OnUnload()
        {

        }

        private void Update()
        {
            _ui.Canvas.Text("Press space to play.", Game.main.width / 2, Game.main.height - 20);
            if (Input.GetKeyDown(Key.SPACE)) SceneManager.LoadScene("Level1");
        }
    }
}
