using MobilApp.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using TinyIoC;

namespace MobilApp.Services
{
    public class THService : ITHService
    {
        private readonly IGenericRepository _genericRepository;
        public THService()
        {
            _genericRepository = TinyIoCContainer.Current.Resolve<IGenericRepository>();
        }
    }
}
