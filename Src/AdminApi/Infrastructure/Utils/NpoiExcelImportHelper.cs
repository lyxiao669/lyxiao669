
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Infrastructure
{
    public class NpoiExcelImportHelper
    {
        private static NpoiExcelImportHelper _excelImportHelper;

        public static NpoiExcelImportHelper _
        {
            get => _excelImportHelper ?? (_excelImportHelper = new NpoiExcelImportHelper());
            set => _excelImportHelper = value;
        }
        public List<DataTable> ExcelToDataTableList(Stream stream, string filePath, int sheetPage, out bool isSuccess, out string resultMsg)
        {
            isSuccess = false;
            resultMsg = "Excel文件流成功转化为DataTable数据源";
            var excelToDataTable = new List<DataTable>();
            var list=System.IO.Path.GetExtension(filePath);
            try
            {
                IWorkbook workbook;
                //获取文件类型
                workbook = GetExcelType(stream, list);


                for (int startRow = 0; startRow < sheetPage; startRow++)
                {
                    var table =new DataTable();
                    table = GetDataTable(startRow,table,workbook);
                    excelToDataTable.Add(table);
                }

                isSuccess = true;
            }
            catch (Exception e)
            {
                resultMsg = e.Message;
                throw new ServiceException(resultMsg);
            }

            return excelToDataTable;
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IWorkbook GetExcelType(Stream stream,string type)
        {
            IWorkbook workbook;
            switch (type)
            {
                //.XLSX是07版(或者07以上的)的Office Excel
                case ".xlsx":
                    workbook = new XSSFWorkbook(stream);
                    break;
                //.XLS是03版的Office Excel
                case ".xls":
                    workbook = new HSSFWorkbook(stream);
                    break;
                default:
                    throw new Exception("Excel文档格式有误");
            }
            return workbook;
        }
        /// <summary>
        /// 获得DataTable
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="table"></param>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(int startRow,DataTable table,IWorkbook workbook)
        {
            var sheet = workbook.GetSheetAt(startRow);
            foreach (var cell in sheet.GetRow(0).Cells)
            {
                var coulum = table.Columns;
                var cells = cell.CellType == CellType.Numeric ? typeof(double) : typeof(string);
                coulum.Add(new DataColumn(cell.StringCellValue, cells));

            }
            for (int i = 1; i < sheet.PhysicalNumberOfRows; i++)
            {
                var row = table.NewRow();
                var cells = sheet.GetRow(i)?.Cells;
                if (cells == null || cells.All(a => string.IsNullOrEmpty(a.ToString())))
                {
                    continue;
                }
                foreach (var cell in cells)
                {
                    var columnIndex = cell.ColumnIndex;
                    if (columnIndex >= table.Columns.Count)
                    {
                        continue;
                    }
                    if (cell.CellType == CellType.Numeric)
                    {
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            row[columnIndex] = cell.DateCellValue;
                        }
                        else
                        {
                            row[columnIndex] = cell.NumericCellValue;
                        }
                    }
                    else if (cell.CellType == CellType.Formula)
                    {
                        row[columnIndex] = cell.CellFormula;
                    }
                    else
                    {
                        row[columnIndex] = cell.StringCellValue;
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }

}





///// <summary>
///// 读取excel表格中的数据,将Excel文件流转化为dataTable数据源  
///// 默认第一行为标题 
///// </summary>
///// <param name="stream">excel文档文件流</param>
///// <param name="filePath">文件地址</param>
///// <param name="sheetIndex">表格索引</param>
///// <param name="isSuccess">是否转化成功</param>
///// <param name="resultMsg">转换结果消息</param>
///// <returns></returns>
//public DataTable ExcelToDataTable(Stream stream, string filePath, int sheetIndex, out bool isSuccess, out string resultMsg)
//{
//    isSuccess = false;
//    resultMsg = "Excel文件流成功转化为DataTable数据源";
//    var excelToDataTable = new DataTable();
//    string[] list = filePath.Split(".");

//    string text = list[list.Length - 1];
//    try
//    {
//        //Workbook对象代表一个工作簿,首先定义一个Excel工作薄
//        IWorkbook workbook;

//        //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
//        #region 判断Excel版本
//        switch (text)
//        {
//            //.XLSX是07版(或者07以上的)的Office Excel
//            case "xlsx":
//                workbook = new XSSFWorkbook(stream);
//                break;
//            //.XLS是03版的Office Excel
//            case "xls":
//                workbook = new HSSFWorkbook(stream);
//                break;
//            default:
//                throw new Exception("Excel文档格式有误");
//        }
//        #endregion
//        var sheet = workbook.GetSheetAt(sheetIndex);
//        var rows = sheet.GetRowEnumerator();

//        var headerRow = sheet.GetRow(0);
//        int cellCount = headerRow.LastCellNum;//最后一行列数（即为总列数）

//        //获取第一行标题列数据源,转换为dataTable数据源的表格标题名称
//        for (var j = 0; j < cellCount; j++)
//        {
//            var cell = headerRow.GetCell(j);
//            excelToDataTable.Columns.Add(cell.ToString());
//        }

//        //获取Excel表格中除标题以为的所有数据源，转化为dataTable中的表格数据源
//        for (var i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
//        {
//            var dataRow = excelToDataTable.NewRow();

//            var row = sheet.GetRow(i);

//            if (row == null) continue; //没有数据的行默认是null　

//            for (int j = row.FirstCellNum; j < cellCount; j++)
//            {
//                if (row.GetCell(j) != null)//单元格内容非空验证
//                {
//                    #region NPOI获取Excel单元格中不同类型的数据
//                    //获取指定的单元格信息
//                    var cell = row.GetCell(j);
//                    switch (cell.CellType)
//                    {
//                        //首先在NPOI中数字和日期都属于Numeric类型
//                        //通过NPOI中自带的DateUtil.IsCellDateFormatted判断是否为时间日期类型
//                        case CellType.Numeric when DateUtil.IsCellDateFormatted(cell):
//                            dataRow[j] = cell.DateCellValue;
//                            break;
//                        case CellType.Numeric:
//                            //其他数字类型
//                            dataRow[j] = cell.NumericCellValue;
//                            break;
//                        //空数据类型
//                        case CellType.Blank:
//                            dataRow[j] = "";
//                            break;
//                        //公式类型
//                        case CellType.Formula:
//                            {
//                                HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(workbook);
//                                dataRow[j] = eva.Evaluate(cell).StringValue;
//                                break;
//                            }
//                        //布尔类型
//                        case CellType.Boolean:
//                            dataRow[j] = row.GetCell(j).BooleanCellValue;
//                            break;
//                        ////错误
//                        //case CellType.Error:
//                        //    dataRow[j] = HSSFErrorConstants.GetText(row.GetCell(j).ErrorCellValue);
//                        //    break;
//                        //其他类型都按字符串类型来处理（未知类型CellType.Unknown，字符串类型CellType.String）
//                        default:
//                            dataRow[j] = cell.StringCellValue;
//                            break;
//                    }
//                    #endregion
//                }
//            }
//            excelToDataTable.Rows.Add(dataRow);
//        }

//        isSuccess = true;
//    }
//    catch (Exception e)
//    {
//        resultMsg = e.Message;
//    }

//    return excelToDataTable;
//}

//public List<DataTable> ExcelToDataTableLists(Stream stream, string filePath, int sheetPage, out bool isSuccess, out string resultMsg)
//{
//    isSuccess = false;
//    resultMsg = "Excel文件流成功转化为DataTable数据源";
//    var excelToDataTable = new List<DataTable>();
//    string[] list = filePath.Split(".");

//    string text = list[list.Length - 1];
//    try
//    {
//        //Workbook对象代表一个工作簿,首先定义一个Excel工作薄
//        IWorkbook workbook;

//        //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
//        #region 判断Excel版本
//        switch (text)
//        {
//            //.XLSX是07版(或者07以上的)的Office Excel
//            case "xlsx":
//                workbook = new XSSFWorkbook(stream);
//                break;
//            //.XLS是03版的Office Excel
//            case "xls":
//                workbook = new HSSFWorkbook(stream);
//                break;
//            default:
//                throw new Exception("Excel文档格式有误");
//        }
//        #endregion


//        for (int s = 0; s < sheetPage; s++)
//        {
//            var sheet = workbook.GetSheetAt(s);
//            var rows = sheet.GetRowEnumerator();

//            var headerRow = sheet.GetRow(0);
//            int cellCount = headerRow.LastCellNum;//最后一行列数（即为总列数）

//            //获取第一行标题列数据源,转换为dataTable数据源的表格标题名称
//            for (var j = 0; j < cellCount; j++)
//            {
//                var cell = headerRow.GetCell(j);
//                excelToDataTable[s].Columns.Add(cell.ToString());
//            }

//            //获取Excel表格中除标题以为的所有数据源，转化为dataTable中的表格数据源
//            for (var i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
//            {
//                var dataRow = excelToDataTable[s].NewRow();

//                var row = sheet.GetRow(i);

//                if (row == null) continue; //没有数据的行默认是null　

//                for (int j = row.FirstCellNum; j < cellCount; j++)
//                {
//                    if (row.GetCell(j) != null)//单元格内容非空验证
//                    {
//                        #region NPOI获取Excel单元格中不同类型的数据
//                        //获取指定的单元格信息
//                        var cell = row.GetCell(j);
//                        switch (cell.CellType)
//                        {
//                            //首先在NPOI中数字和日期都属于Numeric类型
//                            //通过NPOI中自带的DateUtil.IsCellDateFormatted判断是否为时间日期类型
//                            case CellType.Numeric when DateUtil.IsCellDateFormatted(cell):
//                                dataRow[j] = cell.DateCellValue;
//                                break;
//                            case CellType.Numeric:
//                                //其他数字类型
//                                dataRow[j] = cell.NumericCellValue;
//                                break;
//                            //空数据类型
//                            case CellType.Blank:
//                                dataRow[j] = "";
//                                break;
//                            //公式类型
//                            case CellType.Formula:
//                                {
//                                    HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(workbook);
//                                    dataRow[j] = eva.Evaluate(cell).StringValue;
//                                    break;
//                                }
//                            //布尔类型
//                            case CellType.Boolean:
//                                dataRow[j] = row.GetCell(j).BooleanCellValue;
//                                break;
//                            ////错误
//                            //case CellType.Error:
//                            //    dataRow[j] = HSSFErrorConstants.GetText(row.GetCell(j).ErrorCellValue);
//                            //    break;
//                            //其他类型都按字符串类型来处理（未知类型CellType.Unknown，字符串类型CellType.String）
//                            default:
//                                dataRow[j] = cell.StringCellValue;
//                                break;
//                        }
//                        #endregion
//                    }
//                }
//                excelToDataTable[s].Rows.Add(dataRow);
//            }
//        }

//        isSuccess = true;
//    }
//    catch (Exception e)
//    {
//        resultMsg = e.Message;
//    }

//    return excelToDataTable;
//}