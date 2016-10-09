using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace MatStudioROBOT2016.Models
{
    public class SoundM : NotificationObject
    {
        static SoundM()
        {
            Current = new SoundM();
            Sound.Initialize();

            string[] files = System.IO.Directory.GetFiles(@"Sounds", "*", System.IO.SearchOption.TopDirectoryOnly);

            foreach(string path in files)
            {
                string name = path.Split('\\')[1];
                name = name.Split('.')[0];

                Sound s = new Sound();
                s.ReadSoundFile(path);

                Current.Album.Add(name, s);
            }

            App.Current.Exit += Current_Exit;
        }

        private static void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            Sound.DisposeAudioContext();
        }

        public static SoundM Current { get; private set; }

        private Dictionary<string, Sound> Album = new Dictionary<string, Sound>();

        public void Play(string name)
        {
            Album[name].Play();
        }

        public void Stop()
        {
            foreach(Sound s in Sound.SoundList)
            {
                s.Stop();
            }
        }
    }
}
