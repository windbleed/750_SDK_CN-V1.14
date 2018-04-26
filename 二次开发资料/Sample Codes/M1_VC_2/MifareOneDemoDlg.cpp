// MifareOneDemoDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MifareOneDemo.h"
#include "MifareOneDemoDlg.h"

#include "DllLoad.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

WORD  g_wVID = 0x0416;  //USB device Vendor ID
WORD  g_wPID = 0X8020;  //USB device Product ID
int g_nCurSelDev; //-1 Indicates that no valid device is selected, 0 and above represent a valid device
HID_DEVICE g_hDevice[MAX_DEVICE_NUM];  //Store pointer array of Device class object

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
// CMifareOneDemoDlg dialog

CMifareOneDemoDlg::CMifareOneDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMifareOneDemoDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMifareOneDemoDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMifareOneDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMifareOneDemoDlg)
	DDX_Control(pDX, IDC_STATIC_TIPS, m_static_tips);
	DDX_Control(pDX, IDC_EDIT_SERIAL, m_edit_serial);
	DDX_Control(pDX, IDC_EDIT_KEY, m_edit_key);
	DDX_Control(pDX, IDC_EDIT_DATA, m_edit_data);
	DDX_Control(pDX, IDC_COMBO_DEVICE, m_combo_device);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMifareOneDemoDlg, CDialog)
	//{{AFX_MSG_MAP(CMifareOneDemoDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_CONNECT, OnButtonConnect)
	ON_BN_CLICKED(IDC_BUTTON_SEARCH, OnButtonSearch)
	ON_BN_CLICKED(IDC_BUTTON_READ, OnButtonRead)
	ON_BN_CLICKED(IDC_BUTTON_WRITE, OnButtonWrite)
	ON_BN_CLICKED(IDC_BUTTON_HALT, OnButtonHalt)
	ON_CBN_DROPDOWN(IDC_COMBO_DEVICE, OnDropdownComboDevice)
	ON_CBN_SELCHANGE(IDC_COMBO_DEVICE, OnSelchangeComboDevice)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMifareOneDemoDlg message handlers

BOOL CMifareOneDemoDlg::OnInitDialog()
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

    //Load DLL
    if(!LoadDll())
    {
        return FALSE;
    }

    //Init g_nCurSelDev
    g_nCurSelDev = -1;
    
    //Init g_hDevice[]
    //g_hDevice must be initialized as -1 before use
    for(int i=0; i<MAX_DEVICE_NUM; i++)
    {
        g_hDevice[i] = INVALID_HANDLE_VALUE;
    }
    
    //Initialization Device drop-down list
    InitDropdownComboDevice();
    
    //Initializes the key of edit box
    m_edit_key.SetWindowText(_T("FFFFFFFFFFFF"));
    
    //Select Key A
    ((CButton*)GetDlgItem(IDC_RADIO_KEYA))->SetCheck(TRUE);
    
    //Init Combo data
    CComboBox *pMassCBB = (CComboBox*)GetDlgItem(IDC_COMBO_MASS);
    CString str;
    
    for(i = 0 ;i < 64;i++)
    {		
        str.Format(_T("%d"), i);
        pMassCBB->AddString( str);		
        pMassCBB->SetItemData(i, i);		
    } 
    
    pMassCBB->SetCurSel(1);
    
    //Length limit	
    m_edit_key.SetLimitTextEx(6);
	m_edit_data.SetLimitTextEx(16);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMifareOneDemoDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CMifareOneDemoDlg::OnPaint() 
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
HCURSOR CMifareOneDemoDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

BOOL CMifareOneDemoDlg::DestroyWindow() 
{
    // TODO: Add your specialized code here and/or call the base class

    //Free library
 	if(m_hInstMaster) FreeLibrary(m_hInstMaster);
    
    return CDialog::DestroyWindow();
}


BOOL CMifareOneDemoDlg::LoadDll()
{
    //load masterrd.dll to app
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
        (FARPROC&)TyA_Request                = GetProcAddress(m_hInstMaster,_T("TyA_Request"));
        (FARPROC&)TyA_Anticollision          = GetProcAddress(m_hInstMaster,_T("TyA_Anticollision"));
        (FARPROC&)TyA_Select                 = GetProcAddress(m_hInstMaster,_T("TyA_Select"));	
        (FARPROC&)TyA_Halt                   = GetProcAddress(m_hInstMaster,_T("TyA_Halt"));
        (FARPROC&)TyA_CS_Authentication2     = GetProcAddress(m_hInstMaster,_T("TyA_CS_Authentication2"));
        (FARPROC&)TyA_CS_Read                = GetProcAddress(m_hInstMaster,_T("TyA_CS_Read"));
        (FARPROC&)TyA_CS_Write               = GetProcAddress(m_hInstMaster,_T("TyA_CS_Write"));
        
        if(
            NULL == Sys_GetDeviceNum           ||
            NULL == Sys_GetHidSerialNumberStr  ||
            NULL == Sys_Open                   ||
            NULL == Sys_IsOpen                 ||
            NULL == Sys_Close                  ||
            NULL == Sys_SetLight               ||
            NULL == Sys_SetAntenna             ||
            NULL == Sys_InitType               ||
            NULL == TyA_Request                ||
            NULL == TyA_Anticollision          ||
            NULL == TyA_Select                 ||
            NULL == TyA_Halt                   ||
            NULL == TyA_CS_Authentication2     ||
            NULL == TyA_CS_Read                ||
            NULL == TyA_CS_Write               
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

//Init the device drop-down list,
//and select the first device as the default device if there is device
void CMifareOneDemoDlg::InitDropdownComboDevice()
{
    BYTE byStatus;
    DWORD dwNum;
    char buffer[256];
    
    m_combo_device.ResetContent(); //Clear content
    m_combo_device.AddString("- Select -"); //Clear content
    
    //Get the number of readers connected to the computer 
    byStatus = Sys_GetDeviceNum(g_wVID,g_wPID,&dwNum);
    if(byStatus != SUCCESS)
    {
        m_combo_device.SetCurSel(0);
        return;
    }
    
    //Display device list
    for(DWORD i=0; i<dwNum; i++)
    {
        byStatus = Sys_GetHidSerialNumberStr(i,g_wVID,g_wPID,buffer,256);
        if(byStatus != SUCCESS)
        {
            break;
        }
        m_combo_device.AddString(buffer);
    }
    
    //Select the first device as default device
    int nIndex = m_combo_device.SetCurSel(1);	
    if(nIndex == CB_ERR)
    {
        m_combo_device.SetCurSel(0);
        return;
    }
    
    g_nCurSelDev = 0;
}

void CMifareOneDemoDlg::OnDropdownComboDevice() 
{
    // TODO: Add your control notification handler code here
    BYTE byStatus;
    CString strOldSerial;
    DWORD dwNum;
    char buffer[256];
    
    m_combo_device.GetWindowText(strOldSerial); //Save the contents of the currently selected
    m_combo_device.ResetContent(); //Clear content
    
    //	CString strComboData;
    m_combo_device.AddString("- Select -"); //Add content
    
    //Get the number of readers connected to the computer
    byStatus = Sys_GetDeviceNum(g_wVID,g_wPID,&dwNum);
    if(byStatus != 0)
    {
 		m_static_tips.SetWindowText("Sys_GetDeviceNum failed !");
 		return;
 	}
    
    //Update device list
    for(DWORD i=0; i<dwNum; i++)
    {
        byStatus = Sys_GetHidSerialNumberStr(i,g_wVID,g_wPID,buffer,256);
        if(byStatus != SUCCESS)
        {
            break;
        }
        m_combo_device.AddString(buffer);
    }
    
    //Select the previous device
    int nIndex = m_combo_device.SelectString(-1, strOldSerial); 
    if(CB_ERR == nIndex)
    {
        m_combo_device.SetCurSel(0);	
	}	    
}

void CMifareOneDemoDlg::OnSelchangeComboDevice() 
{
    // TODO: Add your control notification handler code here
    int nIndex = m_combo_device.GetCurSel();
    if(LB_ERR == nIndex)
    {
        m_combo_device.SetCurSel(0);	
        g_nCurSelDev = -1;
    }
    else if(0 == nIndex)
    {
        g_nCurSelDev = -1;
    }
    else
    {
        g_nCurSelDev = nIndex - 1;
	}	    
}


void CMifareOneDemoDlg::OnButtonConnect() 
{
	// TODO: Add your control notification handler code here
    BYTE byStatus;
    
    //Check whether the reader has been selected
    if(g_nCurSelDev == -1)
    {
        MessageBox("Please select a device !",_T("Error"),MB_OK|MB_ICONERROR);
        return;
    }
    
    //Check whether the reader is open or not
    //If the reader is already open , close it firstly
    if(g_hDevice[g_nCurSelDev] != INVALID_HANDLE_VALUE)
    {
        byStatus = Sys_Close(&g_hDevice[g_nCurSelDev]);
        if(byStatus != SUCCESS)
        {
            m_static_tips.SetWindowText("Sys_Close failed !");
            return;
        }
    }
    
    //Open the device
    byStatus = Sys_Open(&g_hDevice[g_nCurSelDev],g_nCurSelDev,g_wVID,g_wPID);
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_Open failed !");
        return;
    }
    
    //Init the reader before operating the card
    //Close antenna of the reader
    byStatus = Sys_SetAntenna(g_hDevice[g_nCurSelDev],0);
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_SetAntenna failed !");
        return;
    }
    Sleep(5); //Appropriate delay after Sys_SetAntenna operating
    
    //Set the reader's working mode
    byStatus = Sys_InitType(g_hDevice[g_nCurSelDev],'A');
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_InitType failed !");
        return;
    }
    Sleep(5); //Appropriate delay after Sys_InitType operating
    
    //Open antenna of the reader
    byStatus = Sys_SetAntenna(g_hDevice[g_nCurSelDev],1);
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_SetAntenna failed !");
        return;
    }
    Sleep(5); //Appropriate delay after Sys_SetAntenna operating
    
    //Success Tips
    //Beep 200ms
    byStatus = Sys_SetBuzzer(g_hDevice[g_nCurSelDev],20);
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_SetBuzzer failed !");
        return;
	}
    
    //Tips
    m_static_tips.SetWindowText("Connect reader succeed !");   
}

void CMifareOneDemoDlg::OnButtonSearch() 
{
	// TODO: Add your control notification handler code here
    unsigned char mode = 0x52;
    int status;
    unsigned short TagType;
    unsigned char bcnt = 0; 
    unsigned char Snr[MAX_RF_BUFFER];
    unsigned char len;
    unsigned char sak;
    
    //Check whether the reader has been selected
    if(g_nCurSelDev == -1)
    {
        MessageBox(_T("Please select a device !"), _T("Error"), MB_OK|MB_ICONERROR);
        return;
    }
    
    //Check whether the reader is open or not
    if(g_hDevice[g_nCurSelDev] == INVALID_HANDLE_VALUE)
    {
        MessageBox(_T("The device is not open !"), _T("Error"), MB_OK|MB_ICONERROR);
        return;
    }

    //Clear content
    m_edit_serial.SetWindowText("");
    
    //Request card
    status = TyA_Request(g_hDevice[g_nCurSelDev], mode, &TagType);
    if(status) 
    {
        m_static_tips.SetWindowText("TyA_Request failed !");
        m_edit_serial.SetWindowText("");
        return ;
    }
    
    //Anticollision ,then you can get to the card serial number
    status = TyA_Anticollision(g_hDevice[g_nCurSelDev], bcnt, Snr, &len);
    if(status || len != 4) 
    { 
        m_static_tips.SetWindowText("TyA_Anticollision failed !");
        return ;
    }
    
    //Select card
    status = TyA_Select(g_hDevice[g_nCurSelDev], Snr, len, &sak);
    if(status) 
    {
        m_static_tips.SetWindowText("TyA_Select failed !");
        return ;
    }
    
	m_edit_serial.SetWindowTextEx(Snr, len);	
    
    //Tips
    m_static_tips.SetWindowText("Select card succeed !"); 
}

void CMifareOneDemoDlg::OnButtonRead() 
{
	// TODO: Add your control notification handler code here
    unsigned char mode = 0x60;
    unsigned char secnr = 0x00;
    int state;
    CString strKey;	
    unsigned char pData[MAX_RF_BUFFER];
    unsigned char cLen;
    
    //Check whether the reader has been selected
    if(g_nCurSelDev == -1)
    {
        MessageBox(_T("Please select a device !"), _T("Error"), MB_OK|MB_ICONERROR);
        return;
    }
    
    //Check whether the reader is open or not
    if(g_hDevice[g_nCurSelDev] == INVALID_HANDLE_VALUE)
    {
        MessageBox(_T("The device is not open !"), _T("Error"), MB_OK|MB_ICONERROR);
        return;
    }
    
    //Get key type
    CButton* pButton = (CButton*)GetDlgItem(IDC_RADIO_KEYA);
    if((pButton->GetCheck()))
    {//Key A
        mode = 0x60;
    }
    else
    {//Key B
        mode = 0x61;
    }
    
    //Get key
    m_edit_key.GetWindowTextEx(strKey);
    if(m_edit_key.GetTextLenEx() != 6)
    {
        MessageBox(_T("Please enter the 6-byte key in the key area !"), _T("Error"), MB_OK|MB_ICONERROR);
        return ;
    }
    
    //Get block address
    CComboBox *pCBB = (CComboBox*)GetDlgItem(IDC_COMBO_MASS);
    int nSel = pCBB->GetCurSel();
    secnr = (unsigned char)(pCBB->GetItemData(nSel));	

    //Clear content
    m_edit_data.SetWindowText("");
    
    //Verify a sector of Mifare One card with a specified key
    state = TyA_CS_Authentication2(g_hDevice[g_nCurSelDev], mode, (secnr/4)*4, (unsigned char*)strKey.GetBuffer(strKey.GetLength()));
    if(state)
    {
        m_static_tips.SetWindowText("TyA_CS_Authentication2 failed !");
        return;	
    }

    //Read a block data of Mifare One card
    state = TyA_CS_Read(g_hDevice[g_nCurSelDev], secnr, pData, &cLen);
    if(state || cLen != 16)
    {
        m_static_tips.SetWindowText("TyA_CS_Read failed !");
        return;	
    }
    
	m_edit_data.SetWindowTextEx(pData, 16);	

    //Tips
    m_static_tips.SetWindowText("Read card succeed !"); 
}

void CMifareOneDemoDlg::OnButtonWrite() 
{
	// TODO: Add your control notification handler code here
    unsigned char mode = 0x60;
    unsigned char secnr = 0x00;
    int state;
    CString strKey,strEdit;	
    unsigned char Data[16];	
    
    //Check whether the reader has been selected
    if(g_nCurSelDev == -1)
    {
        MessageBox(_T("Please select a device !"), _T("Error"), MB_OK|MB_ICONERROR);
        return;
    }
    
    //Check whether the reader is open or not
    if(g_hDevice[g_nCurSelDev] == INVALID_HANDLE_VALUE)
    {
        MessageBox(_T("The device is not open !"), _T("Error"), MB_OK|MB_ICONERROR);
        return;
    }
    
    //Get key type
    CButton* pButton = (CButton*)GetDlgItem(IDC_RADIO_KEYA);
    if((pButton->GetCheck()))
    {//Key A
        mode = 0x60; 		
    }
    else{//Key B
        mode = 0x61; 
    }
    
    //Get key
    m_edit_key.GetWindowTextEx(strKey);
    if(m_edit_key.GetTextLenEx() != 6)
    {
        MessageBox(_T("Please enter the 6-byte key in the key area !"),_T("Error"),MB_OK|MB_ICONERROR);
        return ;
    }
    
    //Get block address
    CComboBox *pCBB = (CComboBox*)GetDlgItem(IDC_COMBO_MASS);
    int nSel = pCBB->GetCurSel();
    secnr = (unsigned char)(pCBB->GetItemData(nSel));	  
    
    //Verify a sector of Mifare One card with a specified key
    state = TyA_CS_Authentication2(g_hDevice[g_nCurSelDev], mode, (secnr/4)*4, (unsigned char*)strKey.GetBuffer(strKey.GetLength()));
    if(state)
    {
        m_static_tips.SetWindowText("TyA_CS_Authentication2 failed !");
        return;	
    }
    
    //Get block data
    m_edit_data.GetWindowTextEx(strEdit);			
    memcpy(Data,strEdit.GetBuffer(strEdit.GetLength()), strEdit.GetLength());

    //Write a block data of Mifare One card
    state = TyA_CS_Write(g_hDevice[g_nCurSelDev], secnr, Data);
    if(state)
    {
        m_static_tips.SetWindowText("TyA_CS_Write failed !");
        return;	
	}	

    //Tips
    m_static_tips.SetWindowText("Write card succeed !"); 
}

void CMifareOneDemoDlg::OnButtonHalt() 
{
	// TODO: Add your control notification handler code here
    int state;
    
    //Check whether the reader has been selected
    if(g_nCurSelDev == -1)
    {
        MessageBox(_T("Please select a device !"), _T("Error"), MB_OK|MB_ICONERROR);
        return;
    }
    
    //Check whether the reader is open or not
    if(g_hDevice[g_nCurSelDev] == INVALID_HANDLE_VALUE)
    {
        MessageBox(_T("The device is not open !"), _T("Error"), MB_OK|MB_ICONERROR);
        return;
    }
    
    //Halt card
    state = TyA_Halt(g_hDevice[g_nCurSelDev]);
    if(state)
    {
        m_static_tips.SetWindowText("TyA_Halt failed !");
        return;
	}	

    //Tips
    m_static_tips.SetWindowText("Halt card succeed !"); 
}
