using System.ComponentModel.DataAnnotations;

namespace Services.Helpers;

public class ValidationHelper
{
    /// <summary>
    /// Persons model validationContext and throws ArgumentException in case
    /// of any validation errors
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="ArgumentException">When one or more validation errors found</exception>

    internal static void ModelValidation(object obj)
    {
        // Model validation
        ValidationContext validationContext = new ValidationContext(obj);
        List<ValidationResult> validationResults = new List<ValidationResult>();
        
        // Validate the model object and get errors
        bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults);

        if (!isValid)
        {
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}