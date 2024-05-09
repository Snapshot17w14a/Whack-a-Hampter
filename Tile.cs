using System.Collections.Generic;
using GXPEngine.Physics;

namespace GXPEngine
{
    internal class Tile : AnimationSprite
    {
        private readonly List<Line> _lineSegments = new List<Line>();

        public Tile(string filename, int columns, int rows, int frames) : base(filename, columns, rows, frames, false, false) { }

        public void AddColliders(string colliderProperty)
        {
            if(colliderProperty == "null") return;
            SetOrigin(0, 0);
            switch (colliderProperty)
            {
                case "top":
                    _lineSegments.Add(new Line(new Vec2(x + width, y), new Vec2(x, y)));
                    break;
                case "right":
                    _lineSegments.Add(new Line(new Vec2(x + width, y + height), new Vec2(x + width, y)));
                    break;
                case "bottom":
                    _lineSegments.Add(new Line(new Vec2(x, y + height), new Vec2(x + width, y + height)));
                    break;
                case "left":
                    _lineSegments.Add(new Line(new Vec2(x, y), new Vec2(x, y + height)));
                    break;
            }
            foreach(Line line in _lineSegments) game.AddChild(line);
        }
    }
}
