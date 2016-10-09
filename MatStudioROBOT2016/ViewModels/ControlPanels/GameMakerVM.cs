using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using MatStudioROBOT2016.Models;

namespace MatStudioROBOT2016.ViewModels.ControlPanels
{
    public class GameMakerVM : ViewModel
    {
        public GameMakerVM()
        {
            SoundM dummy = new SoundM();
        }

        #region PlaySoundCommand
        private ListenerCommand<string> _PlaySoundCommand;

        public ListenerCommand<string> PlaySoundCommand
        {
            get
            {
                if (_PlaySoundCommand == null)
                {
                    _PlaySoundCommand = new ListenerCommand<string>(PlaySound);
                }
                return _PlaySoundCommand;
            }
        }

        public void PlaySound(string parameter)
        {
            SoundM.Current.Play(parameter);
        }
        #endregion


        #region StopCommand
        private ViewModelCommand _StopCommand;

        public ViewModelCommand StopCommand
        {
            get
            {
                if (_StopCommand == null)
                {
                    _StopCommand = new ViewModelCommand(Stop);
                }
                return _StopCommand;
            }
        }

        public void Stop()
        {
            SoundM.Current.Stop();
        }
        #endregion

    }
}
