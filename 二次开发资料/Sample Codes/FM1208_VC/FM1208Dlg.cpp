// FM1208Dlg.cpp : implementation file
//

#include "stdafx.h"
#include "FM1208.h"
#include "FM1208Dlg.h"

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
// CFM1208Dlg dialog

CFM1208Dlg::CFM1208Dlg(CWnd* pParent /*=NULL*/)
	: CDialog(CFM1208Dlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CFM1208Dlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CFM1208Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CFM1208Dlg)
	DDX_Control(pDX, IDC_EDIT_RESET_INFO, m_edit_reset_inf);
	DDX_Control(pDX, IDC_STATIC_TIPS, m_static_tips);
	DDX_Control(pDX, IDC_EDIT_RESPONSE, m_edit_response);
	DDX_Control(pDX, IDC_EDIT_CSN, m_edit_csn);
	DDX_Control(pDX, IDC_EDIT_COMMAND, m_edit_command);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CFM1208Dlg, CDialog)
	//{{AFX_MSG_MAP(CFM1208Dlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BTN_CONNECT, OnBtnConnect)
	ON_BN_CLICKED(IDC_BTN_RESET, OnBtnReset)
	ON_BN_CLICKED(IDC_BTN_SEND_COMMAND, OnBtnSendCommand)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CFM1208Dlg message handlers

BOOL CFM1208Dlg::OnInitDialog()
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

    m_edit_command.SetWindowText("0084000004"); //COS commands: get 4 bytes random number
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CFM1208Dlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CFM1208Dlg::OnPaint() 
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
HCURSOR CFM1208Dlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

BOOL CFM1208Dlg::LoadDll()
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
        //Auxiliary
        (FARPROC&)Aux_SingleDES              = GetProcAddress(m_hInstMaster,_T("Aux_SingleDES"));
        (FARPROC&)Aux_TripleDES              = GetProcAddress(m_hInstMaster,_T("Aux_TripleDES"));
        (FARPROC&)Aux_SingleMAC              = GetProcAddress(m_hInstMaster,_T("Aux_SingleMAC"));
        (FARPROC&)Aux_TripleMAC              = GetProcAddress(m_hInstMaster,_T("Aux_TripleMAC"));
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
        //ISO14443A-4
        (FARPROC&)TyA_Reset                  = GetProcAddress(m_hInstMaster,_T("TyA_Reset"));
        (FARPROC&)TyA_CosCommand             = GetProcAddress(m_hInstMaster,_T("TyA_CosCommand"));
        (FARPROC&)TyA_Deselect               = GetProcAddress(m_hInstMaster,_T("TyA_Deselect"));
        
        if(
            NULL == Sys_GetDeviceNum           ||
            NULL == Sys_GetHidSerialNumberStr  ||
            NULL == Sys_Open                   ||
            NULL == Sys_IsOpen                 ||
            NULL == Sys_Close                  ||
            NULL == Sys_SetLight               ||
            NULL == Sys_SetAntenna             ||
            NULL == Sys_InitType               ||
            //Auxiliary
            NULL == Aux_SingleDES              ||
            NULL == Aux_TripleDES              ||
            NULL == Aux_SingleMAC              ||
            NULL == Aux_TripleMAC              ||
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
            NULL == TyA_NTAG_ReadSig           ||
            //ISO14443A-4
            NULL == TyA_Reset                  ||
            NULL == TyA_CosCommand             ||
            NULL == TyA_Deselect
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


void CFM1208Dlg::OnBtnConnect() 
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

void CFM1208Dlg::OnBtnReset() 
{
	// TODO: Add your control notification handler code here
    int status;
    BYTE mode = 0x52;
    BYTE arrData[MAX_RF_BUFFER];
    BYTE len;
    
    //Check whether the reader is connected or not
    if(FALSE == Sys_IsOpen(g_hDevice))
    {
        MessageBox(_T("Please connect the device firstly !"), _T("Error"), MB_OK|MB_ICONERROR);
        return ;        
    }
    
    //
    m_edit_csn.SetWindowText("");
    m_edit_reset_inf.SetWindowText("");
    
    //Close antenna of the reader
    status = Sys_SetAntenna(g_hDevice, 0);
    if(status != 0)
    {
        m_static_tips.SetWindowText("Sys_SetAntenna failed !");
        return;
    }
    
    //Open antenna of the reader
    status = Sys_SetAntenna(g_hDevice, 1);
    if(status != 0)
    {
        m_static_tips.SetWindowText("Sys_SetAntenna failed !");
        return;
    }
    
    //Reset
    status = TyA_Reset(g_hDevice, mode, arrData, &len);//search all card
    if(status != 0) 
    {
        m_static_tips.SetWindowText("TyA_Reset failed !");
        return ;
    }
    m_edit_csn.SetWindowTextEx(arrData, 4);
    m_edit_reset_inf.SetWindowTextEx(&arrData[4], len-4);
    
    //Tips
    m_static_tips.SetWindowText("Reset card succeed !");
    Sys_SetBuzzer(g_hDevice, 10);	
}

void CFM1208Dlg::OnBtnSendCommand() 
{
	// TODO: Add your control notification handler code here
    int status;
    BYTE arrCommand[MAX_RF_BUFFER];
    BYTE arrMsg[MAX_RF_BUFFER];
    BYTE cmdLen;
    BYTE msgLen;
    CString str;
    
    //Check whether the reader is connected or not
    if(FALSE == Sys_IsOpen(g_hDevice))
    {
        MessageBox(_T("Please connect the device firstly !"), _T("Error"), MB_OK|MB_ICONERROR);
        return ;        
    }
    
    //Send command
    m_edit_command.GetWindowTextEx(str);			
    memcpy(arrCommand, str.GetBuffer(str.GetLength()), str.GetLength());
    cmdLen = str.GetLength();
    status = TyA_CosCommand(g_hDevice, arrCommand, cmdLen, arrMsg, &msgLen);
    if(status != 0) 
    {
        m_static_tips.SetWindowText("TyA_CosCommand failed !");
        return ;
    }
    
    m_edit_response.SetWindowTextEx(arrMsg, msgLen);
    
    //Tips
    m_static_tips.SetWindowText("The command was executed successfully !");
    Sys_SetBuzzer(g_hDevice, 10);		
}
