using System;
using System.Collections.Generic;

namespace MarsRover.Services.InputProviderSection
{
    public class Input
    {
        public string SurfaceParameter { get; set; }
        public List<Tuple<string, string>> VehicleAndCommandsParameterList { get; set; } = new List<Tuple<string, string>>();
    }
}