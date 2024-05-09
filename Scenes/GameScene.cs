using GXPEngine.Physics;
using GXPEngine.SceneManager;
using System.Collections.Generic;

namespace GXPEngine.Scenes
{
    internal class GameScene : Scene
    {
        private Player player;
        private List<Line> lines = new List<Line>();

        public GameScene() { LoadTestBackground(); }

        public override void OnLoad()
        {
            // Load all objects in the scene
            Game.main.OnBeforeStep += PhysicsManager.Step;
            player = new Player();
            AddChild(player);
            lines.Add(new Line(new Vec2(50, 580), new Vec2(750, 580)));
            lines.Add(new Line(new Vec2(750, 580), new Vec2(700, 10)));
            lines.Add(new Line(new Vec2(700, 10), new Vec2(15, 20)));
            lines.Add(new Line(new Vec2(15, 20), new Vec2(50, 580)));
            foreach (Line line in lines) AddChild(line);
        }

        private void LoadTestBackground()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    AddChild(new Sprite("BG_T.png", false, false)
                    {
                        x = i * 32,
                        y = j * 32
                    });
                }

            }
        }

        public override void OnUnload()
        {
            Game.main.OnBeforeStep -= PhysicsManager.Step;
        }
    }
}
