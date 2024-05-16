using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class GameSoundHandler
    {
        private SoundChannel background_music;
        private SoundChannel Bush;
        private SoundChannel Fire;
        private SoundChannel HitHampter;
        private SoundChannel HitWall;
        private SoundChannel LevelPass;
        private SoundChannel MenuSelect;
        private SoundChannel Mud;
        // private SoundChannel Putt;
        private SoundChannel Water;

        public void PlayBGM()
        {
            background_music = new Sound("Sound/BGM.wav", true, true).Play();
        }

        public void BushSFX()
        {
            Bush = new Sound("Sound/Bush.wav", false, false).Play();
        }

        public void FireSFX()
        {
            Fire = new Sound("Sound/Fire.wav", false, false).Play();
        }

        public void HitHampterSFX()
        {
            HitHampter = new Sound("Sound/HitHamptere.wav", false, false).Play();
        }

        public void HitWallSFX()
        {
            HitWall = new Sound("Sound/HitWall.wav", false, false).Play();
        }

        public void LevelPassSFX()
        {
            LevelPass = new Sound("Sound/LevelPass.wav", false, false).Play();
        }

        public void MenuSelectSFX()
        {
            MenuSelect = new Sound("Sound/MenuSelect.wav", false, false).Play();
        }

        public void MudSFX()
        {
            Mud = new Sound("Sound/Mud.wav", false, false).Play();
        }

        public void WaterSFX()
        {
            Water = new Sound("Sound/Water.wav", false, false).Play();
        }
    }
}
