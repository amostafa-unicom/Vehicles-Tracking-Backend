using System.Collections.Generic;

namespace E_Vision.Core.Helper
{
    public static class ConnectionManagment
    {
        #region Props and Fields

        // Volatile dictionary object hold customers with it's vechicles
        public static volatile Dictionary<int, int> _Conncections; 
        #endregion

        #region Ctor
        static ConnectionManagment()
        {
            _Conncections = new Dictionary<int, int>();
        } 
        #endregion
    }
}
