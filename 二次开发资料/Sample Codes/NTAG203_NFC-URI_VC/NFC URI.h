// NFC URI.h : main header file for the NFC URI application
//

#if !defined(AFX_NFCURI_H__8192055C_B886_4E3E_B351_CDBFBBD2E2CC__INCLUDED_)
#define AFX_NFCURI_H__8192055C_B886_4E3E_B351_CDBFBBD2E2CC__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CNFCURIApp:
// See NFC URI.cpp for the implementation of this class
//

class CNFCURIApp : public CWinApp
{
public:
	CNFCURIApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CNFCURIApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CNFCURIApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_NFCURI_H__8192055C_B886_4E3E_B351_CDBFBBD2E2CC__INCLUDED_)
