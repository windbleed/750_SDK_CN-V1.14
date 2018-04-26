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

HID_DEVICE g_hDevice = HID_DEVICE(-1);  //g_hDevice must be initialized as -1 before use

/////////////////////////////////////////////////////////////////////////////
// CMifareOneDemoDlg dialog

CMifareOneDemoDlg::CMifareOneDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMifareOneDemoDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMifareOneDemoDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hInstMaster = NULL;
}

void CMifareOneDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMifareOneDemoDlg)
	DDX_Control(pDX, IDC_STATIC_TIPS, m_static_tips);
	DDX_Control(pDX, IDC_EDIT_DATA, m_edit_data);
	DDX_Control(pDX, IDC_EDIT_KEY, m_edit_key);
	DDX_Control(pDX, IDC_EDIT_SERIAL, m_edit_serial);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMifareOneDemoDlg, CDialog)
	//{{AFX_MSG_MAP(CMifareOneDemoDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BUTTON_SEARCH, OnButtonSearch)
	ON_BN_CLICKED(IDC_BUTTON_READ, OnButtonRead)
	ON_BN_CLICKED(IDC_BUTTON_WRITE, OnButtonWrite)
	ON_BN_CLICKED(IDC_BUTTON_HALT, OnButtonHalt)
	ON_BN_CLICKED(IDC_BUTTON_CONNECT, OnButtonConnect)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMifareOneDemoDlg message handlers

BOOL CMifareOneDemoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here

    //================== Load DLL ====================
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
        }
    }
    else
    {
        AfxMessageBox(_T("Load hfrdapi.dll failed !"));       
    }
    
    //================== Choose Key A =====================
    ((CButton*)GetDlgItem(IDC_RADIO_KEYA))->SetCheck(TRUE);
    
    
    //================ Init block address =================
    CComboBox *pMassCBB = (CComboBox*)GetDlgItem(IDC_COMBO_MASS);
    CString str;
    
    for(int i = 0; i < 64; i++)
    {		
        str.Format(_T("%d"), i);
        pMassCBB->AddString( str);		
        pMassCBB->SetItemData(i, i);		
    } 
    
    pMassCBB->SetCurSel(1);

    //=================== Init key value ==================
    m_edit_key.SetWindowText("FFFFFFFFFFFF");

	//=============== set edit control size ===============
	m_edit_key.SetLimitTextEx(6);
	m_edit_data.SetLimitTextEx(16);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
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

void CMifareOneDemoDlg::OnDestroy() 
{
	CDialog::OnDestroy();
	
 	//Release library
    if(m_hInstMaster) FreeLibrary(m_hInstMaster);	
}


void CMifareOneDemoDlg::OnButtonConnect() 
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


//************* search card **************//
void CMifareOneDemoDlg::OnButtonSearch() 
{
    int status;
	BYTE mode = 0x52;
	WORD TagType;
	BYTE bcnt = 0; 
	BYTE Snr[MAX_RF_BUFFER];
	BYTE len;
	BYTE sak;

    //Check whether the reader is connected or not
    if(FALSE == Sys_IsOpen(g_hDevice))
    {
        MessageBox(_T("Please connect the device firstly !"), _T("Error"), MB_OK|MB_ICONERROR);
		return ;        
    }

    //
    m_edit_serial.SetWindowText("");
	
    //Request card
	status = TyA_Request(g_hDevice, mode, &TagType);//search all card
	if(status) 
    {
		m_static_tips.SetWindowText("TyA_Request failed !");
		return ;
	}
	
    //Anticollision
	status = TyA_Anticollision(g_hDevice, bcnt, Snr, &len);//return serial number of card
	if(status || len != 4) 
    { 
		m_static_tips.SetWindowText("TyA_Anticollision failed !");
		return ;
	}
	
    //Select card
	status = TyA_Select(g_hDevice, Snr, len, &sak);
	if(status) 
    {
		m_static_tips.SetWindowText("TyA_Select failed !");
		return ;
	}
	
	m_edit_serial.SetWindowTextEx(Snr,len);	

    //Tips
    m_static_tips.SetWindowText("Select card succeed !");
}


//************* read *************// 
void CMifareOneDemoDlg::OnButtonRead() 
{
    int status;
	BYTE mode = 0x60;
	BYTE secnr = 0x00;
	CString strKey;	
	BYTE pData[MAX_RF_BUFFER];
	BYTE cLen;

    //Get key type
	CButton* pButton = (CButton*)GetDlgItem(IDC_RADIO_KEYA);
	if((pButton->GetCheck()))
    {//key A
		mode = 0x60; 
	}
	else
    {//key B
		mode = 0x61; 
	}

    //Get key
	m_edit_key.GetWindowTextEx(strKey);
	if(m_edit_key.GetTextLenEx() != 6)
    {
		MessageBox(_T("Please input 6 bytes in key area !"), _T("Error"), MB_OK|MB_ICONERROR);
		return ;
	}

	//Get block address
	CComboBox *pCBB = (CComboBox*)GetDlgItem(IDC_COMBO_MASS);
	int nSel = pCBB->GetCurSel();
	secnr = (unsigned char)(pCBB->GetItemData(nSel));

    //Check whether the reader is connected or not
    if(FALSE == Sys_IsOpen(g_hDevice))
    {
        MessageBox(_T("Please connect the device firstly !"), _T("Error"), MB_OK|MB_ICONERROR);
        return ;        
    }
    
    //
    m_edit_data.SetWindowText("");

	//Authentication
	status = TyA_CS_Authentication2(g_hDevice, mode, (secnr/4)*4, (unsigned char*)strKey.GetBuffer(strKey.GetLength()));
	if(status)
    {
        m_static_tips.SetWindowText("TyA_CS_Authentication2 failed !");
		return ;
	}
	
    //Read card
	status = TyA_CS_Read(g_hDevice, secnr, pData, &cLen);
	if(status || cLen != 16)
    {
        m_static_tips.SetWindowText("TyA_CS_Read failed !");
		return ;
	}

	m_edit_data.SetWindowTextEx(pData, 16);

    //Tips
    m_static_tips.SetWindowText("Read card succeed !");
}


//************* write *************//
void CMifareOneDemoDlg::OnButtonWrite() 
{
    int status;
	BYTE mode = 0x60;
	BYTE secnr = 0x00;
	CString strKey,strEdit;	
	BYTE Data[16];	

    //Get key type
	CButton* pButton = (CButton*)GetDlgItem(IDC_RADIO_KEYA);
	if((pButton->GetCheck()))
    {//KEY A
		mode = 0x60; 		
	}
	else
    {//key B
		mode = 0x61; 
	}

    //Get key
	m_edit_key.GetWindowTextEx(strKey);
	if(m_edit_key.GetTextLenEx() != 6)
    {
		MessageBox(_T("Please input 6 bytes in key area !"), _T("Error"), MB_OK|MB_ICONERROR);
		return ;
	}

	//Get block address
	CComboBox *pCBB = (CComboBox*)GetDlgItem(IDC_COMBO_MASS);
	int nSel = pCBB->GetCurSel();
	secnr = (unsigned char)(pCBB->GetItemData(nSel));	

    //Check whether the reader is connected or not
    if(FALSE == Sys_IsOpen(g_hDevice))
    {
        MessageBox(_T("Please connect the device firstly !"), _T("Error"), MB_OK|MB_ICONERROR);
        return ;        
    }
	
	//Authentication	
	status = TyA_CS_Authentication2(g_hDevice, mode, (secnr/4)*4, (unsigned char*)strKey.GetBuffer(strKey.GetLength()));
	if(status)
    {
		m_static_tips.SetWindowText("TyA_CS_Authentication2 failed !");
		return;
	}

	//Write card
	m_edit_data.GetWindowTextEx(strEdit);			
	memcpy(Data, strEdit.GetBuffer(strEdit.GetLength()), strEdit.GetLength());
	status = TyA_CS_Write(g_hDevice, secnr, Data);
	if(status)
    {
		m_static_tips.SetWindowText("TyA_CS_Write failed !");
		return;	
	}

    //Tips
    m_static_tips.SetWindowText("Write card succeed !");

}


//************* halt *************//
void CMifareOneDemoDlg::OnButtonHalt() 
{
	int status;

    //Check whether the reader is connected or not
    if(FALSE == Sys_IsOpen(g_hDevice))
    {
        MessageBox(_T("Please connect the device firstly !"), _T("Error"), MB_OK|MB_ICONERROR);
        return ;        
    }

    //Halt card
	status = TyA_Halt(g_hDevice);
	if(status)
    {
		m_static_tips.SetWindowText("TyA_Halt failed !");
	}
    
    //Tips
    m_static_tips.SetWindowText("Halt card succeed !");
}

