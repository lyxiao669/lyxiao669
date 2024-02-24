using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Juzhen.MiniProgramAPI.Infrastructure.Utils
{
    public class ExcelUtil
    {
        /// <summary>
        /// 将DataTable转化成HSSFWorkbook
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static HSSFWorkbook Write(DataTable table)
        {
            #region 初始化工作薄
            //创建工作薄
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            var sheet = hssfworkbook.CreateSheet("Sheet1");
            #endregion

            #region 设置标题
            //var row = sheet.CreateRow(sheet.PhysicalNumberOfRows);
            //row.Height = (short)(row.Height * 1.8);

            //for (int i = 1; i < table.Columns.Count; i++)
            //{
            //    _ = row.CreateCell(i);
            //}
            //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
            #endregion

            #region 设置表头
            var row = sheet.CreateRow(sheet.PhysicalNumberOfRows);

            row.Height = (short)(row.Height * 1.4);
            var cell = row.CreateCell(0);
            var style = hssfworkbook.CreateCellStyle();
            var font = hssfworkbook.CreateFont();
            font.Boldweight = short.MaxValue;
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.SetFont(font);
            cell.CellStyle = style;
            cell.CellStyle = style;
            cell.SetCellValue(table.TableName);
            //设置列名
            foreach (DataColumn tc in table.Columns)
            {

                style = hssfworkbook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Center;
                style.BorderBottom = BorderStyle.Thin;
                style.BorderTop = BorderStyle.Thin;
                style.BorderLeft = BorderStyle.Thin;
                style.BorderRight = BorderStyle.Thin;
                style.SetFont(font);
                cell = row.CreateCell(table.Columns.IndexOf(tc));
                cell.SetCellValue(tc.ColumnName);
                cell.SetCellType(CellType.String);
                cell.CellStyle = style;
            }
            #endregion

            #region 设置表行
            //设置每一行的数据
            style = hssfworkbook.CreateCellStyle();
            foreach (DataRow tr in table.Rows)
            {
                row = sheet.CreateRow(sheet.PhysicalNumberOfRows);
                #region 设置每列数据
                foreach (DataColumn tc in table.Columns)
                {
                    #region 根据数据类型选择单元格格式
                    cell = row.CreateCell(table.Columns.IndexOf(tc));
                    style.Alignment = HorizontalAlignment.Center;
                    style.BorderBottom = BorderStyle.Thin;
                    style.BorderTop = BorderStyle.Thin;
                    style.BorderLeft = BorderStyle.Thin;
                    style.BorderRight = BorderStyle.Thin;
                    cell.CellStyle = style;
                    var value = tr[tc.ColumnName];
                    if (tc.DataType.IsValueType && (tc.DataType != typeof(DateTime)))
                    {
                        var strvalue = value == null ? "0" : value.ToString();
                        double.TryParse(strvalue, out double dvalue);
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue(dvalue);
                    }
                    else
                    {
                        cell.SetCellType(CellType.String);
                        cell.SetCellValue(value.ToString());
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            return hssfworkbook;
        }
        /// <summary>
        /// 保存到本地文件
        /// </summary>
        /// <param name="table"></param>
        /// <param name="filePath"></param>
        public static void WriteToFile(DataTable table, string filePath)
        {
            //保存到本机
            var hssfworkbook = Write(table);
            FileStream stream = new FileStream(filePath, FileMode.CreateNew);
            hssfworkbook.Write(stream);
            stream.Close();
            hssfworkbook.Close();
        }
        /// <summary>
        /// 保存到本地文件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        public static void WriteToFile<T>(List<T> list, string filePath)
        {
            //保存到本机
            var hssfworkbook = Write(ListToDataTable(list));
            FileStream stream = new FileStream(filePath, FileMode.CreateNew);
            hssfworkbook.Write(stream);
            stream.Close();
            hssfworkbook.Close();
        }
        /// <summary>
        /// 响应到浏览器
        /// </summary>
        /// <param name="body"></param>
        /// <param name="table"></param>
        public static void WriteToClient(Stream body, DataTable table)
        {
            var hssfworkbook = Write(table);
            hssfworkbook.Write(body);
            hssfworkbook.Close();
        }
        /// <summary>
        /// 响应到浏览器
        /// </summary>
        /// <param name="body"></param>
        /// <param name="list"></param>
        public static void WriteToClient<T>(Stream body, List<T> list)
        {
            var hssfworkbook = Write(ListToDataTable(list));
            hssfworkbook.Write(body);
            hssfworkbook.Close();
        }

        /// <summary>
        /// 将Excel转化成HSSFWorkbook
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="extension"></param> 
        /// <param name="sheetAt">sheet下标</param>
        /// <param name="startRow">开始行</param>
        /// <returns></returns>
        public static DataTable Read(Stream stream, string extension, int sheetAt = 0, int startRow = 0)
        {
            var table = new DataTable();
            IWorkbook workbook = null;

            #region 判断文件类型,并实例化Workbook
            if (".xls".Equals(extension, StringComparison.InvariantCultureIgnoreCase))
            {
                workbook = new HSSFWorkbook(stream);
            }
            else if (".xlsx".Equals(extension, StringComparison.InvariantCultureIgnoreCase))
            {
                workbook = new XSSFWorkbook(stream);
            }
            else
            {
                throw new Exception("文件类型不合法");
            }
            var sheet = workbook.GetSheetAt(sheetAt);
            #endregion

            #region 初始化DataTable结构
            table.TableName = sheet.SheetName;
            foreach (var cell in sheet.GetRow(startRow).Cells)
            {
                table.Columns.Add(new DataColumn(cell.StringCellValue, cell.CellType == CellType.Numeric ? typeof(double) : typeof(string)));
            }
            #endregion

            #region 设置DataTable数据
            for (int i = startRow + 1; i < sheet.PhysicalNumberOfRows; i++)
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
            #endregion

            return table;
        }
        /// <summary>
        /// 把excel文件转换成datatable
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="sheetAt"></param>
        /// <param name="startRow"></param>
        /// <returns></returns>
        public static DataTable Read(string filename, int sheetAt, int startRow)
        {
            using (var fs = new FileStream(filename, FileMode.Open))
            {
                return Read(fs, Path.GetExtension(filename));
            }
        }
        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();
            var properties = typeof(T).GetProperties().ToList().FindAll(f => f.Name != "Fields");
            var columnsMapper = new Dictionary<string, string>();
            //注入列
            foreach (var property in properties)
            {
                //判断是否有描述注解
                var attribute = property.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                var desc = property.Name;
                if (attribute.Length > 0)
                {
                    desc = (attribute[0] as System.ComponentModel.DescriptionAttribute).Description;
                }
                //非空处理
                var name = string.IsNullOrEmpty(desc) ? property.Name : desc;
                //重复备注处理
                name = columnsMapper.ContainsValue(name) ? string.Format("{0}{1}", name, columnsMapper.Values.ToList().FindAll(v => v == name).Count) : name;
                dt.Columns.Add(name, property.PropertyType.IsValueType && property.PropertyType != typeof(DateTime) ? typeof(double) : typeof(string));
                columnsMapper.Add(property.Name, name);
            }
            //注入值
            foreach (var item in list)
            {
                DataRow row = dt.NewRow();
                foreach (var property in properties)
                {
                    row[columnsMapper[property.Name]] = property.GetValue(item);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
