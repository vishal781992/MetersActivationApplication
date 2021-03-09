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
    class FileCreatorClass
    {

        #region TextForDataBaseUpdate
        public static void FileForDataBase(string Dataset_MeterID, string IPAddr)
        {
            File.AppendAllText(DeclarationClass.DIRECTORY_SIMAUTOMATION_DATABASE + @"\OneFile.txt", Dataset_MeterID+","+ IPAddr + Environment.NewLine);
        }
        #endregion TextForDataBaseUpdate

        #region TxTForInitialFileUpload
        public static string TxTForInitialFileUpload(string FilePath, int count, List<string> DatasetC1, List<string> DatasetC2, List<string> DatasetC3)
        {
            //METERID,IMEI,SIMCARDID - initial file
            string IsFileCreatedAlready = FilePath;
            string path = IsFileCreatedAlready;
            // Set the variable "delimiter" to ", ".
            string delimiter = ",";

            // This text is added only once to the file.
            string createLine = "MeterID" + delimiter + "IMEI" + delimiter + "SimCardID"+Environment.NewLine;
            File.WriteAllText(path, createLine);

            for(int counter = 0; counter < count; counter++)
            {
                try
                {
                    createLine = "AE"+DatasetC3[counter] + delimiter + DatasetC2[counter] + delimiter + DatasetC1[counter] + Environment.NewLine;
                    File.AppendAllText(path, createLine);
                }
                catch
                {
                    counter++;
                }
            }
            return IsFileCreatedAlready;

        }
        #endregion TxTForInitialFileUpload

        #region TextForFinalFileUpload
        public static bool TextForFinalFileUpload(string path,[Optional] string Dataset_MeterID, [Optional] string Dataset_SimID, [Optional] long? IMEIFromTable39, [Optional] string IPAddr, [Optional] string Dataset_MTypeCode, [Optional] string Dataset_batch, [Optional] string  Dataset_commChkDate)
        {
            if(string.IsNullOrEmpty(Dataset_MeterID))
            {
                if (File.Exists(path))
                    File.Delete(path);
                //startiong the File with the final format
                File.WriteAllText(path, "meterid,simcardid,imei,ipaddress,metertypecode,batch,commchkdate" + DeclarationClass.newLine);
                return true;
            }
            else
            {

                try
                {
                    //Dataset_commChkDate = null?? DateTime.Now.ToString("yyyyddMM");
                    File.AppendAllText(path, "AE"+Dataset_MeterID + "," + Dataset_SimID + "," + IMEIFromTable39 + "," + IPAddr + "," + Dataset_MTypeCode + "," + Dataset_batch + "," + Dataset_commChkDate+ DeclarationClass.newLine);

                    return true;
                }
                catch { return false; }
            }
           
        }
        #endregion TextForFinalFileUpload

        //at this stage everything is verified and application is ready to create a file.
        #region FinalFile
        public static bool CSVcreateAsFinalFile(List<string> ColumnHeads_M, List<string> ColumnData_M, string PathForAPIFile,List<long?> IMEIFromPWS, List<string> CorrectionCheck, [Optional] string FileNameOnly)
        {
            try
            {
                string headerS = string.Empty; string newLine = "\r\n";
                foreach (string Header in ColumnHeads_M)
                {
                    headerS += Header + ",";
                }
                headerS = headerS.Trim(DeclarationClass.trimmerArray);
                string TempFileExtension = PathForAPIFile + FileNameOnly + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".csv";
                Directory.CreateDirectory(PathForAPIFile);
                //File.Create(TempFileExtension);
                File.WriteAllText(TempFileExtension, headerS + ",imeiPWDfromSIMID,result" + "\r\n");

                headerS = string.Empty;
                int lengthOfString = ColumnHeads_M.Count; int temp = 0; int num = 1; int tempCounterForHeaderCount = 0;
                int count = 0;
                for (int DataCount = 0; DataCount <= ColumnData_M.Count; DataCount++)
                {
                    if (temp < ColumnData_M.Count)
                    {
                        headerS = string.Empty;
                        while (tempCounterForHeaderCount < ColumnHeads_M.Count)
                        {
                            if (ColumnData_M[temp + tempCounterForHeaderCount] != null)
                            {
                                headerS += ColumnData_M[temp + tempCounterForHeaderCount] + ",";
                            }
                            else
                            {
                                headerS += "null" + ",";
                            }
                            tempCounterForHeaderCount++;
                        }
                        tempCounterForHeaderCount = 0; headerS = headerS.Trim(DeclarationClass.trimmerArray);
                        if (count >= IMEIFromPWS.Count)
                            File.AppendAllText(TempFileExtension, headerS + "," + "IMEIFromPWS(Blank)" + "," + "CorrectionCheck(Blank)" + newLine);
                        else
                            File.AppendAllText(TempFileExtension, headerS + "," + IMEIFromPWS[count] + "," + CorrectionCheck[count] + "\r\n");

                        temp = num * lengthOfString;
                        num++; count++;
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(string.Empty+e);
                return false; 
            }
        }
        #endregion FinalFile

        #region ActivationRepost
        public static string CsvWritingAsActivationRepost(string textBox_tab1_browse, List<string> ResponseActivation, List<string> Dataset_SimID, List<string> Dataset_IMEI)
        {
            string ReportFileCompleteName = string.Empty;

            ReportFileCompleteName = textBox_tab1_browse.EndsWith(".xls") ? textBox_tab1_browse.Replace(".xls", "_ActReport.csv") : textBox_tab1_browse.Replace(".xlsx", "_ActReport.csv");

            string path = ReportFileCompleteName;

            // Set the variable "delimiter" to ",".
            string delimiter = ",";

            // This text is added only once to the file. Heading
            string HeadingS = "SimCardID" + delimiter + "IMEI" + delimiter + "Status/Time(of last act. check)" + delimiter + Environment.NewLine;
            File.WriteAllText(path, HeadingS);

            int counter = 0;
            foreach (string str in ResponseActivation)
            {
                try
                {
                    string createText = "\'" + Dataset_SimID[counter] + delimiter + "\'" + Dataset_IMEI[counter] + delimiter + ResponseActivation[counter] + ": " + DateTime.Now.ToString("MM/dd/yyyy.HH:mm:ss") + delimiter + Environment.NewLine;
                    File.AppendAllText(path, createText);
                    counter++;
                }
                catch
                {
                    counter++;
                }
            }
            return ReportFileCompleteName;
        }
        #endregion ActivationRepost

        #region DirectoryCreator
        public static void DirectoryCreator()
        {
            if (!Directory.Exists(DeclarationClass.DIRECTORY_SIMAUTOMATION_DATABASE))
            {
                Directory.CreateDirectory(DeclarationClass.DIRECTORY_SIMAUTOMATION_DATABASE);
            }
            if (!Directory.Exists(DeclarationClass.DIRECTORY_SIMAUTOMATIONPARENT))
            {
                Directory.CreateDirectory(DeclarationClass.DIRECTORY_SIMAUTOMATIONPARENT);
            }
            if(!Directory.Exists(DeclarationClass.DIRECTORY_SIMAUTOMATION_FINALF))
            {
                Directory.CreateDirectory(DeclarationClass.DIRECTORY_SIMAUTOMATION_FINALF);
            }
            if (!Directory.Exists(DeclarationClass.DIRECTORY_SIMAUTOMATION_INITIALF))
            {
                Directory.CreateDirectory(DeclarationClass.DIRECTORY_SIMAUTOMATION_INITIALF);
            }
            if (!Directory.Exists(DeclarationClass.DIRECTORY_SIMAUTOMATION_REPORT))
            {
                Directory.CreateDirectory(DeclarationClass.DIRECTORY_SIMAUTOMATION_REPORT);
            }
            if (!Directory.Exists(DeclarationClass.DIRECTORY_SIMAUTOMATION_ACTIVATIONREPORTF))
            {
                Directory.CreateDirectory(DeclarationClass.DIRECTORY_SIMAUTOMATION_ACTIVATIONREPORTF);
            }

        }
        #endregion DirectoryCreator

        #region From API
        public static string CSVcreateFromAPI(List<string> ColumnHeads_M,List<string> ColumnData_M, List<string> IMEIFromPWS, List<string> CorrectionCheck, [Optional] string FileNameOnly)
        {
            try
            {
                string headerS = string.Empty;
                foreach (string Header in ColumnHeads_M)
                {
                    headerS += Header + ",";
                }
                headerS = headerS.Trim(DeclarationClass.trimmerArray);
                string TempFileExtension = DeclarationClass.PATHFORAPIFILE + FileNameOnly + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".csv";
                Directory.CreateDirectory(DeclarationClass.PATHFORAPIFILE);
                //File.Create(TempFileExtension);
                File.WriteAllText(TempFileExtension, headerS + ",imeiPWDfromSIMID,result" + "\r\n");

                headerS = string.Empty;
                int lengthOfString = ColumnHeads_M.Count; int temp = 0; int num = 1; int tempCounterForHeaderCount = 0;
                int count = 0;
                for (int DataCount = 0; DataCount <= ColumnData_M.Count; DataCount++)
                {
                    if (temp < ColumnData_M.Count)
                    {
                        headerS = string.Empty;
                        while (tempCounterForHeaderCount < ColumnHeads_M.Count)
                        {
                            if (ColumnData_M[temp + tempCounterForHeaderCount] != null)
                            {
                                headerS += ColumnData_M[temp + tempCounterForHeaderCount] + ",";
                            }
                            else
                            {
                                headerS += "null" + ",";
                            }
                            tempCounterForHeaderCount++;
                        }
                        tempCounterForHeaderCount = 0; headerS = headerS.Trim(DeclarationClass.trimmerArray);
                        if (count >= IMEIFromPWS.Count)
                            File.AppendAllText(TempFileExtension, headerS + "," + "IMEIFromPWS(Blank)" + "," + "CorrectionCheck(Blank)" + DeclarationClass.newLine);
                        else
                            File.AppendAllText(TempFileExtension, headerS + "," + IMEIFromPWS[count] + "," + CorrectionCheck[count] + "\r\n");

                        temp = num * lengthOfString;
                        num++; count++;
                    }
                }
                return "\r\nFile is Created Successfully.(manual response)";
            }
            catch { return "\r\nFile creation catch.(manual response)"; }
        }
        #endregion From API
    }
}
