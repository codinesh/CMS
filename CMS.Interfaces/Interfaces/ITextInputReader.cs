using CMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Shared.Interfaces
{
    public interface ITextInputReader
    {
        Task<IList<Talk>> LoadTalksAsync(string inputFileName);
    }
}
