namespace Updater.Helpers
{
    public static class MathHelper
    {
        public static int CalculatePercentage(int part, int whole)
        {
            return (int)Math.Round(((double)part / whole) * 100, 0);
        }
    }
}
