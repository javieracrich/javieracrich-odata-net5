using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataWithNet5Demo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODataWithNet5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        [EnableQuery]
        public async Task<IEnumerable<Student>> GetStudents() =>
            //simulate async db call
            await Task.FromResult(new List<Student>()
            {
                new Student(1,"Josh","McCall",new DateTime(1990,7,15),true,95),
                new Student(2,"Kailu","Hu",new DateTime(1984,7,27),false,60),
                new Student(3,"Mayra","Stephenson",new DateTime(1977,1,1),true,87),
                new Student(4,"Winston","Smith",new DateTime(1984,1,27),true,100),
                new Student(5,"Sophia","Willams",new DateTime(2000,2,8),false,99),
                new Student(6,"Jacob","Mason",new DateTime(2002,1,2),true,55),
                new Student(7,"Olivia","Macklemore",new DateTime(2001,9,11),false,23)
            });

    }
}
