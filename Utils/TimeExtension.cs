namespace Rogue.Scripts.Utils
{
    public static class TimeExtension
    {
        public static string ToTimeString(this float time)
        {
            var timeMinText = $"{(int)time / 60:D2}";
            var timeSecText = $"{(int)time % 60:D2}";
            var timeMilliSecText = $"{(int)(time * 1000) % 1000:D3}";
            var timeRichText =
                $"<size=50>{timeMinText}</size>:<size=50>{timeSecText}</size>.<size=40>{timeMilliSecText}</size>";
            return timeRichText;
        }
    }
}