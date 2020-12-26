namespace E_Vision.SharedKernel.Enum
{
    public class SharedKernelEnums
    {
        public enum SortDirection
        {
            Ascending = 0,
            Descending = 1
        }
        public enum TokenInfo
        {
            UserId = 1,
            UserScope = 2,
            UserType = 3
        }
        public enum Audiance
        {
            Client = 1,
            Technician = 2,
            Vendor = 3,
            Service = 4,
            Web = 4
        }
        public enum UserActivationStatus
        {
            InActive = 0,
            Active = 1
        }
        public enum UserVerifiedStatus
        {
            NotVerified = 0,
            IsVerified = 1
        }
        public enum UserType
        {
            Client = 1,
            Technician = 2,
            Vendor = 3
        }

        public enum LocalizationType
        {
            Messages = 1
        }
        public enum Language
        {
            en = 1,
            ar = 2
        }
        public enum DeleteStatus
        {
            NotDeleted = 0,
            Deleted = 1
        }

        public enum ForgetPasswordKey
        {
            Email = 0,
            Phone = 1
        }

        public enum UserSearchBy
        {
            UserId = 0,
            Email = 1,
            Phone = 2
        }

        public enum ContentType
        {
            CarBrand = 1,
            UserCar = 2,
            UserId = 3,
            Service = 4,
            Company=5,
            Category=6,
            OrderAttachment=7
        }

        public enum OrderStatus
        {
            Confirmed = 1,
            Inprogress = 2,
            MeetTheTechnician = 3,
            Completed = 4
        }

        public enum PaymentType
        {
            Cash = 1,
            Credit = 2,
            ApplePay = 3
        }

        public enum ProviderStatus
        {
            NotAvailable = 0,
            Available = 1,
            Busy = 2
        }
        public enum OrderAttachmentType
        {
            BeforeFixing=0,
            AfterFixing=1
        }
    }
}
