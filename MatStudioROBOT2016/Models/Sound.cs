using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

using System.IO;

namespace MatStudioROBOT2016.Models
{
    /// <summary>
    /// 使用時には、ビルドオプションのPrefer 32bitのチェックを外すこと
    /// </summary>
    public class Sound
    {
        public static List<Sound> SoundList = new List<Sound>();
        private static AudioContext ac;

        private static bool isInitialized = false;

        /// <summary>
        /// 定期的に再生状態を調べるためのクロック
        /// </summary>
        private static DispatcherTimer WatchDogTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };

        /// <summary>
        /// サウンドクラスを初期化します
        /// </summary>
        public static void Initialize()
        {
            if (isInitialized)
                return;

            ac = new AudioContext();

            WatchDogTimer.Tick += WatchDogTimer_Tick;
            WatchDogTimer.Start();

            AL.GetError();

            AL.DistanceModel(ALDistanceModel.LinearDistance);

            isInitialized = true;
        }

        private int buffer;
        private int source = -1;

        private float _Gain = 1.0f;
        /// <summary>
        /// 音量
        /// </summary>
        public float Gain
        {
            get { return _Gain; }
            set
            {
                if (value < 0) value = 0;
                AL.Source(source, ALSourcef.Gain, value);
                _Gain = value;
            }
        }

        private Vector3 _Position;
        /// <summary>
        /// 発生位置
        /// </summary>
        public Vector3 Position
        {
            get { return _Position; }
            set
            {
                AL.Source(source, ALSource3f.Position, ref value);
                _Position = value;
            }
        }

        private Vector3 _Velocity;
        /// <summary>
        /// 移動速度
        /// </summary>
        public Vector3 Velocity
        {
            get { return _Velocity; }
            set
            {
                AL.Source(source, ALSource3f.Velocity, ref value);
                _Velocity = value;
            }
        }

        /// <summary>
        /// このサウンドにWAVEファイルを読み込みます
        /// </summary>
        /// <param name="@wavePath"></param>
        public void ReadSoundFile(string @wavePath)
        {
            buffer = AL.GenBuffer();
            source = AL.GenSource();

            int channels, bits_per_ample, sample_rate;
            var sound_data = LoadWave(File.Open(wavePath, FileMode.Open), out channels, out bits_per_ample, out sample_rate);
            var sound_format =
                            channels == 1 && bits_per_ample == 8 ? ALFormat.Mono8 :
                            channels == 1 && bits_per_ample == 16 ? ALFormat.Mono16 :
                            channels == 2 && bits_per_ample == 8 ? ALFormat.Stereo8 :
                            channels == 2 && bits_per_ample == 16 ? ALFormat.Stereo16 :
                            (ALFormat)0;

            AL.BufferData(buffer, sound_format, sound_data, sound_data.Length, sample_rate);

            AL.Source(source, ALSourcei.Buffer, buffer);

            SoundList.Add(this);
        }

        /// <summary>
        /// リスナの位置を設定します。
        /// 方向と上方向ベクトルは固定です。
        /// </summary>
        /// <param name="pos"></param>
        public static void SetListener(Vector3 pos)
        {
            Vector3 listenerDirection = new Vector3(0.0f, 0.0f, -1.0f);
            Vector3 up = Vector3.UnitY;

            AL.Listener(ALListener3f.Position, ref pos);
            AL.Listener(ALListenerfv.Orientation, ref listenerDirection, ref up);
        }

        /// <summary>
        /// このサウンドを再生します
        /// </summary>
        public void Play()
        {
            if (source == -1) return;

            AL.GetError();
            AL.SourcePlay(source);
        }

        /// <summary>
        /// このサウンドを停止します
        /// </summary>
        public void Stop()
        {
            if (source == -1) return;

            AL.SourcePause(source);
        }

        /// <summary>
        /// このサウンドのクローンコピーを作ります
        /// </summary>
        /// <returns></returns>
        public Sound Clone()
        {
            Sound s = new Sound();

            s.source = source;
            s.Gain = Gain;
            s.Position = Position;
            s.Velocity = Velocity;

            return s;
        }

        /// <summary>
        /// 定期的に全てのサウンドの再生状態を調べます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WatchDogTimer_Tick(object sender, EventArgs e)
        {
            if (Sound.SoundList.Count == 0) return;

            foreach (Sound s in Sound.SoundList)
            {
                s.CheckAndStop();
            }
        }

        /// <summary>
        /// サウンドの再生状態を調べます。停止状態だった場合、そのサウンドのソースを停止させます
        /// </summary>
        public void CheckAndStop()
        {
            int state;
            AL.GetSource(source, ALGetSourcei.SourceState, out state);

            if ((ALSourceState)state != ALSourceState.Playing)
                AL.SourceStop(source);
        }

        /// <summary>
        /// このサウンドを削除します
        /// </summary>
        public void Dispose()
        {
            AL.DeleteSource(source);
            AL.DeleteBuffer(buffer);
        }

        /// <summary>
        /// サウンドクラスのオーディオコンテキストを削除します
        /// </summary>
        public static void DisposeAudioContext()
        {
            if (ac != null)
                ac.Dispose();
        }

        /// <summary>
        /// WAVEファイルを読み込みます
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="channels"></param>
        /// <param name="bits"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        private static byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // RIFF header
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                int riff_chunck_size = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                // WAVE header
                string format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string dummy = new string(reader.ReadChars(2));

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int data_chunk_size = reader.ReadInt32();

                channels = num_channels;
                bits = bits_per_sample;
                rate = sample_rate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }
    }
}
