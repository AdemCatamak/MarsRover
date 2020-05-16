using System;
using MarsRover.Exceptions;

namespace MarsRover.Models.Surfaces.Exceptions
{
    public class PlateauSizeNotValidException : NotValidException
    {
        public PlateauSizeNotValidException(int xSize, int ySize) : base($"Plateau size not valid [{xSize},{ySize}]", null)
        {
        }

        public PlateauSizeNotValidException(string message) : this(message, null)
        {
        }

        public PlateauSizeNotValidException(string message, Exception innerEx) : base(message, innerEx)
        {
        }
    }
}