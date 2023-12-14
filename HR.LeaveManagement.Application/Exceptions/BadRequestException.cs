using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }

        //ValidationResult - yra FluentValidation.Results bibliotekos dalis.
        //kuris tikrina pritaikytu taisykliu rezultata. 
        //sukuriamas naujas List tam, kad butu galima sudeti visus error i list
        // ir tada ji perziureti, kas buvo negerai. 
        //validationResult.Errors - nurodo, kad reikes deti butent Error
        //error.ErrorMessage - nurodo, kad deti tik erroro messages.
        public BadRequestException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = validationResult.ToDictionary();
        }

        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }
}
