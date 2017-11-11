using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

using AigisTimer.Models;
using AigisTimer.ViewModels.Commands;


namespace AigisTimer.ViewModels
{
    public class TimerViewModel : ViewModelBase
    {
        // constructor
        public TimerViewModel()
        {
            this.timerModel = new TimerModel();
            this.timerModel.MainTimerTicked += (sender, e) =>
            {
                NotifyPropertyChanged(nameof(ForgroundColor));
                NotifyPropertyChanged(nameof(Stamina));
                NotifyPropertyChanged(nameof(StaminaMax));
                NotifyPropertyChanged(nameof(RecoveryTimeMin));
                NotifyPropertyChanged(nameof(RecoveryTimeSec));
                NotifyPropertyChanged(nameof(Hour));
                NotifyPropertyChanged(nameof(Minute));
                NotifyPropertyChanged(nameof(Second));
            };
        }


        // model
        private TimerModel timerModel;


        // field
        private Dictionary<TimerModel.States, SolidColorBrush> forgroundColorMap = new Dictionary<TimerModel.States, SolidColorBrush>
        {
            { TimerModel.States.Ready,   new SolidColorBrush(Colors.Black)  },
            { TimerModel.States.Setting, new SolidColorBrush(Colors.BlueViolet)  },
            { TimerModel.States.Running, new SolidColorBrush(Colors.Brown) },
            { TimerModel.States.End,     new SolidColorBrush(Colors.Gray)  },
        };
        private DelegateCommand<string> _testDelegateCommand;


        // タイマータイプ
        public TimerModel.TimerTypes TimerType
        {
            get { return this.timerModel.TimerType; }
            set { this.timerModel.TimerType = value; }
        }

        // 状態
        public TimerModel.States State { get { return timerModel.State; } }

        // スタミナ
        public int Stamina {
            get { return this.timerModel.Stamina; }
            set
            {
                this.timerModel.Stamina = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged( nameof(ForgroundColor) );
            }
        }

        // スタミナ最大値
        public int StaminaMax {
            get { return this.timerModel.StaminaMax; }
            set
            {
                this.timerModel.StaminaMax = value;
                NotifyPropertyChanged();
            }
        }

        // 「回復まで」(分)
        public int RecoveryTimeMin
        {
            get { return this.timerModel.RecoveryTimeMin; }
            set
            {
                this.timerModel.RecoveryTimeMin = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RecoveryTimeSec));
            }
        }

        // 「回復まで」(秒)
        public int RecoveryTimeSec
        {
            get { return this.timerModel.RecoveryTimeSec; }
            set
            {
                this.timerModel.RecoveryTimeSec = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RecoveryTimeMin));
            }
        }

        // 時間
        public int Hour
        {
            get { return this.timerModel.Hour; }
            set
            {
                this.timerModel.Hour = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged( nameof(ForgroundColor) );
            }
        }

        // 分
        public int Minute
        {
            get { return this.timerModel.Minute; }
            set
            {
                this.timerModel.Minute = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged( nameof(ForgroundColor) );
            }
        }

        // 秒
        public int Second
        {
            get { return this.timerModel.Second; }
            set
            {
                this.timerModel.Second = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged( nameof(ForgroundColor) );
            }
        }

        // 文字色
        public SolidColorBrush ForgroundColor { get { return forgroundColorMap[timerModel.State]; } }

        // 背景色
        public SolidColorBrush BackgroundColor { get; set; } = new SolidColorBrush(Colors.LightSteelBlue);


        // delegate command test
        public DelegateCommand<string> TestDelegateCommand
        {
            get
            {
                if(_testDelegateCommand == null)
                {
                    _testDelegateCommand = new DelegateCommand<string>( param => {
                        System.Windows.MessageBox.Show(param); } );
                }
                return _testDelegateCommand;
            }
        }

        // パラメーターを動かす(回転させる)コマンド
        private RollTimerParamCommand _rollTimerParamCommand;
        public RollTimerParamCommand RollTimerParamCommand
        {
            get
            {
                return _rollTimerParamCommand ??
                  (_rollTimerParamCommand = new RollTimerParamCommand(this));
            }
        }


        // タイマー停止コマンド
        private DelegateCommand<object> _timerStopCommand;
        public DelegateCommand<object> TimerStopCommand
        {
            get
            {
                if(_timerStopCommand == null)
                {
                    _timerStopCommand = new DelegateCommand<object>( param => {
                        this.timerModel.Stop();
                        NotifyPropertyChanged(nameof(ForgroundColor));
                        NotifyPropertyChanged(nameof(RecoveryTimeMin));
                        NotifyPropertyChanged(nameof(RecoveryTimeSec));
                    } );
                }
                return _timerStopCommand;
            }
        }


    }
}
