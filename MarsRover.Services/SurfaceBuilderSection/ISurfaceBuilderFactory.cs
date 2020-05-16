using System;

namespace MarsRover.Services.SurfaceBuilderSection
{
    public interface ISurfaceBuilderFactory
    {
        ISurfaceBuilder Generate(Type surfaceTypes);
    }
}