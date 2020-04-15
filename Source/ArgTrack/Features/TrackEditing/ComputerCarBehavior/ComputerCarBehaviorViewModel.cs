using System.Windows.Input;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ComputerCarBehavior
{
    public class ComputerCarBehaviorViewModel : BaseViewModel
    {
        private short _formationLength;

        public short FormationLength
        {
            get { return _formationLength; }
            set { SetProperty(ref _formationLength, value, () => FormationLength); }
        }


        private short _lateBrakingFactorNonRace;

        public short LateBrakingFactorNonRace
        {
            get { return _lateBrakingFactorNonRace; }
            set { SetProperty(ref _lateBrakingFactorNonRace, value, () => LateBrakingFactorNonRace); }
        }


        private short _lateBrakingFactorRace;

        public short LateBrakingFactorRace
        {
            get { return _lateBrakingFactorRace; }
            set { SetProperty(ref _lateBrakingFactorRace, value, () => LateBrakingFactorRace); }
        }


        private short _lateBrakingFactorWetRace;

        public short LateBrakingFactorWetRace
        {
            get { return _lateBrakingFactorWetRace; }
            set { SetProperty(ref _lateBrakingFactorWetRace, value, () => LateBrakingFactorWetRace); }
        }


        private short _powerFactor;

        public short PowerFactor
        {
            get { return _powerFactor; }
            set { SetProperty(ref _powerFactor, value, () => PowerFactor); }
        }


        private short _strategyChance;

        public short StrategyChance
        {
            get { return _strategyChance; }
            set { SetProperty(ref _strategyChance, value, () => StrategyChance); }
        }


        private short _strategyFirstPitStopLap;

        public short StrategyFirstPitStopLap
        {
            get { return _strategyFirstPitStopLap; }
            set { SetProperty(ref _strategyFirstPitStopLap, value, () => StrategyFirstPitStopLap); }
        }


        private byte[] _unknownData;

        public byte[] UnknownData
        {
            get { return _unknownData; }
            set { SetProperty(ref _unknownData, value, () => UnknownData); }
        }


        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/computer-car-behavior-window/");
    }
}
