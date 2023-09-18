using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Runtime.InteropServices;

namespace TextRPG
{
    internal class SoundManager
    {
        // Sound api functions
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        string hitPath = "Audio/hitHurt.wav";
        string pickUpPath = "Audio/pickUp.wav";
        string winPath = "Audio/Victory.wav";
        string losePath = "Audio/Loss.wav";
        string questPath = "Audio/questComplete.wav";

        public enum Noise
        {
            hit,
            pickUp,
            win,
            lose,
            quest
        }

        public void Play(Noise noise)
        {
            switch (noise)
            {
                case Noise.hit:
                    PlaySound(hitPath);
                    break;
                case Noise.pickUp:
                    PlaySound(pickUpPath);
                    break;
                case Noise.win:
                    PlaySound(winPath);
                    break;
                case Noise.lose:
                    PlaySound(losePath);
                    break;
                case Noise.quest:
                    PlaySound(questPath);
                    break;
            }
        }

        static void PlaySound(string soundFileName)
        {
            string command = $"open \"{soundFileName}\" type waveaudio alias {soundFileName}";
            mciSendString(command, null, 0, IntPtr.Zero);

            command = $"play {soundFileName}";
            mciSendString(command, null, 0, IntPtr.Zero);

            command = $"close {soundFileName}";
            mciSendString(command, null, 0, IntPtr.Zero);
        }

    }
}
