using System.Data.Entity;
using Hsr.Data;
using Hsr.Models;

namespace Hsr
{
    public class PublicDataContext : BaseObjectContext
    {
        public IDbSet<Ryan> Ryans { get; set; }
        public IDbSet<Hsr.Models.Test> Test { get; set; }
    }
}