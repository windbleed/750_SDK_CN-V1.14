// MifareOneDemoDlg.h : header file
//

#if !defined(AFX_MIFAREONEDEMODLG_H__3A95BC9C_B6BA_4AB5_A60A_8913F9DA4CC0__INCLUDED_)
#define AFX_MIFAREONEDEMODLG_H__3A95BC9C_B6BA_4AB5_A60A_8913F9DA4CC0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "HexEdit.h"

#define MAX_DEVICE_NUM 10 //定义最大设备数

/////////////////////////////////////////////////////////////////////////////
// CMifareOneDemoDlg dialog

class CMifareOneDemoDlg : public CDialog
{
// Construction
public:
	CMifareOneDemoDlg(CWnd* pParent = NULL);	// standard constructor

    HINSTANCE m_hInstMaster;
    void InitDropdownComboDevice();
	BOOL LoadDll();

// Dialog Data
	//{{AFX_DATA(CMifareOneDemoDlg)
	enum { IDD = IDD_MIFAREONEDEMO_DIALOG };
	CStatic	m_static_tips;
	CHexEdit	m_edit_serial;
	CHexEdit	m_edit_key;
	CHexEdit	m_edit_data;
	CComboBox	m_combo_device;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMifareOneDemoDlg)
	public:
	virtual BOOL DestroyWindow();
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CMifareOneDemoDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButtonConnect();
	afx_msg void OnButtonSearch();
	afx_msg void OnButtonRead();
	afx_msg void OnButtonWrite();
	afx_msg void OnButtonHalt();
	afx_msg void OnDropdownComboDevice();
	afx_msg void OnSelchangeComboDevice();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MIFAREONEDEMODLG_H__3A95BC9C_B6BA_4AB5_A60A_8913F9DA4CC0__INCLUDED_)
