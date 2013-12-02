using System.ComponentModel;
using System.Runtime.CompilerServices;

using ZE.UI.WPF.Spy.Annotations;

namespace ZE.UI.WPF.Spy.Common
{
  public abstract class NotificationObject : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void NotifyChanged([CallerMemberName] string propertyName = null)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}