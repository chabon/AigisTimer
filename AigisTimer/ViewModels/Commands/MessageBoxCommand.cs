using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace AigisTimer.ViewModels.Commands
{
    public class MessageBoxCommand : ICommand
    {
        public MessageBoxCommand()
        {

        }

        /// <summary>
        /// メッセージボックスを表示
        /// </summary>
        /// <param name="parameter">メッセージ</param>
        public void Execute(object parameter)
        {
            var str = parameter as string;
            if(str != null)
            {
                MessageBox.Show(str);
            }

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        // コマンド実行の可否の変更した時のイベントハンドラ
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
