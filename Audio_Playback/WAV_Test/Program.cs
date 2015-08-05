using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace WAV_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            short[] myArray = readFile();
            int numsamples = myArray.Length;  //44100;
            ushort numchannels = 1;
            ushort samplelength = 2; // in bytes
            uint samplerate = 16000;

            FileStream f = new FileStream("test.wav", FileMode.Create);
            BinaryWriter wr = new BinaryWriter(f);
            // 8 bytes
            wr.Write(Encoding.ASCII.GetBytes("RIFF"));  // RIFF
            wr.Write((int)(36 + numsamples * numchannels * samplelength)); // Filesize - 8 (Int32 + RIFF)
            // 36 + (160001 * 1 * 2) = 320038

            // 28 bytes
            wr.Write(Encoding.ASCII.GetBytes("WAVEfmt "));  // WAVEfmt (8)
            wr.Write(16);   // size of waveformat data (4) int
                wr.Write((ushort)1);    // wFormatTag (2) ushort
                wr.Write(numchannels);  // nChannels (2) ushort
                wr.Write(samplerate);   // sample rate (4) uint
                wr.Write(samplerate * samplelength * numchannels);  // nAvgBytesPerSec (4) uint
                wr.Write((ushort)(samplelength * numchannels));   // nBlockAlign (2) ushort
                wr.Write((ushort)(8 * samplelength));   // wBitsPerSample (2) ushort
            
            // 8
            wr.Write(Encoding.ASCII.GetBytes("data"));  // data (4)
            wr.Write((int)(numsamples * samplelength));    // length of data in bytes (4) int

            for (int i = 0; i < numsamples; i++)
            {
                //wr.Write((byte)((a.sample(t) + (samplelength == 1 ? 128 : 0)) & 0xff));
                //wr.Write((byte)((32760 * (int)Math.Sin(t * i)) & 0xff));
                wr.Write(myArray[i]);
            }
            wr.Close();
            f.Close();
            SoundPlayer player = new SoundPlayer("test.wav");
            player.Play();
            Console.ReadKey();
            //SoundPlayer player = new SoundPlayer(@"C:\Users\Trevor\Documents\GitHub\virs\Audio_Playback\WAV_Test\steth.wav");
            //player.Play();
            //Console.ReadKey();
        }

        private static short[] readFile()
        {
            int[] rawarray = new int[160001];
            StreamReader reader = new StreamReader(@"C:\Users\Trevor\Documents\GitHub\virs\Audio_Playback\WAV_Test\steth3.txt");

            // read values into array
            int i = 0;
            while (!reader.EndOfStream)
            {
                string value = reader.ReadLine();
                //Console.WriteLine(value);
                rawarray[i++] = Convert.ToInt32(value);
            }

            // find average value
            int sum = 0;
            foreach (int s in rawarray)
            {
                sum += s;
            }
            int average = sum / rawarray.Length;

            // center waveform at zero by subtracting average
            int[] averagedArray = new int[rawarray.Length];
            i = 0;
            foreach (int s in rawarray)
            {
                averagedArray[i++] = s - average;
            }



            /////////////////////////////////
            int max = 0;
            foreach (int s in averagedArray)
            {
                if (Math.Abs(s) > max)
                    max = s;
            }
            i = 0;
            
            int averages = 50;
            int[] circqueue = new int[averages];
            int j = 0;
            int tempavg = 0;
            StreamWriter sw = new StreamWriter("temp.txt");
            foreach (int s in averagedArray)
            {
            	circqueue[j++] = averagedArray[i];
                if(j == averages)
                	j = 0;
                	
                tempavg = 0;
                for (int k = 0; k < averages; k++)
                {
                    tempavg += circqueue[k];
                }
                
                averagedArray[i++] = tempavg / averages;
                sw.WriteLine(averagedArray[i - 1].ToString());
            }
            sw.Close();            
            i = 0;
            foreach (int s in averagedArray)
            {
                averagedArray[i++] = (int)(((float)s) / ((float)max) * 32000.0f);
            }
            /////////////////////////////////

            // convert to array of shorts
            short[] final = new short[averagedArray.Length];
            
            i = 0;
            foreach (int s in averagedArray)
            {               
                final[i] = Convert.ToInt16(averagedArray[i++]);
            }
            return final;
        }
    }
}
