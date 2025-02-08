namespace Updater.Helpers
{
    public static class MathHelper
    {
        public static int Percentage(int part, int whole)
        {
            return (int)Math.Round((double)part / whole * 100, 0);
        }

        public static int Percentage(uint part, uint whole)
        {
            return (int)Math.Round((double)part / whole * 100, 0);
        }
    }
}
