using System.Drawing;
using GXPEngine.Physics;

namespace GXPEngine
{
    internal static class ColliderLoader
    {
        private static readonly int[][] coordinates = { new int[] { 16, 0 }, new int[] { 32, 16 }, new int[] { 16, 32 }, new int[] { 0, 16 } };

        private static bool[] GetSides(AnimationSprite animSprite)
        {
            Bitmap bitmap = animSprite.texture.bitmap;
            Color baseColor = bitmap.GetPixel(bitmap.Width / 2, bitmap.Height / 2);
            bool[] addCollider = new bool[4];
            for (int i = 0; addCollider.Length > i; i++) if (bitmap.GetPixel(coordinates[i][0], coordinates[i][1]) != baseColor) addCollider[i] = true;
            return addCollider;
        }

        public static void AddColliders(AnimationSprite animSprite)
        {
            bool[] addCollider = GetSides(animSprite);
            Tile tile = CreateTileObject(animSprite);
            for (int i = 0; i < addCollider.Length; i++)
            {
                
            }
        }

        private static Tile CreateTileObject(AnimationSprite animationSprite)
        {
            Tile tile = new Tile(animationSprite.texture.bitmap, 1, 1, 1);
            tile.SetXY(animationSprite.x, animationSprite.y);
            return tile;
        }
    }
}
