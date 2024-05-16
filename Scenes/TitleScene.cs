using GXPEngine.Physics;
using GXPEngine.SceneManagement;

namespace GXPEngine.Scenes
{
    internal class TitleScene : Scene
    {
        private UI _ui = new UI(Game.main.width, Game.main.height);

        public TitleScene()
        {
            AddChild(_ui);
            _ui.SetBackground("title_background.png");
            _ui.Canvas.TextAlign(CenterMode.Center, CenterMode.Max);
            _ui.Canvas.TextSize(24);
        }

        public override void OnLoad()
        {

        }

        public override void OnUnload()
        {

        }

        private void Update()
        {
            PhysicsManager.PrintColliders();
            _ui.Canvas.Fill(0);
            _ui.Canvas.Text("Press space to play.", Game.main.width / 2, Game.main.height - 20);
            _ui.Canvas.Fill(255);
            _ui.Canvas.Text("Press space to play.", Game.main.width / 2 - 2, Game.main.height - 22);
            if (Input.GetKeyDown(Key.SPACE)) { SceneManager.LoadScene("Level1"); GameData.SoundHandler.PlaySound("MenuSelect"); }
        }
    }
}
