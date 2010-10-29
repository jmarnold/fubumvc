using FubuValidation.Strategies;

namespace FubuValidation
{
    public class EmailAttribute : FieldMarkerAttribute
    {
        public EmailAttribute()
            : base(typeof(EmailFieldStrategy))
        {
        }
    }
}