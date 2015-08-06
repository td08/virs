using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace Virs_Client_Form
{
    class WavBuilder
    {
        static void generateWav(int[] rawData)
        {
            string writeFilePath = "test.wav";
            short[] processedData = processRawData(rawData);
            int numsamples = processedData.Length;  
            ushort numchannels = 1; // number of channels used in audio data
            ushort samplelength = 2; // sample size in bytes (using 12-bit adc)
            uint samplerate = 16000; // number of samples per second

            FileStream fs = new FileStream(writeFilePath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            // BEGIN HEADER
            // 8 bytes
            bw.Write(Encoding.ASCII.GetBytes("RIFF"));  // RIFF
            bw.Write((int)(36 + numsamples * numchannels * samplelength)); // Filesize - 8 (Int32 + RIFF), also total size of file in bytes starting with WAVEfmt portion of header
            // 28 bytes for WAVEfmt header section, 8 bytes for data and size of data header section = 36 bytes

            // 28 bytes
            bw.Write(Encoding.ASCII.GetBytes("WAVEfmt "));  // WAVEfmt (8)
            bw.Write(16);   // size of waveformat data (4) int
                bw.Write((ushort)1);    // wFormatTag (2) ushort
                bw.Write(numchannels);  // nChannels (2) ushort
                bw.Write(samplerate);   // sample rate (4) uint
                bw.Write(samplerate * samplelength * numchannels);  // nAvgBytesPerSec (4) uint
                bw.Write((ushort)(samplelength * numchannels));   // nBlockAlign (2) ushort
                bw.Write((ushort)(8 * samplelength));   // wBitsPerSample (2) ushort
            
            // 8 bytes
            bw.Write(Encoding.ASCII.GetBytes("data"));  // data (4)
            bw.Write((int)(numsamples * samplelength));    // length of data in bytes (4) int
            // END HEADER

            // write rest of audio data
            for (int i = 0; i < numsamples; i++)
            {
                bw.Write(processedData[i]);
            }
            bw.Close();
            fs.Close();

            //SoundPlayer player = new SoundPlayer(writeFilePath);
            //player.Play();
            //Console.ReadKey();
        }

        private static short[] processRawData(int[] rawData)
        {
            int[] rawarray = rawData;

            //StreamReader reader = new StreamReader(@"C:\Users\Trevor\Documents\GitHub\virs\Audio_Playback\WAV_Test\steth3.txt");

            //// read values into array
            //int i = 0;
            //while (!reader.EndOfStream)
            //{
            //    string value = reader.ReadLine();
            //    //Console.WriteLine(value);
            //    rawarray[i++] = Convert.ToInt32(value);
            //}

            // find average value
            int sum = 0;
            foreach (int s in rawarray)
            {
                sum += s;
            }
            int average = sum / rawarray.Length;

            // center waveform at zero by subtracting average
            int[] averagedArray = new int[rawarray.Length];
            int i = 0;
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
