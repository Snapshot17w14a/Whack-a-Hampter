using System.Collections.Generic;
using GXPEngine.SceneManagement;
using GXPEngine.Scenes;

namespace GXPEngine.Physics
{
    internal static class PhysicsObjectManager
    {
        private static readonly List<AnimationSprite> _waterTiles = new List<AnimationSprite>();
        private static readonly List<AnimationSprite> _bushes = new List<AnimationSprite>();
        private static readonly List<WindCurrent> _windCurrents = new List<WindCurrent>();
        private static readonly List<Fire> _fires = new List<Fire>();

        private static AnimationSprite _flag;

        private static bool _isPlayerInWindCurrent = false;
        private static WindCurrent _overlappedWindCurrent = null;

        /// <summary>Add a <see cref="WindCurrent"/> current to the list of actively checked wind currents</summary>
        public static void AddWindCurrent(WindCurrent windCurrent) => _windCurrents.Add(windCurrent);
        public static void AddWater(AnimationSprite waterTile) => _waterTiles.Add(waterTile);
        public static void AddBush(AnimationSprite bush) => _bushes.Add(bush);
        public static void SetFlag(AnimationSprite flag) => _flag = flag;
        public static void AddFire(Fire fire) => _fires.Add(fire);


        /// <summary>Call every frame to update the wind currents and apply the wind force to the player</summary>
        public static void Update()
        {
            if(_flag.HitTest(GameData.ActivePlayer) && GameData.ActivePlayer.Collider.IsActive)
            {
                GameData.ActivePlayer.Collider.IsActive = false;
                GameData.ActivePlayer.Collider.SetVelocity(Vec2.zero);
                GameData.SoundHandler.PlaySound("LevelPass");
                ((TiledScene)SceneManager.CurrentScene).HitFlag();
            }

            if (!_isPlayerInWindCurrent)
            {
                foreach (var windCurrent in _windCurrents)
                {
                    if (!windCurrent.HitTest(GameData.ActivePlayer)) continue;
                    GameData.ActivePlayer.Collider.AddAcceleration(windCurrent.NormalizedDirection * windCurrent.Strength);
                    _overlappedWindCurrent = windCurrent;
                    _isPlayerInWindCurrent = true;
                    break;
                }
            }
            else if (!_overlappedWindCurrent.HitTest(GameData.ActivePlayer))
            {
                GameData.ActivePlayer.Collider.AddAcceleration(-(_overlappedWindCurrent.NormalizedDirection * _overlappedWindCurrent.Strength));
                _overlappedWindCurrent = null;
                _isPlayerInWindCurrent = false;
            }

            foreach (var fire in _fires)
            {
                if (!fire.HitTest(GameData.ActivePlayer)) continue;
                GameData.ActivePlayer.ResetPosition();
                ((TiledScene)SceneManager.CurrentScene).hitCount = 0;
                GameData.SoundHandler.PlaySound("FireSFX");
                break;
            }

            foreach (var bush in _bushes)
            {
                if (GameData.ActivePlayer.collider.TimeOfImpact(bush.collider, GameData.ActivePlayer.Collider.Velocity.x, GameData.ActivePlayer.Collider.Velocity.y, out Core.Vector2 normal) > 1) continue;
                if (GameData.ActivePlayer.Collider.Velocity.Length() >= GameData.BushRequiredVelocity) continue;
                else
                {
                    GameData.ActivePlayer.Collider.ReflectVelocity(Vec2.ToVec2(normal));
                    GameData.SoundHandler.PlaySound("BushSFX");
                }
            }

            foreach (var waterTile in _waterTiles)
            {
                if (!waterTile.HitTest(GameData.ActivePlayer)) continue;
                else if (Vec2.IsZero(GameData.ActivePlayer.Collider.Velocity, GameData.PlayerIsZeroThreshold))
                {
                    GameData.ActivePlayer.ResetPosition();
                    ((TiledScene)SceneManager.CurrentScene).hitCount = 0;
                    GameData.SoundHandler.PlaySound("WaterSFX");
                }
            }
        }

        public static void Reset()
        {
            _isPlayerInWindCurrent = false;
            _overlappedWindCurrent = null;
            _flag = null;
            _windCurrents.ForEach(windCurrent => windCurrent.Destroy());
            _waterTiles.ForEach(waterTile => waterTile.Destroy());
            _bushes.ForEach(bush => bush.Destroy());
            _fires.ForEach(fire => fire.Destroy());
            _windCurrents.Clear();
            _waterTiles.Clear();
            _bushes.Clear();
            _fires.Clear();
        }
    }
}
