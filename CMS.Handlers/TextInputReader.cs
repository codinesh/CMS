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
            Regex talkDurationRegex = new Regex(@"(lightning)$|(\d+)?.{3}$");
            string[] lines = await File.ReadAllLinesAsync(inputFileName);
            List<Talk> talks = new List<Talk>();
            foreach (var talk in lines)
            {
                var match = talkDurationRegex.Match(talk);
                if (match.Success)
                {
                    var duration = 0;

                    var isLightningTalk = match.Groups[1].Success;
                    if (isLightningTalk)
                    {
                        duration = 5;
                    }
                    else
                    {
                        duration = Convert.ToInt16(match.Groups[2].Value);
                    }
                    talks.Add(new Talk(talk, duration));
                }
                else {
                    throw new InvalidInputException();
                }
            }

            return talks;
        }
    }
}
