using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AigisTimer.ViewModels.Commands
{
    // パラメータを動かす(回転させる)コマンド。主にマウスホイールで行う
    public class RollTimerParamCommand : ICommand
    {
        // constructor
        public RollTimerParamCommand(TimerViewModel timerViewModel)
        {
            this.vm = timerViewModel;
        }


        // field
        private TimerViewModel vm;


        // property
        public  string TargetPropertyName { get; set; }


        public void Execute(object parameter)
        {
            int val = (int)parameter;
            ModifierKeys modifierKeys = Keyboard.Modifiers;
            if ((modifierKeys & ModifierKeys.Shift) != ModifierKeys.None) val *= 10;
            else if ((modifierKeys & ModifierKeys.Control) != ModifierKeys.None) val *= 100;

            switch (TargetPropertyName)
            {
                case nameof(vm.Stamina):
                    vm.Stamina += val;
                    break;
                case nameof(vm.StaminaMax):
                    vm.StaminaMax += val;
                    break;
                case nameof(vm.RecoveryTimeMin):
                    vm.RecoveryTimeMin += val;
                    break;
                case nameof(vm.RecoveryTimeSec):
                    vm.RecoveryTimeSec += val;
                    break;
                case nameof(vm.Hour):
                    vm.Hour += val;
                    break;
                case nameof(vm.Minute):
                    vm.Minute += val;
                    break;
                case nameof(vm.Second):
                    vm.Second += val;
                    break;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
