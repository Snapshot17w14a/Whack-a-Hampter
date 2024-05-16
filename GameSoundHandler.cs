using System;
using System.Collections.Generic;

namespace GXPEngine
{
    internal class GameSoundHandler
    {
        private readonly Dictionary<string, Sound> _sounds = new Dictionary<string, Sound>();

        public GameSoundHandler()
        {
            _sounds.Add("BGM", new Sound("Sound/BGM.wav", true, true));
            _sounds.Add("BushSFX", new Sound("Sound/Bush.wav", false, false));
            _sounds.Add("FireSFX", new Sound("Sound/Fire.wav", false, false));
            _sounds.Add("HitHampter", new Sound("Sound/HitHampter.wav", false, false));
            _sounds.Add("HitWall", new Sound("Sound/HitWall.wav", false, false));
            _sounds.Add("LevelPass", new Sound("Sound/LevelPass.wav", false, false));
            _sounds.Add("MenuSelect", new Sound("Sound/MenuSelect.wav", false, false));
            _sounds.Add("MudSFX", new Sound("Sound/Mud.wav", false, false));
            _sounds.Add("WaterSFX", new Sound("Sound/Water.wav", false, false));
        }

        public void PlaySound(string soundName, uint channelId = 1)
        {
            if (_sounds.ContainsKey(soundName))
            {
                _sounds[soundName].Play(false, channelId);
            }
            else
            {
                Console.WriteLine("Sound not found: " + soundName);
            }
        }
    }
}
