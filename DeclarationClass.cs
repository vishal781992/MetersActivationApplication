using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAutomation
{
    class DeclarationClass
    {
        //Root folders are declared here
        static public string DIRECTORY_SIMAUTOMATIONPARENT = @"C:\SimAutomationLocalFiles";
        static public string DIRECTORY_SIMAUTOMATION_REPORT = @"C:\SimAutomationLocalFiles\Reports";
        static public string DIRECTORY_SIMAUTOMATION_FINALF = @"C:\SimAutomationLocalFiles\finalFiles";
        static public string DIRECTORY_SIMAUTOMATION_INITIALF = @"C:\SimAutomationLocalFiles\initialFiles";
        static public string DIRECTORY_SIMAUTOMATION_ACTIVATIONREPORTF = @"C:\SimAutomationLocalFiles\Activation_Tab1_Reports";
        static public string DIRECTORY_SIMAUTOMATION_DATABASE = @"\\192.168.1.8\VisionShared\VisionSharedFiles\SimAutomationServerFiles";

        static public string DOCUMENTATION_PWS = @"\\192.168.1.8\VisionShared\VisionSharedFiles\SimAutomationDocumentation\TextFiles\PWS.txt";
        static public string DOCUMENTATION_LOCUS = @"\\192.168.1.8\VisionShared\VisionSharedFiles\SimAutomationDocumentation\TextFiles\Locus.txt";
        static public string DOCUMENTATION_ErrorCodes = @"\\192.168.1.8\VisionShared\VisionSharedFiles\SimAutomationDocumentation\TextFiles\ErrorCodes.txt";
        static public string DOCUMENTATION_SUPPORT = @"\\192.168.1.8\VisionShared\VisionSharedFiles\SimAutomationDocumentation\TextFiles\SupportAndfolderLinks.txt";
        static public string DOCUMENTATION_VERSION = @"\\192.168.1.8\VisionShared\VisionSharedFiles\SimAutomationDocumentation\TextFiles\version.txt";
        static public string DOCUMENTATION_VERSIONDISC = @"\\192.168.1.8\VisionShared\VisionSharedFiles\SimAutomationDocumentation\TextFiles\versionInfo.txt";
        static public string DOCUMENTATION_PDF = @"\\192.168.1.8\VisionShared\VisionSharedFiles\SimAutomationDocumentation\Documentation for SimAutomation.pdf";

        //API link
        static public string APILINK = "https://api.lattigo.com/v2/";
        static public string APILINK_REGISTERFILE = "https://api.locusenergy.com/v3/meters/register";
        static public string APILINK_COMMISSIONFILE = "https://api.locusenergy.com/v3/meters/commission";

        //General Declarations here for string and chars
        static public string REQUESTTYPE = "\"request_type\"",
                    NODATA = "NODATA",
                    IMEI = "\"imei\"",
                    SIMNUMBER = "\"iccid\"",
                    ERROR = "\"error\":",
                    PROCESSEDSTRING = "\"processed\":",
                    UNPROCESSEDSTRING = "\"unprocessed\":",
                    STATEOFSIM = "\"state\"",
                    IMEISTRING = "\"imei\":\"",
                    IPADDRSTRING = "\"ip_address\":\"",
                    PATHFORAPIFILE = @"C:\SimAutomationLocalFiles\",
                    PATHFORREPORT = @"C:\SimAutomationLocalFiles\Reports\",
                    PATHFORINITIALFILE = @"C:\SimAutomationLocalFiles\initialFiles\",
                    PATHFORFINALFILE = @"C:\SimAutomationLocalFiles\finalFiles\",
                    PATHFORACTIVATIONREPOST = @"C:\SimAutomationLocalFiles\Activation_Tab1_Reports\",
                    newLine = "\r\n", multiLine = "\r\n\r\n\r\n",

                    RATEPLAN_API = "\"rate_plan_name\"",
                    ACTIVATIONREQUEST = "\"activation_requests\"",

                    TextFormat = ".txt";

        static public char[] trimmerArray = new char[] { ',', 'A', 'E', '\"', '{', '}', ' ', ':', ']', '[', };

        //Version and Help text Here
       

        static public string DISCLAIMER = DeclarationClass.multiLine + "The terms used below," + DeclarationClass.newLine + "\t[mID] - meterID." + DeclarationClass.newLine + "\t[fV] - data received from Vision." + DeclarationClass.newLine + "\t[LR] - Locus reads meters directly." + DeclarationClass.newLine + "\t[IP] - IP address read from Locus." + DeclarationClass.newLine + "\t[eIP] - IP address expected." + DeclarationClass.multiLine;

        static public string DISCLAIMER2 = "All the Results with \"**\" are good to go";




    }
}
