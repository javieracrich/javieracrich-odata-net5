using System;

namespace ODataWithNet5Demo.Models
{
    public record Student(int Id,
        string FirstName,
        string LastName,
        DateTime BirthDate,
        bool IsAdvanced,
        int Grade);

}
