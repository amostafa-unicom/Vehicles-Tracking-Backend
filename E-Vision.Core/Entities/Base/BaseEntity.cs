using System;
using static E_Vision.SharedKernel.Enum.SharedKernelEnums;

namespace E_Vision.Core.Entities
{
    public class BaseEntity
    {
        #region Properties 
        public int Id { get; set; }
        public byte IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        #endregion
        #region Constructor
        internal BaseEntity()
        {
            IsDeleted = (byte) DeleteStatus.NotDeleted;
        }
        #endregion
    }
}
