using System;
using System.Collections.Generic;
using MarsRover.Exceptions;
using MarsRover.Extensions;
using MarsRover.Services.InputProviderSection;
using MarsRover.Services.SurfaceBuilderSection;
using MarsRover.Services.VehicleContextFactorySection;
using MarsRover.Services.VehicleFactorySection;
using MarsRover.Models;
using MarsRover.Models.Surfaces;
using MarsRover.Models.VehicleContexts.Exceptions;
using MarsRover.Services.InputProviderSection.InputProviderFactories;
using MarsRover.Services.StreamSection;
using MarsRover.Services.StreamSection.FileProcessors;
using MarsRover.Services.StreamSection.StreamReaders;
using MarsRover.Services.SurfaceBuilderSection.SurfaceBuilderFactories;
using MarsRover.Services.VehicleActionProviderSection;
using MarsRover.Services.VehicleActionProviderSection.VehicleActionProviders;
using MarsRover.Services.VehicleContextFactorySection.VehicleContextFactories;
using MarsRover.Services.VehicleFactorySection.VehicleFactories;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IFileProcessor, BasicFileProcessor>();
            serviceCollection.AddTransient<IStreamReader, BasicStreamReader>();
            serviceCollection.AddSingleton<IVehicleFactory, VehicleFactory>();
            serviceCollection.AddImplementedTypes<IVehicleBuilder>(ServiceLifetime.Singleton, typeof(IVehicleBuilder).Assembly);
            serviceCollection.AddSingleton<IVehicleContextFactory, VehicleContextFactory>();
            serviceCollection.AddSingleton<ISurfaceBuilderFactory, SurfaceBuilderFactory>();
            serviceCollection.AddImplementedTypes<ISurfaceBuilder>(ServiceLifetime.Singleton, typeof(ISurfaceBuilder).Assembly);
            serviceCollection.AddTransient<IInputProviderFactory, InputProviderFactory>();
            serviceCollection.AddImplementedTypes<IInputProvider>(ServiceLifetime.Transient, typeof(IInputProvider).Assembly);
            serviceCollection.AddSingleton<IVehicleActionProvider, BulkStringVehicleActionProvider>();

            using (ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider())
            {
                InputProviderTypes inputProviderType = SelectInputProviderType();

                var inputProviderFactory = serviceProvider.GetRequiredService<IInputProviderFactory>();
                IInputProvider inputProvider = inputProviderFactory.Generate(inputProviderType);

                var surfaceBuilderFactory = serviceProvider.GetRequiredService<ISurfaceBuilderFactory>();

                var vehicleFactory = serviceProvider.GetRequiredService<IVehicleFactory>();

                var vehicleContextFactory = serviceProvider.GetRequiredService<IVehicleContextFactory>();

                var vehicleActionProvider = serviceProvider.GetRequiredService<IVehicleActionProvider>();

                string inputProviderArgument = GetInputProviderArgument(inputProvider);
                Input input = inputProvider.Provide(inputProviderArgument);

                ISurfaceBuilder surfaceBuilder = surfaceBuilderFactory.Generate(typeof(Plateau));
                Surface surface = surfaceBuilder.Build(input.SurfaceParameter);

                var vehicleAndActionPairList = new List<Tuple<IVehicleContext, IEnumerable<VehicleActions>>>();
                foreach ((string vehicleParameter, string moveCommandParameter) in input.VehicleAndCommandsParameterList)
                {
                    Vehicle movable = vehicleFactory.Generate(VehicleTypes.Rover, vehicleParameter);
                    IVehicleContext vehicleContext = vehicleContextFactory.Generate(surface, movable);

                    IEnumerable<VehicleActions> vehicleActions = vehicleActionProvider.Provide(moveCommandParameter);

                    vehicleAndActionPairList.Add(new Tuple<IVehicleContext, IEnumerable<VehicleActions>>(vehicleContext, vehicleActions));
                }

                foreach ((IVehicleContext vehicleContext, IEnumerable<VehicleActions> vehicleActions) in vehicleAndActionPairList)
                {
                    try
                    {
                        foreach (VehicleActions action in vehicleActions)
                        {
                            vehicleContext.Move(action);
                        }

                        Console.WriteLine(vehicleContext.Vehicle.ToString());
                    }
                    catch (VehicleConnectionLostException)
                    {
                        Console.WriteLine($"Connection lost. Rover is not on surface");
                    }
                }

                Console.WriteLine("Press any key for exit");
                Console.ReadKey();
            }
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is NotValidException notValidException)
            {
                Console.WriteLine($"Validation exception: {notValidException.Message}");
            }

            if (e.ExceptionObject is NotFoundException notFoundException)
            {
                Console.WriteLine($"Requirement could not found: {notFoundException.Message}");
            }

            if (e.ExceptionObject is DevelopmentException developmentException)
            {
                Console.WriteLine($"This is hint for developer: {developmentException.Message}");
            }

            Environment.Exit(1);
        }

        private static string GetInputProviderArgument(IInputProvider inputProvider)
        {
            Console.WriteLine(inputProvider.FormatInfo);
            string argument = Console.ReadLine();
            return argument;
        }

        private static InputProviderTypes SelectInputProviderType()
        {
            Console.WriteLine($"Data provider:{Environment.NewLine}" +
                              $"[1] Console\t[2] File");

            InputProviderTypes? inputProviderType = null;
            do
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                Console.WriteLine();

                int.TryParse(consoleKeyInfo.KeyChar.ToString(), out int choice);
                if (Enum.IsDefined(typeof(InputProviderTypes), choice))
                {
                    inputProviderType = (InputProviderTypes) choice;
                }
            } while (!inputProviderType.HasValue);

            return inputProviderType.Value;
        }
    }
}