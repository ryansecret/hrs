using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Hsr.Core.Infrastructure;
using Hsr.Models;
using Hsr.Models.ViewModel;

namespace Hsr.App_Start
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public int Order { get { return 0; }}
        public void Execute()
        {
            //Mapper.CreateMap<ControllerFilterDataVm, ControllerFilterData>();
        }
    }
}