using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAutomation
{
    class VersionClass
    {
        static public string VERSIONNUMBER = "V 0.0.1(G)";//GitVisual Comment
        static public string VERSIONDETAILS = "Final version, Added The Locus File handle. Stable version!";
        static public string HELPTEXT =
                            "\r\nTab1: Sim Info" +
                            "\r\n\r\nActivate Individual Sim:" +
                            "\r\nIt is used to activate any single SimID. Manually Paste/Type the details and Hit Activate Sim." +
                            "\r\n\r\nCheck Status of Individual Sim Card:" +
                            "\r\nIt is used to check the activation status of any Individual Sim. Manually Paste/Type the details and Hit Check status." +
                            "\r\n to view all the techincal info of that specific ID, use Checkbox \"view Technical Details\"" +
                            "\r\nTo view the status of the File you have selected, use Checkbox\"view Status for all" +
                            "\"\r\n\r\nBrowse:\r\nHelps you to navigate through the File explorer to select the File." +
                            "\r\nTab 2: Documentation is empty.";

        static public string HELPERRORTEXT =
                            "Successful 2xx class of status code indicates that the client's request was successfully received, understood, and accepted." + DeclarationClass.newLine +
                            "201 - The request has been fulfilled and resulted in a new resource being created. " + DeclarationClass.newLine + DeclarationClass.newLine +
                            "Client Error 4xx class of status code is intended for cases in which the client seems to have erred." + DeclarationClass.newLine +
                            "400 - The request could not be understood by the server due to malformed syntax." + DeclarationClass.newLine +
                            "401 - The request requires user authentication." + DeclarationClass.newLine;
    }
}
