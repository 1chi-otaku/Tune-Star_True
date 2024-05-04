
using Tune_Star.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tune_Star.Models
{
    public interface ILangRead
    {
        List<Language> languageList();
    }
}
