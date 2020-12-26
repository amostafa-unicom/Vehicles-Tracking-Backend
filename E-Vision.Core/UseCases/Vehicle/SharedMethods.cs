using E_Vision.SharedKernel.FireBase;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace E_Vision.Core.UseCases.Vehicle
{
    public class SharedMethods
    {

        public void ChangeVehicleStatus(List<int> vehicleIds, string dbUrl,string eventUrl)
        {
            FirebaseDB firebaseDB = new FirebaseDB(dbUrl);
            firebaseDB = firebaseDB.Node(eventUrl);
            FirebaseResponse putResponse = firebaseDB.Put(JsonConvert.SerializeObject(vehicleIds));
           
        }
    }
}
