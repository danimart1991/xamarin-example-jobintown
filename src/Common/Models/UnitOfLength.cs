namespace Models
{
    public class UnitOfLength
    {
        private static UnitOfLength _kilometers = new UnitOfLength(1.609344);
        private static UnitOfLength _nauticalMiles = new UnitOfLength(0.8684);
        private static UnitOfLength _miles = new UnitOfLength(1);

        private readonly double _fromMilesFactor;

        private UnitOfLength(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }

        public static UnitOfLength Kilometers => _kilometers;

        public static UnitOfLength NauticalMiles => _nauticalMiles;

        public static UnitOfLength Miles => _miles;

        public double ConvertFromMiles(double input)
        {
            return input * _fromMilesFactor;
        }
    }
}
