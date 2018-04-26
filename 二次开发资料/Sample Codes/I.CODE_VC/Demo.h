// Demo.h : main header file for the DEMO application
//

#if !defined(AFX_DEMO_H__929D3FEA_5610_4087_B378_5841BC9E4B88__INCLUDED_)
#define AFX_DEMO_H__929D3FEA_5610_4087_B378_5841BC9E4B88__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CDemoApp:
// See Demo.cpp for the implementation of this class
//

class CDemoApp : public CWinApp
{
public:
	CDemoApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDemoApp)
	public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();
	//}}AFX_VIRTUAL
private:
//	BOOL LoadMasterRDLib();
//	void ReleaseMasterRDLib();

private:
//	HINSTANCE m_hInstMaster;
public:
// Implementation

	//{{AFX_MSG(CDemoApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DEMO_H__929D3FEA_5610_4087_B378_5841BC9E4B88__INCLUDED_)
