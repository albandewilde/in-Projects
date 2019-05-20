
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace inProjects.WebApp.Services.CSV
{
    class CsvStudentMapping: CsvMapping<StudentList>
    {
        public CsvStudentMapping()
            : base()
        {
            MapProperty( 0, x => x.FirstName );
            MapProperty( 1, x => x.Name );
            MapProperty( 2, x => x.Mail );
            MapProperty( 3, x => x.Promotion );
        }

        public static void Test(IFormFile path)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions( true, ',' );
            CsvStudentMapping csvStudentMapping = new CsvStudentMapping();
            CsvParser<StudentList> csvParser = new CsvParser<StudentList>( csvParserOptions, csvStudentMapping );
            var a = path.OpenReadStream();



            var result = csvParser
                .ReadFromStream( a, Encoding.UTF8 )
                .ToList();

        }

    }

   
}
