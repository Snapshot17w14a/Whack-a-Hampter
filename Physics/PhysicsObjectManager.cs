using System.Collections.Generic;

namespace GXPEngine.Physics
{
    internal static class PhysicsObjectManager
    {
        private static readonly List<WindCurrent> _windCurrents = new List<WindCurrent>();

        private static bool _isPlayerInWindCurrent = false;
        private static WindCurrent _overlappedWindCurrent = null;

        /// <summary>Add a <see cref="WindCurrent"/> current to the list of actively checked wind currents</summary>
        public static void AddWindCurrent(WindCurrent windCurrent) => _windCurrents.Add(windCurrent);
        
        /// <summary>Call every frame to update the wind currents and apply the wind force to the player</summary>
        public static void Update()
        {
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
        }
    }
}
