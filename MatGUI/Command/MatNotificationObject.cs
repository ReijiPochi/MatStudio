﻿using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MatGUI
{
    internal static class MatEventArgsFactory
    {
        private static ConcurrentDictionary<string, PropertyChangedEventArgs> _propertyChangedEventArgsDictionary = new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        public static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            return _propertyChangedEventArgsDictionary.GetOrAdd(propertyName, name => new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// 変更通知オブジェクトの基底クラスです。
    /// </summary>
    [Serializable]
    public class MatNotificationObject : INotifyPropertyChanged
    {
        /// <summary>
        /// プロパティ変更通知イベントです。
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティ変更通知イベントを発生させます。
        /// </summary>
        /// <param name="propertyExpression">() => プロパティ形式のラムダ式</param>
        /// <exception cref="NotSupportedException">() => プロパティ 以外の形式のラムダ式が指定されました。</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");

            if (!(propertyExpression.Body is MemberExpression)) throw new NotSupportedException("このメソッドでは ()=>プロパティ の形式のラムダ式以外許可されません");

            var memberExpression = (MemberExpression)propertyExpression.Body;
            RaisePropertyChanged(memberExpression.Member.Name);
        }

        /// <summary>
        /// プロパティ変更通知イベントを発生させます
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName="")
        {
            var threadSafeHandler = Interlocked.CompareExchange(ref PropertyChanged, null, null);
            if (threadSafeHandler != null)
            {
                var e = MatEventArgsFactory.GetPropertyChangedEventArgs(propertyName);
                threadSafeHandler(this, e);
            }
        }

    }

}