using System;
using System.Collections.Generic;
using System.Linq;
using MarsRover.Exceptions;
using MarsRover.Services.InputProviderSection.Exceptions;
using MarsRover.Services.InputProviderSection.InputProviders;

namespace MarsRover.Services.InputProviderSection.InputProviderFactories
{
    public class InputProviderFactory : IInputProviderFactory
    {
        private readonly IEnumerable<IInputProvider> _inputProviders;

        public InputProviderFactory(IEnumerable<IInputProvider> inputProviders)
        {
            _inputProviders = inputProviders ?? throw new DevelopmentException($"{nameof(inputProviders)} should not be null");
        }

        public IInputProvider Generate(InputProviderTypes inputProviderType)
        {
            if (!Enum.IsDefined(typeof(InputProviderTypes), inputProviderType))
            {
                throw new InputProviderNotValidException(inputProviderType.ToString());
            }

            IInputProvider inputProvider;
            switch (inputProviderType)
            {
                case InputProviderTypes.Console:
                    inputProvider = _inputProviders.FirstOrDefault(p => p is IConsoleInputProvider);
                    break;
                case InputProviderTypes.File:
                    inputProvider = _inputProviders.FirstOrDefault(p => p is IFileInputProvider);
                    break;
                default:
                    throw new DevelopmentException($"There is uncovered switch-case state [{inputProviderType.ToString()}]");
            }

            if (inputProvider == null)
            {
                throw new DevelopmentException($"There is no predefined {nameof(IInputProvider)} for {inputProviderType}");
            }

            return inputProvider;
        }
    }
}