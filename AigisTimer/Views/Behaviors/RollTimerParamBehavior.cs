using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Input;



namespace AigisTimer.Views.Behaviors
{
    /// <summary>
    /// タイマーのパラメータ部分をマウスホイールで回転させる処理のBehavior
    /// </summary>
    class RollTimerParamBehavior : Behavior<TextBlock>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(RollTimerParamBehavior)
            );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public string TargetPropertyName { get; set; }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseWheel += AssociatedObject_MouseWheel;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseWheel -= AssociatedObject_MouseWheel;
        }

        void AssociatedObject_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(this.Command != null)
            {
                var cmd = this.Command as ViewModels.Commands.RollTimerParamCommand;
                
                if(cmd != null)
                {
                    cmd.TargetPropertyName = this.TargetPropertyName;
                    int value = 1;
                    if (e.Delta > 0) this.Command.Execute(value);
                    else if (e.Delta < 0) this.Command.Execute(-value);
                }
            }
        }
    }
}
