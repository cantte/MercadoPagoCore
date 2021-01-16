using MercadoPagoCore.Client.Common;

namespace MercadoPagoCore.Client.Preference
{
    /// <summary>
    /// Passenger info.
    /// </summary>
    public class PreferencePassengerRequest
    {
        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Identification.
        /// </summary>
        public IdentificationRequest Identification { get; set; }
    }
}
