using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Utility
{
    public static class SD
    {
        public const string ROOM_AVAILABLE = "Available";
        public const string ROOM_OCCUPIED = "Occupied";
        public const string ROOM_MAINTENANCE = "Under Maintenance";

        public const string ROLE_CLIENT = "Client";
        public const string ROLE_ADMIN = "Admin";

        public const string STATUS_APPROVED = "Approved";
        public const string STATUS_CANCELLED = "Cancelled";
        public const string STATUS_PENDING = "Pending";
        public const string STATUS_REFUNDED = "Refunded";
        public const string STATUS_COMPLETED = "Completed";
    }
}
