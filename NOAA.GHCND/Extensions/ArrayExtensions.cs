namespace NOAA.GHCND.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] MakeArray<T>(this T element)
        {
            return new T[] {element};
        }
    }
}