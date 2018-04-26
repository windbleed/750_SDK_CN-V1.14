// DemoDlg.h : header file
//

#if !defined(AFX_DEMODLG_H__CB47B120_62EC_4E86_9310_7508D6A88762__INCLUDED_)
#define AFX_DEMODLG_H__CB47B120_62EC_4E86_9310_7508D6A88762__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "HexEdit.h"
/////////////////////////////////////////////////////////////////////////////
// CDemoDlg dialog

class CDemoDlg : public CDialog
{
// Construction
public:
	CDemoDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CDemoDlg)
	enum { IDD = IDD_DEMO_DIALOG };
	CStatic	m_static_tips;
	CHexEdit	m_edit_status;
	CHexEdit	m_edit_data;
	CHexEdit	m_edit_uid1;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDemoDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
private:
	BOOL  m_bConnectDevice;
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CDemoDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButtonReadcard();
	afx_msg void OnButtonWritecard();
	afx_msg void OnButtonLockcard();
	afx_msg void OnButtonInventory();
	afx_msg void OnButtonConnect();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DEMODLG_H__CB47B120_62EC_4E86_9310_7508D6A88762__INCLUDED_)
