using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace TextRPG
{
    internal class SoundManager
    {
        string hitPath = "Audio/hitHurt.wav";
        string pickUpPath = "Audio/pickUp.wav";
        string winPath = "Audio/Victory.wav";
        string losePath = "Audio/Loss.wav";
        private SoundPlayer soundPlayer = new SoundPlayer();

        public enum Noise
        {
            hit,
            pickUp,
            win,
            lose
        }

        public void Play(Noise noise)
        {
            switch (noise)
            {
                case Noise.hit:
                    soundPlayer.SoundLocation = hitPath;
                    break;
                case Noise.pickUp:
                    soundPlayer.SoundLocation = pickUpPath;
                    break;
                case Noise.win:
                    soundPlayer.SoundLocation = winPath;
                    break;
                case Noise.lose:
                    soundPlayer.SoundLocation = losePath;
                    break;
            }
            soundPlayer.Play();
        }

    }
}
