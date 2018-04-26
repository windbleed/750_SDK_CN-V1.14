// FM1208Dlg.h : header file
//

#if !defined(AFX_FM1208DLG_H__E8145134_6171_4951_88A2_5B350077A8CF__INCLUDED_)
#define AFX_FM1208DLG_H__E8145134_6171_4951_88A2_5B350077A8CF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CFM1208Dlg dialog

#include "HexEdit.h"

class CFM1208Dlg : public CDialog
{
// Construction
public:
	CFM1208Dlg(CWnd* pParent = NULL);	// standard constructor

    HINSTANCE m_hInstMaster;
	LoadDll();

// Dialog Data
	//{{AFX_DATA(CFM1208Dlg)
	enum { IDD = IDD_FM1208_DIALOG };
	CHexEdit	m_edit_reset_inf;
	CStatic	m_static_tips;
	CHexEdit	m_edit_response;
	CHexEdit	m_edit_csn;
	CHexEdit	m_edit_command;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFM1208Dlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CFM1208Dlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnBtnConnect();
	afx_msg void OnBtnReset();
	afx_msg void OnBtnSendCommand();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FM1208DLG_H__E8145134_6171_4951_88A2_5B350077A8CF__INCLUDED_)
