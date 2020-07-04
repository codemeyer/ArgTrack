using System;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class EditGenericByteDataCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as byte[];

            if (vm == null)
                return;

            byte[] data = new byte[vm.Length];
            Array.Copy(vm, 0, data, 0, vm.Length);

            var clone = new GenericByteListEditingViewModel();
            clone.SetData(data);

            var window = new TrackGenericByteListDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                for (int i = 0; i <= clone.ByteData.Count - 1; i++)
                {
                    vm[i] = clone.ByteData[i].Value;
                }
            }
        }
    }

    public class EditSingleByteValueCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as EditableByteData;

            if (vm == null)
                return;

            var clone = new EditableByteData(vm.Index, vm.Value);

            var window = new TrackGenericByteValueDetailsWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                vm.Value = clone.Value;
            }
        }
    }
}
