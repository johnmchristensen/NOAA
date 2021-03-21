using System;

namespace NOAA.GHCND.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) {}
    }
}