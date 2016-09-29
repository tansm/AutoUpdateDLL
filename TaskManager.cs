using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace AutoUpdate {
    
    //只有一个处理线程的任务，比如去扫描源路径下的所有文件，在需要重新扫描时，原先的扫描程序可以停止了。
    internal sealed class TaskManager<TArgs, TResult> {
        internal delegate void DoWorkFunc(DoWorkEventArgs e);

        public TaskManager(DoWorkFunc func) {
            this._doWorkFunc = func;
            this._okEvent = new AutoResetEvent(false);
        }

        //当前正在运行的任务。
        private volatile Task _currentTask;

        public void Start(object args) {
            //还是不能有两个同时。
            lock (this) {

                //当前任务正在执行，需要停止之前的任务，但没有必要等待他结束。
                var currentTask = _currentTask;
                if (currentTask != null) {
                    currentTask.Cancel();
                }

                Task newTask = new Task(this,args);
                Thread newThread = new Thread(newTask.Run);
                _currentTask = newTask;
                newThread.Start();
            }
        }

        private DoWorkFunc _doWorkFunc;

        private TArgs _lastArgs;
        /// <summary>
        /// 最后一次结果对应的参数。
        /// </summary>
        public TArgs LastArgs {
            get { return _lastArgs; }
        }

        private TResult _lastResult;
        /// <summary>
        /// 最后一次的结果。
        /// </summary>
        public TResult Result {
            get {
                if (_currentTask != null) {
                    _okEvent.WaitOne();
                }
                return _lastResult;
            }
        }

        public void Cancel() {
            var task = _currentTask;
            if (task != null) {
                task.Cancel();
            }
        }

        private AutoResetEvent _okEvent;

        //单个的任务实例。
        private sealed class Task {
            private DoWorkEventArgs _doWorkArgs;
            private TaskManager<TArgs, TResult> _manager;

            public Task(TaskManager<TArgs, TResult> manager,object args) {
                _manager = manager;
                _doWorkArgs = new DoWorkEventArgs(args);
            }

            public void Run() {
                try {
                    _manager._doWorkFunc(_doWorkArgs);
                    if (!_doWorkArgs.Cancel && _manager._currentTask == this) {
                        _manager._lastArgs = (TArgs)_doWorkArgs.Argument;
                        _manager._lastResult = (TResult)_doWorkArgs.Result;
                    }
                }
                catch {
                    //出错了，
                    _doWorkArgs.Cancel = true;
                }
                finally {
                    if (_manager._currentTask == this) {
                        _manager._okEvent.Set();
                        _manager._currentTask = null;
                    }
                }
            }

            public void Cancel() {
                _doWorkArgs.Cancel = true;
            }
        }
    }
    
}
