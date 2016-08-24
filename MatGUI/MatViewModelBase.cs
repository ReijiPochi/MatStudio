using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MatGUI
{
    public class MatViewModelBase : MatNotificationObject, IDisposable
    {

        [NonSerialized]
        private bool _disposed;
        [NonSerialized]
        private MatCompositeDisposable _compositeDisposable;

        /// <summary>
        /// このViewModelクラスの基本CompositeDisposableです。
        /// </summary>
        [XmlIgnore]
        public MatCompositeDisposable CompositeDisposable
        {
            get
            {
                if (_compositeDisposable == null)
                {
                    _compositeDisposable = new MatCompositeDisposable();
                }
                return _compositeDisposable;
            }
            set
            {
                _compositeDisposable = value;
            }
        }

        /// <summary>
        /// このインスタンスによって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                if (_compositeDisposable != null)
                {
                    _compositeDisposable.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
