using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    clsBapiCall obj = new clsBapiCall();

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //   [WebMethod]
    //   public DataSet ZBAPIDOCLIST(string strAufnr, string strC_001, string strC_002, string strC_003, string strC_004, string strC_005, string strC_007, string strC_008, string strC_009, string strC_010,
    //       string strC_011, string strC_012, string strC_013, string strC_014, string strC_015, string strC_016, string strC_017, string strC_018, string strC_019, string strC_020, string strC_021, string strC_022,
    //       string strC_023, string strC_024, string strC_025, string strC_026, string strC_027, string strC_028, string strC_029, string strC_030, string strC_031, string strC_032, string strC_033, string strC_034,
    //       string strC_035, string strC_036, string strC_037, string strC_038, string strC_039, string strC_040, string strC_041, string strC_070, string strR_Cdll, string strR_Occ, string strR_Own, string strZ_Appltype)
    //   {
    //       DataSet Ds = obj.Get_ZBAPIDOCLIST(strAufnr, strC_001.ToUpper(), strC_002.ToUpper(), strC_003.ToUpper(), strC_004.ToUpper(), strC_005.ToUpper(), strC_007.ToUpper(), strC_008.ToUpper(), strC_009.ToUpper(), strC_010.ToUpper(),
    //        strC_011.ToUpper(), strC_012.ToUpper(), strC_013.ToUpper(), strC_014.ToUpper(), strC_015.ToUpper(), strC_016.ToUpper(), strC_017.ToUpper(), strC_018.ToUpper(), strC_019.ToUpper(), strC_020.ToUpper(), strC_021.ToUpper(), strC_022.ToUpper(),
    //        strC_023.ToUpper(), strC_024.ToUpper(), strC_025.ToUpper(), strC_026.ToUpper(), strC_027.ToUpper(), strC_028.ToUpper(), strC_029.ToUpper(), strC_030.ToUpper(), strC_031.ToUpper(), strC_032.ToUpper(), strC_033.ToUpper(), strC_034.ToUpper(),
    //        strC_035.ToUpper(), strC_036.ToUpper(), strC_037.ToUpper(), strC_038.ToUpper(), strC_039.ToUpper(), strC_040.ToUpper(), strC_041.ToUpper(), strC_070.ToUpper(), strR_Cdll.ToUpper(), strR_Occ.ToUpper(), strR_Own.ToUpper(), strZ_Appltype.ToUpper());
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_DISPLAY_BILL_WEB(string strCANumber, string strBillMonth)
    //   {
    //      DataSet Ds = obj.Get_ZBAPI_DISPLAY_BILL_WEB(strCANumber, strBillMonth);
    //      return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_IVR_CREATESO_ISU(string strCANumber, string strCACrn, string strCAKNumber, string strMeterNumber, string strISUOrder, 
    //                                                           string strComplaintType, string strContractNumber, string strTelephoneNo, string strDescription)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_IVR_CREATESO_ISU(strCANumber, strCACrn, strCAKNumber, strMeterNumber, strISUOrder, strComplaintType, strContractNumber, strTelephoneNo, strDescription);
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet Z_BAPI_CMS_ISU_CA_DISPLAY(string strCANumber, string strSerialNumber, string strConsumerNumber, string strTelephoneNumber, string strKNumber, string strContractNumber)
    //   {
    //       DataSet Ds = obj.Get_Z_BAPI_CMS_ISU_CA_DISPLAY(strCANumber, strSerialNumber, strConsumerNumber, strTelephoneNumber, strKNumber, strContractNumber);
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_CREATESO_POST(string strPMAufart, string strPlanPlant, string strRegioGroup, string strShortText, string strILA, string strMFText, string strUserFieldCH20, string StrControkey, string strSerialNumber, string strComplaintGroup, 
    //                                                                                           string strCANumber, string strContract, string strMFText1)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_CREATESO_POST( strPMAufart,  strPlanPlant,  strRegioGroup,  strShortText,  strILA,  strMFText,  strUserFieldCH20,  StrControkey, 
    //                                                               strSerialNumber,  strComplaintGroup, strCANumber,  strContract,  strMFText1);
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_CALERT(string strCompanyCode, string strDate, string strDivision, string strUnit)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_CALERT(strCompanyCode, strDate, strDivision, strUnit);
    //       return Ds;
    //   }

    ////   [WebMethod]
    //   public DataSet ZBAPI_ONLINE_BILL_PDF(string strCANumber, string strEBSKNO) // Testing required
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_ONLINE_BILL_PDF(strCANumber,strEBSKNO);
    //       return Ds;
    //   }

    // //  [WebMethod]
    //   public DataSet ZBAPI_DSS_SO(string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, 
    //                               string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, 
    //                               string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string LEGITTYPE, 
    //                               string IDNUMBER, string ORDER_TYPE, string SHORTTEXT, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, 
    //                               string PMACTIVITYTYPE, string REQUESTNUM, string NNUMBER, string APPLIEDCAT, string APPLIEDLOAD, string CONNECTIONTYPE, 
    //                               string ORDERID, string FLAG)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_DSS_SO( PARTNERCATEGORY,  PARTNERTYPE,  TITLE_KEY,  FIRSTNAME,  LASTNAME,  MIDDLENAME, 
    //                                FATHERSNAME,  HOUSE_NO,  BUILDING,  STR_SUPPL1,  STR_SUPPL2,  STR_SUPPL3,  POSTL_COD1, 
    //                                CITY,  E_MAIL,  LANDLINE,  MOBILE,  FEMALE,  MALE,  JOBGR,  LEGITTYPE, 
    //                                IDNUMBER,  ORDER_TYPE,  SHORTTEXT,  PLANNINGPLANT,  WORKCENTRE,  SYSTEMCOND, 
    //                                PMACTIVITYTYPE,  REQUESTNUM,  NNUMBER,  APPLIEDCAT,  APPLIEDLOAD,  CONNECTIONTYPE, 
    //                                ORDERID,  FLAG);
    //       return Ds;
    //   }



    ////   [WebMethod]
    //   public DataSet ZBAPI_DSS_SO_ECC(string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, string LASTNAME, string MIDDLENAME, 
    //                               string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, string STR_SUPPL3, string POSTL_COD1, 
    //                               string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, string JOBGR, string IDTYPE, 
    //                               string IDNUMBER, string ORDER_TYPE, string SHORTTEXT, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, 
    //                               string PMACTIVITYTYPE, string REQUESTNUM, string NNUMBER, string APPLIEDCAT, string APPLIEDLOAD, string CONNECTIONTYPE, 
    //                               string ORDERID, string FLAG)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_DSS_SO_ECC(PARTNERCATEGORY, PARTNERTYPE, TITLE_KEY, FIRSTNAME, LASTNAME, MIDDLENAME,
    //                                FATHERSNAME, HOUSE_NO, BUILDING, STR_SUPPL1, STR_SUPPL2, STR_SUPPL3, POSTL_COD1,
    //                                CITY, E_MAIL, LANDLINE, MOBILE, FEMALE, MALE, JOBGR, IDTYPE,
    //                                IDNUMBER, ORDER_TYPE, SHORTTEXT, PLANNINGPLANT, WORKCENTRE, SYSTEMCOND,
    //                                PMACTIVITYTYPE, REQUESTNUM, NNUMBER, APPLIEDCAT, APPLIEDLOAD, CONNECTIONTYPE,
    //                                ORDERID, FLAG);
    //       return Ds;
    //   }


    //   [WebMethod]
    //   public DataSet Z_BAPI_ZDSS_WEB_LINK(string I_ILART, string I_VKONT, string I_VKONA, string PARTNERCATEGORY, string PARTNERTYPE, string TITLE_KEY, string FIRSTNAME, 
    //                                   string LASTNAME, string MIDDLENAME, string FATHERSNAME, string HOUSE_NO, string BUILDING, string STR_SUPPL1, string STR_SUPPL2, 
    //                                   string STR_SUPPL3, string POSTL_COD1, string CITY, string E_MAIL, string LANDLINE, string MOBILE, string FEMALE, string MALE, 
    //                                   string JOBGR, string IDTYPE, string IDNUMBER, string PLANNINGPLANT, string WORKCENTRE, string SYSTEMCOND, string APPLIEDCAT, 
    //                                   string APPLIEDLOAD, string APPLIEDLOADKVA, string CONNECTIONTYPE, string STATEMENT_CA, string START_DATE, string START_TIME, 
    //                                   string FINISH_DATE, string FINISH_TIME, string SORTFIELD, string ABKRS)
    //   {
    //       DataSet Ds = obj.Get_Z_BAPI_ZDSS_WEB_LINK(I_ILART,  I_VKONT,  I_VKONA,  PARTNERCATEGORY,  PARTNERTYPE,  TITLE_KEY,  FIRSTNAME, 
    //                                    LASTNAME,  MIDDLENAME,  FATHERSNAME,  HOUSE_NO,  BUILDING,  STR_SUPPL1,  STR_SUPPL2, 
    //                                    STR_SUPPL3,  POSTL_COD1,  CITY,  E_MAIL,  LANDLINE,  MOBILE,  FEMALE,  MALE, 
    //                                    JOBGR,  IDTYPE,  IDNUMBER,  PLANNINGPLANT,  WORKCENTRE,  SYSTEMCOND,  APPLIEDCAT, 
    //                                    APPLIEDLOAD,  APPLIEDLOADKVA,  CONNECTIONTYPE,  STATEMENT_CA,  START_DATE,  START_TIME, 
    //                                    FINISH_DATE,  FINISH_TIME,  SORTFIELD,  ABKRS);
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_ENFORCEMENT(string strCANumber, string strContract)
    //   {
    //       DataSet Ds = obj.Get_ZBAPI_ENFORCEMENT(strCANumber, strContract);
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet ZBI_WEBBILL_HIST(string strCANumber, string strBillMonth)
    //   {
    //       DataSet Ds = obj.Get_ZBI_WEBBILL_HIST(strCANumber, strBillMonth);
    //       return Ds;
    //   }

    //   [WebMethod]
    //   public DataSet Z_BAPI_IVRS(string strContractAccountNumber)
    //   {
    //       DataSet dsBAPIOutput = obj.Get_Z_BAPI_IVRS(strContractAccountNumber);
    //       return dsBAPIOutput;
    //   }


    //   [WebMethod]
    //   public DataSet Z_BAPI_DSS_ISU_CA_DISPLAY(string strCANumber, string strCRNNumber)
    //   {
    //       DataSet dsBAPIOutput = obj.Get_Z_BAPI_DSS_ISU_CA_DISPLAY(strCANumber, strCRNNumber);

    //       return dsBAPIOutput;
    //   }

    //   [WebMethod]
    //   public DataSet ZBAPI_FICA_PREPAID_MTR(string strDOC_ID, string strTRANS_ID, string strCA, string strCOMPANY,
    //           string strCONSUMER_TYPE, string strMETER_MANFR, string strCONTRACT, string strCA_VALID_ISU, string strENTRY_DATE,
    //           string strS_ENC_TKN_1, string strS_ENC_TKN_2, string strS_ENC_TKN_3, string strS_ENC_TKN_4, string strGENUS_RESP_CODE,
    //           string strMETER_NO, string strACC_CLASS, string strAMNT_BANK, string strAMNT_ISU, string strADDRESS, string strTARIFTYP,
    //           string strTARIFID, string strPAY_METHOD)
    //   {
    //       DataSet dsBAPIOutput = obj.get_ZBAPI_FICA_PREPAID_MTR(strDOC_ID, strTRANS_ID, strCA, strCOMPANY,
    //           strCONSUMER_TYPE, strMETER_MANFR, strCONTRACT, strCA_VALID_ISU, strENTRY_DATE,
    //           strS_ENC_TKN_1, strS_ENC_TKN_2, strS_ENC_TKN_3, strS_ENC_TKN_4, strGENUS_RESP_CODE,
    //            strMETER_NO, strACC_CLASS, strAMNT_BANK, strAMNT_ISU, strADDRESS, strTARIFTYP,
    //            strTARIFID,  strPAY_METHOD);

    //       return dsBAPIOutput;
    //   }


    //[WebMethod]
    //public DataSet ZBAPI_CA_OUTSTANDING_AMT(string strCANumber)
    //{
    //    DataSet dsBAPIOutput = obj.get_ZBAPI_CA_OUTSTANDING_AMT(strCANumber);

    //    return dsBAPIOutput;
    //}

    [WebMethod]
    public DataSet ZBAPI_CA_OUTSTANDING_AMT(string strCANumber)
    {
        DataSet dsBAPIOutput = obj.get_ZFI_CURR_OUTS_FLAG(strCANumber);

        return dsBAPIOutput;
    }
    [WebMethod]
    public DataSet ZBAPI_LAST_MODE_PAY(string CA, string FLAG)
    {
        DataSet Ds = obj.Get_ZBAPI_LAST_MODE_PAY(CA, FLAG);
        return Ds;
    }
    [WebMethod]
    public DataSet BAPI_MTRREADDOC_GETLIST(string METERNO)//By Babalu Kumar
    {
        string KWH = string.Empty;
        string KW = string.Empty;
        string KVAH = string.Empty;
        string KVA = string.Empty;
        string Readdate = string.Empty;
        string MeterNo = string.Empty;
        DataSet Ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("Device", typeof(string));
        dt.Columns.Add("BillDate", typeof(string));
        dt.Columns.Add("KWH", typeof(string));
        dt.Columns.Add("KW", typeof(string));
        dt.Columns.Add("KVAH", typeof(string));
        dt.Columns.Add("KVA", typeof(string));
        try
        {
            DataSet ds = obj.Get_BAPI_MTRREADDOC_GETLIST(METERNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtnew = SelectDistinct(ds.Tables[0], "MRDATEFORBILLING");
                DataRow dr;
                for (int i = 0; i <= dtnew.Rows.Count - 1; i++)
                {
                    KWH = "NA";
                    KW = "NA";
                    KVAH = "NA";
                    KVA = "NA";
                    MeterNo = ds.Tables[0].Rows[i]["DEVICE"].ToString();
                    Readdate = dtnew.Rows[i]["MRDATEFORBILLING"].ToString();
                    dr = dt.NewRow();
                    DataRow[] result = ds.Tables[0].Select("MRDATEFORBILLING ='" + dtnew.Rows[i]["MRDATEFORBILLING"].ToString() + "'");
                    foreach (DataRow row in result)
                    {
                        if (row[3].ToString() == "001")
                        {
                            KWH = row[13].ToString();

                        }
                        else if (row[3].ToString() == "002")
                        {
                            KW = row[13].ToString();
                        }
                        else if (row[3].ToString() == "003")
                        {
                            KVAH = row[13].ToString();
                        }
                        else if (row[3].ToString() == "004")
                        {
                            KVA = row[13].ToString();
                        }
                    }
                    dr["Device"] = MeterNo;
                    dr["BillDate"] = Readdate;
                    dr["KWH"] = KWH;
                    dr["KW"] = KW;
                    dr["KVAH"] = KVAH;
                    dr["KVA"] = KVA;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
                Ds.Tables.Add(dt);
            }
            else
            {
                dt.Rows.Add("Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again");
                Ds.Tables.Add(dt);
            }
            return Ds;
        }
        catch (Exception ex)
        {
            dt.Rows.Add(ex.Message.ToString(), "", "", "", "", "");
            Ds.Tables.Add(dt);
            return Ds;
        }
    }
    //public DataSet BAPI_MTRREADDOC_GETLIST(string METERNO)//By Babalu Kumar
    //{
    //    string KWH = string.Empty;
    //    string KW = string.Empty;
    //    string KVAH = string.Empty;
    //    string KVA = string.Empty;
    //    string Readdate = string.Empty;
    //    DataSet Ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    DataColumn column = new DataColumn();
    //    dt.Columns.Add("BillDate", typeof(string));
    //    dt.Columns.Add("KWH", typeof(string));
    //    dt.Columns.Add("KW", typeof(string));
    //    dt.Columns.Add("KVAH", typeof(string));
    //    dt.Columns.Add("KVA", typeof(string));
    //    try
    //    {
    //        DataSet ds = obj.Get_BAPI_MTRREADDOC_GETLIST(METERNO);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataTable dtnew = SelectDistinct(ds.Tables[0], "MRDATEFORBILLING");
    //            DataRow dr;
    //            for (int i = 0; i <= dtnew.Rows.Count - 1; i++)
    //            {
    //                KWH = "NA";
    //                KW = "NA";
    //                KVAH = "NA";
    //                KVA = "NA";
    //                Readdate = dtnew.Rows[i]["MRDATEFORBILLING"].ToString();
    //                dr = dt.NewRow();
    //                DataRow[] result = ds.Tables[0].Select("MRDATEFORBILLING ='" + dtnew.Rows[i]["MRDATEFORBILLING"].ToString() + "'");
    //                foreach (DataRow row in result)
    //                {
    //                    if (row[3].ToString() == "001")
    //                    {
    //                        KWH = row[13].ToString();

    //                    }
    //                    else if (row[3].ToString() == "002")
    //                    {
    //                        KW = row[13].ToString();
    //                    }
    //                    else if (row[3].ToString() == "003")
    //                    {
    //                        KVAH = row[13].ToString();
    //                    }
    //                    else if (row[3].ToString() == "004")
    //                    {
    //                        KVA = row[13].ToString();
    //                    }
    //                }
    //                dr["BillDate"] = Readdate;
    //                dr["KWH"] = KWH;
    //                dr["KW"] = KW;
    //                dr["KVAH"] = KVAH;
    //                dr["KVA"] = KVA;
    //                dt.Rows.Add(dr);
    //                dt.AcceptChanges();
    //            }
    //            Ds.Tables.Add(dt);
    //        }
    //        else
    //        {
    //            dt.Rows.Add("Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again");
    //            Ds.Tables.Add(dt);
    //        }
    //        return Ds;
    //    }
    //    catch (Exception ex)
    //    {
    //        dt.Rows.Add(ex.Message.ToString(), "", "", "", "");
    //        Ds.Tables.Add(dt);
    //        return Ds;
    //    }
    //}
    private bool ColumnEqual(object A, object B)
    {
        if (A == DBNull.Value && B == DBNull.Value)
            return true;
        if (A == DBNull.Value || B == DBNull.Value)
            return false;
        return (A.Equals(B));
    }
    public DataTable SelectDistinct(DataTable SourceTable, string FieldName)
    {
        DataTable dt = new DataTable(SourceTable.TableName);
        dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);
        object LastValue = null;
        foreach (DataRow dr in SourceTable.Select("", FieldName))
        {
            if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
            {
                LastValue = dr[FieldName];
                dt.Rows.Add(new object[] { LastValue });
            }
        }
        return dt;
    }

    [WebMethod]
    public DataSet ZBAPI_FETCH_ENF_USER_DET(string CA_NUMBER) //Added By Babalu Kumar 14082020
    {
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("CASE_ID", typeof(string));
        dt.Columns.Add("COMPLAINT_ID", typeof(string));
        dt.Columns.Add("INSPECTION_DATE", typeof(string));
        dt.Columns.Add("INSP_CA_NUMBER", typeof(string));
        dt.Columns.Add("USER_NAME", typeof(string));
        dt.Columns.Add("HOUSEFLATNO", typeof(string));
        dt.Columns.Add("BUILDING_NAME1", typeof(string));
        dt.Columns.Add("STREET1", typeof(string));
        dt.Columns.Add("COLONY_AREA1", typeof(string));
        dt.Columns.Add("LANDMARK", typeof(string));
        dt.Columns.Add("CITY_CODE1", typeof(string));
        dt.Columns.Add("PIN_CODE1", typeof(string));
        dt.Columns.Add("CASE_TYPE", typeof(string));
        dt.Columns.Add("ENF_ORDER", typeof(string));
        dt.Columns.Add("ENF_CA", typeof(string));
        dt.Columns.Add("SOURCE_OF_COMPLA", typeof(string));
        dt.Columns.Add("COKEY", typeof(string));
        dt.Columns.Add("SUB_DIV", typeof(string));
        DataSet Ds = obj.Get_ZBAPI_FETCH_ENF_USER_DET(CA_NUMBER);
        if (Ds.Tables[0].Rows.Count > 0)
        {
            Ds.Tables.Add(dt);
            Ds.Tables.Remove("Table2");
            Ds.Tables.Remove("messageTable");
            dt.AcceptChanges();
        }
        else
        {
            Ds.Tables.Remove("Table1");
            Ds.Tables.Remove("messageTable");
            dt.TableName = "Table1";
            dt.Rows.Add("Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again",
                "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again",
                "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again", "Data not available try again",
                "Data not available try again", "Data not available try again", "Data not available try again");
            Ds.Tables.Add(dt);
            dt.AcceptChanges();

        }
        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_BILL_DET(string CA_NUMBER)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        string _sCircle = string.Empty, _sDivision = string.Empty, _sCompany = string.Empty, _sName = string.Empty;
        string _sSerReqDateTime = System.DateTime.Now.ToString();
        string _sSerReqOutDateTime = string.Empty;

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_DISPLAY_BILL_WEB(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "");
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //CA_NUMBER = CA_NUMBER.ToString().Substring(3, 9);
                _sName = Convert.ToString(dt.Rows[0]["FIRSTNAME"]) + " " + Convert.ToString(dt.Rows[0]["MIDDLE_NAME"]) + " " + Convert.ToString(dt.Rows[0]["LASTNAME"]);
                _sCircle = Convert.ToString(dt.Rows[0]["CIRCLE_DESCR"]);
                _sDivision = Convert.ToString(dt.Rows[0]["UNIT_DESCR"]);
                _sCompany = Convert.ToString(dt.Rows[0]["COMPANY_CODE"]);

                Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, "", "");
            }
        }

        DataSet Ds = obj.Get_ZBAPI_BILL_DET(CA_NUMBER);
        Insert_Service_Log(CA_NUMBER, null, _sName, _sCircle, _sDivision, _sCompany, "", "");

        return Ds;
    }

    private void Insert_Service_Log(string _sCA_No, string _sReqInDateTime, string Name, string Circle, string Division, string CompName, string MobileNo,
                                   string ResponseData)
    {
        string _sSql = string.Empty;
        string _sReqInTime = string.Empty;
        if (_sReqInDateTime != null)
            _sReqInTime = _sReqInDateTime;
        else
            _sReqInDateTime = null;

        if (_sReqInDateTime != null)
        {
            // System.DateTime.Now.ToString("dd/MM/yyyy hh:m:ss")
            _sReqInDateTime = Convert.ToDateTime(_sReqInDateTime).ToString("dd/MM/yyyy hh:m:ss");
        }

        if (_sReqInDateTime != null)
        {
            _sSql = "INSERT INTO mobapp.BRPL_SERVICE_LOG_DT(CA_NUMBER,SERVICE_TYPE,CONS_NAME,CIRCLE,DIVISION,COMPANY_NAME,MOBILE_NO,REQ_SERVICE_INDATE,REQ_SERVICE_OUTDATE,RESPONSE) VALUES ";
            _sSql += " ('" + _sCA_No + "','BILLEXPLAIN-WHATSAPP','" + Name + "','" + Circle + "','" + Division + "','" + CompName + "','" + MobileNo + "',TO_DATE('" + _sReqInDateTime + "','DD/MM/YYYY HH24:MI:SS'),sysdate,'" + ResponseData + "')";


            //_sSql = "INSERT INTO mobapp.BRPL_SERVICE_LOG_DT(CA_NUMBER,SERVICE_TYPE,CONS_NAME,CIRCLE,DIVISION,COMPANY_NAME,MOBILE_NO,REQ_SERVICE_INDATE,REQ_SERVICE_OUTDATE,RESPONSE) VALUES ";
            //_sSql += " ('" + _sCA_No + "','BILLEXPLAIN-WHATSAPP','" + Name + "','" + Circle + "','" + Division + "','" + CompName + "','" + MobileNo + "',sysdate,null,'" + ResponseData + "')";
        }
        //else
        //{
        //    _sSql = "INSERT INTO mobapp.BRPL_SERVICE_LOG_DT(CA_NUMBER,SERVICE_TYPE,CONS_NAME,CIRCLE,DIVISION,COMPANY_NAME,MOBILE_NO,REQ_SERVICE_INDATE,REQ_SERVICE_OUTDATE,RESPONSE) VALUES ";
        //    _sSql += " ('" + _sCA_No + "','BILLEXPLAIN-WHATSAPP','" + Name + "','" + Circle + "','" + Division + "','" + CompName + "','" + MobileNo + "',null,sysdate,'" + ResponseData + "')";
        //}

        dmlsinglequery(_sSql);
    }

    public bool dmlsinglequery(string sql)
    {
        OleDbCommand dbcommand;
        OleDbTransaction dbtrans;
        NDS objNDS = new NDS();
        OleDbConnection ocon = new OleDbConnection(objNDS.con());
        try
        {
            if (ocon.State == ConnectionState.Closed)
            {
                ocon.Open();
            }

            dbcommand = new OleDbCommand(sql, ocon);
            //dbcommand.Transaction = dbtrans;
            dbcommand.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
                ocon.Dispose();
            }
        }
    }

    [WebMethod]
    public DataSet ZBAPI_BILL_DET_API(string CA_NUMBER)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        string _sCircle = string.Empty, _sDivision = string.Empty, _sCompany = string.Empty, _sName = string.Empty;
        string _sSerReqDateTime = System.DateTime.Now.ToString();
        string _sSerReqOutDateTime = string.Empty;

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_DISPLAY_BILL_WEB(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "");
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //CA_NUMBER = CA_NUMBER.ToString().Substring(3, 9);
                _sName = Convert.ToString(dt.Rows[0]["FIRSTNAME"]) + " " + Convert.ToString(dt.Rows[0]["MIDDLE_NAME"]) + " " + Convert.ToString(dt.Rows[0]["LASTNAME"]);
                _sCircle = Convert.ToString(dt.Rows[0]["CIRCLE_DESCR"]);
                _sDivision = Convert.ToString(dt.Rows[0]["UNIT_DESCR"]);
                _sCompany = Convert.ToString(dt.Rows[0]["COMPANY_CODE"]);

                Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, "", "");
            }
        }

        string str = "";
        DataSet ds = obj.Get_ZBAPI_BILL_DET(CA_NUMBER);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += ds.Tables[0].Rows[i][0].ToString().Trim() + Environment.NewLine;
        }

        Insert_Service_Log(CA_NUMBER, null, _sName, _sCircle, _sDivision, _sCompany, "", "");

        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(str);
        ds.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();

        return ds;
    }

    [WebMethod]
    public DataSet ZBAPI_BILL_DET_API_PDF(string CA_NUMBER, string _sMobileNo)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        string _sCircle = string.Empty, _sDivision = string.Empty, _sCompany = string.Empty, _sName = string.Empty;
        string _sSerReqDateTime = System.DateTime.Now.ToString();
        string _sSerReqOutDateTime = string.Empty;
        string str = "";

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_DISPLAY_BILL_WEB(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "");
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //CA_NUMBER = CA_NUMBER.ToString().Substring(3, 9);
                _sName = Convert.ToString(dt.Rows[0]["FIRSTNAME"]) + " " + Convert.ToString(dt.Rows[0]["MIDDLE_NAME"]) + " " + Convert.ToString(dt.Rows[0]["LASTNAME"]);
                _sCircle = Convert.ToString(dt.Rows[0]["CIRCLE_DESCR"]);
                _sDivision = Convert.ToString(dt.Rows[0]["UNIT_DESCR"]);
                _sCompany = Convert.ToString(dt.Rows[0]["COMPANY_CODE"]);

                // Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, _sMobileNo, "");
                // 

                if (_sCompany.Trim() == "BRPL")
                {
                    str = "http://125.22.84.50:7850/DelhiV2/BillPDF_DTAPI.aspx?CA_NO=" + CA_NUMBER;
                    Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, _sMobileNo, str);
                }
                else
                {
                    str = "Details not available for CA No. " + CA_NUMBER + " .Please call us at 19123 (Toll-Free) or write to us at brpl.customercare@relianceada.com. Thank you.";
                    Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, _sMobileNo, str);
                }

            }
        }
        else
        {
           
            str = "Details not available for CA No. " + CA_NUMBER + " .Please call us at 19123 (Toll-Free) or write to us at brpl.customercare@relianceada.com. Thank you.";
            Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, _sMobileNo, str);
        }

    
        DataSet ds = new DataSet();
        DataTable dtOutPut = new DataTable();
        DataColumn column = new DataColumn();
        dtOutPut.Columns.Add("OUT_PUT", typeof(string));
        dtOutPut.Rows.Add(str);
        ds.Tables.Add(dtOutPut);
        dtOutPut.AcceptChanges();

        return ds;
    }

    [WebMethod]
    public DataSet ZBAPI_BILL_DET_64(string CA_NUMBER)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        string _sCircle = string.Empty, _sDivision = string.Empty, _sCompany = string.Empty, _sName = string.Empty;
        string _sSerReqDateTime = System.DateTime.Now.ToString();
        string _sSerReqOutDateTime = string.Empty;

        DelhiWSV2.WebService obj1 = new DelhiWSV2.WebService();
        dsCAInfo = new DataSet();
        dsCAInfo = obj1.ZBAPI_DISPLAY_BILL_WEB(CA_NUMBER.Length == 9 ? "000" + CA_NUMBER : CA_NUMBER, "");
        if (dsCAInfo.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();
            dt = dsCAInfo.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //CA_NUMBER = CA_NUMBER.ToString().Substring(3, 9);
                _sName = Convert.ToString(dt.Rows[0]["FIRSTNAME"]) + " " + Convert.ToString(dt.Rows[0]["MIDDLE_NAME"]) + " " + Convert.ToString(dt.Rows[0]["LASTNAME"]);
                _sCircle = Convert.ToString(dt.Rows[0]["CIRCLE_DESCR"]);
                _sDivision = Convert.ToString(dt.Rows[0]["UNIT_DESCR"]);
                _sCompany = Convert.ToString(dt.Rows[0]["COMPANY_CODE"]);

                Insert_Service_Log(CA_NUMBER, _sSerReqDateTime, _sName, _sCircle, _sDivision, _sCompany, "", "");
            }
        }

        DataSet Ds = obj.Get_ZBAPI_BILL_DET_64(CA_NUMBER);
        Insert_Service_Log(CA_NUMBER, null, _sName, _sCircle, _sDivision, _sCompany, "", "");

        return Ds;
    }

    [WebMethod]
    public DataSet ZBAPI_STREET_DET_UPD(string COMPANY, string CANUMBER, string DATA_PROCESS_DATE, string STLWATT, string NO_OF_POINT, string INSTALLATION_DATE, string MOVEOUT_DATE, string ACTIVATION, string DEACTIVATION, string REQUESTID, string REQUEST_DATE, string DOCUMENT_UPLOADED)//By Babalu Kumar
    {
        DataSet Ds = obj.Get_ZBAPI_STREET_DET_UPD(COMPANY, CANUMBER, DATA_PROCESS_DATE, STLWATT, NO_OF_POINT, INSTALLATION_DATE, MOVEOUT_DATE, ACTIVATION, DEACTIVATION, REQUESTID, REQUEST_DATE, DOCUMENT_UPLOADED);
        return Ds;
    }

    [WebMethod]
    public DataTable HES_GETLATESTBALANCE(string _sConsumerID, string _sMeterID)
    {
        DataSet dsCAInfo = new DataSet();
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn();
        dt.Columns.Add("ErrorCode", typeof(string));
        dt.Columns.Add("Balance", typeof(string));
        dt.Columns.Add("MeterReading", typeof(string));
        dt.Columns.Add("MeterRTC", typeof(string));
        dt.Columns.Add("LastRechargeAmount", typeof(string));
        dt.Columns.Add("LastRechargeAmountDateTime", typeof(string));
        dt.Columns.Add("MeterID", typeof(string));

        string _sErrorCode = string.Empty, _sBalance = string.Empty, _sMeterReading = string.Empty;
        string _sMeterRTC = string.Empty, _sLastRechargeAmount = string.Empty, _sLastRechargeAmountDateTime = string.Empty, _sMeter_ID = string.Empty;

        HESWebReference.Service1 obj = new HESWebReference.Service1();
        HESWebReference.ConsumerLatestBalance objSer = new HESWebReference.ConsumerLatestBalance();
        objSer = obj.GetLatestBalance(_sConsumerID, _sMeterID);

        if (objSer.ErrorCode != null)
            _sErrorCode = objSer.ErrorCode;
        if (objSer.Balance != null)
            _sBalance = objSer.Balance.ToString();
        if (objSer.MeterReading != null)
            _sMeterReading = objSer.MeterReading.ToString();
        if (objSer.MeterRTC != null)
            _sMeterRTC = objSer.MeterRTC;
        if (objSer.LastRechargeAmount != null)
            _sLastRechargeAmount = objSer.LastRechargeAmount.ToString();
        if (objSer.LastRechargeAmountDateTime != null)
            _sLastRechargeAmountDateTime = objSer.LastRechargeAmountDateTime;
        if (objSer.MeterID != null)
            _sMeter_ID = objSer.MeterID;

        dt.TableName = "Table1";
        dt.Rows.Add(_sErrorCode, _sBalance, _sMeterReading, _sMeterRTC, _sLastRechargeAmount, _sLastRechargeAmountDateTime, _sMeter_ID);


        dt.AcceptChanges();

        return dt;
    }

    [WebMethod]
    public DataSet ZBAPI_ZBI_PREPAID_MTR(string CA_NUMBER)
    {
        DataSet ds = obj.Get_ZBI_PREPAID_MTR(CA_NUMBER);
        return ds;
    }

    [WebMethod]
    public DataTable ZBI_WEBBILL_HIST(string CA_NUMBER)
    {
        DataTable dt = new DataTable();
        DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
        dt = obj.ZBI_WEBBILL_HIST(CA_NUMBER,"").Tables[0];
        return dt;
    }

    [WebMethod]
    public DataTable Z_BAPI_DSS_ISU_CA_DISPLAY(string CA_NUMBER)
    {
        DataTable dt = new DataTable();
        DelhiWSV2.WebService obj = new DelhiWSV2.WebService();
        dt = obj.Z_BAPI_DSS_ISU_CA_DISPLAY(CA_NUMBER, "").Tables[0];
        return dt;
    }

    [WebMethod]
    public DataSet ZBI_BAPI_SOLAR1(string CA_NUMBER, string BILL_MONTH)//Added By Babalu Kumar on 28052021
    {
        DataSet Ds = obj.Get_ZBI_BAPI_SOLAR1(CA_NUMBER, BILL_MONTH);
        return Ds;
    }
}
