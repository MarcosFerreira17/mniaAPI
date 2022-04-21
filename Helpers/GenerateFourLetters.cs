using System;

namespace mniaAPI.Helpers
{
    public static class GenerateFourLetters
    {
        public static string Generate(string characters)
        {
            var transformChar = characters.Replace(" ", "").ToUpper();

            var Charsarr = new char[4];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = transformChar[random.Next(transformChar.Length)];
            }

            var resultString = new String(Charsarr);
            return resultString;
        }

    }
}