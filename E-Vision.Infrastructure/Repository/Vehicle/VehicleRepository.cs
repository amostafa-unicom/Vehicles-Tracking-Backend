using E_Vision.Core.Interfaces.Repository.Vehicle;
using E_Vision.Infrastructure.Context;
using E_Vision.Infrastructure.Repository.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vision.Infrastructure.Repository.Vehicle
{
    public class VehicleRepository : BaseRepository<Core.Entities.Vehicle>, IVehicleRepository
    {
        DbSet<Core.Entities.Vehicle> entity;
        private AppDbContext _context;
        public VehicleRepository(AppDbContext context) : base(context)
        {
            entity = context.Set<Core.Entities.Vehicle>();
            _context = context;
        }

        public async Task<List<int>> GetDisconnectedVehicle(int second)
        {
            List<int> vehiclesId = new List<int>();
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@TotalSeccond", second) ,
                new SqlParameter(){ ParameterName = "@Res", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.NVarChar,Size=3000}
                };

            try
            {
                var blogs = await _context.Database.ExecuteSqlRawAsync("EXEC dbo.SP_DiconnectedVehicles @TotalSeccond,@Res output ", sqlParameters.ToArray());
                var res = Convert.ToString(sqlParameters[1].Value).Split(',').ToList();
                res.ForEach(id => { if (int.TryParse(id, out int i)) vehiclesId.Add(i); });
            }
            catch (System.Exception ex)
            {
                throw;
            }
            return vehiclesId;
        }
    }
}
