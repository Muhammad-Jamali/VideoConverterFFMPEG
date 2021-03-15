using System;
using System.Diagnostics;
using System.IO;

namespace VideoConverterFFMPEG
{
    class Program
    {


        static void Main(string[] args)
        {
            while (true)
            {
                int option = 0, framesPerSecond = 0, exit = 0;
                ConvertVideoFormat convert = new ConvertVideoFormat();
                string path, videoName, format, folderName;

                Console.WriteLine("Video Converter FFMPEG \nSelect Options\n\n");
                Console.WriteLine("1: Video Conversion");
                Console.WriteLine("2: Video Convert to Frames");
                Console.WriteLine("3: Exit");
                Console.Write("Enter Option: ");

                try
                {
                    option = Int16.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid entry!");
                    Console.WriteLine(ex);
                }

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Enter path to video: ");
                        path = Console.ReadLine();

                        Console.Write("Enter video name with format: ");
                        videoName = Console.ReadLine();

                        Console.Write("Enter format you want to convert to: ");
                        format = Console.ReadLine();

                        if (format[0] == '.')
                        {
                            char[] newFormat = new char[format.Length - 1];
                            string formatWithOutDot = new string(newFormat);
                            for (int i = 1; i < format.Length; i++)
                            {
                                newFormat[i - 1] = format[i];
                            }

                            if (convert.ConvertVideo(path, videoName, formatWithOutDot))
                            {
                                Console.Clear();
                                Console.WriteLine("Video Converted Successfully!");
                                Console.WriteLine("Press any key to continue!");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            if (convert.ConvertVideo(path, videoName, format))
                            {
                                Console.Clear();
                                Console.WriteLine("Video Converted Successfully!");
                                Console.WriteLine("Press any key to continue!");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        break;

                    case 2:
                        Console.Clear();

                        Console.Write("Enter path to video: ");
                        path = Console.ReadLine();

                        Console.Write("Enter video name with format: ");
                        videoName = Console.ReadLine();

                        Console.Write("Enter image format you want to convert to: ");
                        format = Console.ReadLine();

                        Console.Write("Enter folder name or leave empty: ");
                        folderName = Console.ReadLine();

                        Console.Write("Enter number of frames per second: ");
                        try
                        {
                            framesPerSecond = Int16.Parse(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid entry!");
                            Console.WriteLine(ex);
                            break;
                        }

                        if (format[0] == '.')
                        {
                            char[] newFormat = new char[format.Length - 1];
                            string formatWithOutDot = new string(newFormat);
                            for (int i = 1; i < format.Length; i++)
                            {
                                newFormat[i - 1] = format[i];
                            }

                            if (convert.ConvertVideoToImage(path, videoName, format, folderName, framesPerSecond))
                            {
                                Console.Clear();
                                Console.WriteLine("Frames extracted Successfully!");
                                Console.WriteLine("Press any key to continue!");
                                Console.ReadKey();
                                Console.Clear();
                            }

                        }
                        else
                        {
                            if (convert.ConvertVideoToImage(path, videoName, format, folderName, framesPerSecond))
                            {
                                Console.Clear();
                                Console.WriteLine("Frames extracted Successfully!");
                                Console.WriteLine("Press any key to continue!");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        break;

                    case 3:
                        exit = 1;
                        break;

                    default:
                        break;
                }

                if (exit == 1)
                {
                    break;
                }
            }

        }


    }
}
