using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace VideoConverterFFMPEG
{
    class ConvertVideoFormat
    {
        private ProcessStartInfo startInfo = new ProcessStartInfo();
        public bool ConvertVideo(string path, string videoName, string format)
        {
            Console.Clear();
            startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg\\ffmpeg.exe"),
                Arguments = "-i \"" + path + "\\" + videoName + "\" \"" + path + "\\output." + format + "\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            try
            {
                using (Process process = Process.Start(startInfo))
                {
                    string output = null;
                    while (!process.StandardError.EndOfStream)
                    {
                        output = process.StandardError.ReadLine();
                        Console.WriteLine(output);
                    }

                    process.WaitForExit();
                    if (process.ExitCode == 1)
                    {
                        Console.Clear();
                        Console.WriteLine(output);
                        Console.WriteLine("\nVideo Conversion not successful!");
                        Console.ReadKey();
                        Console.Clear();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Console.WriteLine("Video Conversion not successful!");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
            return true;
        }

        public bool ConvertVideoToImage(string path, string videoName, string format, string folderName, int framesPerSecond)
        {
            Console.Clear();
            string dir = path + "\\frames";

            if (folderName.Length > 0)
            {
                dir = path + "\\" + folderName;
            }

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg\\ffmpeg.exe"),
                Arguments = "-i \"" + path + "\\" + videoName + "\" -r " + framesPerSecond + "/1 \"" + dir + "\\frame%03d." + format + "\"",
                RedirectStandardInput = true,
                RedirectStandardError = true
            };


            try
            {
                using (Process process = Process.Start(startInfo))
                {
                    string output = null;
                    while (!process.StandardError.EndOfStream)
                    {
                        output = process.StandardError.ReadLine();
                        Console.WriteLine(output);
                    }

                    process.WaitForExit();
                    if (process.ExitCode == 1)
                    {
                        Console.Clear();
                        Console.WriteLine(output);
                        Console.WriteLine("\nFrames not extracted successfully");
                        Console.ReadKey();
                        Console.Clear();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                Console.WriteLine("Frames not extracted successfully");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
            return true;
        }


    }
}
