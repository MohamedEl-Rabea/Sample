using Microsoft.Extensions.Localization;
using Moj.CMS.Shared.CustomAttributes;
using Moj.CMS.Shared.Helpers;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Moj.CMS.Shared.Services
{
    public class FileBuilder : IFileBuilder
    {
        private readonly IStringLocalizer<CMSLocalizer> _localizer;
        private string[] _propertiesNames;
        public FileBuilder(IStringLocalizer<CMSLocalizer> localizer)
        {
            _localizer = localizer;
            _propertiesNames = new string[] { };
        }

        public byte[] GenerateExcel<T>(IEnumerable<T> itemList, string sheetName)
        {
            var dt = ConvertToDataTable(itemList, sheetName);
            var file = GenerateExcel(dt, sheetName);
            return file;
        }
        public byte[] GenerateExcel<T>(IEnumerable<T> itemList, string sheetName, string[] properties)
        {
            _propertiesNames = properties;
            var dt = ConvertToDataTable(itemList, sheetName);
            var file = GenerateExcel(dt, sheetName);
            return file;
        }

        private DataTable ConvertToDataTable<T>(IEnumerable<T> itemList, string sheetName)
        {
            DataTable dataTable = new DataTable(_localizer[sheetName]);
            //Get all the exportable properties of that model  
            var props = GetProperties<T>(itemList).ToList();

            //Loop through all the properties, Adding Column name to our datatable  
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(_localizer[prop.Name]);
            }

            if (props.Count == 0) 
                return dataTable;
            
            // Adding Row and its value to our dataTable  
            foreach (T item in itemList)
            {
                var values = new object[props.Count];
                for (int i = 0; i < props.Count; i++)
                {
                    //inserting property values to datatable rows    
                    var value = props[i].GetValue(item, null);
                    var isLocalizeValue = Attribute.IsDefined(props[i], typeof(LocalizeValueAttribute));
                    values[i] = isLocalizeValue ? _localizer[value.ToString()] : value;
                }
                // Finally add value to datatable
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        private IEnumerable<PropertyInfo> GetProperties<T>(IEnumerable<T> itemList)
        {
            var props = _propertiesNames.Any()
                ? itemList.First().GetType().GetProperties().Where(p => _propertiesNames.Contains(p.Name))
                .OrderBy(p => ((DisplayAttribute)p.GetCustomAttribute(typeof(DisplayAttribute), false)).Order)
                : typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ExportableAttribute)))
                           .OrderBy(p => ((ExportableAttribute)p.GetCustomAttribute(typeof(ExportableAttribute), false)).Order);
            return props;
        }

        private byte[] GenerateExcel(DataTable dataTable, string sheetName)
        {
            if (dataTable.Rows.Count == 0)
                return new byte[0];

            byte[] file;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(_localizer[sheetName]);
                worksheet.View.RightToLeft = ExcelHelper.IsRightToLeft();
                worksheet.Cells.LoadFromDataTable(dataTable, true);
                package.Save();
                file = package.GetAsByteArray();
            }
            return file;
        }
    }
}
