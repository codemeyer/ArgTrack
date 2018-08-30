using System;
using ArgTrack.Commands;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;
using FluentAssertions;
using Xunit;

namespace ArgTrack.Tests.Commands
{
    public class TypedCommandFacts
    {
        [Fact]
        public void Execute_ParameterIsOfExpectedType_SetsFlag()
        {
            var command = new TestableTypedCommand();
            object parameter = new MainViewModel();

            command.Execute(parameter);

            (parameter as MainViewModel).HasLoadedTrack.Should().BeTrue();
        }

        [Fact]
        public void Execute_ParameterIsNull_ThrowsArgumentNullException()
        {
            var command = new TestableTypedCommand();
            object parameter = null;

            Action action = () => command.Execute(parameter);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Execute_ParameterIsDifferentType_ThrowsArgumentOutOfRangeException()
        {
            var command = new TestableTypedCommand();
            var parameter = new TrackViewModel();

            Action action = () => command.Execute(parameter);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CanExecute_ParameterIsOfExpectedType_ReturnsTrue()
        {
            var command = new TestableTypedCommand();
            object parameter = new MainViewModel();

            bool canExecute = command.CanExecute(parameter);

            canExecute.Should().BeTrue();
        }

        [Fact]
        public void CanExecute_ParameterIsNull_ThrowsArgumentNullException()
        {
            var command = new TestableTypedCommand();
            object parameter = null;

            Action action = () => command.CanExecute(parameter);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CanExecute_ParameterIsDifferentType_ThrowsArgumentOutOfRangeException()
        {
            var command = new TestableTypedCommand();
            var parameter = new TrackViewModel();

            Action action = () => command.CanExecute(parameter);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }

    public class TestableTypedCommand : CommandBase<MainViewModel>
    {
        public override void Execute(MainViewModel parameter)
        {
            parameter.HasLoadedTrack = true;
        }

        public override bool CanExecute(MainViewModel parameter)
        {
            return true;
        }
    }


    public class CommandFacts
    {
        [Fact]
        public void GetParameterAs_ParameterIsOfExpectedType_ReturnsParameterAsType()
        {
            var command = new TestableCommand();
            var parameter = new TrackViewModel();

            var value = command.GetParameterAsWrapper<TrackViewModel>(parameter);

            value.Should().BeSameAs(parameter);
        }

        [Fact]
        public void GetParameterAs_ParameterIsNull_ReturnsNull()
        {
            var command = new TestableCommand();
            object parameter = null;

            var value = command.GetParameterAsWrapper<object>(parameter);

            value.Should().BeNull();
        }

        [Fact]
        public void GetParameterAs_ParameterIsDifferentType_ReturnsNull()
        {
            var command = new TestableCommand();
            var parameter = new TrackViewModel();

            var value = command.GetParameterAsWrapper<MainViewModel>(parameter);

            value.Should().BeNull();
        }
    }

    public class TestableCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
        }

        public T GetParameterAsWrapper<T>(object parameter) where T : class
        {
            return GetParameterAs<T>(parameter);
        }
    }
}
