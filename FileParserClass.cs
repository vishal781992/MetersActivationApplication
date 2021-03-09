using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;//excel workbooks are using this
using _Excel1 = Microsoft.Office.Interop.Excel;//excel workbooks are using this

using System.Runtime.InteropServices;
using System.Collections;
namespace SimAutomation
{
    class FileParserClass
    {
        #region Declaration
        public List<string> dataset_SimID = new List<string>();
        public List<string> dataset_IMEI = new List<string>();
        public List<string> dataset_MeterID = new List<string>();
        public List<string> dataset_batch = new List<string>();
        public List<string> dataset_MTypeCode = new List<string>();
        public List<string> dataset_commChkDate = new List<string>();

        #endregion Declaration


        public int ExcelExtraction(string InputFilename)
        {
            int? DatasetC1_ColumnNumber = 0, DatasetC2_ColumnNumber = 0, DatasetC3_ColumnNumber = 0, DatasetC4_ColumnNumber = 0, DatasetC5_ColumnNumber = 0, DatasetC6_ColumnNumber = 0; bool Flag_Nextstep = false;
            try
            {
                //_Application excel = new _Excel.Application();
                _Application excel1 = new _Excel1.Application();

                int sheetColumn = 1;
                int sheetRow = 1;

                _Excel1.Workbook wb;
                _Excel1.Worksheet ws;

                wb = excel1.Workbooks.Open(InputFilename);
                ws = wb.Worksheets[1];//sheet

                try
                {
                    while (ws.Cells[sheetRow, sheetColumn].Value != null)
                    {
                        string isEqual = ws.Cells[1, sheetColumn].Value2;
                        if (isEqual.ToUpper() == "SIMCARDID" || string.Equals(isEqual.ToUpper(), "SIMCARDID"))
                            DatasetC1_ColumnNumber = sheetColumn;
                        else if (isEqual.ToUpper() == "IMEI" || string.Equals(isEqual.ToUpper(), "IMEI"))
                            DatasetC2_ColumnNumber = sheetColumn;
                       
                        else if (isEqual.ToUpper() == "BATCH" || string.Equals(isEqual.ToUpper(), "BATCH"))
                            DatasetC4_ColumnNumber = sheetColumn;
                        else if (isEqual.ToUpper() == "METERTYPECODE" || string.Equals(isEqual.ToUpper(), "METERTYPECODE"))
                            DatasetC5_ColumnNumber = sheetColumn;
                        else if (isEqual.ToUpper() == "AMRCHKDATE" || string.Equals(isEqual.ToUpper(), "AMRCHKDATE"))
                            DatasetC6_ColumnNumber = sheetColumn;

                        else if (isEqual.ToUpper() == "METERID" || string.Equals(isEqual.ToUpper(), "METERID"))
                            DatasetC3_ColumnNumber = sheetColumn;
                        sheetColumn++; Flag_Nextstep = true;
                    }

                    //added

                    sheetRow = 1; sheetColumn = 1;
                    while (ws.Cells[sheetRow, sheetColumn].Value != null)
                    {
                        try
                        {
                            dynamic TempDynamicString1 = ws.Cells[sheetRow, DatasetC1_ColumnNumber].Value2;
                            dataset_SimID.Add(TempDynamicString1 + string.Empty);
                            dynamic TempDynamicString2 = ws.Cells[sheetRow, DatasetC2_ColumnNumber].Value2;
                            dataset_IMEI.Add(TempDynamicString2 + string.Empty);
                            
                            dynamic TempDynamicString4;
                            try
                            {
                                TempDynamicString4 = ws.Cells[sheetRow, DatasetC4_ColumnNumber].Value2;
                                dataset_batch.Add(TempDynamicString4 + string.Empty);
                            }
                            catch { dataset_batch.Add(null); }

                            try
                            {
                                TempDynamicString4 = ws.Cells[sheetRow, DatasetC5_ColumnNumber].Value2;
                                dataset_MTypeCode.Add(TempDynamicString4 + string.Empty);
                            }
                            catch { dataset_MTypeCode.Add(null); }

                            try
                            {
                                TempDynamicString4 = ws.Cells[sheetRow, DatasetC6_ColumnNumber].Value2;
                                dataset_commChkDate.Add(TempDynamicString4 + string.Empty);
                            }
                            catch { dataset_commChkDate.Add(null); }

                            dynamic TempDynamicString3 = ws.Cells[sheetRow, DatasetC3_ColumnNumber].Value2;
                            string tempString = TempDynamicString3 + string.Empty;
                            tempString = tempString.ToUpper();
                            tempString = tempString.Trim(DeclarationClass.trimmerArray);
                            tempString = tempString.Trim(DeclarationClass.trimmerArray);
                            tempString = tempString.Trim(DeclarationClass.trimmerArray);
                            tempString = tempString.Trim(DeclarationClass.trimmerArray);
                            tempString = tempString.Trim(DeclarationClass.trimmerArray);

                            dataset_MeterID.Add(tempString);

                            sheetRow++;
                        }
                        catch
                        {
                            sheetRow++;
                        }
                    }
                    try { dataset_SimID.RemoveAt(0); dataset_MeterID.RemoveAt(0); dataset_IMEI.RemoveAt(0); } catch { }

                    try { dataset_batch.RemoveAt(0); } catch { }

                    try { dataset_commChkDate.RemoveAt(0); } catch { }

                    try { dataset_MTypeCode.RemoveAt(0); } catch { }

                    //added
                }


                catch { wb.Close(0); Flag_Nextstep = false; }
                wb.Close();
                while (Marshal.ReleaseComObject(wb) > 0) { }
                wb = null;
                while (Marshal.ReleaseComObject(ws) > 0) { }
                ws = null;

                excel1.Quit();

                #region SecondExcel
                //_Excel.Workbook wb1;
                //_Excel.Worksheet ws1;


                //wb1 = excel.Workbooks.Open(InputFilename);
                //ws1 = wb1.Worksheets[1];

                //if (File.Exists(InputFilename) && Flag_Nextstep)
                //{
                //    sheetRow = 1; sheetColumn = 1;
                //    while (ws1.Cells[sheetRow, sheetColumn].Value != null)//  Cells [sheetRow, sheetColumn].Value != null)
                //    {
                //        try
                //        {
                //            dynamic TempDynamicString1 = ws1.Cells[sheetRow, DatasetC1_ColumnNumber].Value2;
                //            Dataset_SimID.Add(TempDynamicString1 + string.Empty);
                //            dynamic TempDynamicString2 = ws1.Cells[sheetRow, DatasetC2_ColumnNumber].Value2;
                //            Dataset_IMEI.Add(TempDynamicString2 + string.Empty);
                //            //

                //            dynamic TempDynamicString4;
                //            try
                //            {
                //                TempDynamicString4 = ws1.Cells[sheetRow, DatasetC4_ColumnNumber].Value2;
                //                Dataset_batch.Add(TempDynamicString4 + string.Empty);
                //            }
                //            catch { Dataset_batch.Add(null); }

                //            try
                //            {
                //                TempDynamicString4 = ws1.Cells[sheetRow, DatasetC5_ColumnNumber].Value2;
                //                Dataset_MTypeCode.Add(TempDynamicString4 + string.Empty);
                //            }
                //            catch { Dataset_MTypeCode.Add(null); }

                //            try
                //            {
                //                TempDynamicString4 = ws1.Cells[sheetRow, DatasetC6_ColumnNumber].Value2;
                //                Dataset_commChkDate.Add(TempDynamicString4 + string.Empty);
                //            }
                //            catch { Dataset_commChkDate.Add(null); }

                //            //
                //            dynamic TempDynamicString3 = ws1.Cells[sheetRow, DatasetC3_ColumnNumber].Value2;
                //            string tempString = TempDynamicString3 + string.Empty;
                //            tempString = tempString.Trim(trimmerArray);
                //            tempString = tempString.Trim(trimmerArray);
                //            tempString = tempString.Trim(trimmerArray);
                //            tempString = tempString.Trim(trimmerArray);
                //            tempString = tempString.Trim(trimmerArray);

                //            Dataset_MeterID.Add(tempString);

                //            sheetRow++;
                //        }
                //        catch
                //        {
                //            sheetRow++;}
                //    }
                //    try{    Dataset_SimID.RemoveAt(0); Dataset_MeterID.RemoveAt(0); Dataset_IMEI.RemoveAt(0);   }catch { }

                //    try{   Dataset_batch.RemoveAt(0);   }catch { }

                //    try{   Dataset_commChkDate.RemoveAt(0); } catch { }

                //    try{   Dataset_MTypeCode.RemoveAt(0);   } catch { }

                //    wb1.Close(0);
                //    excel1.Quit();
                //    while (Marshal.ReleaseComObject(excel1) > 0) { }
                //    excel1 = null;
                //    GC();
                //    PreviousFile = InputFilename;
                //}
                #endregion SecondExcel

                return dataset_SimID.Count();
            }
            catch (Exception e) { MessageBox.Show(string.Empty + e); return 0; }
        }
    }
}
