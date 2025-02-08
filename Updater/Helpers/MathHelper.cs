namespace Updater.Helpers
{
    public static class MathHelper
    {
        public static int Percentage(double part, double whole)
        {
            return (int)Math.Round(part / whole * 100, 0);
        }
    }
}
