// NFC URIDlg.h : header file
//

#if !defined(AFX_NFCURIDLG_H__8C9A2F53_32D5_434B_A18C_00CC0683C121__INCLUDED_)
#define AFX_NFCURIDLG_H__8C9A2F53_32D5_434B_A18C_00CC0683C121__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CNFCURIDlg dialog

class CNFCURIDlg : public CDialog
{
// Construction
public:
	CNFCURIDlg(CWnd* pParent = NULL);	// standard constructor

	HINSTANCE m_hInstMaster;
	LoadDll();

// Dialog Data
	//{{AFX_DATA(CNFCURIDlg)
	enum { IDD = IDD_NFCURI_DIALOG };
	CStatic	m_static_tips;
	CEdit	m_edit_uri_field;
	CComboBox	m_combo_identifier;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CNFCURIDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CNFCURIDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnBtnConnect();
	afx_msg void OnBtnWrite();
	afx_msg void OnDestroy();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_NFCURIDLG_H__8C9A2F53_32D5_434B_A18C_00CC0683C121__INCLUDED_)
