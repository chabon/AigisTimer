using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace AigisTimer.ViewModels.Commands

{
    /// <summary>
    /// デリゲートを受け取るICommandの実装
    /// </summary>
    public class DelegateCommand<T> : ICommand
    {
        private Action<T> execute;

        private Func<bool> canExecute;

        /// <summary>
        /// コマンドのExecuteメソッドで実行する処理を指定してDelegateCommandのインスタンスを
        /// 作成します。
        /// </summary>
        /// <param name="execute">Executeメソッドで実行する処理</param>
        public DelegateCommand(Action<T> execute) : this(execute, () => true)
        {
        }

        /// <summary>
        /// コマンドのExecuteメソッドで実行する処理とCanExecuteメソッドで実行する処理を指定して
        /// DelegateCommandのインスタンスを作成します。
        /// </summary>
        /// <param name="execute">Executeメソッドで実行する処理</param>
        /// <param name="canExecute">CanExecuteメソッドで実行する処理</param>
        public DelegateCommand (Action<T> execute, Func<bool> canExecute)
	    {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
	    }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        public void Execute(object parameter)
        {
            this.execute( (T)parameter );
        }

        /// <summary>
        /// コマンドが実行可能な状態化どうか問い合わせます。
        /// </summary>
        /// <returns>実行可能な場合はtrue</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute();
        }

        /// <summary>
        /// CanExecuteの結果に変更があったことを通知するイベント。
        /// WPFのCommandManagerのRequerySuggestedイベントをラップする形で実装しています。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }


}
