using NAudio.Wave;
using System.Timers;

namespace TimerV2
{
    internal static class Program

    {
        static int counter = 0;


        public static T GetFromConsole<T>(string prompt, Func<T, bool>? validator = null) where T : IParsable<T>
        {
            while (true)
            {
                Console.Write(prompt);
                if (T.TryParse(Console.ReadLine(), null, out var value))
                {
                    if (validator?.Invoke(value) ?? true)
                    {
                        return value;
                    }

                    Console.WriteLine("Invalid value, try again.");
                    continue;
                }

                Console.WriteLine("Invalid format, try again.");
            }
        }
        static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(timedEvent);

            int interval = GetFromConsole<int>("Enter your interval: ");

            timer.Interval = interval;

            // while (timer.Interval <= 1) ;
            //{
            // timer.Enabled = false;
            //  Console.WriteLine("Please enter the duration of the timer in seconds");
            //  timer.Interval = Convert.ToInt64(Console.ReadLine());
            //}
            timer.Start();
            Console.WriteLine("Press \'t\' to terminate the timer.");

            while (true)
            {
                if ((Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.T) || counter >= 5)
                {
                    timer.Stop(); 
                    break;
                }

                
                
                
            }
            
        }
        private static void timedEvent(object source, ElapsedEventArgs e)
        //Sound file should be accessed an played within this method
        {
            Console.WriteLine("Sound should be playing now");
            using (var audioFile = new AudioFileReader("alarm.wav"))
            using (var outputDevice = new WaveOutEvent())

            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Sound had finished playing");
                counter++;

            }


        }


    }
    
}