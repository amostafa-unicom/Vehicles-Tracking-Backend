using AutoMapper;
using E_Vision.Core.Interfaces.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace E_Vision.Core.UseCases.Base
{
    public class BaseUseCase
    {
        #region Props
        public IConfiguration Configuration { get; set; }
        public IMapper Mapper { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        #endregion
        #region Ctor
        public BaseUseCase() { }

        /// <summary>
        /// Get Min Time in Minutes
        /// </summary>
        /// <returns></returns>
        public int GetRequestInterval()
        {
            int.TryParse(Configuration.GetSection("RequestInterval").Value, out int requestInterval);
            return requestInterval;
        }
        #endregion
    }
}
