using AnnulusGames.LucidTools.Audio;

namespace Rogue.Scripts.Data.Repository
{
    public static class BGMRepository
    {
        public static AudioPlayer MainP1 { get; private set; }
        public static AudioPlayer MainP2 { get; private set; }
        public static AudioPlayer MainP3 { get; private set; }
        public static AudioPlayer MainP4 { get; private set; }

        public static void SetAllBGM(AudioPlayer mainP1, AudioPlayer mainP2, AudioPlayer mainP3, AudioPlayer mainP4)
        {
            MainP1 = mainP1;
            MainP2 = mainP2;
            MainP3 = mainP3;
            MainP4 = mainP4;
        }
    }
}