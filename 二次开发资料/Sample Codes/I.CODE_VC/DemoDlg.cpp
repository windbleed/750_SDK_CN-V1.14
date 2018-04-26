// DemoDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Demo.h"
#include "DemoDlg.h"
#include "DllLoad.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

#define MAX_DEVICE_NUM 10

WORD  g_wVID = 0x0416;  //USB device Vendor ID
WORD  g_wPID = 0X8020;  //USB device Product ID
int g_nCurSelDev;  //-1 represents device is not selected£¬0 and above represent a valid device
HID_DEVICE g_hDevice[MAX_DEVICE_NUM];  //Array of Hid device class
bool m_bConnectDevice;  //Device connection falg, TRUE represents device is open, FALSE represents device is not open

/***************************************************************************/

/////////////////////////////////////////////////////////////////////////////
// CDemoDlg dialog

CDemoDlg::CDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CDemoDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDemoDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDemoDlg)
	DDX_Control(pDX, IDC_STATIC_TIPS, m_static_tips);
	DDX_Control(pDX, IDC_EDIT_STATUS, m_edit_status);
	DDX_Control(pDX, IDC_EDIT_DATA, m_edit_data);
	DDX_Control(pDX, IDC_EDIT_UID1, m_edit_uid1);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CDemoDlg, CDialog)
	//{{AFX_MSG_MAP(CDemoDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_READCARD, OnButtonReadcard)
	ON_BN_CLICKED(IDC_BUTTON_WRITECARD, OnButtonWritecard)
	ON_BN_CLICKED(IDC_BUTTON_LOCKCARD, OnButtonLockcard)
	ON_BN_CLICKED(IDC_BUTTON_INVENTORY, OnButtonInventory)
	ON_BN_CLICKED(IDC_BUTTON_CONNECT, OnButtonConnect)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDemoDlg message handlers

BOOL CDemoDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
    int i;
    CString str;

    //Init g_nCurSelDev as -1
	g_nCurSelDev = -1;

    //Init m_bConnectDevice as FALSE
    m_bConnectDevice = FALSE;

    //Init array of Hid device class
    for(i=0; i<MAX_DEVICE_NUM; i++)
    {
        g_hDevice[i] = INVALID_HANDLE_VALUE;
	}

	//
	CComboBox *pBlockCBB = (CComboBox*)GetDlgItem(IDC_COMBO_BLOCK);
	for(i = 0 ; i < 28; i++)
    {		
		str.Format(_T("%d"), i);
		pBlockCBB->AddString( str);		
		pBlockCBB->SetItemData(i, i);		
	} 
	pBlockCBB->SetCurSel(0);

	//
	m_edit_uid1.SetLimitTextEx(8);
	m_edit_uid1.SetOnlyRead(TRUE);

	m_edit_data.SetLimitTextEx(4);
	m_edit_status.SetLimitTextEx(1);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CDemoDlg::OnPaint() 
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
HCURSOR CDemoDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CDemoDlg::OnButtonConnect() 
{
    // TODO: Add your control notification handler code here
    BYTE byStatus; 
    
    //Set current select device index as 0
    g_nCurSelDev = 0; 
    
    //=========================== Connect reader =========================
    //Check if the reader is already open
    //If the reader has already open, then close it first 
    if(g_hDevice[g_nCurSelDev] != INVALID_HANDLE_VALUE)
    {
        byStatus = Sys_Close(&g_hDevice[g_nCurSelDev]);
        if(byStatus != SUCCESS)
        {
            m_static_tips.SetWindowText("Sys_Close failed !");
            return;
        }
	}

    //Open the reader
    byStatus = Sys_Open(&g_hDevice[g_nCurSelDev], g_nCurSelDev, g_wVID, g_wPID);
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_Open failed !");
        return;
	}
    m_bConnectDevice = TRUE;

    //============= Init the reader before operating the card ============
    //Close antenna
    byStatus = Sys_SetAntenna(g_hDevice[g_nCurSelDev],0);
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_SetAntenna failed !");
        return;
    }
    Sleep(5); //Appropriate delay after Sys_SetAntenna operating
    
    //Set the reader's working mode
    byStatus = Sys_InitType(g_hDevice[g_nCurSelDev],'1');
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_InitType failed !");
        return;
    }
    Sleep(5); //Appropriate delay after Sys_InitType operating
    
    //Open antenna
    byStatus = Sys_SetAntenna(g_hDevice[g_nCurSelDev],1);
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_SetAntenna failed !");
        return;
	}
    Sleep(5); //Appropriate delay after Sys_SetAntenna operating
    
    //============================ Success Tips ==========================
    //Beep 200ms
    byStatus = Sys_SetBuzzer(g_hDevice[g_nCurSelDev], 20);
    if(byStatus != SUCCESS)
    {
        m_static_tips.SetWindowText("Sys_SetBuzzer failed !");
        return;
	}

    m_static_tips.SetWindowText("Connect reader succeed !");
}


void CDemoDlg::OnButtonInventory() 
{
	int status;		
	unsigned char pData[MAX_RF_BUFFER];
	unsigned char len;
	unsigned char type = 0x31;	//"1"

    //Check whether the reader is connected or not
	if(!m_bConnectDevice)
    {	 
        m_static_tips.SetWindowText("Please connect the device firstly !");
		return;
	}

	m_edit_uid1.SetWindowText("");
	
    //Inventory
	 status = I15693_Inventory(g_hDevice[g_nCurSelDev], pData, &len);
	 if(status)
     {		 
        m_static_tips.SetWindowText("I15693_Inventory failed !");
		return;
	 }     
 
	  m_edit_uid1.SetWindowTextEx(&pData[1], 8);

     //Success Tips
     m_static_tips.SetWindowText("Inventory card succeed !");
}

void CDemoDlg::OnButtonReadcard() 
{
	// TODO: Add your control notification handler code here
	int status;	
	unsigned char  model = 0x02;
	unsigned char  UID[8];
	unsigned char  block = 0;
    unsigned char  number;	
	unsigned char  pData[MAX_RF_BUFFER];	
	unsigned char  len;
	
	CString strUIDEdit;

    //Check whether the reader is connected or not
	if(!m_bConnectDevice)
    {		
        m_static_tips.SetWindowText("Please connect the device firstly !");
		return;
	}

    memset(pData, 0, MAX_RF_BUFFER);

    //Get UID
    m_edit_uid1.GetWindowTextEx(strUIDEdit);
	if(strUIDEdit.GetLength ()  != 8)
    {
		MessageBox("UID number is less than 8 bytes !", "Error", MB_OK|MB_ICONERROR);
		return;
	}
	memcpy(UID, strUIDEdit.GetBuffer(strUIDEdit.GetLength()), strUIDEdit.GetLength());

	//Get block address	
	block = 0;        
	CComboBox *pBlockCBB = (CComboBox*)GetDlgItem(IDC_COMBO_BLOCK);
	block = (unsigned char)pBlockCBB->GetItemData(pBlockCBB->GetCurSel());	

    //Clear old data
    m_edit_data.SetWindowText("");
    m_edit_status.SetWindowText("");
	
    //Read card
	len = 0 ;	
	number = 0x01;
	status = I15693_Read(g_hDevice[g_nCurSelDev], model, UID, block, number, pData, &len);
	if(status || len < 4)
    {
        m_static_tips.SetWindowText("I15693_Read failed !");
		return;
	}
	m_edit_data.SetWindowTextEx(pData, len);
	
	//Get block security	
	number = 0x01;
	status = I15693_GetBlockSecurity(g_hDevice[g_nCurSelDev], model, UID, block, number, pData, &len);
	if(status || len != 0x01)
    {
        m_static_tips.SetWindowText("I15693_GetBlockSecurity failed !");
		return;
	}
	m_edit_status.SetWindowTextEx(pData, len);

    //Success Tips
    m_static_tips.SetWindowText("Read card succeed !");
}

void CDemoDlg::OnButtonWritecard() 
{
	// TODO: Add your control notification handler code here
	int status;	
	unsigned char model = 0x02;
	unsigned char UID[8];	  
	CString strUIDEdit;
	unsigned char pData[MAX_RF_BUFFER];
	unsigned char block = 0;

    //Check whether the reader is connected or not
	if(!m_bConnectDevice)
    {		
        m_static_tips.SetWindowText("Please connect the device firstly !");
		return;
	}
	
    //Get UID
	m_edit_uid1.GetWindowTextEx(strUIDEdit);
	if(strUIDEdit.GetLength() != 8) 
    {
		MessageBox("UID number is less than 8 bytes !", "Error", MB_OK|MB_ICONERROR);
		return;
	}
	memcpy(UID, strUIDEdit.GetBuffer(strUIDEdit.GetLength()), strUIDEdit.GetLength());

    //Get block address
	block = 0;        
	CComboBox *pBlockCBB = (CComboBox*)GetDlgItem(IDC_COMBO_BLOCK);
	block = (unsigned char)pBlockCBB->GetItemData(pBlockCBB->GetCurSel());	
	
    //Get data
	m_edit_data.GetWindowTextEx(strUIDEdit);	
	if(strUIDEdit.GetLength() != 4)
	{
		MessageBox("Data is less than 4 bytes !", "Error", MB_OK|MB_ICONERROR);
		return;
	}
	memcpy(pData, strUIDEdit.GetBuffer(strUIDEdit.GetLength()), strUIDEdit.GetLength());
	pData[strUIDEdit.GetLength()] = '\0';

    //Write card
	status = I15693_Write(g_hDevice[g_nCurSelDev], model, UID, block, (unsigned char*)pData);		
	if(status)
    {
        m_static_tips.SetWindowText("I15693_Write failed !");
        return;
	}

    //Success Tips
    m_static_tips.SetWindowText("Write card succeed !");
}

void CDemoDlg::OnButtonLockcard() 
{
	// TODO: Add your control notification handler code here
	int status;	
	unsigned char  model = 0x02;
	unsigned char  UID[8];
	unsigned char  block = 0;
	CString strUIDEdit;

    //Check whether the reader is connected or not
	if(!m_bConnectDevice)
    {		
        m_static_tips.SetWindowText("Please connect the device firstly !");
		return;
	}

    //Get UID
	m_edit_uid1.GetWindowTextEx(strUIDEdit);
	if(strUIDEdit.GetLength() != 8) 
    {
	    MessageBox("UID number is less than 8 bytes", "Error", MB_OK|MB_ICONERROR);
	    return;
	}
	memcpy(UID, strUIDEdit.GetBuffer(strUIDEdit.GetLength()), strUIDEdit.GetLength());

    //Get block address
	CComboBox *pBlockCBB = (CComboBox*)GetDlgItem(IDC_COMBO_BLOCK);
	block = (unsigned char)pBlockCBB->GetItemData(pBlockCBB->GetCurSel());

    //Confirm operation tips
    if(MessageBox(_T("Are you sure to perform the lock operation ? After the operation, the content can not be changed anymore !"), _T("Warning"), MB_OKCANCEL|MB_ICONEXCLAMATION ) == IDCANCEL)
		return;

    //Lock block
	status = I15693_LockBlock(g_hDevice[g_nCurSelDev], model, UID, block);
	if(status)
    {
		m_static_tips.SetWindowText("I15693_LockBlock failed !");
		return;
	}

    //Success Tips
	 m_static_tips.SetWindowText("Lock block succeed !");
}
