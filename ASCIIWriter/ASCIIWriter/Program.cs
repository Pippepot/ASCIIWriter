using System;
using System.Drawing;

namespace ASCIIWriter
{
	class Program
	{
		static string path;
		static int pixelInterval = 8;
		static double brightnessMultiplier = 1;

		static void Main()
		{
			Console.Title = "ASCII Writer";

			// The pictures look better when the background is white and the text is black
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;

			Console.WriteLine("To create an image, type the following syntax:");
			Console.WriteLine("path; pixel interval; brightness multiplier");
			Console.WriteLine("The default values are: pixel interval = {0}, brightness multiplier = {1}", pixelInterval, brightnessMultiplier);
			Console.WriteLine(@"Example: C:\Users\User\Pictures\FunnyCat.png; 8; 1");
			Console.WriteLine("");


			string input = Console.ReadLine();
			string[] inputSplit = input.Split(';');

			path = inputSplit[0];

			if (inputSplit.Length > 1 && int.TryParse(inputSplit[1], out int intervalOut))
			{
				pixelInterval = intervalOut;
			}

			if (inputSplit.Length > 2 && double.TryParse(inputSplit[2], out double brightnessOut))
			{
				brightnessMultiplier = brightnessOut;
			}


			try
			{
				ConvertToText();
			}
			catch (Exception e)
			{

				Console.WriteLine(e);
				Main();
			}
		}

		static void ConvertToText()
		{
			Bitmap bmp = (Bitmap)Image.FromFile(path);
			string WrittenLine = "";
			for (int y = 0; y < bmp.Size.Height - (bmp.Size.Height % pixelInterval); y += pixelInterval)
			{
				for (int x = 0; x < bmp.Size.Width; x++)
				{
					if (x % pixelInterval == 0 || x % pixelInterval == 1) // The character height-width-ratio is approximately 2/1
					{
						WrittenLine += GetSymbolFromBrightness(bmp.GetPixel(x, y).GetBrightness() * brightnessMultiplier);
					}
				}

				Console.WriteLine(WrittenLine);
				WrittenLine = "";
			}

			Main();
		}

        static string GetSymbolFromBrightness(double brightness)
        {
            switch ((int)(brightness*10))
            {
				case 0:
					return "@";
				case 1:
					return "$";
				case 2:
					return "#";
				case 3:
					return "*";
				case 4:
					return "!";
				case 5:
					return "+";
				case 6:
					return ":";
				case 7:
					return "~";
				case 8:
					return "-";
				case 9:
					return ".";
				default:
					return " ";
			}
        }
    }
}

