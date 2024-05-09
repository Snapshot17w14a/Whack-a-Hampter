using System.Drawing;
using GXPEngine.Physics;

namespace GXPEngine
{
    internal class Tile : AnimationSprite
    {
        private Line[] _lineSegments = new Line[4]; // 0 = top, 1 = right, 2 = bottom, 3 = left

        public Tile(Bitmap bmp, int columns, int rows, int frames) : base(bmp, columns, rows, frames)
        {
            SetOrigin(width / 2, height / 2);
        }

        public void SetCollider(Line collider, int index) { _lineSegments[index] = collider; }
    }
}
