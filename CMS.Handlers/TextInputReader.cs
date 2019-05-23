using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CMS.Model;
using CMS.Shared.Interfaces;

namespace CMS.Core
{
    public class TextInputReader : ITextInputReader
    {
        public async Task<IList<Talk>> LoadTalksAsync(string inputFileName)
        {
            Regex talkDurationRegex = new Regex(@"lightning$|\d+?$");
            //var fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string[] lines = await File.ReadAllLinesAsync(inputFileName);

            foreach (var talk in lines)
            {
                var duration = talkDurationRegex.Match(talk);
            }

            return new List<Talk>();
        }
    }
}
