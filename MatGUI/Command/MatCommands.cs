using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MatGUI
{
    public abstract class MatCommand
    {
        private List<WeakReference<EventHandler>> _canExecuteChangedHandlers = new List<WeakReference<EventHandler>>();

        /// <summary>
        /// コマンドが実行可能かどうかが変化した時に発生します。
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChangedHandlers.Add(new WeakReference<EventHandler>(value));
            }
            remove
            {
                foreach (var weakReference in _canExecuteChangedHandlers
                    .Where(r =>
                    {
                        EventHandler result;
                        if (r.TryGetTarget(out result) && result == value)
                        {
                            return true;
                        }
                        return false;
                    }).ToList())
                {
                    _canExecuteChangedHandlers.Remove(weakReference);
                }
            }
        }

        /// <summary>
        /// コマンドが実行可能かどうかが変化した時に呼び出されます。
        /// </summary>
        protected void OnCanExecuteChanged()
        {
            foreach (var handlerWeakReference in _canExecuteChangedHandlers.ToList())
            {
                EventHandler result;

                if (handlerWeakReference.TryGetTarget(out result))
                {
                    MatDispatcherHelper.UIDispatcher.InvokeAsync(() => result(this, EventArgs.Empty));
                }
                else
                {
                    _canExecuteChangedHandlers.Remove(handlerWeakReference);
                }
            }
        }
    }

    /// <summary>
    /// ViewModelにおいて、Viewからメッセージ、あるいはオブジェクトを受け取って動作するコマンドを表します。
    /// </summary>
    /// <typeparam name="T">Viewから受け取るオブジェクトの型</typeparam>
    public sealed class MatListenerCommand<T> : MatCommand, ICommand, INotifyPropertyChanged
    {
        Action<T> _execute;
        Func<bool> _canExecute;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute">コマンドが実行するAction</param>
        public MatListenerCommand(Action<T> execute) : this(execute, null) { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="execute">コマンドが実行するAction</param>
        /// <param name="canExecute">コマンドが実行可能かどうかをあらわすFunc&lt;bool&gt;</param>
        public MatListenerCommand(Action<T> execute, Func<bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// コマンドが実行可能かどうかを取得します。
        /// </summary>
        public bool CanExecute
        {
            get { return _canExecute == null ? true : _canExecute(); }
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="parameter">Viewから渡されたオブジェクト</param>
        public void Execute(T parameter)
        {
            _execute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter == null)
            {
                Execute(default(T));
            }
            else
            {
                Execute((T)parameter);
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        /// <summary>
        /// コマンドが実行可能かどうかが変化した時に発生します。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged()
        {
            var handler = Interlocked.CompareExchange(ref PropertyChanged, null, null);
            if (handler != null)
            {
                handler(this, MatEventArgsFactory.GetPropertyChangedEventArgs("CanExecute"));
            }
        }

        /// <summary>
        /// コマンドが実行可能かどうかが変化したことを通知します。
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged()
        {
            OnPropertyChanged();
            OnCanExecuteChanged();
        }

    }
}
