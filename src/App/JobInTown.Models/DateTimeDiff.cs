using JobInTown.Models.Enums;

namespace JobInTown.Models
{
    public class DateTimeDiff
    {
        private int _number;
        private DateTimeDiffFormatType _format;

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public DateTimeDiffFormatType Format
        {
            get { return _format; }
            set { _format = value; }
        }
    }
}
