// NFC URIDlg.cpp : implementation file
//

#include "stdafx.h"
#include "NFC URI.h"
#include "NFC URIDlg.h"

#include "hfrdapi.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

HID_DEVICE g_hDevice = HID_DEVICE(-1);  //g_hDevice must be initialized as -1 before use

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CNFCURIDlg dialog

CNFCURIDlg::CNFCURIDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CNFCURIDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CNFCURIDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CNFCURIDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CNFCURIDlg)
	DDX_Control(pDX, IDC_STATIC_TIPS, m_static_tips);
	DDX_Control(pDX, IDC_EDIT_URI_FIELD, m_edit_uri_field);
	DDX_Control(pDX, IDC_COMBO_IDENTIFIER, m_combo_identifier);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CNFCURIDlg, CDialog)
	//{{AFX_MSG_MAP(CNFCURIDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BTN_CONNECT, OnBtnConnect)
	ON_BN_CLICKED(IDC_BTN_WRITE, OnBtnWrite)
	ON_WM_DESTROY()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CNFCURIDlg message handlers

BOOL CNFCURIDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	if(!LoadDll())
    {
        return FALSE;
    }

	m_combo_identifier.SetCurSel(1);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CNFCURIDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CNFCURIDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CNFCURIDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

BOOL CNFCURIDlg::LoadDll()
{
    TCHAR szBuf[MAX_PATH];	
    GetModuleFileName(NULL, (LPTSTR)szBuf, MAX_PATH);
    *strrchr( szBuf, '\\' ) = 0;    
    strcat(szBuf, _T("\\hfrdapi.dll"));
    
    m_hInstMaster = LoadLibrary(szBuf);	 
    
    if(m_hInstMaster)
    {
        (FARPROC&)Sys_GetDeviceNum           = GetProcAddress(m_hInstMaster,_T("Sys_GetDeviceNum"));
        (FARPROC&)Sys_GetHidSerialNumberStr  = GetProcAddress(m_hInstMaster,_T("Sys_GetHidSerialNumberStr"));
        (FARPROC&)Sys_Open                   = GetProcAddress(m_hInstMaster,_T("Sys_Open"));
        (FARPROC&)Sys_IsOpen                 = GetProcAddress(m_hInstMaster,_T("Sys_IsOpen"));
        (FARPROC&)Sys_Close                  = GetProcAddress(m_hInstMaster,_T("Sys_Close"));
        (FARPROC&)Sys_SetLight               = GetProcAddress(m_hInstMaster,_T("Sys_SetLight"));
        (FARPROC&)Sys_SetBuzzer              = GetProcAddress(m_hInstMaster,_T("Sys_SetBuzzer"));
        (FARPROC&)Sys_SetAntenna             = GetProcAddress(m_hInstMaster,_T("Sys_SetAntenna"));
        (FARPROC&)Sys_InitType               = GetProcAddress(m_hInstMaster,_T("Sys_InitType"));
        (FARPROC&)Sys_Transceive             = GetProcAddress(m_hInstMaster,_T("Sys_Transceive"));
		//M1
        (FARPROC&)TyA_Request                = GetProcAddress(m_hInstMaster,_T("TyA_Request"));
        (FARPROC&)TyA_Anticollision          = GetProcAddress(m_hInstMaster,_T("TyA_Anticollision"));
        (FARPROC&)TyA_Select                 = GetProcAddress(m_hInstMaster,_T("TyA_Select"));	
        (FARPROC&)TyA_Halt                   = GetProcAddress(m_hInstMaster,_T("TyA_Halt"));
        (FARPROC&)TyA_CS_Authentication2     = GetProcAddress(m_hInstMaster,_T("TyA_CS_Authentication2"));
        (FARPROC&)TyA_CS_Read                = GetProcAddress(m_hInstMaster,_T("TyA_CS_Read"));
        (FARPROC&)TyA_CS_Write               = GetProcAddress(m_hInstMaster,_T("TyA_CS_Write"));
		//NTAG
		(FARPROC&)TyA_NTAG_AnticollSelect    = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_AnticollSelect"));
		(FARPROC&)TyA_NTAG_GetVersion        = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_GetVersion"));
		(FARPROC&)TyA_NTAG_Read              = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_Read"));
		(FARPROC&)TyA_NTAG_FastRead          = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_FastRead"));
		(FARPROC&)TyA_NTAG_Write             = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_Write"));
		(FARPROC&)TyA_NTAG_CompWrite         = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_CompWrite"));
		(FARPROC&)TyA_NTAG_ReadCnt           = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_ReadCnt"));
		(FARPROC&)TyA_NTAG_PwdAuth           = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_PwdAuth"));
		(FARPROC&)TyA_NTAG_ReadSig           = GetProcAddress(m_hInstMaster,_T("TyA_NTAG_ReadSig"));
        
        if(
            NULL == Sys_GetDeviceNum           ||
            NULL == Sys_GetHidSerialNumberStr  ||
            NULL == Sys_Open                   ||
            NULL == Sys_IsOpen                 ||
            NULL == Sys_Close                  ||
            NULL == Sys_SetLight               ||
            NULL == Sys_SetAntenna             ||
            NULL == Sys_InitType               ||
            NULL == Sys_Transceive             ||
			//M1
            NULL == TyA_Request                ||
            NULL == TyA_Anticollision          ||
            NULL == TyA_Select                 ||
            NULL == TyA_Halt                   ||
            NULL == TyA_CS_Authentication2     ||
            NULL == TyA_CS_Read                ||
            NULL == TyA_CS_Write               ||
			//NTAG
            NULL == TyA_NTAG_AnticollSelect    ||
			NULL == TyA_NTAG_GetVersion        ||
            NULL == TyA_NTAG_Read              ||
			NULL == TyA_NTAG_FastRead          ||
			NULL == TyA_NTAG_Write             ||
			NULL == TyA_NTAG_CompWrite         ||
			NULL == TyA_NTAG_ReadCnt           ||
			NULL == TyA_NTAG_PwdAuth           ||
			NULL == TyA_NTAG_ReadSig
            )
        {			
            AfxMessageBox(_T("Load hfrdapi.dll failed !"));
            return FALSE;
        }
    }
    else
    {
        AfxMessageBox(_T("Load hfrdapi.dll failed !"));
        return FALSE;
    }
    
    return TRUE;
}

void CNFCURIDlg::OnBtnConnect() 
{
	// TODO: Add your control notification handler code here
    int status;
	
    //=================== Connect the reader ===================
    //Check whether the reader is connected or not
    //If the reader is already open , close it firstly
    if(FALSE != Sys_IsOpen(g_hDevice))
    {
        status = Sys_Close(&g_hDevice);
        if(status != 0)
        {
            m_static_tips.SetWindowText("Sys_Close failed !");
        }
    }
	
    //Connect
    status = Sys_Open(&g_hDevice, 0, 0x0416, 0x8020);
    if(status != 0)
    {
        m_static_tips.SetWindowText("Sys_Open failed !");
        return;
    }
	
	
    //========== Init the reader before operating the card ==========
    //Close antenna of the reader
    status = Sys_SetAntenna(g_hDevice, 0);
    if(status != 0)
    {
        m_static_tips.SetWindowText("Sys_SetAntenna failed !");
        return;
    }
    Sleep(5); //Appropriate delay after Sys_SetAntenna operating
    
    //Set the reader's working mode
    status = Sys_InitType(g_hDevice, 'A');
    if(status != 0)
    {
        m_static_tips.SetWindowText("Sys_InitType failed !");
        return;
    }
    Sleep(5); //Appropriate delay after Sys_InitType operating
    
    //Open antenna of the reader
    status = Sys_SetAntenna(g_hDevice, 1);
    if(status != 0)
    {
        m_static_tips.SetWindowText("Sys_SetAntenna failed !");
        return;
    }
    Sleep(5); //Appropriate delay after Sys_SetAntenna operating
    
    //==================== Success Tips ====================
    //Beep 200 ms
    status = Sys_SetBuzzer(g_hDevice, 20);
    if(status != 0)
    {
        m_static_tips.SetWindowText("Sys_SetBuzzer failed !");
        return;
    }
    
    //Tips
    m_static_tips.SetWindowText("Connect reader succeed !");	
}

void CNFCURIDlg::OnBtnWrite() 
{
	// TODO: Add your control notification handler code here
    int status;
	int i;
    BYTE pageAddr;
    BYTE reqMode = 0x52; //request all card
	WORD tagType;
    CString str;
    BYTE length;
    BYTE arryUID[7];
    BYTE UriIdentifierCode;
    int uriFieldLen;
    int len1;
    int len2;
	BYTE bytesBuffer1[200];
	BYTE bytesBuffer2[200];

    //Check whether the reader is connected or not
    if(FALSE == Sys_IsOpen(g_hDevice))
    {
        m_static_tips.SetWindowText("Please connect the device firstly !");
        return ;        
    }

    //========================= Request card ===========================
    status = TyA_Request(g_hDevice, reqMode, &tagType);
    if(status != 0)
    {
        m_static_tips.SetWindowText("TyA_Request faild !");
        return;
    }
    
    //================++========= Get UID ==++==========================
    status = TyA_NTAG_AnticollSelect(g_hDevice, arryUID, &length);
    if(status != 0)
    {
        m_static_tips.SetWindowText("TyA_NTAG_AnticollSelect faild !");
        return;
    }

    //=========================== URI data ===========================
    UriIdentifierCode = m_combo_identifier.GetCurSel(); //Get URI identifier code
    uriFieldLen = m_edit_uri_field.GetWindowTextLength(); //Get URI field length
    //
    len1 = 0;
    bytesBuffer1[len1++] = 0xD1; //SR = 1, TNF = 0x01 (NFC Forum Well Known Type), ME=1, MB=1
	bytesBuffer1[len1++] = 0x01; //Length of the Record Type (1 byte)
    bytesBuffer1[len1++] = 1 + uriFieldLen; //Length of the payload(identifier code length + URI field length)
    bytesBuffer1[len1++] = 0x55; //The URI record type (¡°U¡±)
    bytesBuffer1[len1++] = UriIdentifierCode;
    
    if((len1+uriFieldLen)>144) //144 is the user data area length of NTAG203
    {
        m_static_tips.SetWindowText("The data length is out of range !");
    }

    m_edit_uri_field.GetWindowText(str);
    memcpy(&bytesBuffer1[len1], str.GetBuffer(uriFieldLen), uriFieldLen);
	len1 = len1 + uriFieldLen;

    //=========================== TLV data ===========================
    len2 = 0;
    bytesBuffer2[len2++] = 0x01; //The factory default data of NTAG203
    bytesBuffer2[len2++] = 0x03; //The factory default data of NTAG203
    bytesBuffer2[len2++] = 0xA0; //The factory default data of NTAG203
    bytesBuffer2[len2++] = 0x10; //The factory default data of NTAG203
    bytesBuffer2[len2++] = 0x44; //The factory default data of NTAG203
    bytesBuffer2[len2++] = 0x03;
    bytesBuffer2[len2++] = len1;

    if((len2+len1+1)>144) //+1: 0xFE
    {
        m_static_tips.SetWindowText("The data length is out of range !");
    }

    memcpy(&bytesBuffer2[len2], &bytesBuffer1[0], len1);
    len2 = len2 + len1;

    bytesBuffer2[len2++] = 0xFE;

    //=========================== Write data ===========================
    for(i=0; i<len2; i=i+4)
    {
        pageAddr = 4 + i/4; //begin to write from Page 4
        status = TyA_NTAG_Write(g_hDevice, pageAddr, &bytesBuffer2[i]);
        if(status != 0)
        {
            str.Format("Write faild: Page %d !", pageAddr);
            m_static_tips.SetWindowText(str);
            return;
		}
    }

    //========================== Success Tips ==========================
    m_static_tips.SetWindowText("Write URI success !");
    Sys_SetBuzzer(g_hDevice, 10); //Beep 100ms
	Sleep(110);
}

void CNFCURIDlg::OnDestroy() 
{
	CDialog::OnDestroy();
	
	// TODO: Add your message handler code here
	if(m_hInstMaster)
    {
        FreeLibrary(m_hInstMaster);
        m_hInstMaster = NULL;
    }
}
