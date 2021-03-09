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
using System.Net.NetworkInformation;
using System.Net.Http;


namespace SimAutomation
{
    public partial class Form1 : Form
    {
        #region Initilizers
        #region Declaration

        _Application excel = new _Excel.Application();
        _Application excel1 = new _Excel1.Application();


        #region Lists
        //From File
        public List<string> FromFile_SimID = new List<string>();
        public List<string> FromFile_IMEI = new List<string>();
        public List<string> FromFile_MeterID = new List<string>();
        public List<string> FromFile_batch = new List<string>();
        public List<string> FromFile_MTypeCode = new List<string>();
        public List<string> FromFile_commChkDate = new List<string>();

        //Appl - Lists used in application only.
        public List<string> Appl_Responses_CompareIMEI = new List<string>();
        public List<string> Appl_CorrectionCheck = new List<string>();
        public List<string> Appl_ResponseActivation_ToFile = new List<string>();
        public List<string> Appl_DisplayStringList = new List<string>();

        //from vision
        public List<string> FromVision_ExpIPAddr = new List<string>();
        public List<long?>  FromVision_ExpIMEI = new List<long?>();  
                                //Vision submits meter IDs, IMEIs, and ICCIDs to AE (we save EXPECTED IMEI) IP). 

        //from PWS
        public List<string> FromPWS_IPAddr = new List<string>();
        public List<long?>  FromPWS_IMEI = new List<long?>();

        //From Locus
        public List<DateTime?>  FromLocus_LastReadingTime = new List<DateTime?>();
        public List<string>     FromLocus_SimcardID = new List<string>();
        public List<long?>      FromLocus_T39IMEI = new List<long?>();   
                                    //AE’s server reads meter ID, IMEI, and IP from the meters (we save these as IMEI and ACTUAL
        public List<string>     FromLocus_MetersListGood = new List<string>();
        public List<string>     FromLocus_MetersListFaulty = new List<string>();
        public List<string>     FromLocus_T39IPAddr = new List<string>();
        public List<string>     FromLocus_ColumnHeads_M = new List<string>();
        public List<string>     FromLocus_ColumnData_M = new List<string>();

        //To Locus
        public List<string> ToLocus_MetersList = new List<string>();


        #endregion Lists

        string[] StringStorage = new string[5000];

        public bool flag_WorkDone = false, 
                    flag_FaultyMeter = false, 
                    flag_CancelProgress = false, 
                    flag_BlankFile = false;

        public int  counterForFileGeneratedInXml = 0;

        public string       _filePath,
                            _processedResponse,
                            _unProcessedResponse,
                            _tempString = string.Empty,
                            _tempStringResponse = string.Empty,
                            _previousFile = string.Empty,
                            _fileNameOnly;


        #endregion Declaration

        #region Form Init
        public Form1()
        {
            InitializeComponent();
            #region mybackgroundWorker
            myBackgroundWorker = new BackgroundWorker();
            myBackgroundWorker.WorkerReportsProgress = true;
            myBackgroundWorker.WorkerSupportsCancellation = false;
            myBackgroundWorker.DoWork += myBackgroundWorker1_DoWork;
            myBackgroundWorker.RunWorkerCompleted += myBackgroundWorker1_RunWorkerCompleted;
            myBackgroundWorker.ProgressChanged += myBackgroundWorker1_ProgressChanged;
            #endregion myBackgroundWorker
        }
        #endregion Form Init

        #region Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            RTB1.Text = "Hello, Version: "+ VersionClass.VERSIONNUMBER +"\r\nVersion Details: "+ VersionClass.VERSIONDETAILS +"\r\n";
            label_StartButton.Visible = false;
            RTB1.AppendText(VersionClass.HELPTEXT);
            PB_Tab1.Visible = false;//universal Progress Bar
            this.Text = "Sim Automation Tool " + VersionClass.VERSIONNUMBER;//Application name Headline
            textBox_CustomRateEnter.Visible = false;
            textBox_CustomRateEnter.Text = "AlsoEnergy VZW PN 1MB";
            toolTip_technicalhelp.SetToolTip(CB_t1_viewTechDetails,"More info about the Device other than activation status.");

            this.Icon = SimAutomation.Properties.Resources.CONNECT1; //textBox_tab1_browse.Text = @"F:\simAutomationFile\0115_activations-2 11-24-20.xlsx";
            label6.Visible = false; button_start.BackColor = Color.Transparent;

            //Tab 2
            groupBox2_2.Visible = false;
            groupBox2_3.Visible = false;
            label11.Visible = false;

            File.WriteAllText(DeclarationClass.DOCUMENTATION_VERSION, "Version " + VersionClass.VERSIONNUMBER);

            FileCreatorClass.DirectoryCreator();//creates directory for the user initially.
        }
        #endregion Form Load
        #endregion Initilizers

        #region Main button functions
        private void button_tab1_browse_Click(object sender, EventArgs e)
        {
            ClearListsStrings();
            RTB1.Clear(); _filePath = string.Empty;
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Filter = "xls files (*.xls)|*.xls |xlsx files" +
                                         " (*.xlsx)|*.xlsx | All files (*.*)|*.*";

                openFileDialog1.FilterIndex = 3;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _fileNameOnly = openFileDialog1.SafeFileName;
                    _filePath = openFileDialog1.FileName;
                    if(!_filePath.EndsWith(".xls") && !_filePath.EndsWith(".xlsx"))
                        RTB1.AppendText("You have not Selected any Valid File.");

                    if (string.IsNullOrEmpty(_filePath))
                        RTB1.AppendText("You have not Selected any Valid File.");

                    if(_filePath.EndsWith(".xls") || _filePath.EndsWith(".xlsx"))
                        textBox_tab1_browse.Text = _filePath;
                }
                if(textBox_tab1_browse.Text.ToUpper().EndsWith("_ACTREPORT.CSV"))
                    RTB1.Text  = "You have not Selected any Valid File.";
            }
        }   

        private async void button_tab1_start_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox_tab1_browse.Text))
            {
                #region init
                label6.Visible = true; button_start.BackColor = Color.Red;
                ClearListsStrings();
                label_StartButton.Visible = true;
                label_StartButton.Text = "You have pressed the Start button!";
                #endregion init

                try
                {
                    
                    if (string.IsNullOrEmpty(textBox_tab1_browse.Text))
                    {
                        MessageBox.Show("Please select the appropriate File.");
                        label_StartButton.Visible = false;
                    }

                    RTB1.Clear();

                    int n_Count = 0;
                    APICalls_PWS APC = new APICalls_PWS(textBox_CustomRateEnter.Text);

                    FileParserClass FPC = new FileParserClass();
                    //function to parse the Excel
                    counterAsReply = FPC.ExcelExtraction(_filePath);

                    this.FromFile_SimID.AddRange(FPC.dataset_SimID);
                    this.FromFile_IMEI.AddRange(FPC.dataset_IMEI);
                    this.FromFile_MeterID.AddRange(FPC.dataset_MeterID);
                    this.FromFile_batch.AddRange(FPC.dataset_batch);
                    this.FromFile_MTypeCode.AddRange(FPC.dataset_MTypeCode);
                    this.FromFile_commChkDate.AddRange(FPC.dataset_commChkDate);

                    //Clearing the unused data from the FileParser
                    FPC.dataset_SimID.Clear(); FPC.dataset_IMEI.Clear(); FPC.dataset_MeterID.Clear(); FPC.dataset_batch.Clear(); FPC.dataset_MTypeCode.Clear(); FPC.dataset_commChkDate.Clear();

                    PB_Tab1.Visible = true;
                    PB_Tab1.Maximum = FromFile_SimID.Count;
                    PB_Tab1.Value = PB_Tab1.Minimum;

                    foreach (string ThisSimID in FromFile_SimID)//string IMEI C2, string SIMNo C1, string MeterID C3
                    {
                        _responseString = await APC.DeviceDetails(ThisSimID);
                        string _tempStringResponse = string.Empty;

                        #region string modification
                        try
                        {
                            if (!string.IsNullOrEmpty(_responseString) || !string.Equals(_responseString.ToUpper(), "NODATA"))
                            {
                                _tempStringResponse = _responseString.Substring(_responseString.IndexOf(DeclarationClass.STATEOFSIM) + 7);
                                _tempStringResponse = _tempStringResponse.Substring(0, _tempStringResponse.IndexOf(","));
                                _tempStringResponse = _tempStringResponse.TrimStart('\"', ':');
                                _tempStringResponse = _tempStringResponse.TrimEnd('\"', ':');
                                Appl_ResponseActivation_ToFile.Add(_tempStringResponse);
                                RTB1.AppendText(DeclarationClass.newLine + ThisSimID + " --Act. Status: " + _tempStringResponse);
                            }
                        }
                        catch { RTB1.AppendText(DeclarationClass.newLine+ ThisSimID + " --Act. Status: " + "NO DATA Received- Activation Requested"); Appl_ResponseActivation_ToFile.Add("Updating Status."); }
                        #endregion string modification

                        Thread.Sleep(200);

                        if (_tempStringResponse.ToLower() != "active" || string.IsNullOrEmpty(_responseString) || _responseString.ToUpper() == "NODATA")//needs inactive sim cards
                        {
                            _responseString = await APC.SimActivationRequest(FromFile_IMEI[n_Count], ThisSimID);
                            try
                            {
                                RTB1.AppendText(DeclarationClass.newLine);
                                _processedResponse = _responseString.Substring(_responseString.IndexOf(DeclarationClass.PROCESSEDSTRING) + 12);
                                _processedResponse = _processedResponse.Substring(0, _processedResponse.IndexOf("],"));
                                _unProcessedResponse = _responseString.Substring(_responseString.IndexOf(DeclarationClass.UNPROCESSEDSTRING) + 14);
                                _unProcessedResponse = _unProcessedResponse.Substring(0, _unProcessedResponse.LastIndexOf("]"));
                            }
                            catch { }
                            ResponseParser(_processedResponse, _unProcessedResponse);

                            foreach (string DisplayString in Appl_DisplayStringList)
                                RTB1.AppendText(DisplayString);
                            //check The text file fore commented code
                        }
                        PB_Tab1.Value = n_Count; Appl_DisplayStringList.Clear();
                        PB_Tab1.Refresh();
                        n_Count++;
                    }
                    PB_Tab1.Value = PB_Tab1.Maximum;

                    string FileName = FileCreatorClass.CsvWritingAsActivationRepost(DeclarationClass.PATHFORACTIVATIONREPOST + _fileNameOnly, Appl_ResponseActivation_ToFile, FromFile_SimID, FromFile_IMEI);

                    RTB1.AppendText("\r\nFile Is created: " + FileName);
                    label_StartButton.Visible = false;
                    Thread.Sleep(1000);
                    PB_Tab1.Visible = false;
                }
                catch 
                { 
                    label6.Visible = false;
                    button_start.BackColor = Color.Transparent;
                    RTB1.AppendText("\r\nError in API calls ");
                }
                label6.Visible = false; 
                button_start.BackColor = Color.Transparent;
            }
        }

        private async void button_tab1_ActivateSim_Click(object sender, EventArgs e)
        {
            RTB1.Clear(); ClearListsStrings(); 
            button_tab1_ActivateSim.BackColor = Color.Red;
            textBox_tab1_SimCardID.Text = textBox_tab1_SimCardID.Text.Trim(' ');
            textBox_tab1_IMEI.Text = textBox_tab1_IMEI.Text.Trim(' ');

            if (textBox_tab1_SimCardID.TextLength == 20 && textBox_tab1_IMEI.TextLength == 15)
            {
                try
                {
                    flag_WorkDone = false;
                    textBox_tab1_SimCardID.Text = textBox_tab1_SimCardID.Text.Trim(' ');
                    textBox_tab1_IMEI.Text = textBox_tab1_IMEI.Text.Trim(' ');

                    APICalls_PWS APC = new APICalls_PWS(textBox_CustomRateEnter.Text);
                    _responseString = await APC.SimActivationRequest(textBox_tab1_IMEI.Text, textBox_tab1_SimCardID.Text);
                    //Thread.Sleep(400);
                    if(checkBox1_RawResp.Checked)
                    {
                        RTB1.Text = _responseString;
                    }

                    #region string modification
                    else
                    {
                        try
                        {
                            _processedResponse = _responseString.Substring(_responseString.IndexOf(DeclarationClass.PROCESSEDSTRING) + 12);
                            _processedResponse = _processedResponse.Substring(0, _processedResponse.IndexOf("],"));
                            _unProcessedResponse = _responseString.Substring(_responseString.IndexOf(DeclarationClass.UNPROCESSEDSTRING) + 14);
                            _unProcessedResponse = _unProcessedResponse.Substring(0, _unProcessedResponse.LastIndexOf("]"));
                        }
                        catch { }
                        ResponseParser(_processedResponse, _unProcessedResponse);
                        foreach (string str2 in Appl_DisplayStringList)
                            RTB1.AppendText(str2);

                    }


                    flag_WorkDone = true;
                    #endregion string modification
                }
                catch { RTB1.AppendText("\r\nError in API calls "); button_tab1_ActivateSim.BackColor = Color.Transparent; }
                button_tab1_ActivateSim.BackColor = Color.Transparent;
            }
            else { RTB1.AppendText("\r\nEnter the valid information."); button_tab1_ActivateSim.BackColor = Color.Transparent; }
            button_tab1_ActivateSim.BackColor = Color.Transparent;
        }

        private async void button_tab1_checkStatus_Click(object sender, EventArgs e)
        {
            //trim the text for possible white spaces.
            textBox_tab1_IMEI_ChechStatus.Text = textBox_tab1_IMEI_ChechStatus.Text.Trim(DeclarationClass.trimmerArray);

            //clear the lists
            RTB1.Clear(); ClearListsStrings();
            
            button_tab1_checkStatus.BackColor = Color.Red;
            APICalls_PWS APC = new APICalls_PWS(textBox_CustomRateEnter.Text);
            int Count = 0;

            if (textBox_tab1_IMEI_ChechStatus.TextLength == 20 && int.TryParse(textBox_tab1_IMEI_ChechStatus.Text.Substring(textBox_tab1_IMEI_ChechStatus.Text.Length - 4),out int result))
            {
                _responseString = await APC.DeviceDetails(textBox_tab1_IMEI_ChechStatus.Text);

                if (!CB_t1_viewTechDetails.Checked)
                {
                    try
                    {
                        #region string modification
                        //needs try catch here.
                        _tempStringResponse = _responseString.Substring(_responseString.IndexOf(DeclarationClass.STATEOFSIM) + 7);
                        _tempStringResponse = _tempStringResponse.Substring(0, _tempStringResponse.IndexOf(","));
                        _tempStringResponse = _tempStringResponse.TrimStart(DeclarationClass.trimmerArray);
                        _tempStringResponse = _tempStringResponse.TrimEnd(DeclarationClass.trimmerArray);
                        #endregion string modification
                        RTB1.AppendText("\r\n" + textBox_tab1_IMEI_ChechStatus.Text + "-- Activation Status: " + _tempStringResponse);
                    }
                    catch {}
                }
                else if (CB_t1_viewTechDetails.Checked)
                {
                    try
                    {
                        StringStorage = _responseString.Split(',');
                        RTB1.AppendText(textBox_tab1_IMEI_ChechStatus.Text + "--");
                        foreach (string str3 in StringStorage)
                        {
                            string temp = str3.Replace("\"", " "); temp = temp.Trim(DeclarationClass.trimmerArray);
                            RTB1.AppendText("\r\n" + temp);
                        }
                    }
                    catch {}
                }
                CB_t1_viewTechDetails.Checked = false;
            }
            else if(textBox_tab1_IMEI_ChechStatus.TextLength==0 && checkBox_CheckStatusALL.Checked)
            {
                try
                {
                    FileParserClass FPC = new FileParserClass();
                    PB_Tab1.Step = 3; PB_Tab1.Visible = true;
                    PB_Tab1.Value = PB_Tab1.Minimum;
                    checkBox_CheckStatusALL.Checked = false;
                    RTB1.Clear();
                    label_StartButton.Visible = true;
                    label_StartButton.Text = "You have opted for \"CheckStatus All\" button!";
                    if (string.IsNullOrEmpty(_filePath))
                    {
                        MessageBox.Show("Please select the appropriate File.");
                        label_StartButton.Visible = false;
                    }
                    counterAsReply = FPC.ExcelExtraction(_filePath);

                    this.FromFile_SimID.AddRange(FPC.dataset_SimID);
                    this.FromFile_IMEI.AddRange(FPC.dataset_IMEI);
                    this.FromFile_MeterID.AddRange(FPC.dataset_MeterID);
                    this.FromFile_batch.AddRange(FPC.dataset_batch);
                    this.FromFile_MTypeCode.AddRange(FPC.dataset_MTypeCode);
                    this.FromFile_commChkDate.AddRange(FPC.dataset_commChkDate);

                    //Clearing the unused data from the FileParser
                    FPC.dataset_SimID.Clear(); FPC.dataset_IMEI.Clear(); FPC.dataset_MeterID.Clear(); FPC.dataset_batch.Clear(); FPC.dataset_MTypeCode.Clear(); FPC.dataset_commChkDate.Clear();


                    PB_Tab1.Maximum = FromFile_SimID.Count;
                    foreach (string str2 in FromFile_SimID)
                    {
                        _responseString = await APC.DeviceDetails(str2);
                        #region string modification
                        try
                        {
                            _tempStringResponse = _responseString.Substring(_responseString.IndexOf(DeclarationClass.STATEOFSIM) + 7);
                            _tempStringResponse = _tempStringResponse.Substring(0, _tempStringResponse.IndexOf(","));
                            _tempStringResponse = _tempStringResponse.TrimStart(DeclarationClass.trimmerArray);
                            _tempStringResponse = _tempStringResponse.TrimEnd(DeclarationClass.trimmerArray);
                        }
                        catch
                        {
                            _tempStringResponse = "No Data Generated yet. <manual response>";
                        }
                        #endregion string modification

                        RTB1.AppendText("\r\n" + str2 + "-- Activation Status: " + _tempStringResponse);

                        PB_Tab1.Value = Count;
                        PB_Tab1.Refresh();
                        Count++;
                    }
                    RTB1.AppendText("\r\n Activation Checked for: " + FromFile_SimID.Count + " SimCards.");
                    CB_t1_viewTechDetails.Checked = false;
                    PB_Tab1.Value = PB_Tab1.Maximum;
                }
                catch {}
            }
            else { RTB1.AppendText("\r\nEnter the valid information. counterAsReply: "+ counterAsReply); }
            Thread.Sleep(1000);
            PB_Tab1.Visible = false;
            button_tab1_checkStatus.BackColor = Color.Transparent;
        }

        #endregion Main button functions

        #region StatusChanged Button
        private void checkBox_tab1_CheckStatusALL_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox_CheckStatusALL.Checked)
            {
                CB_t1_viewTechDetails.Checked = false;
                textBox_tab1_IMEI_ChechStatus.Clear();

                using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
                {
                    openFileDialog1.InitialDirectory = @"C:\";
                    openFileDialog1.Filter = "xls files (*.xls)|*.xls |xlsx files" +
                                             " (*.xlsx)|*.xlsx | All files (*.*)|*.*";

                    openFileDialog1.FilterIndex = 3;
                    openFileDialog1.RestoreDirectory = true;

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        _filePath = openFileDialog1.FileName;

                        if (_filePath.EndsWith(".xls") || _filePath.EndsWith(".xlsx"))
                            RTB1.Text = "File Selected: " + _filePath + DeclarationClass.multiLine;
                    }
                    else
                        checkBox_CheckStatusALL.Checked = false;
                }
            }
        }

        private void checkBox_tab1_CheckStatus_CheckStateChanged(object sender, EventArgs e)
        {
            if (CB_t1_viewTechDetails.Checked)
                checkBox_CheckStatusALL.Checked = false;

        }

        #endregion StatusChanged Button

        #region Button tab2
        private async void button_Tab2_Start(object sender, EventArgs e)
        {
            #region init
            progressBar_StartButton.Value = 0; progressBar_StartButton.Maximum = 100; progressBar_StartButton.Step = 10;
            RTB1.Clear(); //ClearListsStrings(); 
            groupBox2_3.Visible = false;
            //bool flag_DataBaseFileCreated = false, flag_CantWriteFile = false;
            //string d = dateTimePicker_t2.Value.ToString("yyyy_MM_dd");

            APICalls_AE APLO = new APICalls_AE();
            APICalls_PWS APPWS = new APICalls_PWS("AlsoEnergy VZW PN 1MB");//hardcoded the plan value her only.

            #endregion init

            #region If File exists(OneFile.txt)
            if (!File.Exists(DeclarationClass.DIRECTORY_SIMAUTOMATION_DATABASE + @"\OneFile.txt"))
            {
                #region authentication for Locus
                if (await APLO.AuthenticationStarter())
                {
                    RTB1.AppendText(DeclarationClass.newLine + "User Authenticated!"); progressBar_StartButton.PerformStep();
                    string MeterString = string.Empty;

                    //this creates a single string for API call
                    foreach (string meterID in FromFile_MeterID)
                        MeterString = MeterString + meterID + ",";

                    //trims unnecesary symbols in the string
                    MeterString = MeterString.TrimEnd(DeclarationClass.trimmerArray);

                    if (await APLO.MeterInfo(MeterString, FromFile_MeterID.Count) == true)
                    {
                        //copying the data from the API call to main
                        progressBar_StartButton.PerformStep();
                        this.FromLocus_ColumnHeads_M = APLO.Appl_ColumnHeads;
                        this.FromLocus_ColumnData_M = APLO.Appl_ColumnData;
                    }
                }
                #endregion authentication

                #region FinalFile can be created - Listparser
                if (FromLocus_ColumnHeads_M.Count > 1)
                    Listparser(ToLocus_MetersList.Count);
                else
                    RTB1.Text = "Not received any data for the requested meters. Process Continues...";

                int count = 0;

                FileCreatorClass.TextForFinalFileUpload(DeclarationClass.PATHFORFINALFILE + _fileNameOnly + DeclarationClass.TextFormat);
                progressBar_StartButton.PerformStep();
                //we are using all the Sim Card IDs we received from Locus after reading the meter IDs.
                progressBar_StartButton.Maximum = FromLocus_SimcardID.Count * 10 + 10;

                #endregion Final file

                #region PWS cross verification

                RTB1.Text = DeclarationClass.DISCLAIMER2;
                RTB1.AppendText(DeclarationClass.DISCLAIMER);

                //Simcard Id from Locus is being used to check the IMEI
                foreach (string SimID in FromLocus_SimcardID)
                {
                    //White char trimming
                    string SimID_Trimmed = SimID.Trim(' ');

                    //asking the PWS for dvice details, Authentication is static
                    _responseString = await APPWS.DeviceDetails(SimID_Trimmed);

                    progressBar_StartButton.PerformStep();

                    if (!_responseString.ToUpper().Contains("NODATA"))
                    {

                        if (long.TryParse(_responseString.Substring(_responseString.IndexOf(DeclarationClass.IMEISTRING) + DeclarationClass.IMEISTRING.Length, 15), out long tempSt))
                            FromPWS_IMEI.Add(tempSt);
                        try
                        {
                            string tempST = _responseString.Substring(_responseString.IndexOf(DeclarationClass.IPADDRSTRING) + DeclarationClass.IPADDRSTRING.Length);
                            tempST = tempST.Substring(0, tempST.IndexOf(',') - 1);
                            tempST.Trim(DeclarationClass.trimmerArray);

                            FromPWS_IPAddr.Add(tempST);
                        }
                        catch { FromPWS_IPAddr.Add(null); }

                    }
                    else
                        FromPWS_IMEI.Add(null);

                    try
                    {
                        if ((FromVision_ExpIMEI[count] == FromPWS_IMEI[count]) && (FromLocus_T39IMEI[count] == FromPWS_IMEI[count]))
                        //IMEI comparison here
                        {
                            if ((FromVision_ExpIPAddr[count] == FromPWS_IPAddr[count]) && (FromLocus_T39IPAddr[count] == FromPWS_IPAddr[count]))
                            //IP comparison here
                            {
                                flag_BlankFile = true;
                                int IndexToOrgRefr = FromFile_MeterID.IndexOf(FromLocus_MetersListGood[count]);
                                //index to original reference

                                RTB1.AppendText(Appl_Responses_CompareIMEI[count] + "[PWS] " + FromPWS_IMEI[count] + "[Pip] " + FromPWS_IPAddr[count] + "**" + DeclarationClass.newLine); Appl_CorrectionCheck.Add("true");
                                string _Date = DateTime.Now.ToString("yyyyMMddHHmmss");
                                FileCreatorClass.TextForFinalFileUpload(DeclarationClass.PATHFORFINALFILE + _fileNameOnly + DeclarationClass.TextFormat, FromFile_MeterID[IndexToOrgRefr], FromFile_SimID[IndexToOrgRefr], FromLocus_T39IMEI[count], FromLocus_T39IPAddr[count], FromFile_MTypeCode[IndexToOrgRefr], FromFile_batch[IndexToOrgRefr], _Date);

                                FileCreatorClass.FileForDataBase(FromFile_MeterID[IndexToOrgRefr], FromLocus_T39IPAddr[count]);
                                //if the bool is false then prompt the user for file already exists.
                            }
                        }
                        else
                        {
                            int demo = FromFile_MeterID.IndexOf(FromLocus_MetersListGood[count]);
                            RTB1.AppendText(Appl_Responses_CompareIMEI[count] + "[PWS] " + FromPWS_IMEI[count] + "[Pip] " + FromPWS_IPAddr[count] + DeclarationClass.newLine);
                            Appl_CorrectionCheck.Add("false");
                        }
                    }
                    catch { if (FromLocus_T39IMEI.Count < 1) { RTB1.AppendText(FromLocus_MetersListFaulty[count] + " Error in reading IMEI from Table39. Locus is reading(manual response)" + "\r\n"); } }
                    count++;
                }
                #endregion PWS verification

                #region CSV file creator/ Report
                progressBar_StartButton.Value = progressBar_StartButton.Maximum;

                RTB1.AppendText(FileCreatorClass.CSVcreateAsFinalFile(FromLocus_ColumnHeads_M, FromLocus_ColumnData_M, DeclarationClass.PATHFORREPORT, FromPWS_IMEI, Appl_CorrectionCheck, _fileNameOnly) ? "\r\nFile created" : "\r\nFile not created");

                RTB1.AppendText(flag_BlankFile ? DeclarationClass.multiLine + "--" : DeclarationClass.multiLine + "The file created is empty. No data matched the criteria!");

                #endregion CSV file creator/ Report

                groupBox2_3.Visible = true;

            }
            else
            {
                RTB1.Text = "Wait, The file is being uploaded to the server on metershop."+DeclarationClass.newLine+"Try again after 10 mins.";
            }
            #endregion If file exists
        }
        #endregion Button Tab2

        #region ListParser Tab2
        public void Listparser(int Count)
        {
            int     lengthOfString =  FromLocus_ColumnHeads_M.Count, ShiftToNewSetofData = 0, SetOfDataIncrementor = 1;
            string  ExpectedIMEI = string.Empty, IMEI = string.Empty, MeterID = string.Empty;
            int counterForLoop = 0;

            #region Getting the Column Number we Need
            int countExpImei = 0, countImei = 0, countMeterID = 0, countSimID = 0, countIpAddress = 0, countExpIpAddress = 0, countLRT /*last reading time*/= 0;

            while (!FromLocus_ColumnHeads_M[countExpImei].ToUpper().Contains("EXPECTEDIMEI"))
                countExpImei++;

            while (!FromLocus_ColumnHeads_M[countImei].ToUpper().Contains("IMEI"))
                countImei++;

            while (!FromLocus_ColumnHeads_M[countIpAddress].ToUpper().Contains("IPADDRESS"))
                countIpAddress++;

            while (!FromLocus_ColumnHeads_M[countExpIpAddress].ToUpper().Contains("EXPECTEDIPADDRESS"))
                countExpIpAddress++;

            while (!FromLocus_ColumnHeads_M[countMeterID].ToUpper().Contains("METERID"))
                countMeterID++;

            while (!FromLocus_ColumnHeads_M[countSimID].ToUpper().Contains("SIMCARDID"))
                countSimID++;

            while (!FromLocus_ColumnHeads_M[countLRT].ToUpper().Contains("LASTREADINGTIME"))
                countLRT++;



            #endregion Getting the Column Number we Need

            foreach (string data in FromLocus_ColumnData_M)
            {
                if (ShiftToNewSetofData < FromLocus_ColumnData_M.Count)
                {
                    #region EXPECTEDIMEI
                    try
                    {
                        if (!FromLocus_ColumnData_M[ShiftToNewSetofData + countExpImei].ToUpper().Contains("NULL"))
                        {
                            if(long.TryParse(FromLocus_ColumnData_M[ShiftToNewSetofData + countExpImei], out long tempLong))
                                FromVision_ExpIMEI.Add(tempLong);
                        }  
                        else
                        {
                            flag_FaultyMeter = true;
                            FromVision_ExpIMEI.Add(null);//null to put
                        }
                            
                    }
                    catch {}
                    #endregion EXPECTEDIMEI

                    #region Last reading Time
                    try
                    {

                        if (!FromLocus_ColumnData_M[ShiftToNewSetofData + countLRT].ToUpper().Contains("NULL"))
                        {
                            if (DateTime.TryParse(FromLocus_ColumnData_M[ShiftToNewSetofData + countLRT], out DateTime result))
                                FromLocus_LastReadingTime.Add(result);
                        }

                        else
                        {
                            flag_FaultyMeter = true;    //add null here
                            FromLocus_LastReadingTime.Add(null);
                        }

                    }
                    catch { }
                    #endregion LRT

                    #region IMEI
                    try
                    {

                        if (!FromLocus_ColumnData_M[ShiftToNewSetofData + countImei].ToUpper().Contains("NULL"))
                        {
                            if (long.TryParse(FromLocus_ColumnData_M[ShiftToNewSetofData + countImei], out long tempLong))
                                FromLocus_T39IMEI.Add(tempLong);
                        }
                            
                        else
                        {
                            flag_FaultyMeter = true;    //add null here
                            FromLocus_T39IMEI.Add(null);
                        }
                            
                    }
                    catch {}
                    #endregion IMEI

                    #region IP
                    try
                    {

                        if (!FromLocus_ColumnData_M[ShiftToNewSetofData + countIpAddress].ToUpper().Contains("NULL"))
                        {
                            FromLocus_T39IPAddr.Add(FromLocus_ColumnData_M[ShiftToNewSetofData + countIpAddress].Trim(' '));
                        }

                        else
                        {
                            flag_FaultyMeter = true;
                            FromLocus_T39IPAddr.Add(null);
                        }
                    }
                    catch { }
                    #endregion IP

                    #region ExpIP
                    try
                    {

                        if (!FromLocus_ColumnData_M[ShiftToNewSetofData + countExpIpAddress].ToUpper().Contains("NULL"))
                        {
                            FromVision_ExpIPAddr.Add(FromLocus_ColumnData_M[ShiftToNewSetofData + countExpIpAddress].Trim(' '));
                        }

                        else
                        {
                            flag_FaultyMeter = true;
                            FromVision_ExpIPAddr.Add(null);
                        }
                    }
                    catch { }
                    #endregion ExpIP

                    #region MeterID
                    try
                    {
                        FromLocus_MetersListGood.Add(FromLocus_ColumnData_M[ShiftToNewSetofData + countMeterID].Trim(' '));
                        //if (!flag_faultyMeter)
                        //{
                        //    MetersListfromLocus_Good.Add(ColumnData_M[ShiftToNewSetofData + countMeterID].Trim(' '));
                        //}

                        //else if (flag_faultyMeter)
                        //{
                        //    MetersListfromLocus_Faulty.Add(ColumnData_M[ShiftToNewSetofData + countMeterID].Trim(' '));   
                        //    //debug commented, else uncomment
                        //    flag_faultyMeter = false; counterForLoop--;

                        //    //NOTE: if the meter is defined faulty then the counter decrements
                        //    //to keep the Index of the lists controlled and not out of range.
                        //}
                    }
                    catch {}
                    #endregion MeterID

                    #region SimCard
                    try
                    {
                        if (!FromLocus_ColumnData_M[ShiftToNewSetofData + countSimID].ToUpper().Contains("NULL"))
                        {
                            FromLocus_SimcardID.Add(FromLocus_ColumnData_M[ShiftToNewSetofData + countSimID].Trim(' '));
                        }
                        else
                        {
                            flag_FaultyMeter = true;
                            FromLocus_SimcardID.Add(null);
                        }
                    }
                    catch {}
                    #endregion SimCard

                    #region Comparing the results
                    try
                    {
                        if ((FromVision_ExpIMEI[counterForLoop] == FromLocus_T39IMEI[counterForLoop]) && (FromLocus_T39IPAddr[counterForLoop] == FromVision_ExpIPAddr[counterForLoop]))//IPAddr[counterForLoop].Contains(ExpectedIPAddr[counterForLoop])
                        {
                            Appl_Responses_CompareIMEI.Add("\r\n" + "[mID]" + FromLocus_MetersListGood[counterForLoop] + " [fV]" + FromVision_ExpIMEI[counterForLoop] + " [LR]" + FromLocus_T39IMEI[counterForLoop] + " are same. [IP]"+ FromLocus_T39IPAddr[counterForLoop]+"-- [eIP]"+FromVision_ExpIPAddr[counterForLoop]);
                        }
                        else
                            Appl_Responses_CompareIMEI.Add("\r\n" + "[mID]"+FromLocus_MetersListGood[counterForLoop] + " [fV]" + FromVision_ExpIMEI[counterForLoop] + " [LR]" + FromLocus_T39IMEI[counterForLoop] + " are NOT same. [IP]" + FromLocus_T39IPAddr[counterForLoop] + "-X-[eIP]" + FromVision_ExpIPAddr[counterForLoop]);
                    }
                    catch {}
                    #endregion Comparing the results

                    ShiftToNewSetofData = SetOfDataIncrementor * lengthOfString;
                    //changes the set to next meter, if 12 rows for single set then it shifts to 13th row.
                    SetOfDataIncrementor++; counterForLoop++;
                }
            }
        }

        private async void button_FinalFileUpload_Click(object sender, EventArgs e)
        {
            ClearListsStrings();

            #region Progressbar
            progressBar_FinalFile.Value = 0; progressBar_FinalFile.Maximum = 200; progressBar_FinalFile.Step = 10;
            #endregion Progressbar

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = DeclarationClass.PATHFORFINALFILE;
                openFileDialog1.Filter = "txt files (*.txt)|*.txt"; //"All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 3;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    progressBar_FinalFile.PerformStep();
                    _filePath = openFileDialog1.FileName;
                    textBox3_finalFile.Text = _filePath;

                    label10.AutoSize = true;
                    label10.ForeColor = _filePath.Contains(DeclarationClass.PATHFORFINALFILE + _fileNameOnly + DeclarationClass.TextFormat) ? Color.Green : Color.Red;
                    label10.Text = _filePath.Contains(DeclarationClass.PATHFORFINALFILE + _fileNameOnly + DeclarationClass.TextFormat) ? "File name matches to the recently created file." : "File name does not match to the recently created file, Verify!";


                    ConfirmationBox DCB = new ConfirmationBox(openFileDialog1.FileName);
                    DialogResult Dialog12 = DCB.ShowDialog();

                    if (Dialog12 == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(_filePath))
                        {
                            //write the code here for uploading the text file with the meters
                            APICalls_AE API = new APICalls_AE();
                            progressBar_FinalFile.PerformStep();

                            if (await API.AuthenticationStarter())
                            {
                                progressBar_FinalFile.PerformStep();
                                string result = await API.FileUploadToLocus(false, _filePath);
                                RTB1.Text = result;
                            }
                            progressBar_FinalFile.PerformStep();
                        }
                        else
                            RTB1.AppendText("\r\nThe file is not selected corectly, try again!");
                    }
                    else
                    {
                        RTB1.Text = "\r\nThe file selection is CANCELLED, try again!";
                    }
                }
            }
            progressBar_FinalFile.Value = progressBar_FinalFile.Maximum;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_finalFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            ClearListsStrings(); groupBox2_2.Visible = false; groupBox2_3.Visible = false;
        }

        #endregion ListParser

        #region ResponseParser Tab1

        public void ResponseParser(string ProcessedResponse, string unProcessedResponse)
        {
            if (ProcessedResponse.Length > 10)
            {
                ProcessedResponse = ProcessedResponse.Substring(ProcessedResponse.IndexOf("{") + 1);
                _tempString = ProcessedResponse.Substring(ProcessedResponse.IndexOf(DeclarationClass.SIMNUMBER) + 7);
                _tempString = _tempString.Substring(_tempString.IndexOf("\"") + 1, 21);
                _tempString = _tempString.TrimStart(DeclarationClass.trimmerArray);//'\"', ':' --- '\"', ','
                _tempString = _tempString.TrimEnd(DeclarationClass.trimmerArray);
                
                Appl_DisplayStringList.Add("SimID: " + _tempString);

                _tempString = ProcessedResponse.Substring(ProcessedResponse.IndexOf(DeclarationClass.IMEI) + 6);
                _tempString = _tempString.Substring(_tempString.IndexOf("\"") + 1, 16);
                _tempString = _tempString.TrimStart(DeclarationClass.trimmerArray);
                _tempString = _tempString.TrimEnd(DeclarationClass.trimmerArray);
                
                Appl_DisplayStringList.Add("IMEI: " + _tempString);

                _tempString = ProcessedResponse.Substring(ProcessedResponse.IndexOf(DeclarationClass.REQUESTTYPE) + 14); 
                _tempString = _tempString.Substring(_tempString.IndexOf("\"") + 1);
                _tempString = _tempString.Substring(0, _tempString.IndexOf("\""));
                _tempString = _tempString.TrimStart(DeclarationClass.trimmerArray);
                _tempString = _tempString.TrimEnd(DeclarationClass.trimmerArray);
                
                Appl_DisplayStringList.Add("Request Type: " + _tempString);
                Appl_DisplayStringList.Add("---------------");
            }
            if (unProcessedResponse.Length > 10)
            {
                _tempString = unProcessedResponse.Substring(unProcessedResponse.IndexOf(DeclarationClass.ERROR) + 8);
                _tempString = _tempString.Substring(_tempString.IndexOf("\"") + 1);
                _tempString = _tempString.Substring(0, _tempString.IndexOf("\","));
                _tempString = _tempString.TrimStart(DeclarationClass.trimmerArray);
                _tempString = _tempString.TrimEnd(DeclarationClass.trimmerArray);
                _tempString = _tempString.Replace('\"', ' ');
                _tempString = _tempString.Replace('\\', ' ');

                Appl_DisplayStringList.Add("Error Message: " + _tempString);
            }
        }

        #endregion ResponseParser

        #region Excel Tab1

        #region Shifted codes
        /*
        public int ExcelExtraction(string InputFilename)
        {
            int? DatasetC1_ColumnNumber = 0, DatasetC2_ColumnNumber = 0, DatasetC3_ColumnNumber = 0, DatasetC4_ColumnNumber = 0, DatasetC5_ColumnNumber = 0, DatasetC6_ColumnNumber = 0; bool Flag_Nextstep = false;
            try
            {
                _Application excel = new _Excel.Application();
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
                        //
                        else if (isEqual.ToUpper() == "BATCH" || string.Equals(isEqual.ToUpper(), "BATCH"))
                            DatasetC4_ColumnNumber = sheetColumn;
                        else if (isEqual.ToUpper() == "METERTYPECODE" || string.Equals(isEqual.ToUpper(), "METERTYPECODE"))
                            DatasetC5_ColumnNumber = sheetColumn;
                        else if (isEqual.ToUpper() == "AMRCHKDATE" || string.Equals(isEqual.ToUpper(), "AMRCHKDATE"))
                            DatasetC6_ColumnNumber = sheetColumn;

                        //
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
                            Dataset_SimID.Add(TempDynamicString1 + string.Empty);
                            dynamic TempDynamicString2 = ws.Cells[sheetRow, DatasetC2_ColumnNumber].Value2;
                            Dataset_IMEI.Add(TempDynamicString2 + string.Empty);
                            //

                            dynamic TempDynamicString4;
                            try
                            {
                                TempDynamicString4 = ws.Cells[sheetRow, DatasetC4_ColumnNumber].Value2;
                                Dataset_batch.Add(TempDynamicString4 + string.Empty);
                            }
                            catch { Dataset_batch.Add(null); }

                            try
                            {
                                TempDynamicString4 = ws.Cells[sheetRow, DatasetC5_ColumnNumber].Value2;
                                Dataset_MTypeCode.Add(TempDynamicString4 + string.Empty);
                            }
                            catch { Dataset_MTypeCode.Add(null); }

                            try
                            {
                                TempDynamicString4 = ws.Cells[sheetRow, DatasetC6_ColumnNumber].Value2;
                                Dataset_commChkDate.Add(TempDynamicString4 + string.Empty);
                            }
                            catch { Dataset_commChkDate.Add(null); }

                            dynamic TempDynamicString3 = ws.Cells[sheetRow, DatasetC3_ColumnNumber].Value2;
                            string tempString = TempDynamicString3 + string.Empty;
                            tempString = tempString.Trim(trimmerArray);
                            tempString = tempString.Trim(trimmerArray);
                            tempString = tempString.Trim(trimmerArray);
                            tempString = tempString.Trim(trimmerArray);
                            tempString = tempString.Trim(trimmerArray);

                            Dataset_MeterID.Add(tempString);

                            sheetRow++;
                        }
                        catch
                        {
                            sheetRow++;
                        }
                    }
                    try { Dataset_SimID.RemoveAt(0); Dataset_MeterID.RemoveAt(0); Dataset_IMEI.RemoveAt(0); } catch { }

                    try { Dataset_batch.RemoveAt(0); } catch { }

                    try { Dataset_commChkDate.RemoveAt(0); } catch { }

                    try { Dataset_MTypeCode.RemoveAt(0); } catch { }

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

                return Dataset_SimID.Count();
            }
            catch(Exception e){ MessageBox.Show(string.Empty + e); return 0; }
        }

        public string CsvWritingAsActivationRepost()
        {
            string ReportFileCompleteName = string.Empty;
            //if (textBox_tab1_browse.Text.EndsWith(".xls"))
            //    ReportFileCompleteName = textBox_tab1_browse.Text.Replace(".xls", "_ActReport.csv");
            //else
            //    ReportFileCompleteName = textBox_tab1_browse.Text.Replace(".xlsx", "_ActReport.csv");

            ReportFileCompleteName = textBox_tab1_browse.Text.EndsWith(".xls") ? textBox_tab1_browse.Text.Replace(".xls", "_ActReport.csv") : textBox_tab1_browse.Text.Replace(".xlsx", "_ActReport.csv");

            string path = ReportFileCompleteName;

            // Set the variable "delimiter" to ",".
            string delimiter = ",";

            // This text is added only once to the file.
            string createText = "SimCardID" + delimiter + "IMEI" + delimiter + "Status/Time" + delimiter + Environment.NewLine;
            File.WriteAllText(path, createText);

            int counter = 0;

            foreach (string str in ResponseActivation)
            {
                try
                {
                    createText = "\'"+ Dataset_SimID[counter] + delimiter + "\'" + Dataset_IMEI[counter] + delimiter + ResponseActivation[counter] + ": " + DateTime.Now.ToString("MM/dd/yyyy.HH:mm:ss") + delimiter + Environment.NewLine;
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
        */
        #endregion Shifted codes

        public static void GC()
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            //System.GC.Collect();
            //System.GC.WaitForPendingFinalizers();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Help.Checked)
                RTB1.Text = VersionClass.HELPTEXT;
            else
                RTB1.Clear();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            ClearListsStrings();

            label11.Visible = true;
            label11.ForeColor = Color.Green;
            label11.AutoSize = true;
            label11.Text = "The File is being worked on, Wait!!";

            groupBox2_2.Visible = false; groupBox2_3.Visible = false; label_2.Visible = false; label_3.Visible = false;
            progressBar_StartButton.Value = 0; progressBar_FinalFile.Value = 0;
            textBox3_finalFile.Clear();

            #region ProgressBar
            progressBar_initialFile.Maximum = 200; progressBar_initialFile.Visible = true;
            progressBar_initialFile.Value = 0;
            progressBar_initialFile.Step = 10;
            #endregion ProgressBar

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = @"F:\simAutomationFile";
                openFileDialog1.Filter = "xls files (*.xls)|*.xls |xlsx files" +
                                         " (*.xlsx)|*.xlsx | All files (*.*)|*.*";

                RTB1.Clear();

                openFileDialog1.FilterIndex = 3;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    progressBar_initialFile.PerformStep();
                    _filePath = openFileDialog1.FileName;

                    if (string.IsNullOrEmpty(_filePath))
                        RTB1.AppendText("You have not Selected any Valid File.");

                    else if (textBox_tab1_browse.Text.ToUpper().EndsWith("_ACTREPORT.CSV"))
                        RTB1.Text = "You have not Selected any Valid File.";

                    else if (_filePath.EndsWith(".xls") || _filePath.EndsWith(".xlsx"))
                    {
                        TextBox_InitialFileUpload.Text = _filePath; progressBar_initialFile.PerformStep();
                        _fileNameOnly = string.Empty;
                        _fileNameOnly = _filePath.Substring(_filePath.LastIndexOf("\\")+1);
                        _fileNameOnly = _fileNameOnly.Substring(0,_fileNameOnly.LastIndexOf(".xls"));

                        string FilePaThWithoutExtension = _filePath.Substring(0, _filePath.LastIndexOf(".xls"));

                        FileParserClass FPC = new FileParserClass();
                        //function ot parse the Excel
                        int count = FPC.ExcelExtraction(_filePath);

                        this.FromFile_SimID.AddRange(FPC.dataset_SimID);
                        this.FromFile_IMEI.AddRange(FPC.dataset_IMEI);
                        this.FromFile_MeterID.AddRange(FPC.dataset_MeterID);
                        this.FromFile_batch.AddRange(FPC.dataset_batch);
                        this.FromFile_MTypeCode.AddRange(FPC.dataset_MTypeCode);
                        this.FromFile_commChkDate.AddRange(FPC.dataset_commChkDate);

                        //Clearing the unused data from the FileParser
                        FPC.dataset_SimID.Clear(); FPC.dataset_IMEI.Clear(); FPC.dataset_MeterID.Clear(); FPC.dataset_batch.Clear(); FPC.dataset_MTypeCode.Clear(); FPC.dataset_commChkDate.Clear();

                        if (count>1)
                        {
                            progressBar_initialFile.PerformStep();
                            FileCreatorClass.TxTForInitialFileUpload(DeclarationClass.PATHFORINITIALFILE + _fileNameOnly + DeclarationClass.TextFormat, count, FromFile_SimID, FromFile_IMEI, FromFile_MeterID);
                            progressBar_initialFile.PerformStep();
                        }
                        ConfirmationBox DCB = new ConfirmationBox(DeclarationClass.PATHFORINITIALFILE + _fileNameOnly + DeclarationClass.TextFormat);
                        DialogResult Dialog12 = DCB.ShowDialog();

                        progressBar_initialFile.PerformStep();
                        if (Dialog12 == DialogResult.OK)
                        {
                            flag_CancelProgress = false;
                            APICalls_AE API = new APICalls_AE();
                            if (await API.AuthenticationStarter())
                            {
                                progressBar_initialFile.PerformStep();
                                string result = await API.FileUploadToLocus(true, DeclarationClass.PATHFORINITIALFILE + _fileNameOnly + DeclarationClass.TextFormat);//initial file upload

                                //Formats
                                //meterid, simcardid, imei, ipaddress, metertypecode, batch, commchkdate - final file
                                //METERID,IMEI,SIMCARDID - initial file

                                progressBar_initialFile.PerformStep();
                                RTB1.SelectionColor = result.Contains("\"statusCode\":400") ? Color.Red : Color.Green;
                                RTB1.Text = result;
                                RTB1.AppendText(result.Contains("\"statusCode\":400") ? Environment.NewLine + "File is NOT uploaded." : Environment.NewLine + "File is uploaded");

                                RTB1.AppendText( DeclarationClass.multiLine + VersionClass.HELPERRORTEXT);
                                progressBar_initialFile.PerformStep();
                                groupBox2_2.Visible = true; label_2.Visible = true;
                            }
                        }
                        else
                            flag_CancelProgress = true;
                    } 
                }
            }
            if (flag_CancelProgress){
                flag_CancelProgress = false; groupBox2_2.Visible = false; label_2.Visible = false;
                progressBar_initialFile.Value = 0; label11.Visible = false;
            }

            else{
                progressBar_initialFile.Value = progressBar_initialFile.Maximum; label11.Visible = false;
            }
                
        }

        #endregion Excel

        #region CSV maker Tab2

        public void CSVcreateFromAPI([Optional] string FileNameOnly)
        {
            try
            {
                string headerS = string.Empty;
                foreach (string Header in FromLocus_ColumnHeads_M)
                {
                    headerS += Header + ",";
                }
                headerS = headerS.Trim(DeclarationClass.trimmerArray);
                string TempFileExtension = DeclarationClass.PATHFORAPIFILE + FileNameOnly + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".csv";
                Directory.CreateDirectory(DeclarationClass.PATHFORAPIFILE);
                //File.Create(TempFileExtension);
                File.WriteAllText(TempFileExtension, headerS +",imeiPWDfromSIMID,result"+ "\r\n");

                headerS = string.Empty;
                int lengthOfString = FromLocus_ColumnHeads_M.Count; int temp = 0; int num = 1; int tempCounterForHeaderCount = 0;
                int count = 0;
                for (int DataCount = 0; DataCount <= FromLocus_ColumnData_M.Count; DataCount++)
                {
                    if (temp < FromLocus_ColumnData_M.Count)
                    {
                        headerS = string.Empty;
                        while (tempCounterForHeaderCount < FromLocus_ColumnHeads_M.Count)
                        {
                            if(FromLocus_ColumnData_M[temp + tempCounterForHeaderCount]!=null)
                            {
                                headerS += FromLocus_ColumnData_M[temp + tempCounterForHeaderCount] + ",";
                            }
                            else
                            {
                                headerS += "null" + ",";
                            }
                            tempCounterForHeaderCount++;
                        }
                        tempCounterForHeaderCount = 0; headerS = headerS.Trim(DeclarationClass.trimmerArray);
                        if(count>= FromPWS_IMEI.Count)
                            File.AppendAllText(TempFileExtension, headerS + "," + "IMEIFromPWS(Blank)" + "," + "CorrectionCheck(Blank)"+ DeclarationClass.newLine);
                        else
                            File.AppendAllText(TempFileExtension, headerS +","+FromPWS_IMEI[count]+","+ Appl_CorrectionCheck[count] + "\r\n");

                        temp = num * lengthOfString;
                        num++;count++;
                    }
                }
            }
            catch { RTB1.AppendText("\r\nFile creation catch.(manual response)"); }
        }

        #endregion CSV maker

        #region ClearFunction
        public void ClearListsStrings()
        {
            //Lists
            FromFile_SimID.Clear();
            FromFile_IMEI.Clear();
            FromFile_MeterID.Clear();
            //RemovedColumns.Clear();
            Appl_ResponseActivation_ToFile.Clear();
            Appl_DisplayStringList.Clear();
            FromLocus_SimcardID.Clear();
            Appl_Responses_CompareIMEI.Clear();
            FromPWS_IMEI.Clear();
            Appl_CorrectionCheck.Clear();
            ToLocus_MetersList.Clear();
            FromLocus_ColumnHeads_M.Clear();
            FromLocus_ColumnData_M.Clear();
            FromVision_ExpIMEI.Clear();
            FromLocus_T39IMEI.Clear();
            FromLocus_MetersListGood.Clear();
            FromLocus_T39IPAddr.Clear();
            FromVision_ExpIPAddr.Clear();
            FromFile_batch.Clear();
            FromFile_MTypeCode.Clear();
            FromFile_commChkDate.Clear();
            FromLocus_MetersListFaulty.Clear();


            //Strings and variables
            //filePath = string.Empty;
            _processedResponse = string.Empty;
            _unProcessedResponse = string.Empty;
            _tempString = string.Empty;
            _tempStringResponse = string.Empty;

            //Arrays
            Array.Clear(StringStorage, 0, StringStorage.Length);
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox_customRateEnter.Checked)
            {
                textBox_CustomRateEnter.Visible = true;
            }
            if (!checkBox_customRateEnter.Checked)
            {
                textBox_CustomRateEnter.Clear();
                textBox_CustomRateEnter.Text = "AlsoEnergy VZW PN 1MB";
                textBox_CustomRateEnter.Visible = false;
            }
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                //groupBox_2_1.Visible = true;
                groupBox2_2.Visible = true;
                groupBox2_3.Visible = true; label_2.Visible = true; label_3.Visible = true;
            }
            else
            {
                //groupBox_2_1.Visible = false;
                groupBox2_2.Visible = false;
                groupBox2_3.Visible = false; label_2.Visible = false; label_3.Visible = false;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treeView1.SelectedNode.Text.ToUpper() == "PWS")
            {
                RTB1.Clear();
                RTB1.Text = File.ReadAllText(DeclarationClass.DOCUMENTATION_PWS);
            }
            else if (treeView1.SelectedNode.Text.ToUpper() == "LOCUS")
            {
                RTB1.Clear();
                RTB1.Text = File.ReadAllText(DeclarationClass.DOCUMENTATION_LOCUS);
            }
            else if (treeView1.SelectedNode.Text.ToUpper() == "ERROR CODES")
            {
                RTB1.Clear();
                RTB1.Text = File.ReadAllText(DeclarationClass.DOCUMENTATION_ErrorCodes);
            }
            else if (treeView1.SelectedNode.Text.ToUpper() == "SUPPORT AND FOLDER LINKS")
            {
                RTB1.Clear();
                RTB1.Text = File.ReadAllText(DeclarationClass.DOCUMENTATION_SUPPORT);
            }
            else if (treeView1.SelectedNode.Text.ToUpper() == "VERSION NUMBER")
            {
                RTB1.Clear();
                RTB1.Text = File.ReadAllText(DeclarationClass.DOCUMENTATION_VERSION);
            }
            else if (treeView1.SelectedNode.Text.ToUpper() == "VERSION INFORMATION")
            {
                RTB1.Clear();
                RTB1.Text = File.ReadAllText(DeclarationClass.DOCUMENTATION_VERSIONDISC);
            }
        }

        private void treeView1_MouseEnter(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(DeclarationClass.DOCUMENTATION_PDF);
            RTB1.Text = "YOu can paste the data in the Address bar to open the File Directly."+DeclarationClass.newLine+ DeclarationClass.DOCUMENTATION_PDF;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
        }

        private void textBox_tab1_IMEI_ChechStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        #endregion ClearFunction

        #region Threading BG

        private BackgroundWorker myBackgroundWorker;//myBackgroundWorker.RunWorkerAsync(2)
        #region myBackgroundWorker
        int counterAsReply; string _responseString = string.Empty;
        
        private void myBackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            //ExcelWriting(textBox_tab1_browse.Text);
        }
        private void myBackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null) {string msg = String.Format("An error occurred: {0}", e.Error.Message); MessageBox.Show(msg);}
            else
            {
                string tempAddress = textBox_tab1_browse.Text.Replace(".xls", "_ActReport001.xls");
                RTB1.AppendText("\r\nFile Created in the Directory! - "+ tempAddress);
            }
        }

        private void myBackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e){}
        #endregion #region myBackgroundWorker


        #endregion Threading BG

        #region CommentedCode Important
        //if (!File.Exists(IsFileCreatedAlready))
        //{
        //    ws.Cells[sheetRow, 1].Value2 = "SimCardID"; 
        //    ws.Cells[sheetRow, 2].Value2 = "IMEI"; 
        //    ws.Cells[sheetRow, 3].Value2 = "Status/Time"; sheetRow++;
        //    foreach (string str in ResponseActivation)
        //    {
        //        try
        //        {
        //            ws.Cells[sheetRow, 1].Value2 = "\'" + DatasetC1[counter];
        //            ws.Cells[sheetRow, 2].Value2 = "\'"+ DatasetC2[counter];
        //            ws.Cells[sheetRow, 3].Value2 = ResponseActivation[counter]+": "+DateTime.Now.ToString("MM/dd/yyyy.HH:mm");

        //            counter++;
        //            sheetRow++;
        //        }
        //        catch
        //        {
        //            sheetRow++; counter++;
        //        }
        //    }
        //    string tempAddress = string.Empty;
        //    if (textBox_tab1_browse.Text.EndsWith(".xls"))
        //        tempAddress = textBox_tab1_browse.Text.Replace(".xls", "_ActReport.xlsx");
        //    else
        //        tempAddress = textBox_tab1_browse.Text.Replace(".xlsx", "_ActReport.xlsx");
        //    try
        //    {
        //        wb.SaveAs(tempAddress);
        //    }
        //    catch
        //    {
        //        DialogResult DR =  MessageBox.Show("The File, application is trying to overwrite\r\nis already open in MS office.\r\nClose that File and hit OK.\r\nHit Cancel to create a new File.", "File already Open", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

        //        if (DR== DialogResult.Cancel)
        //        {
        //            if (textBox_tab1_browse.Text.EndsWith(".xls"))
        //                tempAddress = textBox_tab1_browse.Text.Replace(".xls", "_ActReport001.xlsx");
        //            else
        //                tempAddress = textBox_tab1_browse.Text.Replace(".xlsx", "_ActReport001.xlsx");

        //            wb.SaveAs(tempAddress);}
        //        else 
        //        {
        //            try { wb.SaveAs(tempAddress); }
        //            catch { MessageBox.Show("Wrong selection, the file is not created."); }
        //        }
        //    }
        //    wb.Close(0);
        //}
        #endregion CommentedCode Important
    }

}
