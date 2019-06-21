using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OfficeOpenXml.Style;

namespace inProjects.WebApp.Services.Excel
{
    class ExcelUtilis
    {

        public ExcelUtilis()
        {

        }


        public async  Task<bool> CreateExcel( List<Data.Data.ProjectStudent.ProjectForumResultData> allProjectsForumResult )
        {
            using( ExcelPackage excel = new ExcelPackage() )
            {
                excel.Workbook.Worksheets.Add( "ResultatsFP" );

                List<string[]> firstRowContents = new List<string[]>() { new string[] { "RESULTAT FORUM PROJET INFORMATIQUE" } };
                List<string[]> secondRowContents = new List<string[]>() { new string[] { "Stand", "Projet informatique", "Semestre Et Filiere", "Les totaux ne correspondent pas à l'ordre des passages","","","Total","" }};

                // Determine the header range (e.g. A1:D1)
                string firstRow = "A1:" + Char.ConvertFromUtf32( secondRowContents[0].Length + 64 ) + "1";
                string secondRow = "A2:" + Char.ConvertFromUtf32( secondRowContents[0].Length + 64 ) + "2";
                string column7 = "G2:G" + (allProjectsForumResult.Count + 2);
                string gradeRow = "D3:G" + (allProjectsForumResult.Count + 2);
                string contents = "A3:H" + (allProjectsForumResult.Count + 2);
                string moyenneTotText = "D" + (allProjectsForumResult.Count + 3) +":G" + (allProjectsForumResult.Count + 3);
                string lastRow = "D" + (allProjectsForumResult.Count + 3) +":F" + (allProjectsForumResult.Count + 3);


                // Target a worksheet
                var worksheet = excel.Workbook.Worksheets["ResultatsFP"];

                worksheet.Cells["A1:H1"].Merge = true;
                worksheet.Cells["D2:F2"].Merge = true;
                worksheet.Cells[lastRow].Merge = true;

                //Set Style excel
                worksheet.Cells[firstRow].Style.Font.Bold = true;
                worksheet.Cells[firstRow].Style.Font.Size = 20;
                worksheet.Cells[firstRow].Style.Font.Color.SetColor( Color.Green );

                worksheet.Cells["D2:F2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["D2:F2"].Style.Fill.BackgroundColor.SetColor( Color.Yellow );
                worksheet.Cells["D2:F2"].Style.Font.Bold = true;
                worksheet.Cells["D2:F2"].Style.Border.BorderAround( ExcelBorderStyle.Thin, Color.Black );

                worksheet.Cells[moyenneTotText].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[moyenneTotText].Style.Fill.BackgroundColor.SetColor( Color.Yellow );
                worksheet.Cells[moyenneTotText].Style.Font.Bold = true;
                worksheet.Cells[moyenneTotText].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[moyenneTotText].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                worksheet.Cells[column7].Style.Font.Color.SetColor(Color.Green );
                worksheet.Cells[column7].Style.Font.Bold = true;

                worksheet.Cells[gradeRow].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[gradeRow].Style.Border.Left.Color.SetColor( Color.Purple );
                worksheet.Cells[gradeRow].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[gradeRow].Style.Border.Right.Color.SetColor( Color.Purple );
                worksheet.Cells[gradeRow].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[gradeRow].Style.Border.Top.Color.SetColor( Color.Purple );
                worksheet.Cells[gradeRow].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[gradeRow].Style.Border.Bottom.Color.SetColor( Color.Purple );

                worksheet.Cells[contents].Style.Font.SetFromFont( new Font( "Calibri", 13 ) );

                worksheet.Cells["A2:G" + (allProjectsForumResult.Count + 2)].Style.WrapText = true;
                worksheet.Cells["A2:G" + (allProjectsForumResult.Count + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:G" + (allProjectsForumResult.Count + 2)].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                string formula = "=ROUND(MOYENNE(G3:G" + (allProjectsForumResult.Count + 2) + "),2)";
                //Set Math formule
                worksheet.Cells["G" + (allProjectsForumResult.Count + 3)].Formula = formula;


                List<object[]> cellData = new List<object[]>();
                int idx = 0;
                int formulaIdx = 3;
                foreach( var item in allProjectsForumResult )
                {
                    idx++;
                    object[] project = new object[8];
                    project[0] = idx;
                    project[1] = item.Name;
                    project[2] = "S05";
                    
                    int count = 3;
                    int numberJury = 0;
                    foreach( var indivGrade in item.IndividualGrade )
                    {
                        project[count] = indivGrade.Value;
                        count++;
                        numberJury++;
                    }

                    if(count != 6 )
                    {
                        for( int i = count; i < 6; i++ )
                        {
                            project[count] = "";
                        }
                    }

                    project[6] = "";
                    project[7] = idx;
                    cellData.Add( project );

                    formula = "=ROUND(((D" + formulaIdx + "+E" + formulaIdx + "+F" + formulaIdx + ")/" + numberJury + "),2)";
                    worksheet.Cells["G" + formulaIdx].Formula = formula;
                    formulaIdx++;

                }


                // Set text on the cells
                worksheet.Cells[firstRow].LoadFromArrays( firstRowContents );
                worksheet.Cells[secondRow].LoadFromArrays( secondRowContents );
                worksheet.Cells[3, 1].LoadFromArrays( cellData );
                worksheet.Cells["D" + (allProjectsForumResult.Count + 3)].LoadFromText("Moyenne FPI");

                FileInfo excelFile = new FileInfo( @"C:\Users\DCHIC\Desktop\test.xlsx" );
                excel.SaveAs( excelFile );
            }

            return true;

        }

    }
}
