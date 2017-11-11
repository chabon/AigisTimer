using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;


namespace AigisTimer.Models
{

    public class TimerModel
    {
        // define
        public enum TimerTypes
        {
            Stamina,
            Kitchen
        }

        public enum States
        {
            Ready,
            Setting,
            Running,
            End
        };

        // static constructor
        static TimerModel()
        {
            
            mainTimer = new Timer();
            mainTimer.Interval = 1000;
            mainTimer.Elapsed += (sender, e) =>
            {
                currentTime = DateTime.Now;
            };
            mainTimer.Start();
        }

        // constructor
        public TimerModel()
        {
            // timer
            mainTimer.Elapsed += MainTimer_Elapsed;
        }

        // static field
        private static Timer mainTimer;
        private static DateTime currentTime;


        // field
        private DateTime notificationTime;  // 通知(終了)時刻
        private int timerCountOfSetting = 0;    // セッティング経過時間(秒)
        private int _stamina = 100;
        private int _staminaMax = 400;
        private int _recoveryTimeMin = 60; 
        private int _recoveryTimeSec = 0;
        private int _hour = 0;
        private int _minute = 0;
        private int _second = 0;


        // event
        public event EventHandler MainTimerTicked;


        // タイマータイプ
        public TimerTypes TimerType { get; set; } = TimerTypes.Stamina;

        // タイマーの状態
        public States State { get; private set; } = States.Ready;

        // スタミナ
        public int Stamina
        {
            get
            {
                return _stamina;
            }
            set
            {
                if (State == States.Running) return;

                int tmp = _stamina;
                _stamina = value;
                if (_stamina < 0)
                {
                    if (tmp == 0) _stamina = _staminaMax;
                    else _stamina = 0;
                }
                else if(_stamina > _staminaMax)
                {
                    if (tmp == _staminaMax) _stamina = 0;
                    else _stamina = _staminaMax;
                }

                if(State != States.Setting) State = States.Setting;
                this.timerCountOfSetting = 0;
            }
        }

        // スタミナ最大値
        public int StaminaMax
        {
            get
            {
                return _staminaMax;
            }
            set
            {
                if (State == States.Running) return;

                _staminaMax = value;
                if (_stamina > _staminaMax)
                {
                    _staminaMax = _stamina;
                }
                this.timerCountOfSetting = 0;
            }
        }

        // スタミナが１回復するまでの時間(分)の設定値
        public int RecoveryTimeSettingMin { get; private set; } = 60;

        // 「回復まで」の時間(分)
        public int RecoveryTimeMin
        {
            get
            {
                return _recoveryTimeMin;
            }
            set
            {
                if (State != States.Setting) return;

                int prevVal = _recoveryTimeMin;
                _recoveryTimeMin = value;

                // 最大値を超えた時
                if(value > RecoveryTimeSettingMin)
                {
                    if(prevVal == RecoveryTimeSettingMin)
                    {
                        _recoveryTimeMin = 0;
                        _recoveryTimeSec = 0;
                    }
                    else
                    {
                        _recoveryTimeMin = RecoveryTimeSettingMin;
                        _recoveryTimeSec = 0;
                    }
                }

                // 最大値になった時
                else if(value == RecoveryTimeSettingMin)
                {
                    _recoveryTimeSec = 0;
                }

                // 0を下回った時
                else if(value < 0)
                {
                    if(prevVal == 0)
                    {
                        _recoveryTimeMin = RecoveryTimeSettingMin;
                        _recoveryTimeSec = 0;
                    }
                    else
                    {
                        _recoveryTimeMin = 0;
                    }
                }

                this.timerCountOfSetting = 0;
            }
        }

        // 「回復まで」の時間(秒)
        public int RecoveryTimeSec
        {
            get
            {
                return _recoveryTimeSec;
            }
            set
            {
                if (State != States.Setting) return;

                int prevVal = _recoveryTimeSec;
                int diffVal = value - _recoveryTimeSec;
                _recoveryTimeSec = value;

                if(_recoveryTimeMin == RecoveryTimeSettingMin)
                {
                    if(diffVal > 0) // 60:00からスクロールアップした時
                    {
                        _recoveryTimeMin = 0;
                        _recoveryTimeSec = 0;
                    }
                    else if(diffVal < 0) // 同じくスクロールダウンした時
                    {
                        _recoveryTimeMin -= 1;
                        _recoveryTimeSec = 60 + diffVal;
                    }
                }
                else if (value > 59) _recoveryTimeSec = 0;
                else if(value < 0)
                {
                    if (prevVal == 0) _recoveryTimeSec = 60 + diffVal;
                    else _recoveryTimeSec = 0;
                }

                if (_recoveryTimeSec < 0) _recoveryTimeSec = 0;
                this.timerCountOfSetting = 0;
            }
        }

        // 時間
        public int Hour
        {
            get { return _hour; }
            set
            {
                if (State == States.Running) return;

                if (_hour == 0 && value < 0)
                {
                    value = 100 + value;
                }
                else if(_hour == 99 && value > 99)
                {
                    value = value - 100;
                }

                if (value < 0) value = 0;
                else if (value > 99) value = 99;

                _hour = value;
                if(State != States.Setting) State = States.Setting;
                this.timerCountOfSetting = 0;
            }
        }

        // 分
        public int Minute
        {
            get { return _minute; }
            set
            {
                if (State == States.Running) return;

                if (_minute == 0 && value < 0)
                {
                    value = 60 + value;
                }
                else if(_minute == 59 && value > 59)
                {
                    value = value - 60;
                }

                if (value < 0) value = 0;
                else if (value > 59) value = 59;

                _minute = value;
                if(State != States.Setting) State = States.Setting;
                this.timerCountOfSetting = 0;
            }
        }

        // 秒
        public int Second
        {
            get { return _second; }
            set
            {
                if (State == States.Running) return;

                if (_second == 0 && value < 0)
                {
                    value = 60 + value;
                }
                else if(_second == 59 && value > 59)
                {
                    value = value - 60;
                }

                if (value < 0) value = 0;
                else if (value > 59) value = 59;

                _second = value;
                if(State != States.Setting) State = States.Setting;
                this.timerCountOfSetting = 0;
            }
        }


        // タイマーイベント
        private void MainTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            switch (State)
            {
                case States.Ready:

                    break;
                case States.Setting:
                    this.timerCountOfSetting += 1;
                    if(this.timerCountOfSetting >= 3)
                    {
                        this.Start();
                        this.timerCountOfSetting = 0;
                    }
                    break;
                case States.Running:
                    // 残り時間を秒単位で取得
                    TimeSpan timeLeft = notificationTime - currentTime;
                    int secLeft = (int)timeLeft.TotalSeconds;
                    if (timeLeft.Milliseconds > 500) secLeft += 1; // 四捨五入

                    // スタミナ
                    _stamina = _staminaMax - (secLeft / (RecoveryTimeSettingMin * 60)) - 1;

                    // 回復まで
                    int recvTimeSec = secLeft % (RecoveryTimeSettingMin * 60);
                    _recoveryTimeMin = recvTimeSec / 60;
                    _recoveryTimeSec = recvTimeSec % 60;

                    // 時間、分、秒
                    _hour = secLeft / 3600;
                    _minute = (secLeft % 3600) / 60;
                    _second = secLeft % 60;

                    // カウント終了
                    if (secLeft <= 0)
                    {
                        _stamina = _staminaMax;
                        _hour = _minute = _second = 0;
                        Notice();
                        Stop();
                    }
                    break;
                case States.End:

                    break;
            }

            this.MainTimerTicked(this, e);
        }


        // タイマースタート
        public void Start()
        {
            int sec;

            // 通知までの秒数(スタミナタイマー形式)
            if(TimerType == TimerTypes.Stamina)
            {
                sec = (_staminaMax - _stamina -1) * RecoveryTimeSettingMin * 60;
                if(sec < 0)
                {
                    Stop();
                    return;
                }
                sec += _recoveryTimeMin * 60;
                sec += _recoveryTimeSec;
            }

            // 通知までの秒数(キッチンタイマー形式)
            else
            {
                sec = _hour * 3600 + _minute * 60 + _second;
            }

            // スタート
            TimeSpan ts = new TimeSpan(0, 0, sec);
            this.notificationTime = DateTime.Now + ts;
            State = States.Running;
        }

        // タイマーストップ
        public void Stop()
        {
            State = States.End;
            _recoveryTimeMin = RecoveryTimeSettingMin;
            _recoveryTimeSec = 0;
        }

        // 通知
        public void Notice()
        {
            System.Media.SystemSounds.Asterisk.Play();
        }
    }
}
