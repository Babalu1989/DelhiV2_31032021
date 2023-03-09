using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for clsBapiCall
/// </summary>
public class clsBapiCall
{
    static bool destinationIsInialised = false;

    public clsBapiCall()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /* ZBAPI_DISPLAY_BILL_WEB */
    public DataSet Get_ZBAPI_DISPLAY_BILL_WEB(string _gCa_nO, string _sBillMnth)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_DISPLAY_BILL_WEB _objOutPut = new ZBAPI_DISPLAY_BILL_WEB();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[5];
        DataTable dtBillDetails = new DataTable();
        DataTable dtMeterDetails = new DataTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        

        try
        {   
            clsConnect cfg = new clsConnect();
           
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DISPLAY_BILL_WEB");
                testfn.SetValue("CONTRACT_ACCOUNT", _gCa_nO);
                testfn.SetValue("BILL_MONTH", _sBillMnth);
                testfn.Invoke(dest);

                IRfcTable billDetailsTable = testfn.GetTable("TX_BILLPRINT");
                IRfcTable meterDetailsTable = testfn.GetTable("TX_BILLMETER");
                IRfcTable SAPDATA_ErrorDataTable = testfn.GetTable("RETURN");
                IRfcTable ErrorTable = testfn.GetTable("ZERR");               
                           
                dtBillDetails = _objOutPut.converttodotnetatble(billDetailsTable);
                dtMeterDetails = _objOutPut.converttodotnetatble(meterDetailsTable);
                dtRet2Table = _objOutPut.converttodotnetatble(SAPDATA_ErrorDataTable);
                dtErrTable = _objOutPut.converttodotnetatble(ErrorTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }        

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.transferBillData(dtBillDetails);
        bapiResult[1] = _objOutPut.transferMeterData(dtMeterDetails);
        bapiResult[2] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[3] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[4] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);

        return dsBAPIOutput;
    }

    /* ZBAPI_IVR_CREATESO_ISU */
    public DataSet Get_ZBAPI_IVR_CREATESO_ISU(string _strCANumber, string _strCACrn, string _strCAKNumber, string _strMeterNumber, string _strISUOrder,
                                                            string _strComplaintType, string _strContractNumber, string _strTelephoneNo, string _strDescription)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_IVR_CREATESO_ISU _objOutPut = new ZBAPI_IVR_CREATESO_ISU();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtOutputDetails = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable dtErrorTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                

                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_IVR_CREATESO_ISU");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPIIVRCOMP1");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("CA_CRN", _strCACrn);
                articol.SetValue("CA_KNO", _strCAKNumber);
                articol.SetValue("CA_NUMBER", _strCANumber);
                articol.SetValue("COMPLAINT_TYPE", _strComplaintType);
                articol.SetValue("CONTRACT_NO", _strContractNumber);
                articol.SetValue("DESCRIPTION", _strDescription);
                articol.SetValue("ISU_ORDER", _strISUOrder);
                articol.SetValue("METER_NO", _strMeterNumber);
                articol.SetValue("TEL1_NUMBR", _strTelephoneNo);

                testfn.SetValue("IMPORT_COMP", articol);              
                testfn.Invoke(dest);
                

                IRfcTable ErrorMessageTable = testfn.GetTable("BAPIMESSAGE");
                IRfcTable OutputTable = testfn.GetTable("EXPORT_COMP");                
                IRfcTable returnTable2 = testfn.GetTable("RETURN");

                dtOutputDetails = _objOutPut.converttodotnetatble(OutputTable);                
                dtreturnTable2 = _objOutPut.converttodotnetatble(returnTable2);
                dtErrorTable = _objOutPut.converttodotnetatble(ErrorMessageTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.getIVRSOISUFormattedTable(dtOutputDetails);
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable2);
        bapiResult[2] = _objOutPut.makeISUERRTable(dtErrorTable);     
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        

        return dsBAPIOutput;
    }

    ///* Z_BAPI_CMS_ISU_CA_DISPLAY */
    public DataSet Get_Z_BAPI_CMS_ISU_CA_DISPLAY(string strCANumber, string strSerialNumber, string strConsumerNumber, 
                                                                            string strTelephoneNumber,string strKNumber, string strContractNumber)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_CMS_ISU_CA_DISPLAY _objOutPut = new Z_BAPI_CMS_ISU_CA_DISPLAY();      

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[5];

        DataTable dtCADetails = new DataTable();
        DataTable dtCSDetails = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable dtErrorTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_CMS_ISU_CA_DISPLAY");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPIIVRST");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("CA_NUMBER", strCANumber);
                articol.SetValue("CONTRACT", strContractNumber);
                articol.SetValue("SERIALNO", strSerialNumber);

                testfn.SetValue("IMPORT_CANUMBER", articol);
                testfn.SetValue("IMPORT_TELEPHONE_NO", strTelephoneNumber);
                testfn.SetValue("IMPORT_KNUMBER", strKNumber);
                testfn.SetValue("IMPORT_CRNNUMBER", strConsumerNumber);

                testfn.Invoke(dest);

                IRfcTable ErrorMessageTable = testfn.GetTable("BAPIMESSAGE");
                IRfcTable OutputTable = testfn.GetTable("EXPORT_CADETAILS");
                IRfcTable CSDetailsTable = testfn.GetTable("EXPORT_CSDETAILS");
                IRfcTable returnTable2 = testfn.GetTable("RETURN");

                dtCADetails = _objOutPut.converttodotnetatble(OutputTable);
                dtCSDetails = _objOutPut.converttodotnetatble(CSDetailsTable);
                dtreturnTable2 = _objOutPut.converttodotnetatble(returnTable2);
                dtErrorTable = _objOutPut.converttodotnetatble(ErrorMessageTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }


        bapiResult[0] = _objOutPut.getCADetailsFormattedTable(dtCADetails);
        bapiResult[1] = _objOutPut.getCSDetailsFormattedTable(dtCSDetails);
        bapiResult[2] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable2);
        bapiResult[3] = _objOutPut.makeISUERRTable(dtErrorTable);
        bapiResult[4] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);

        return dsBAPIOutput;
    }

    ///* ZBAPIDOCLIST */
    public DataSet Get_ZBAPIDOCLIST(string _strAufnr, string _strC_001, string _strC_002, string _strC_003, string _strC_004, string _strC_005, string _strC_007, string _strC_008, string _strC_009, string _strC_010,
        string _strC_011, string _strC_012, string _strC_013, string _strC_014, string _strC_015, string _strC_016, string _strC_017, string _strC_018, string _strC_019, string _strC_020, string _strC_021, string _strC_022,
        string _strC_023, string _strC_024, string _strC_025, string _strC_026, string _strC_027, string _strC_028, string _strC_029, string _strC_030, string _strC_031, string _strC_032, string _strC_033, string _strC_034,
        string _strC_035, string _strC_036, string _strC_037, string _strC_038, string _strC_039, string _strC_040, string _strC_041, string _strC_070, string _strR_Cdll, string _strR_Occ, string _strR_Own, string _strZ_Appltype)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPIDOCLIST _objOutPut = new ZBAPIDOCLIST();        

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[2];

        DataTable dtRet2Table = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();       

        try
        {
            clsConnect cfg = new clsConnect();

            _strAufnr = _strAufnr.PadLeft(12, '0');

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPIDOCLIST");
                testfn.SetValue("AUFNR", _strAufnr);
                testfn.SetValue("C_001", _strC_001);
                testfn.SetValue("C_002", _strC_002);
                testfn.SetValue("C_003", _strC_003);
                testfn.SetValue("C_004", _strC_004);
                testfn.SetValue("C_005", _strC_005);
                testfn.SetValue("C_007", _strC_007);
                testfn.SetValue("C_008", _strC_008);
                testfn.SetValue("C_009", _strC_009);
                testfn.SetValue("C_010", _strC_010);
                testfn.SetValue("C_011", _strC_011);
                testfn.SetValue("C_012", _strC_012);
                testfn.SetValue("C_013", _strC_013);
                testfn.SetValue("C_014", _strC_014);
                testfn.SetValue("C_015", _strC_015);
                testfn.SetValue("C_016", _strC_016);
                testfn.SetValue("C_017", _strC_017);
                testfn.SetValue("C_018", _strC_018);
                testfn.SetValue("C_019", _strC_019);
                testfn.SetValue("C_020", _strC_020);
                testfn.SetValue("C_021", _strC_021);
                testfn.SetValue("C_022", _strC_022);
                testfn.SetValue("C_023", _strC_023);
                testfn.SetValue("C_024", _strC_024);
                testfn.SetValue("C_025", _strC_025);
                testfn.SetValue("C_026", _strC_026);
                testfn.SetValue("C_027", _strC_027);
                testfn.SetValue("C_028", _strC_028);
                testfn.SetValue("C_029", _strC_029);
                testfn.SetValue("C_030", _strC_030);
                testfn.SetValue("C_031", _strC_031);
                testfn.SetValue("C_032", _strC_032);
                testfn.SetValue("C_033", _strC_033);
                testfn.SetValue("C_034", _strC_034);
                testfn.SetValue("C_035", _strC_035);
                testfn.SetValue("C_036", _strC_036);
                testfn.SetValue("C_037", _strC_037);
                testfn.SetValue("C_038", _strC_038);
                testfn.SetValue("C_039", _strC_039);
                testfn.SetValue("C_040", _strC_040);
                testfn.SetValue("C_041", _strC_041);
                testfn.SetValue("C_070", _strC_070);
                testfn.SetValue("R_CDL", _strR_Cdll);
                testfn.SetValue("R_OCC", _strR_Occ);
                testfn.SetValue("R_OWN", _strR_Own);
                testfn.SetValue("Z_APPLTYPE", _strZ_Appltype);

                testfn.Invoke(dest);                

                IRfcStructure bapiTable = testfn.GetStructure("RETURN");
                dtRet2Table = _objOutPut.formDataTableError();

                _objOutPut.addRowToDTError(dtRet2Table, bapiTable.GetString("TYPE"), bapiTable.GetString("ID"), bapiTable.GetString("NUMBER"), bapiTable.GetString("MESSAGE"), bapiTable.GetString("LOG_NO"),
                    bapiTable.GetString("LOG_MSG_NO"), bapiTable.GetString("MESSAGE_V1"), bapiTable.GetString("MESSAGE_V2"), bapiTable.GetString("MESSAGE_V3"), bapiTable.GetString("MESSAGE_V4"), "", "", "", "");


                messageCode = "00";
                messageText = "...";

                //dtRet2Table = _objOutPut.converttodotnetatble(Ret2Table);                
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtRet2Table;      
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);    

        return dsBAPIOutput;
    }


    ///* ZBAPI_CREATESO_POST */
    public DataSet Get_ZBAPI_CREATESO_POST(string _strPMAufart, string _strPlanPlant, string _strRegioGroup, string _strShortText, string _strILA, string _strMFText, string _strUserFieldCH20, 
                        string _StrControkey, string _strSerialNumber, string _strComplaintGroup,string _strCANumber, string _strContract, string _strMFText1)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CREATESO_POST _objOutPut = new ZBAPI_CREATESO_POST();

        string messageText = "";
        string messageCode = "0";
        string strDoc_No = string.Empty, strNoptif_No = string.Empty, strOrderId = string.Empty;

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtOutputResult = new DataTable();
        DataTable dtreturnTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CREATESO_POST");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_CREATESO_POST_IMPORT");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("CONT_ACCT", _strCANumber.Trim());
                articol.SetValue("CONTRACT", _strContract.Trim());
                articol.SetValue("CONTROL_KEY", _StrControkey);
                articol.SetValue("ILA", _strILA);
                articol.SetValue("INV_CMPL_GRP", _strComplaintGroup);
                articol.SetValue("MFTEXT", _strMFText);
                articol.SetValue("MFTEXT1", _strMFText1);
                articol.SetValue("PLANPLANT", _strPlanPlant);
                articol.SetValue("PM_AUFART", _strPMAufart);
                articol.SetValue("REGIOGROUP", _strRegioGroup);
                articol.SetValue("SERIALNO", _strSerialNumber.Trim());
                articol.SetValue("SHORT_TEXT", _strShortText);
                articol.SetValue("USERFIELD_CH20", _strUserFieldCH20);

                testfn.SetValue("IMPORT", articol);               
                testfn.Invoke(dest);


                strDoc_No = (string)testfn.GetValue("DOC_NO");
                strNoptif_No = (string)testfn.GetValue("NOTIF_NO");
                strOrderId = (string)testfn.GetValue("ORDERID");
                IRfcTable returnTable2 = testfn.GetTable("RETURN");

                dtOutputResult = _objOutPut.CreateOutputDataTable(strDoc_No, strNoptif_No, strOrderId);
                dtreturnTable = _objOutPut.converttodotnetatble(returnTable2);                
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }


        bapiResult[0] = dtOutputResult;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable);       
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);     

        return dsBAPIOutput;
    }


    /* ZBAPI_CALERT */
    public DataSet Get_ZBAPI_CALERT(string _strCompanyCode, string _strDate, string _strDivision, string _strUnit)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CALERT _objOutPut = new ZBAPI_CALERT();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtOutputTable = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();


        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CALERT");
                testfn.SetValue("COMPANYCODE", _strCompanyCode);
                testfn.SetValue("DATE", _strDate);
                testfn.SetValue("DIVISION", _strDivision);
                testfn.SetValue("UNIT", _strUnit);               
                testfn.Invoke(dest);

                IRfcStructure bapiTable = testfn.GetStructure("RETURN");
                IRfcTable OutputTable = testfn.GetTable("ZBAPI_ALERTDATA");

                dtOutputTable = _objOutPut.converttodotnetatble(OutputTable);

              dtreturnTable2=_objOutPut.CreateOutputDataTable(bapiTable.GetString("TYPE"), bapiTable.GetString("ID"), bapiTable.GetString("NUMBER"), bapiTable.GetString("MESSAGE"), bapiTable.GetString("LOG_NO"),
                    bapiTable.GetString("LOG_MSG_NO"), bapiTable.GetString("MESSAGE_V1"), bapiTable.GetString("MESSAGE_V2"), bapiTable.GetString("MESSAGE_V3"), bapiTable.GetString("MESSAGE_V4"),
                    bapiTable.GetString("PARAMETER"), bapiTable.GetString("ROW"), bapiTable.GetString("FIELD"), bapiTable.GetString("SYSTEM"));

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.GetFormattedSALERTTable(dtOutputTable);
        bapiResult[1] = dtreturnTable2;       
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        
        return dsBAPIOutput;
    }

    /* ZBAPI_ONLINE_BILL_PDF */
    public DataSet Get_ZBAPI_ONLINE_BILL_PDF(string _strCANumber, string _strEBSKNO)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_ONLINE_BILL_PDF _objOutPut = new ZBAPI_ONLINE_BILL_PDF();

        string messageText = "";
        string messageCode = "0";

        string Contact;
        string Flag = "";

        DataTable[] bapiResult = new DataTable[2];

        DataTable dtOutputTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                if ((_strCANumber.Trim() != "") && (_strCANumber.Length > 3))
                {
                    if (_strCANumber.Substring(3, 1) != "2")
                    {
                        RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                        RfcRepository repo = dest.Repository;
                        IRfcFunction testfn = repo.CreateFunction("ZBAPI_ONLINE_BILL_PDF");
                        testfn.SetValue("CONT_ACCT", _strCANumber);
                        testfn.SetValue("EBS_KNO", _strEBSKNO);

                        testfn.Invoke(dest);

                        Contact = (string)testfn.GetValue("CONTACT");
                        Flag = (string)testfn.GetValue("FLAG");
                        IRfcTable OutputTable = testfn.GetTable("PDF");
                        IRfcTable returnTable2 = testfn.GetTable("RETURN");

                        dtOutputTable = _objOutPut.converttodotnetatble(OutputTable);
                    }
                    else
                    {
                        messageText = "Flag value 2 : Bill Not Available";
                        messageCode = "2";
                    }                   
                }
                else
                {
                    RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                    RfcRepository repo = dest.Repository;
                    IRfcFunction testfn = repo.CreateFunction("ZBAPI_ONLINE_BILL_PDF");
                    testfn.SetValue("CONT_ACCT", _strCANumber);
                    testfn.SetValue("EBS_KNO", _strEBSKNO);

                    testfn.Invoke(dest);

                    Contact = (string)testfn.GetValue("CONTACT");
                    Flag = (string)testfn.GetValue("FLAG");
                    IRfcTable OutputTable = testfn.GetTable("PDF");
                    IRfcTable returnTable2 = testfn.GetTable("RETURN");

                    dtOutputTable = _objOutPut.converttodotnetatble(OutputTable);
                }

                if (Flag == "1")
                {
                    messageText = "Flag value 1 : CA Invalid";
                    messageCode = "1";
                }
                if (Flag == "2")
                {
                    messageText = "Flag value 2 : Bill Not Available";
                    messageCode = "2";
                }
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.GetFormattedZPDFTable(dtOutputTable);      
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
       
        return dsBAPIOutput;
    }

    /* ZBAPI_DSS_SO */
    public DataSet Get_ZBAPI_DSS_SO(string _PARTNERCATEGORY, string _PARTNERTYPE, string _TITLE_KEY, string _FIRSTNAME, string _LASTNAME, string _MIDDLENAME,
                                string _FATHERSNAME, string _HOUSE_NO, string _BUILDING, string _STR_SUPPL1, string _STR_SUPPL2, string _STR_SUPPL3, string _POSTL_COD1,
                                string _CITY, string _E_MAIL, string _LANDLINE, string _MOBILE, string _FEMALE, string _MALE, string _JOBGR, string _LEGITTYPE,
                                string _IDNUMBER, string _ORDER_TYPE, string _SHORTTEXT, string _PLANNINGPLANT, string _WORKCENTRE, string _SYSTEMCOND,
                                string _PMACTIVITYTYPE, string _REQUESTNUM, string _NNUMBER, string _APPLIEDCAT, string _APPLIEDLOAD, string _CONNECTIONTYPE,
                                string _ORDERID, string _FLAG)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_DSS_SO _objOutPut = new ZBAPI_DSS_SO();
        ZBAPI_DSS_SO.I_BUSINESSPARTNER objBPartner = new ZBAPI_DSS_SO.I_BUSINESSPARTNER();
        ZBAPI_DSS_SO.I_NEWCONNECTION objConType = new ZBAPI_DSS_SO.I_NEWCONNECTION();

        bindObjectsZBAPI_DSS_SO(objBPartner, objConType, _PARTNERCATEGORY, _PARTNERTYPE, _TITLE_KEY, _FIRSTNAME, _LASTNAME, _MIDDLENAME, _FATHERSNAME, _HOUSE_NO,
                                _BUILDING, _STR_SUPPL1, _STR_SUPPL2, _STR_SUPPL3, _POSTL_COD1, _CITY, _E_MAIL, _LANDLINE, _MOBILE, _FEMALE, _MALE, _JOBGR, _LEGITTYPE, 
                                _IDNUMBER, _ORDER_TYPE, _SHORTTEXT, _PLANNINGPLANT, _WORKCENTRE, _SYSTEMCOND, _PMACTIVITYTYPE, _REQUESTNUM, _NNUMBER, _APPLIEDCAT, 
                                _APPLIEDLOAD, _CONNECTIONTYPE, _ORDERID, _FLAG);

        string messageText = "";
        string messageCode = "0";

        string E_Flag_Bp = "", E_Flag_So = "", E_New_Partner = "", E_Service_Order = "";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();        
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DSS_SO");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_DSS_BP");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("BUILDING", objBPartner.BUILDING.Trim());
                articol.SetValue("CITY", objBPartner.CITY.Trim());
                articol.SetValue("E_MAIL", objBPartner.E_MAIL);
                articol.SetValue("FATHERSNAME", objBPartner.FATHERSNAME);
                articol.SetValue("FEMALE", objBPartner.FEMALE);
                articol.SetValue("FIRSTNAME", objBPartner.FIRSTNAME);
                articol.SetValue("HOUSE_NO", objBPartner.HOUSE_NO);
                articol.SetValue("IDNUMBER", objBPartner.IDNUMBER);
                articol.SetValue("JOBGR", objBPartner.JOBGR);
                articol.SetValue("LANDLINE", objBPartner.LANDLINE);
                articol.SetValue("LASTNAME", objBPartner.LASTNAME.Trim());
                //articol.SetValue("LEGITTYPE", objBPartner.LEGITTYPE);
                articol.SetValue("MALE", objBPartner.MALE);
                articol.SetValue("MIDDLENAME", objBPartner.MIDDLENAME);
                articol.SetValue("MOBILE", objBPartner.MOBILE);
                articol.SetValue("PARTNERCATEGORY", objBPartner.PARTNERCATEGORY);
                articol.SetValue("PARTNERTYPE", objBPartner.PARTNERTYPE);
                articol.SetValue("POSTL_COD1", objBPartner.POSTL_COD1);
                articol.SetValue("STR_SUPPL1", objBPartner.STR_SUPPL1);
                articol.SetValue("STR_SUPPL2", objBPartner.STR_SUPPL2);
                articol.SetValue("STR_SUPPL3", objBPartner.STR_SUPPL3);
                articol.SetValue("TITLE_KEY", objBPartner.TITLE_KEY);

                testfn.SetValue("I_BUSINESSPARTNER", articol);

                RfcStructureMetadata amI = repo.GetStructureMetadata("ZBAPI_DSS_NEWCONN");
                IRfcStructure articolI = amI.CreateStructure();
                articolI.SetValue("APPLIEDCAT", objConType.APPLIEDCAT.Trim());
                articolI.SetValue("APPLIEDLOAD", objConType.APPLIEDLOAD.Trim());
                articolI.SetValue("CONNECTIONTYPE", objConType.CONNECTIONTYPE);
                articolI.SetValue("FLAG", objConType.FLAG);
                articolI.SetValue("NNUMBER", objConType.NNUMBER);
                articolI.SetValue("ORDER_TYPE", objConType.ORDER_TYPE);
                articolI.SetValue("ORDERID", objConType.ORDERID);
                articolI.SetValue("PLANNINGPLANT", objConType.PLANNINGPLANT);
                articolI.SetValue("PMACTIVITYTYPE", objConType.PMACTIVITYTYPE);
                articolI.SetValue("REQUESTNUM", objConType.REQUESTNUM);
                articolI.SetValue("SHORTTEXT", objConType.SHORTTEXT.Trim());
                articolI.SetValue("SYSTEMCOND", objConType.SYSTEMCOND);
                articolI.SetValue("WORKCENTRE", objConType.WORKCENTRE);

                testfn.SetValue("I_NEWCONNECTION", articolI);

                testfn.Invoke(dest);

                E_Flag_Bp = (string)testfn.GetValue("E_FLAG_BP");
                E_Flag_So = (string)testfn.GetValue("E_FLAG_SO");
                E_New_Partner = (string)testfn.GetValue("E_NEW_PARTNER");
                E_Service_Order = (string)testfn.GetValue("E_SERVICE_ORDER");                
                IRfcTable returnTable2 = testfn.GetTable("RETURN");
                IRfcTable ErrTable = testfn.GetTable("ZERR");

                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
                dtErrTable = _objOutPut.converttodotnetatble(ErrTable);
               
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushFlagsInDataTable(dtFlagsTable, E_Flag_Bp, E_Flag_So, E_New_Partner, E_Service_Order);

        bapiResult[0] = dtFlagsTable;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[2] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    public void bindObjectsZBAPI_DSS_SO(ZBAPI_DSS_SO.I_BUSINESSPARTNER objBPartner, ZBAPI_DSS_SO.I_NEWCONNECTION objConType, string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string LEGITTYPE, string IDNUMBER, string ORDER_TYPE, string SHORTTEXT, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string PMACTIVITYTYPE, string REQUESTNUM, string NNUMBER, string APPLIEDCAT, string APPLIEDLOAD, string CONNECTIONTYPE, string ORDERID, string FLAG)
    {
        try
        {

            objBPartner.PARTNERCATEGORY = PARTNERCATEGORY;
            objBPartner.PARTNERTYPE = PARTNERTYPE;
            objBPartner.TITLE_KEY = TITLE_KEY;
            objBPartner.FIRSTNAME = FIRSTNAME;
            objBPartner.LASTNAME = LASTNAME;
            objBPartner.MIDDLENAME = MIDDLENAME;
            objBPartner.FATHERSNAME = FATHERSNAME;
            objBPartner.HOUSE_NO = HOUSE_NO.Replace("|", ",");
            objBPartner.BUILDING = BUILDING.Replace("|", ",");
            objBPartner.STR_SUPPL1 = STR_SUPPL1.Replace("|", ",");
            objBPartner.STR_SUPPL2 = STR_SUPPL2.Replace("|", ",");
            objBPartner.STR_SUPPL3 = STR_SUPPL3.Replace("|", ",");
            objBPartner.POSTL_COD1 = POSTL_COD1.Replace("|", ",");
            objBPartner.CITY = CITY;
            objBPartner.E_MAIL = E_MAIL;
            objBPartner.LANDLINE = LANDLINE;
            objBPartner.MOBILE = MOBILE;
            objBPartner.FEMALE = FEMALE;
            objBPartner.MALE = MALE;
            objBPartner.JOBGR = JOBGR;
            objBPartner.LEGITTYPE = LEGITTYPE;
            objBPartner.IDNUMBER = IDNUMBER;

            objConType.ORDER_TYPE = ORDER_TYPE;
            objConType.SHORTTEXT = SHORTTEXT.Replace("|", ",");
            objConType.PLANNINGPLANT = PLANNINGPLANT;
            objConType.WORKCENTRE = WORKCENTRE;
            objConType.SYSTEMCOND = SYSTEMCOND;
            objConType.PMACTIVITYTYPE = PMACTIVITYTYPE;
            objConType.REQUESTNUM = REQUESTNUM;
            objConType.NNUMBER = NNUMBER;
            objConType.APPLIEDCAT = APPLIEDCAT;
            objConType.APPLIEDLOAD = APPLIEDLOAD;
            objConType.CONNECTIONTYPE = CONNECTIONTYPE;
            objConType.ORDERID = ORDERID;
            objConType.FLAG = FLAG;

        }
        catch (Exception)
        {

        }

    }


    /* ZBAPI_DSS_SO_ECC */
    public DataSet Get_ZBAPI_DSS_SO_ECC(string _PARTNERCATEGORY, string _PARTNERTYPE, string _TITLE_KEY, string _FIRSTNAME, string _LASTNAME, string _MIDDLENAME,
                                string _FATHERSNAME, string _HOUSE_NO, string _BUILDING, string _STR_SUPPL1, string _STR_SUPPL2, string _STR_SUPPL3, string _POSTL_COD1,
                                string _CITY, string _E_MAIL, string _LANDLINE, string _MOBILE, string _FEMALE, string _MALE, string _JOBGR, string _IDTYPE,
                                string _IDNUMBER, string _ORDER_TYPE, string _SHORTTEXT, string _PLANNINGPLANT, string _WORKCENTRE, string _SYSTEMCOND,
                                string _PMACTIVITYTYPE, string _REQUESTNUM, string _NNUMBER, string _APPLIEDCAT, string _APPLIEDLOAD, string _CONNECTIONTYPE,
                                string _ORDERID, string _FLAG)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_DSS_SO_ECC _objOutPut = new ZBAPI_DSS_SO_ECC();
        ZBAPI_DSS_SO_ECC.I_BUSINESSPARTNER objBPartner = new ZBAPI_DSS_SO_ECC.I_BUSINESSPARTNER();
        ZBAPI_DSS_SO_ECC.I_NEWCONNECTION objConType = new ZBAPI_DSS_SO_ECC.I_NEWCONNECTION();

        bindObjectsZBAPI_DSS_SO_ECC(objBPartner, objConType, _PARTNERCATEGORY, _PARTNERTYPE, _TITLE_KEY, _FIRSTNAME, _LASTNAME, _MIDDLENAME, _FATHERSNAME, _HOUSE_NO,
                                _BUILDING, _STR_SUPPL1, _STR_SUPPL2, _STR_SUPPL3, _POSTL_COD1, _CITY, _E_MAIL, _LANDLINE, _MOBILE, _FEMALE, _MALE, _JOBGR, _IDTYPE,
                                _IDNUMBER, _ORDER_TYPE, _SHORTTEXT, _PLANNINGPLANT, _WORKCENTRE, _SYSTEMCOND, _PMACTIVITYTYPE, _REQUESTNUM, _NNUMBER, _APPLIEDCAT,
                                _APPLIEDLOAD, _CONNECTIONTYPE, _ORDERID, _FLAG);

        string messageText = "";
        string messageCode = "0";

        string E_Flag_Bp = "", E_Flag_So = "", E_New_Partner = "", E_Service_Order = "";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_DSS_SO");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_DSS_BP");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("BUILDING", objBPartner.BUILDING.Trim());
                articol.SetValue("CITY", objBPartner.CITY.Trim());
                articol.SetValue("E_MAIL", objBPartner.E_MAIL);
                articol.SetValue("FATHERSNAME", objBPartner.FATHERSNAME);
                articol.SetValue("FEMALE", objBPartner.FEMALE);
                articol.SetValue("FIRSTNAME", objBPartner.FIRSTNAME);
                articol.SetValue("HOUSE_NO", objBPartner.HOUSE_NO);
                articol.SetValue("IDNUMBER", objBPartner.IDNUMBER);
                articol.SetValue("JOBGR", objBPartner.JOBGR);
                articol.SetValue("LANDLINE", objBPartner.LANDLINE);
                articol.SetValue("LASTNAME", objBPartner.LASTNAME.Trim());
                articol.SetValue("IDTYPE", objBPartner.IDTYPE);
                articol.SetValue("MALE", objBPartner.MALE);
                articol.SetValue("MIDDLENAME", objBPartner.MIDDLENAME);
                articol.SetValue("MOBILE", objBPartner.MOBILE);
                articol.SetValue("PARTNERCATEGORY", objBPartner.PARTNERCATEGORY);
                articol.SetValue("PARTNERTYPE", objBPartner.PARTNERTYPE);
                articol.SetValue("POSTL_COD1", objBPartner.POSTL_COD1);
                articol.SetValue("STR_SUPPL1", objBPartner.STR_SUPPL1);
                articol.SetValue("STR_SUPPL2", objBPartner.STR_SUPPL2);
                articol.SetValue("STR_SUPPL3", objBPartner.STR_SUPPL3);
                articol.SetValue("TITLE_KEY", objBPartner.TITLE_KEY);

                testfn.SetValue("I_BUSINESSPARTNER", articol);

                RfcStructureMetadata amI = repo.GetStructureMetadata("ZBAPI_DSS_NEWCONN");
                IRfcStructure articolI = amI.CreateStructure();
                articolI.SetValue("APPLIEDCAT", objConType.APPLIEDCAT.Trim());
                articolI.SetValue("APPLIEDLOAD", objConType.APPLIEDLOAD.Trim());
                articolI.SetValue("CONNECTIONTYPE", objConType.CONNECTIONTYPE);
                articolI.SetValue("FLAG", objConType.FLAG);
                articolI.SetValue("NNUMBER", objConType.NNUMBER);
                articolI.SetValue("ORDER_TYPE", objConType.ORDER_TYPE);
                articolI.SetValue("ORDERID", objConType.ORDERID);
                articolI.SetValue("PLANNINGPLANT", objConType.PLANNINGPLANT);
                articolI.SetValue("PMACTIVITYTYPE", objConType.PMACTIVITYTYPE);
                articolI.SetValue("REQUESTNUM", objConType.REQUESTNUM);
                articolI.SetValue("SHORTTEXT", objConType.SHORTTEXT.Trim());
                articolI.SetValue("SYSTEMCOND", objConType.SYSTEMCOND);
                articolI.SetValue("WORKCENTRE", objConType.WORKCENTRE);

                testfn.SetValue("I_NEWCONNECTION", articolI);

                testfn.Invoke(dest);

                E_Flag_Bp = (string)testfn.GetValue("E_FLAG_BP");
                E_Flag_So = (string)testfn.GetValue("E_FLAG_SO");
                E_New_Partner = (string)testfn.GetValue("E_NEW_PARTNER");
                E_Service_Order = (string)testfn.GetValue("E_SERVICE_ORDER");

                IRfcTable returnTable2 = testfn.GetTable("RETURN");
                IRfcTable ErrTable = testfn.GetTable("ZERR");

                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
                dtErrTable = _objOutPut.converttodotnetatble(ErrTable);

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushFlagsInDataTable(dtFlagsTable, E_Flag_Bp, E_Flag_So, E_New_Partner, E_Service_Order);

        bapiResult[0] = dtFlagsTable;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[2] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    public void bindObjectsZBAPI_DSS_SO_ECC(ZBAPI_DSS_SO_ECC.I_BUSINESSPARTNER objBPartner, ZBAPI_DSS_SO_ECC.I_NEWCONNECTION objConType, string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE, string IDNUMBER, string ORDER_TYPE, string SHORTTEXT, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string PMACTIVITYTYPE, string REQUESTNUM, string NNUMBER, string APPLIEDCAT, string APPLIEDLOAD, string CONNECTIONTYPE, string ORDERID, string FLAG)
    {
        try
        {

            objBPartner.PARTNERCATEGORY = PARTNERCATEGORY;
            objBPartner.PARTNERTYPE = PARTNERTYPE;
            objBPartner.TITLE_KEY = TITLE_KEY;
            objBPartner.FIRSTNAME = FIRSTNAME;
            objBPartner.LASTNAME = LASTNAME;
            objBPartner.MIDDLENAME = MIDDLENAME;
            objBPartner.FATHERSNAME = FATHERSNAME;
            objBPartner.HOUSE_NO = HOUSE_NO.Replace("|", ",");
            objBPartner.BUILDING = BUILDING.Replace("|", ",");
            objBPartner.STR_SUPPL1 = STR_SUPPL1.Replace("|", ",");
            objBPartner.STR_SUPPL2 = STR_SUPPL2.Replace("|", ",");
            objBPartner.STR_SUPPL3 = STR_SUPPL3.Replace("|", ",");
            objBPartner.POSTL_COD1 = POSTL_COD1.Replace("|", ",");
            objBPartner.CITY = CITY;
            objBPartner.E_MAIL = E_MAIL;
            objBPartner.LANDLINE = LANDLINE;
            objBPartner.MOBILE = MOBILE;
            objBPartner.FEMALE = FEMALE;
            objBPartner.MALE = MALE;
            objBPartner.JOBGR = JOBGR;
            objBPartner.IDTYPE = IDTYPE;
            objBPartner.IDNUMBER = IDNUMBER;

            objConType.ORDER_TYPE = ORDER_TYPE;
            objConType.SHORTTEXT = SHORTTEXT.Replace("|", ",");
            objConType.PLANNINGPLANT = PLANNINGPLANT;
            objConType.WORKCENTRE = WORKCENTRE;
            objConType.SYSTEMCOND = SYSTEMCOND;
            objConType.PMACTIVITYTYPE = PMACTIVITYTYPE;
            objConType.REQUESTNUM = REQUESTNUM;
            objConType.NNUMBER = NNUMBER;
            objConType.APPLIEDCAT = APPLIEDCAT;
            objConType.APPLIEDLOAD = APPLIEDLOAD;
            objConType.CONNECTIONTYPE = CONNECTIONTYPE;
            objConType.ORDERID = ORDERID;
            objConType.FLAG = FLAG;

        }
        catch (Exception)
        {

        }

    }


    /* Z_BAPI_ZDSS_WEB_LINK */
    public DataSet Get_Z_BAPI_ZDSS_WEB_LINK(string _I_ILART, string _I_VKONT, string _I_VKONA, string _PARTNERCATEGORY, string _PARTNERTYPE, string _TITLE_KEY, string _FIRSTNAME,
                                    string _LASTNAME, string _MIDDLENAME, string _FATHERSNAME, string _HOUSE_NO, string _BUILDING, string _STR_SUPPL1, string _STR_SUPPL2,
                                    string _STR_SUPPL3, string _POSTL_COD1, string _CITY, string _E_MAIL, string _LANDLINE, string _MOBILE, string _FEMALE, string _MALE,
                                    string _JOBGR, string _IDTYPE, string _IDNUMBER, string _PLANNINGPLANT, string _WORKCENTRE, string _SYSTEMCOND, string _APPLIEDCAT,
                                    string _APPLIEDLOAD, string _APPLIEDLOADKVA, string _CONNECTIONTYPE, string _STATEMENT_CA, string _START_DATE, string _START_TIME,
                                    string _FINISH_DATE, string _FINISH_TIME, string _SORTFIELD, string _ABKRS)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_ZDSS_WEB_LINK _objOutPut = new Z_BAPI_ZDSS_WEB_LINK();
        Z_BAPI_ZDSS_WEB_LINK.I_BUSINESSPARTNER_ECC objBPartner = new Z_BAPI_ZDSS_WEB_LINK.I_BUSINESSPARTNER_ECC();
        Z_BAPI_ZDSS_WEB_LINK.I_DSSORDER objIdssOrder = new Z_BAPI_ZDSS_WEB_LINK.I_DSSORDER();

        bindObjectsZ_BAPI_ZDSS_WEB_LINK(objBPartner, objIdssOrder, _PARTNERCATEGORY, _PARTNERTYPE, _TITLE_KEY, _FIRSTNAME, _LASTNAME, _MIDDLENAME, _FATHERSNAME, _HOUSE_NO, 
                                    _BUILDING, _STR_SUPPL1, _STR_SUPPL2, _STR_SUPPL3, _POSTL_COD1, _CITY, _E_MAIL, _LANDLINE, _MOBILE, _FEMALE, _MALE, _JOBGR, _IDTYPE, 
                                    _IDNUMBER, _PLANNINGPLANT, _WORKCENTRE, _SYSTEMCOND, _APPLIEDCAT, _APPLIEDLOAD, _APPLIEDLOADKVA, _CONNECTIONTYPE, _STATEMENT_CA, 
                                    _START_DATE, _START_TIME, _FINISH_DATE, _FINISH_TIME, _SORTFIELD, _ABKRS);

        string messageText = "";
        string messageCode = "0";

        string E_Flag_Ap = "", E_Flag_Bp = "", E_Flag_So = "", E_Flag_Us = "", E_New_Partner = "", E_Service_Order = "";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtFlagsTable = _objOutPut.makeFlagsTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();        
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_ZDSS_WEB_LINK");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZBAPI_DSS_BP");
                IRfcStructure articol = am.CreateStructure();
                articol.SetValue("BUILDING", objBPartner.BUILDING.Trim());
                articol.SetValue("CITY", objBPartner.CITY.Trim());
                articol.SetValue("E_MAIL", objBPartner.E_MAIL);
                articol.SetValue("FATHERSNAME", objBPartner.FATHERSNAME);
                articol.SetValue("FEMALE", objBPartner.FEMALE);
                articol.SetValue("FIRSTNAME", objBPartner.FIRSTNAME);
                articol.SetValue("HOUSE_NO", objBPartner.HOUSE_NO);
                articol.SetValue("IDNUMBER", objBPartner.IDNUMBER);
                articol.SetValue("JOBGR", objBPartner.JOBGR);
                articol.SetValue("LANDLINE", objBPartner.LANDLINE);
                articol.SetValue("LASTNAME", objBPartner.LASTNAME.Trim());
                articol.SetValue("IDTYPE", objBPartner.IDTYPE);
                articol.SetValue("MALE", objBPartner.MALE);
                articol.SetValue("MIDDLENAME", objBPartner.MIDDLENAME);
                articol.SetValue("MOBILE", objBPartner.MOBILE);
                articol.SetValue("PARTNERCATEGORY", objBPartner.PARTNERCATEGORY);
                articol.SetValue("PARTNERTYPE", objBPartner.PARTNERTYPE);
                articol.SetValue("POSTL_COD1", objBPartner.POSTL_COD1);
                articol.SetValue("STR_SUPPL1", objBPartner.STR_SUPPL1);
                articol.SetValue("STR_SUPPL2", objBPartner.STR_SUPPL2);
                articol.SetValue("STR_SUPPL3", objBPartner.STR_SUPPL3);
                articol.SetValue("TITLE_KEY", objBPartner.TITLE_KEY);

                testfn.SetValue("I_BUSINESSPARTNER", articol);

                RfcStructureMetadata amI = repo.GetStructureMetadata("ZBAPI_DSS_ORDER");
                IRfcStructure articolI = amI.CreateStructure();
                articolI.SetValue("ABKRS", objIdssOrder.ABKRS.Trim());
                articolI.SetValue("APPLIEDCAT", objIdssOrder.APPLIEDCAT.Trim());
                articolI.SetValue("APPLIEDLOAD", objIdssOrder.APPLIEDLOAD);
                articolI.SetValue("APPLIEDLOADKVA", objIdssOrder.APPLIEDLOADKVA);
                articolI.SetValue("CONNECTIONTYPE", objIdssOrder.CONNECTIONTYPE);
                articolI.SetValue("FINISH_DATE", objIdssOrder.FINISH_DATE);
                articolI.SetValue("FINISH_TIME", objIdssOrder.FINISH_TIME);
                articolI.SetValue("PLANNINGPLANT", objIdssOrder.PLANNINGPLANT);
                articolI.SetValue("SORTFIELD", objIdssOrder.SORTFIELD);
                articolI.SetValue("START_DATE", objIdssOrder.START_DATE);
                articolI.SetValue("START_TIME", objIdssOrder.START_TIME.Trim());
                articolI.SetValue("STATEMENT_CA", objIdssOrder.STATEMENT_CA);
                articolI.SetValue("SYSTEMCOND", objIdssOrder.SYSTEMCOND);
                articolI.SetValue("WORKCENTRE", objIdssOrder.WORKCENTRE);

                testfn.SetValue("I_DSSORDER", articolI);

                testfn.SetValue("I_ILART", _I_ILART);
                testfn.SetValue("I_VKONA", _I_VKONA);
                testfn.SetValue("I_VKONT", _I_VKONT);

                testfn.Invoke(dest);

                E_Flag_Ap = (string)testfn.GetValue("E_FLAG_AP");
                E_Flag_Bp = (string)testfn.GetValue("E_FLAG_BP");
                E_Flag_So = (string)testfn.GetValue("E_FLAG_SO");
                E_Flag_Us = (string)testfn.GetValue("E_FLAG_US");
                E_New_Partner = (string)testfn.GetValue("E_NEW_PARTNER");
                E_Service_Order = (string)testfn.GetValue("E_SERVICE_ORDER");
                IRfcTable returnTable2 = testfn.GetTable("RETURN");
                IRfcTable ErrTable = testfn.GetTable("ZERR");

                dtRet2Table = _objOutPut.converttodotnetatble(returnTable2);
                dtErrTable = _objOutPut.converttodotnetatble(ErrTable);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }
        
        _objOutPut.pushFlagsInDataTable(dtFlagsTable, E_Flag_Ap, E_Flag_Bp, E_Flag_So, E_Flag_Us, E_New_Partner, E_Service_Order);

        bapiResult[0] = dtFlagsTable;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[2] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }

    public void bindObjectsZ_BAPI_ZDSS_WEB_LINK(Z_BAPI_ZDSS_WEB_LINK.I_BUSINESSPARTNER_ECC objBPartner, Z_BAPI_ZDSS_WEB_LINK.I_DSSORDER objDSSOrder, string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE, string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, string APPLIEDLOAD, string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME, string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS)
    {
        try
        {

            objBPartner.PARTNERCATEGORY = PARTNERCATEGORY;
            objBPartner.PARTNERTYPE = PARTNERTYPE;
            objBPartner.TITLE_KEY = TITLE_KEY;
            objBPartner.FIRSTNAME = FIRSTNAME;
            objBPartner.LASTNAME = LASTNAME;
            objBPartner.MIDDLENAME = MIDDLENAME;
            objBPartner.FATHERSNAME = FATHERSNAME;
            objBPartner.HOUSE_NO = HOUSE_NO.Replace("|", ",");
            objBPartner.BUILDING = BUILDING.Replace("|", ",");
            objBPartner.STR_SUPPL1 = STR_SUPPL1.Replace("|", ",");
            objBPartner.STR_SUPPL2 = STR_SUPPL2.Replace("|", ",");
            objBPartner.STR_SUPPL3 = STR_SUPPL3.Replace("|", ",");
            objBPartner.POSTL_COD1 = POSTL_COD1.Replace("|", ",");
            objBPartner.CITY = CITY;
            objBPartner.E_MAIL = E_MAIL;
            objBPartner.LANDLINE = LANDLINE;
            objBPartner.MOBILE = MOBILE;
            objBPartner.FEMALE = FEMALE;
            objBPartner.MALE = MALE;
            objBPartner.JOBGR = JOBGR;
            objBPartner.IDTYPE = IDTYPE;
            objBPartner.IDNUMBER = IDNUMBER;


            objDSSOrder.PLANNINGPLANT = PLANNINGPLANT;
            objDSSOrder.WORKCENTRE = WORKCENTRE;
            objDSSOrder.SYSTEMCOND = SYSTEMCOND;
            objDSSOrder.APPLIEDCAT = APPLIEDCAT;
            objDSSOrder.APPLIEDLOAD = APPLIEDLOAD;
            objDSSOrder.APPLIEDLOADKVA = APPLIEDLOADKVA;
            objDSSOrder.CONNECTIONTYPE = CONNECTIONTYPE;
            objDSSOrder.STATEMENT_CA = STATEMENT_CA;
            objDSSOrder.START_DATE = START_DATE;
            objDSSOrder.START_TIME = START_TIME;
            objDSSOrder.FINISH_DATE = FINISH_DATE;
            objDSSOrder.FINISH_TIME = FINISH_TIME;
            objDSSOrder.SORTFIELD = SORTFIELD;
            objDSSOrder.ABKRS = ABKRS;

        }
        catch (Exception)
        {

        }

    }


    /* ZBAPI_ENFORCEMENT */
    public DataSet Get_ZBAPI_ENFORCEMENT(string _strCANumber, string _strContract)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_ENFORCEMENT _objOutPut = new ZBAPI_ENFORCEMENT();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[3];

        DataTable dtEnfDetails = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        DataTable dtMetChangeTable = new DataTable();
        
        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_ENFORCEMENT");
                testfn.SetValue("CA", _strCANumber);
                testfn.SetValue("CONTRACT", _strContract);
             
                testfn.Invoke(dest);

                IRfcStructure bapiTable = testfn.GetStructure("RETURN");
                IRfcTable EnfDetails = testfn.GetTable("IT_FINAL");
                IRfcTable MetChangeTable = testfn.GetTable("IT_FINAL_MC");
                

                dtEnfDetails = _objOutPut.converttodotnetatble(EnfDetails);
                dtMetChangeTable = _objOutPut.converttodotnetatble(MetChangeTable);

                if (bapiTable.GetString("MESSAGE").Trim() != "")
				{
					messageCode = "0";
                    messageText = bapiTable.GetString("MESSAGE").Trim();
				}
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.transferEnforcementData(dtEnfDetails);
        bapiResult[1] = _objOutPut.transferEnfMeterChangeData(dtMetChangeTable);       
        bapiResult[2] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
       
        return dsBAPIOutput;
    }


    /* ZBI_WEBBILL_HIST */
    public DataSet Get_ZBI_WEBBILL_HIST(string _strCANumber, string _strBillMonth)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBI_WEBBILL_HIST _objOutPut = new ZBI_WEBBILL_HIST();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtWebBillHistory = new DataTable();
        DataTable dtRet2Table = new DataTable();
        DataTable dtErrTable = new DataTable();        
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();        

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBI_WEBBILL_HIST");
                testfn.SetValue("BILL_MONTH", _strBillMonth);
                testfn.SetValue("CONTRACT_ACCOUNT", _strCANumber);

                testfn.Invoke(dest);

                IRfcTable Ret2Table = testfn.GetTable("RETURN");
                IRfcTable WebBillHistory = testfn.GetTable("TX_BILLPRINT1");
                IRfcTable ErrTable = testfn.GetTable("ZERR");


                dtRet2Table = _objOutPut.converttodotnetatble(Ret2Table);
                dtWebBillHistory = _objOutPut.converttodotnetatble(WebBillHistory);
                dtErrTable = _objOutPut.converttodotnetatble(ErrTable);

                
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = _objOutPut.transferWebBillHistoryData(dtWebBillHistory);
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        bapiResult[2] = _objOutPut.pushDataInErrorTable(dtErrTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    /*
     * Z_BAPI_IVRS
     */
    public DataSet Get_Z_BAPI_IVRS(string strContractAccountNumber)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_IVRS _objOutPut = new Z_BAPI_IVRS();

        string messageText = "";
        string messageCode = "0";

        string CURR_DUE_DATE = "", PREV_DUE_DATE = "", LAST_PAID_DATE = "";
        string strCURR_BILL_AMNT = "0", strPREV_BILL_AMNT = "0", strLAST_PAID_AMNT = "0";

        decimal CURR_BILL_AMNT = 0, PREV_BILL_AMNT = 0, LAST_PAID_AMNT = 0;

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtBapiOutput = _objOutPut.makeOutputDataTable();
        DataTable dtRet2Table = _objOutPut.formDataTableError();
        DataTable dtSAPErrorTable = _objOutPut.makeErrorTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_IVRS");
                testfn.SetValue("CONTRACT_ACCOUNT", strContractAccountNumber);
                testfn.Invoke(dest);

                strCURR_BILL_AMNT = (string)testfn.GetValue("CURR_BILL_AMNT");
                CURR_DUE_DATE = (string)testfn.GetValue("CURR_DUE_DATE");
                strLAST_PAID_AMNT = (string)testfn.GetValue("LAST_PAID_AMNT");
                LAST_PAID_DATE = (string)testfn.GetValue("LAST_PAID_DATE");
                strPREV_BILL_AMNT = (string)testfn.GetValue("PREV_BILL_AMNT");
                PREV_DUE_DATE = (string)testfn.GetValue("PREV_DUE_DATE");

                try
                {
                    CURR_BILL_AMNT = Convert.ToDecimal(strCURR_BILL_AMNT);
                }
                catch (Exception e)
                {
                    CURR_BILL_AMNT = 0;
                }

                try
                {
                    LAST_PAID_AMNT = Convert.ToDecimal(strLAST_PAID_AMNT);
                }
                catch (Exception e)
                {
                    LAST_PAID_AMNT = 0;
                }

                try
                {
                    PREV_BILL_AMNT = Convert.ToDecimal(strPREV_BILL_AMNT);
                }
                catch (Exception e)
                {
                    PREV_BILL_AMNT = 0;
                }

                IRfcTable rfcTableRETURN = testfn.GetTable("RETURN");
                IRfcTable rfcTableZERR = testfn.GetTable("ZERR");

                _objOutPut.pushOutputDataInDataTable(dtBapiOutput, CURR_BILL_AMNT, _objOutPut.GetDateFormate_YYYYMMDD(CURR_DUE_DATE), PREV_BILL_AMNT,
                                                    _objOutPut.GetDateFormate_YYYYMMDD(PREV_DUE_DATE), LAST_PAID_AMNT, _objOutPut.GetDateFormate_YYYYMMDD(LAST_PAID_DATE));
                               

                dtRet2Table = _objOutPut.converttodotnetatble(rfcTableRETURN);
                dtRet2Table = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);

                dtSAPErrorTable = _objOutPut.converttodotnetatble(rfcTableZERR);
                dtSAPErrorTable = _objOutPut.makeSAPErrorTable(dtSAPErrorTable);
                //_objOutPut.pushDataInSAPErrorTable(dtSAPErrorTable, dtRet2Table[0]["MESSAGE"].ToString());
                //dtSAPErrorTable.TableName = "SAP_ERROR_TABLE";             

                messageCode = "00";
                messageText = "...";
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }



        //bapiResult[0] = dtBapiOutput;
        //bapiResult[1] = _objOutPut.populateStructureWithData(dtSAPErrorTable, );
        //bapiResult[2] = _objOutPut.makeBAPIRET2TABLE(dtRet2Table);
        //bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(dtBapiOutput);
        dsBAPIOutput.Tables.Add(dtSAPErrorTable);
        dsBAPIOutput.Tables.Add(dtRet2Table);
        dsBAPIOutput.Tables.Add(dtMessageText);

        return dsBAPIOutput;
    }


    /* Z_BAPI_DSS_ISU_CA_DISPLAY */
    public DataSet Get_Z_BAPI_DSS_ISU_CA_DISPLAY(string strCANumber, string strCRNNumber)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        Z_BAPI_DSS_ISU_CA_DISPLAY _objOutPut = new Z_BAPI_DSS_ISU_CA_DISPLAY();

        string messageText = "";
        string messageCode = "0";

        DataTable[] bapiResult = new DataTable[4];

        DataTable dtCADetails = _objOutPut.makeCADetailsDataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable dtErrorTable = new DataTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("Z_BAPI_DSS_ISU_CA_DISPLAY");
                testfn.SetValue("IMPORT_CANUMBER", strCANumber);
                testfn.SetValue("IMPORT_CRNNUMBER", strCRNNumber);

                testfn.Invoke(dest);

                IRfcTable irfcErrorTable = testfn.GetTable("BAPIMESSAGE");
                IRfcTable irfcCADetails = testfn.GetTable("EXPORT_CADETAILS");
                IRfcTable irfcReturnTable2 = testfn.GetTable("RETURN");


                dtCADetails = _objOutPut.converttodotnetatble(irfcCADetails);
                dtCADetails = _objOutPut.makeCADEtails(dtCADetails);

                dtreturnTable2 = _objOutPut.converttodotnetatble(irfcReturnTable2);
                dtErrorTable = _objOutPut.converttodotnetatble(irfcErrorTable);

            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtCADetails;
        bapiResult[1] = _objOutPut.makeBAPIRET2TABLE(dtreturnTable2);
        bapiResult[2] = _objOutPut.makeISUERRTable(dtErrorTable);
        bapiResult[3] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);

        return dsBAPIOutput;
    }


    public DataSet get_ZBAPI_FICA_PREPAID_MTR(string strDOC_ID, string strTRANS_ID, string strCA, string strCOMPANY,
    string strCONSUMER_TYPE, string strMETER_MANFR, string strCONTRACT, string strCA_VALID_ISU, string strENTRY_DATE,
    string strS_ENC_TKN_1, string strS_ENC_TKN_2, string strS_ENC_TKN_3, string strS_ENC_TKN_4, string strGENUS_RESP_CODE,
    string strMETER_NO, string strACC_CLASS, string strAMNT_BANK, string strAMNT_ISU, string strADDRESS, string strTARIFTYP,
    string strTARIFID, string strPAY_METHOD)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_FICA_PREPAID_MTR _objOutPut = new ZBAPI_FICA_PREPAID_MTR();

        string messageText = "";
        string messageCode = "0";
        DataTable[] bapiResult = new DataTable[6];

        DataTable dtIT_Input = new DataTable();
        DataTable dtIT_Output_DUPL = new DataTable();
        DataTable dtIT_Output_NU = new DataTable();
        DataTable dtIT_Output_FIN = new DataTable();
        DataTable dtIT_Return = _objOutPut.formDataTableError();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_FICA_PREPAID_MTR");

                RfcStructureMetadata am = repo.GetStructureMetadata("ZFICA_PREPAID_MTR_STR");
                IRfcStructure articol = am.CreateStructure();
                IRfcTable artable = am.CreateTable();
                articol.SetValue("DOC_ID", strDOC_ID);
                articol.SetValue("TRANS_ID", strTRANS_ID);
                articol.SetValue("CA", strCA);
                articol.SetValue("COMPANY", strCOMPANY);
                articol.SetValue("CONSUMER_TYPE", strCONSUMER_TYPE);
                articol.SetValue("METER_MANFR", strMETER_MANFR);
                articol.SetValue("CONTRACT", strCONTRACT);
                articol.SetValue("CA_VALID_ISU", strCA_VALID_ISU);
                //articol.SetValue("ENTRY_DATE", DateTime.ParseExact(strENTRY_DATE, "yyyymmdd", CultureInfo.InvariantCulture));
                articol.SetValue("ENTRY_DATE", "20190115");
                articol.SetValue("S_ENC_TKN_1", strS_ENC_TKN_1);
                articol.SetValue("S_ENC_TKN_2", strS_ENC_TKN_2);
                articol.SetValue("S_ENC_TKN_3", strS_ENC_TKN_3);
                articol.SetValue("S_ENC_TKN_4", strS_ENC_TKN_4);
                articol.SetValue("GENUS_RESP_CODE", strGENUS_RESP_CODE);
                articol.SetValue("METER_NO", strMETER_NO);
                articol.SetValue("ACC_CLASS", strACC_CLASS);
                articol.SetValue("AMNT_BANK", strAMNT_BANK);
                articol.SetValue("AMNT_ISU", strAMNT_ISU);
                articol.SetValue("ADDRESS", strADDRESS);
                articol.SetValue("TARIFTYP", strTARIFTYP);
                articol.SetValue("TARIFID", strTARIFID);
                articol.SetValue("PAY_METHOD", strPAY_METHOD);
                artable.Insert(articol);

                testfn.SetValue("IT_INPUT", artable);
                testfn.Invoke(dest);

                IRfcTable _IT_INPUT = testfn.GetTable("IT_INPUT");
                IRfcTable _IT_Output_DUPL = testfn.GetTable("IT_OUTPUT_DUPL");
                IRfcTable _IT_Output_NU = testfn.GetTable("IT_OUTPUT_NU");
                IRfcTable _IT_Output_FIN = testfn.GetTable("IT_OUTPUT_FIN");
                IRfcTable irfcReturn = testfn.GetTable("IT_RETURN");

                dtIT_Input = _objOutPut.converttodotnetatble(_IT_INPUT);
                dtIT_Output_DUPL = _objOutPut.converttodotnetatble(_IT_Output_DUPL);
                dtIT_Output_NU = _objOutPut.converttodotnetatble(_IT_Output_NU);
                dtIT_Output_FIN = _objOutPut.converttodotnetatble(_IT_Output_FIN);
                dtIT_Return = _objOutPut.converttodotnetatble(irfcReturn);
            }

        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        bapiResult[0] = dtIT_Input;
        bapiResult[0].TableName = "IT_Input";
        bapiResult[1] = dtIT_Output_DUPL;
        bapiResult[1].TableName = "IT_Output_DUPL";
        bapiResult[2] = dtIT_Output_NU;
        bapiResult[2].TableName = "IT_Output_NU";
        bapiResult[3] = dtIT_Output_FIN;
        bapiResult[3].TableName = "IT_Output_FIN";
        bapiResult[4] = dtIT_Return;
        bapiResult[4].TableName = "IT_Return";
        bapiResult[5] = dtMessageText;
        bapiResult[5].TableName = "MessageText";

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);
        dsBAPIOutput.Tables.Add(bapiResult[2]);
        dsBAPIOutput.Tables.Add(bapiResult[3]);
        dsBAPIOutput.Tables.Add(bapiResult[4]);
        dsBAPIOutput.Tables.Add(bapiResult[5]);

        return dsBAPIOutput;
    }
    public DataSet get_ZBAPI_CA_OUTSTANDING_AMT(string strCA)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZBAPI_CA_OUTSTANDING_AMT _objOutPut = new ZBAPI_CA_OUTSTANDING_AMT();

        string messageText = "";
        string messageCode = "0";
        string _sO_VKONT = "", _sO_FLAG = "", _sO_MSG = "";// 000400612200
        DataTable[] bapiResult = new DataTable[2];
        
        DataTable dtCADetails = _objOutPut.makeFlagsTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();       

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_CA_OUTSTANDING_AMT");

                if (strCA.Length == 11)
                    strCA = "0" + strCA;
                else if (strCA.Length == 10)
                    strCA = "00" + strCA;
                else if (strCA.Length == 9)
                    strCA = "000" + strCA;

                testfn.SetValue("I_VKONT", strCA);                                           
                testfn.Invoke(dest);

                _sO_VKONT = testfn.GetString("O_VKONT").Trim();
                _sO_FLAG = testfn.GetString("O_FLAG").Trim();
                _sO_MSG = testfn.GetString("O_MSG").Trim();

                if (_sO_FLAG == "1")
                    _sO_MSG = "Dear Customer, your request is not processed futher as due are reflecting against the CA, may please pay for processing.";
                else if (_sO_FLAG == "0")
                    _sO_MSG = "Dear Customer, your request is successfully processing. May please contact EESL for futher details.";                
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushOutputDataInDataTable(dtCADetails, _sO_VKONT, _sO_FLAG, _sO_MSG);

        bapiResult[0] = dtCADetails;        
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);        

        return dsBAPIOutput;
    }


    public DataSet get_ZFI_CURR_OUTS_FLAG(string strCA)
    {
        DataSet dsBAPIOutput = new DataSet("BAPI_RESULT");
        ZFI_CURR_OUTS_FLAG _objOutPut = new ZFI_CURR_OUTS_FLAG();
        DataTable dtreturnTable1 = new DataTable();
        DataTable dtreturnTable2 = new DataTable();

        string messageText = "";
        string messageCode = "0";
        string _sO_FLAG = "", _sO_AMT = "", _sO_Name = "", _sO_MSG = "";
        DataTable[] bapiResult = new DataTable[2];

        DataTable dtCADetails = _objOutPut.makeFlagsTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();

        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZFI_CURR_OUTS_FLAG");

                if (strCA.Length == 11)
                    strCA = "0" + strCA;
                else if (strCA.Length == 10)
                    strCA = "00" + strCA;
                else if (strCA.Length == 9)
                    strCA = "000" + strCA;

                testfn.SetValue("VKONT", strCA);
                testfn.Invoke(dest);

                IRfcStructure bapiTable1 = testfn.GetStructure("CA_OUTSTANDING_AMT");
                IRfcStructure bapiTable = testfn.GetStructure("RETURN");


                dtreturnTable1 = _objOutPut.CreateOutputDataTable(bapiTable1.GetString("FLAG"), bapiTable1.GetString("AMOUNT"));

                if (dtreturnTable1.Rows.Count > 0)
                {
                    if (dtreturnTable1.Rows[0][0] != null)
                    {
                        _sO_FLAG = dtreturnTable1.Rows[0][0].ToString().Trim();

                        if (_sO_FLAG == "1")
                            _sO_MSG = "Dear Customer, your request is not processed futher as due are reflecting against the CA, may please pay for processing.";
                        else if (_sO_FLAG == "0")
                            _sO_MSG = "Dear Customer, your request is successfully processing. May please contact EESL for futher details.";
                        else
                            _sO_MSG = "Dear Customer, your request is not processed futher due to not reflecting CA in our ISU-Database.";
                    }

                    if (dtreturnTable1.Rows[0][1] != null)
                    {
                        _sO_AMT = dtreturnTable1.Rows[0][1].ToString().Trim();
                    }

                    _sO_Name = bapiTable1.GetString("NAME");
                }
                else
                {
                    _sO_MSG = "Dear Customer, your request is not processed futher due to not reflecting CA in our ISU-Database.";
                }
            }
        }
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        _objOutPut.pushOutputDataInDataTable(dtCADetails, strCA, _sO_Name, _sO_AMT, _sO_FLAG, _sO_MSG);

        bapiResult[0] = dtCADetails;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }
    public DataSet Get_ZBAPI_LAST_MODE_PAY(string CA, string FLAG)
    {
        DataSet dsOutput = new DataSet("ZBAPI_LAST_MODE_PAY");
        ZBAPI_LAST_MODE_PAY objOutPut = new ZBAPI_LAST_MODE_PAY();
        string strMessage = string.Empty;
        string strMessagecode = "0";
        string sO_VKONT = string.Empty;
        string sO_MOD_OF_PAY = string.Empty;
        string sO_FLAG = string.Empty;
        DataTable[] BapiResult = new DataTable[2];
        DataTable dtOutputTable = new DataTable();
        DataTable dtreturnTable = new DataTable();
        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        DataTable dtCADetails = objOutPut.makeFlagsTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_LAST_MODE_PAY");
                testfn.SetValue("VKONT", CA);
                testfn.SetValue("IFLAG", FLAG);
                testfn.Invoke(dest);
                sO_VKONT = testfn.GetString("VKONT");
                sO_MOD_OF_PAY = testfn.GetString("MOD_OF_PAY");
                sO_FLAG = testfn.GetString("FLAG");
                objOutPut.pushOutputDataInDataTable(dtCADetails, sO_VKONT, sO_MOD_OF_PAY, sO_FLAG);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }
        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtCADetails;
        BapiResult[1] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        dsOutput.Tables.Add(BapiResult[1]);
        return dsOutput;
    }
    public DataSet Get_BAPI_MTRREADDOC_GETLIST(string strSERIALNO)
    {
        DataSet dsOutput = new DataSet("ZBI_NOC_RESULT");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        string strMRDATEFROM = string.Empty;
        string strMRDATETO = string.Empty;
        string strMRDOCUMENTTYPE = string.Empty;
        strMRDATETO = DateTime.Now.Date.ToString("dd-MM-yyyy");
        strMRDATEFROM = DateTime.Now.Date.AddDays(-360).ToString("dd-MM-yyyy");
        BAPI_MTRREADDOC_GETLIST objOutPut = new BAPI_MTRREADDOC_GETLIST();
        DataTable[] BapiResult = new DataTable[3];

        DataTable dtOutputTable = new DataTable();
        DataTable dtreturnTable = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        DataTable dtCADetails = objOutPut.makeFlagsTable();

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("BAPI_MTRREADDOC_GETLIST");
                testfn.SetValue("SERIALNO", "0000000000" + strSERIALNO);
                testfn.SetValue("MRDATEFROM", Convert.ToDateTime(strMRDATEFROM));
                testfn.SetValue("MRDATETO", Convert.ToDateTime(strMRDATETO));
                testfn.SetValue("MRDOCUMENTTYPE", "2");
                testfn.Invoke(dest);
                IRfcTable _IT_INPUT = testfn.GetTable("MRDOCUMENTDATA");
                dtCADetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtCADetails;
        BapiResult[1] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        dsOutput.Tables.Add(BapiResult[1]);
        return dsOutput;
    }

    public DataSet Get_ZBAPI_FETCH_ENF_USER_DET(string CANUMBER)
    {
        DataSet dsOutput = new DataSet("FETCH_ENF_USER_DET");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_FETCH_ENF_USER_DET objOutPut = new ZBAPI_FETCH_ENF_USER_DET();
        DataTable[] BapiResult = new DataTable[3];

        DataTable dtOutputTable = new DataTable();
        DataTable dtreturnTable = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        DataTable dtCADetails = objOutPut.makeFlagsTable();

        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_FETCH_ENF_USER_DET");
                testfn.SetValue("CA_NUMBER", CANUMBER);
                testfn.Invoke(dest);
                IRfcTable _IT_INPUT = testfn.GetTable("ZUSR_DETAIL");
                dtCADetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtCADetails;
        BapiResult[1] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        dsOutput.Tables.Add(BapiResult[1]);
        return dsOutput;
    }

    public DataSet Get_ZBAPI_BILL_DET(string CANUMBER)
    {
        int i = 0;
        DataSet dsOutput = new DataSet("BILL_DET");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_BILL_DET objOutPut = new ZBAPI_BILL_DET();
        DataTable[] BapiResult = new DataTable[1];
        DataTable dtPDFDetails = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_BILL_DET");
                testfn.SetValue("VKONT", CANUMBER);
                testfn.Invoke(dest);

                IRfcTable _IT_INPUT = testfn.GetTable("T_PDF");
                dtPDFDetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }

        BapiResult[0] = dtPDFDetails;
        dsOutput.Tables.Add(BapiResult[0]);

        return dsOutput;
    }

    public DataSet Get_ZBAPI_BILL_DET_64(string CANUMBER)
    {
        int i = 0;
        DataSet dsOutput = new DataSet("BILL_DET");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_BILL_DET_64 objOutPut = new ZBAPI_BILL_DET_64();
        DataTable[] BapiResult = new DataTable[1];
        DataTable dtPDFDetails = new DataTable();

        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_BILL_DET_64");
                testfn.SetValue("VKONT", CANUMBER);
                testfn.Invoke(dest);

                IRfcTable _IT_INPUT = testfn.GetTable("T_PDF");
                dtPDFDetails = objOutPut.converttodotnetatble(_IT_INPUT);
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }

        BapiResult[0] = dtPDFDetails;
        dsOutput.Tables.Add(BapiResult[0]);

        return dsOutput;
    }

    public DataSet Get_ZBAPI_STREET_DET_UPD(string COMPANY, string CANUMBER, string DATA_PROCESS_DATE, string STLWATT, string NO_OF_POINT, string INSTALLATION_DATE, string MOVEOUT_DATE, string ACTIVATION, string DEACTIVATION, string REQUESTID, string REQUEST_DATE, string DOCUMENT_UPLOADED)
    {
        DataSet dsOutput = new DataSet("STREET_DET_UPD");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBAPI_STREET_DET_UPD objOutPut = new ZBAPI_STREET_DET_UPD();
        DataTable[] BapiResult = new DataTable[1];
        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                //RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
                //RfcRepository repo = dest.Repository;
                //IRfcFunction testfn = repo.CreateFunction("ZBAPI_STREET_DET_UPD");
                //testfn.SetValue("COMPANY", COMPANY);
                //testfn.SetValue("CANUMBER", CANUMBER);
                ////testfn.SetValue("DATA_PROCESS_DATE", "20200606");
                ////testfn.SetValue("DATA_PROCESS_DATE", DateTime.ParseExact(DATA_PROCESS_DATE, "yyyyddmm", CultureInfo.InvariantCulture));
                //testfn.SetValue("DATA_PROCESS_DATE", Convert.ToDateTime(DATA_PROCESS_DATE).ToString("MM-dd-yyyy"));
                //// testfn.SetValue("DATA_PROCESS_DATE", DateTime.ParseExact(DATA_PROCESS_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //testfn.SetValue("STLWATT", STLWATT);
                //testfn.SetValue("NO_OF_POINT", NO_OF_POINT);
                //// //testfn.SetValue("INSTALLATION_DATE", DateTime.ParseExact(INSTALLATION_DATE, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                //// //testfn.SetValue("MOVEOUT_DATE", DateTime.ParseExact(MOVEOUT_DATE, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                //// testfn.SetValue("INSTALLATION_DATE", DateTime.ParseExact(INSTALLATION_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //// testfn.SetValue("MOVEOUT_DATE", DateTime.ParseExact(MOVEOUT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //testfn.SetValue("INSTALLATION_DATE", Convert.ToDateTime(INSTALLATION_DATE).ToString("yyyyMMdd"));
                //testfn.SetValue("MOVEOUT_DATE", Convert.ToDateTime(MOVEOUT_DATE).ToString("yyyyMMdd"));
                //testfn.SetValue("ACTIVATION", ACTIVATION);
                //testfn.SetValue("DEACTIVATION", DEACTIVATION);
                //testfn.SetValue("REQUESTID", REQUESTID);
                ////// testfn.SetValue("REQUEST_DATE", DateTime.ParseExact(REQUEST_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //// // testfn.SetValue("REQUEST_DATE", DateTime.ParseExact(REQUEST_DATE, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                //testfn.SetValue("REQUEST_DATE", Convert.ToDateTime(REQUEST_DATE).ToString("yyyyMMdd"));
                //// testfn.SetValue("DOCUMENT_UPLOADED", DOCUMENT_UPLOADED);
                //testfn.Invoke(dest);
                //string _sFlag = testfn.GetString("FLAG");
                //dtMessageText = objOutPut.CreateOutputDataTable(_sFlag);


                //Change By Babalu Kumar

                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBAPI_STREET_DET_UPD");
                DateTime date1 = DateTime.ParseExact(DATA_PROCESS_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime date2 = DateTime.ParseExact(INSTALLATION_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime date3 = DateTime.ParseExact(MOVEOUT_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime date4 = DateTime.ParseExact(REQUEST_DATE, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                testfn.SetValue("COMPANY", COMPANY);
                testfn.SetValue("CANUMBER", CANUMBER);
                testfn.SetValue("DATA_PROCESS_DATE", date1);
                //testfn.SetValue("DATA_PROCESS_DATE",Convert.ToDateTime(DATA_PROCESS_DATE).ToString("MM-dd-yyyy"));
                // testfn.SetValue("DATA_PROCESS_DATE", DateTime.ParseExact(DATA_PROCESS_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                testfn.SetValue("STLWATT", STLWATT);
                testfn.SetValue("NO_OF_POINT", NO_OF_POINT);
                testfn.SetValue("INSTALLATION_DATE", date2);
                testfn.SetValue("MOVEOUT_DATE", date3);
                // testfn.SetValue("INSTALLATION_DATE", DateTime.ParseExact(INSTALLATION_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                // testfn.SetValue("MOVEOUT_DATE", DateTime.ParseExact(MOVEOUT_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
                //testfn.SetValue("INSTALLATION_DATE", Convert.ToDateTime(INSTALLATION_DATE).ToString("MM-dd-yyyy"));
                //testfn.SetValue("MOVEOUT_DATE", Convert.ToDateTime(MOVEOUT_DATE).ToString("MM-dd-yyyy"));
                testfn.SetValue("ACTIVATION", ACTIVATION);
                testfn.SetValue("DEACTIVATION", DEACTIVATION);
                testfn.SetValue("REQUESTID", REQUESTID);
                testfn.SetValue("REQUEST_DATE", date4);
                // testfn.SetValue("REQUEST_DATE", DateTime.ParseExact(REQUEST_DATE, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                //testfn.SetValue("REQUEST_DATE", Convert.ToDateTime(REQUEST_DATE).ToString("MM-dd-yyyy"));
                testfn.SetValue("DOCUMENT_UPLOADED", DOCUMENT_UPLOADED);
                testfn.Invoke(dest);
                string _sFlag = testfn.GetString("FLAG");
                dtMessageText = objOutPut.CreateOutputDataTable(_sFlag);

                //End
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }

    public DataSet Get_ZBI_PREPAID_MTR(string CANUMBER)
    {

        DataSet dsOutput = new DataSet("PREPAID_MTR");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBI_PREPAID_MTR objOutPut = new ZBI_PREPAID_MTR();
        DataTable[] BapiResult = new DataTable[3];
        DataTable dtDetails = new DataTable();
        DataTable dtReturn = new DataTable();


        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBI_PREPAID_MTR");
                testfn.SetValue("IN_VKONT", CANUMBER);

                testfn.Invoke(dest);

                //string Flag = (string)testfn.GetValue("OUT_PREPAID_FLAG");
                //objOutPut.pushOutputDataInDataTable(dtDetails, Flag);


                //IRfcTable irfcReturnTable2 = testfn.GetTable("RETURN");
                //dtReturn = objOutPut.converttodotnetatble(irfcReturnTable2);

                string _sFlag = testfn.GetString("OUT_PREPAID_FLAG");
                dtMessageText = objOutPut.CreateOutputDataTable(_sFlag);
            }
        }
        //catch (RfcCommunicationException ex)
        //{
        //    strMessage = "RfcCommunicationException :" + ex.Message.ToString();
        //    strMessagecode = "91";
        //}
        //catch (RfcLogonException ex)
        //{
        //    strMessage = "RfcLogonException :" + ex.Message.ToString();
        //    strMessagecode = "92";
        //}
        //catch (RfcAbapException ex)
        //{
        //    strMessage = "RfcAbapException :" + ex.Message.ToString();
        //    strMessagecode = "93";
        //}
        //catch (Exception ex)
        //{
        //    strMessage = ex.Message.ToString();
        //    strMessagecode = "94";
        //}

        //if (strMessage.Trim() != "")
        //{
        //    objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        //}

        //BapiResult[0] = dtDetails;
        //BapiResult[1] = objOutPut.makeBAPIRET2TABLE(dtReturn);
        //BapiResult[2] = dtMessageText;

        //dsOutput.Tables.Add(BapiResult[0]);
        //dsOutput.Tables.Add(BapiResult[1]);
        //dsOutput.Tables.Add(BapiResult[2]);

        //return dsOutput;

        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }

    public DataSet Get_ZBI_BAPI_SOLAR13(string CA_NUMBER, string BILL_MONTH)
    {
        DataSet dsOutput = new DataSet("BAPI_SOLAR1");
        string strMessage = string.Empty;
        string strMessagecode = "0";
        ZBI_BAPI_SOLAR1 objOutPut = new ZBI_BAPI_SOLAR1();
        DataTable dtPDFDetails = new DataTable();
        DataTable[] BapiResult = new DataTable[1];
        DataTable dtMessageText = objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();
            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }
            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBI_BAPI_SOLAR1");
                testfn.SetValue("IN_VKONT", CA_NUMBER);
                testfn.SetValue("IN_BILL_MONTH", BILL_MONTH);
                testfn.Invoke(dest);
                IRfcStructure bapiTable1 = testfn.GetStructure("OUT_SOLAR");
          //      IRfcStructure bapiTable = testfn.GetStructure("Return");
            }
        }
        catch (RfcCommunicationException ex)
        {
            strMessage = "RfcCommunicationException :" + ex.Message.ToString();
            strMessagecode = "91";
        }
        catch (RfcLogonException ex)
        {
            strMessage = "RfcLogonException :" + ex.Message.ToString();
            strMessagecode = "92";
        }
        catch (RfcAbapException ex)
        {
            strMessage = "RfcAbapException :" + ex.Message.ToString();
            strMessagecode = "93";
        }
        catch (Exception ex)
        {
            strMessage = ex.Message.ToString();
            strMessagecode = "94";
        }

        if (strMessage.Trim() != "")
        {
            objOutPut.pushMessageTextInDataTable(dtMessageText, strMessagecode, strMessage.Trim());
        }
        BapiResult[0] = dtMessageText;
        dsOutput.Tables.Add(BapiResult[0]);
        return dsOutput;
    }

    public DataSet Get_ZBI_BAPI_SOLAR1(string CA_NUMBER, string BILL_MONTH)
    {
        string messageText = string.Empty;
        string messageCode = "0";
        DataSet dsBAPIOutput = new DataSet("BAPI_SOLAR1");
        ZBI_BAPI_SOLAR1 _objOutPut = new ZBI_BAPI_SOLAR1();
        DataTable dtreturnTable1 = new DataTable();
        DataTable dtreturnTable2 = new DataTable();
        DataTable[] bapiResult = new DataTable[2];
        DataTable dtCADetails = _objOutPut.makeFlagsTable();
        DataTable dtMessageText = _objOutPut.makeMessageTextTable();
        try
        {
            clsConnect cfg = new clsConnect();

            if (destinationIsInialised == false)
            {
                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                destinationIsInialised = true;
            }

            if (destinationIsInialised == true)
            {
                RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination1");
                RfcRepository repo = dest.Repository;
                IRfcFunction testfn = repo.CreateFunction("ZBI_BAPI_SOLAR1");
                testfn.SetValue("IN_VKONT", CA_NUMBER);
                testfn.SetValue("IN_BILL_MONTH", BILL_MONTH);
                testfn.Invoke(dest);
                IRfcStructure bapiTable1 = testfn.GetStructure("OUT_SOLAR");//ZBI_NETMTR_STRUC//OUT_SOLAR
                dtreturnTable1 = _objOutPut.CreateOutputDataTable(bapiTable1.GetString("MANDT"), bapiTable1.GetString("CONSUMER_NO"),
                    bapiTable1.GetString("INVOICE_NO"), bapiTable1.GetString("COMPANY_CODE"),bapiTable1.GetString("DATE_OF_INVOICE"), 
                    bapiTable1.GetString("BILL_MONTH"),bapiTable1.GetString("FROM_DATE"),
                    bapiTable1.GetString("TO_DATE"), bapiTable1.GetString("NO_OF_DAYS"),bapiTable1.GetString("EXPORT_UNIT"), 
                    bapiTable1.GetString("IMPORT_UNIT"),bapiTable1.GetString("SOLAR_MTR_UNIT"), bapiTable1.GetString("FIN_YEAR"),
                    bapiTable1.GetString("CUM_APR_TO_SEP"), bapiTable1.GetString("CUM_OCT_TO_MAR"),bapiTable1.GetString("GBI_APR_TO_SEP"),
                    bapiTable1.GetString("GBI_OCT_TO_MAR"), bapiTable1.GetString("CUM_FOR_FY"), bapiTable1.GetString("CUM_SINCE_INST"));
            }
}
        catch (RfcCommunicationException ex)
        {
            messageText = "RfcCommunicationException :" + ex.Message.ToString();
            messageCode = "91";
        }
        catch (RfcLogonException ex)
        {
            messageText = "RfcLogonException :" + ex.Message.ToString();
            messageCode = "92";
        }
        catch (RfcAbapException ex)
        {
            messageText = "RfcAbapException :" + ex.Message.ToString();
            messageCode = "93";
        }
        catch (Exception ex)
        {
            messageText = ex.Message.ToString();
            messageCode = "94";
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

        if (messageText.Trim() != "")
        {
            _objOutPut.pushMessageTextInDataTable(dtMessageText, messageCode, messageText.Trim());
        }

      //  _objOutPut.pushOutputDataInDataTable(dtCADetails, "", "", "", "", "","","","","","","","","","","","","","","");

        bapiResult[0] = dtreturnTable1;
        bapiResult[1] = dtMessageText;

        dsBAPIOutput.Tables.Add(bapiResult[0]);
        dsBAPIOutput.Tables.Add(bapiResult[1]);

        return dsBAPIOutput;
    }
}