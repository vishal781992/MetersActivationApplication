using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
//using System.Threading.Tasks;
//using System.Windows.Forms;

namespace SimAutomation
{
    /*
     * This calls APICalls_PWs is used for PWS sim activations and related services.
     */

    class APICalls_PWS
    {
        #region Declaration
        string ResponseString = string.Empty;
        private string base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("1bfb476eed1fb95a7ac9defbb14862cd:b9609aafe7537cxxxxxxxxxxxxxxxxxx"));
        public string PlanForActivation = string.Empty;// = "\"AlsoEnergy VZW PN 1MB\"";

        #endregion Declaration

        #region Constructor PlanForAct input
        public APICalls_PWS(string planForActivation)
        {
            this.PlanForActivation = planForActivation;
        }
        #endregion Constructor

        #region ActivationFunction
        public async Task<string> SimActivationRequest(string IMEI, string SIMNo)//Task<void>
        {
            using (var SimActivation = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), DeclarationClass.APILINK + "device_operations/activation_request"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    request.Content = new StringContent("{"+ DeclarationClass.RATEPLAN_API + " : \"" + PlanForActivation + "\", "+DeclarationClass.ACTIVATIONREQUEST + ":[{"+ DeclarationClass.IMEI+ " : \"" + IMEI + "\" , "+ DeclarationClass.SIMNUMBER + " :\"" + SIMNo + "\"}]}");

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    var response = await SimActivation.SendAsync(request);//httpClient
                    ResponseString = await response.Content.ReadAsStringAsync();
                }
                return ResponseString;
            }
        }
        #endregion Activation Function

        #region Device Details
        public async Task<string> DeviceDetails(string SIMNo)//Task<void>
        {
            using (var SimDetails = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), DeclarationClass.APILINK + "devices/" + SIMNo))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");
                    var response = await SimDetails.SendAsync(request);
                    ResponseString = await response.Content.ReadAsStringAsync();
                }
                if (ResponseString == null || ResponseString == "null")
                    return ResponseString = DeclarationClass.NODATA;
                return ResponseString;
            }
        }
        #endregion Device Details

    }
    class APICalls_AE
    {
        #region init
        private const string ACCESSTOKEN = "{\"access_token\":\"";//"\"access_token\": \"";
        private const string STATUSCODE = "{\"statusCode\":";
        private const string LOCUSWEBAPI = "https://api.locusenergy.com/";
        int n_CountForLinesInResponse = 0;
        string TOKEN = string.Empty;

        public List<string> Appl_ColumnHeads = new List<string>();
        public List<string> Appl_ColumnData = new List<string>();

        public string[,] ArrayForDataLocus = new string[3000, 150];
        public char[] TrimArray = new char[] { '\\', '/', '\"', ',', '}', '{' };
        #endregion init

        #region Authentication
        public async Task<bool> AuthenticationStarter()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), LOCUSWEBAPI + "oauth/token"))
                {
                    request.Content = new StringContent("grant_type=password&client_id=xxxxxxxxxxxxx&client_secret=xxxxxxxxxxxx&username=xxxxx@XXX.com&password=xxxx");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    string ResponseString = await response.Content.ReadAsStringAsync();
                    TOKEN = ResponseString.Substring(ResponseString.IndexOf(ACCESSTOKEN) + ACCESSTOKEN.Length, ResponseString.IndexOf(",") - (ACCESSTOKEN.Length + 1));//1 for comma
                    if (!string.IsNullOrEmpty(TOKEN))
                        return true;
                    else
                        return false;
                }
            }
        }
        #endregion Authentication

        #region Get MeterInfo
        public async Task<bool> MeterInfo(string MeterString, int count)//(string MetersList)
        {
            string ResponseString = string.Empty;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), LOCUSWEBAPI+"v3/meters?ids=" + MeterString))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + TOKEN);

                    var response = await httpClient.SendAsync(request);
                    ResponseString = await response.Content.ReadAsStringAsync();
                }
            }
            StringProcessor(ResponseString, count);
            return true;
        }
        #endregion meterinfo

        #region Push File upload(initial and final)
        public async Task<string> FileUploadToLocus(bool FileType_initial, string FileAddr)
        {
            //filetype_initial is true when it is initial file else false 
            string LinkTOUPload = FileType_initial ? DeclarationClass.APILINK_REGISTERFILE : DeclarationClass.APILINK_COMMISSIONFILE;
            string FileNameToSend = FileType_initial ? "Initial_File" : "Final_File";

            string FileAddrString = FileAddr.Substring(FileAddr.LastIndexOf(@"\")+1);
            string ResponseString = string.Empty;


            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), LinkTOUPload))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + TOKEN);

                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(new ByteArrayContent(File.ReadAllBytes(FileAddr)), FileNameToSend, Path.GetFileName(FileAddrString));
                    request.Content = multipartContent;

                    var response = await httpClient.SendAsync(request);
                    ResponseString = await response.Content.ReadAsStringAsync();
                }
            }
            return ResponseString;
        }
        #endregion Push File upload(initial and final)

        #region String Processor
        public void StringProcessor(string ResponseString, int count)
        {
            int d = ResponseString.IndexOf(STATUSCODE);
            try
            {
                string statusCd = ResponseString.Substring(0, ResponseString.IndexOf(","));
                ResponseString = ResponseString.Substring(statusCd.Length, ResponseString.Length - statusCd.Length);
                ResponseString = ResponseString.Substring(ResponseString.IndexOf("{"), ResponseString.Length - ResponseString.IndexOf("{"));
                if (statusCd.Contains("200") && ResponseString.Length > 30)
                {
                    for (int counter = 1; counter <= count; counter++)
                    {
                        int dd = ResponseString.IndexOf("}"); int ddd = ResponseString.IndexOf("{");
                        if (ResponseString.Length > 4)
                        {
                            try
                            {
                                string temp = ResponseString.Substring(ResponseString.IndexOf("{"), ResponseString.IndexOf("}") - (ResponseString.IndexOf("{") + 1));

                                string[] tempStorage = temp.Split(',');
                                int tempCounter = 0;
                                foreach (string n in tempStorage)
                                {
                                    string tempSt = n;
                                    tempSt = tempSt.Trim(TrimArray);
                                    tempSt = tempSt.Replace('\"', ' ');
                                    ArrayForDataLocus[counter - 1, tempCounter] = tempSt;
                                    tempCounter++;
                                }
                                if (counter == 1)
                                    n_CountForLinesInResponse = tempCounter;
                                ResponseString = ResponseString.Substring(ResponseString.IndexOf('}') + 1);
                            }
                            catch { }
                        }
                    }
                    for (int counterOuter = 0; counterOuter < count; counterOuter++)
                    {
                        for (int counterInner = 0; counterInner <= n_CountForLinesInResponse; counterInner++)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(ArrayForDataLocus[counterOuter, counterInner]))
                                {
                                    try
                                    {
                                        if (counterOuter == 0)
                                            Appl_ColumnHeads.Add(ArrayForDataLocus[counterOuter, counterInner].Substring(0, ArrayForDataLocus[counterOuter, counterInner].IndexOf(':')));
                                        Appl_ColumnData.Add(ArrayForDataLocus[counterOuter, counterInner].Substring(ArrayForDataLocus[counterOuter, counterInner].IndexOf(':') + 1));
                                    }
                                    catch { }
                                }
                            }
                            catch { }
                        }//we get the data in columnHeads and columndata
                    }
                }
            }
            catch { }
        }
        #endregion String Processor
    }
}
