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
using MarsRover.Services.InputProviderSection.InputProviders;
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

                foreach ((string vehicleParameter, string moveCommandParameter) in input.VehicleAndCommandsParameterList)
                {
                    IVehicleContext vehicleContext;
                    Vehicle movable = vehicleFactory.Generate(VehicleTypes.Rover, vehicleParameter);
                    try
                    {
                        vehicleContext = vehicleContextFactory.Generate(surface, movable);
                    }
                    catch (VehicleDeployException)
                    {
                        Console.WriteLine($"Rover could not land on surface");
                        continue;
                    }

                    IEnumerable<VehicleActions> vehicleActions = vehicleActionProvider.Provide(moveCommandParameter);

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
                        Console.WriteLine($"Connection lost. Rover is not on surface anymore");
                    }
                }

                Exit(0);
            }
        }

        private static string GetInputProviderArgument(IInputProvider inputProvider)
        {
            var argument = string.Empty;
            if (inputProvider is IConsoleInputProvider)
            {
                Console.WriteLine("Parameters will be taken until empty line");
                while (true)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;
                    argument = $"{argument} | {line}";
                }
            }
            else if (inputProvider is IFileInputProvider)
            {
                Console.WriteLine("Filename");
                while (string.IsNullOrEmpty(argument))
                {
                    argument = Console.ReadLine();
                }

                Console.WriteLine();
            }
            else
            {
                throw new DevelopmentException($"{nameof(GetInputProviderArgument)} executed for not supported provider");
            }

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


        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is NotValidException notValidException)
            {
                Console.WriteLine($"{notValidException.Message}");
            }

            else if (e.ExceptionObject is NotFoundException notFoundException)
            {
                Console.WriteLine($"{notFoundException.Message}");
            }

            else if (e.ExceptionObject is DevelopmentException developmentException)
            {
                Console.WriteLine($"This is hint for developer: {developmentException.Message}");
            }
            else
            {
                Console.WriteLine($"Unhandled exception occurs. {e.ExceptionObject}");
            }

            Exit(1);
        }

        private static void Exit(int exitCode)
        {
            Console.WriteLine("Press any key for exit");
            Console.ReadKey();
            Environment.Exit(exitCode);
        }
    }
}