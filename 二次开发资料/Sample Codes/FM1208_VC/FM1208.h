// FM1208.h : main header file for the FM1208 application
//

#if !defined(AFX_FM1208_H__B5413E80_A6ED_4767_AB96_8E7809F2DB44__INCLUDED_)
#define AFX_FM1208_H__B5413E80_A6ED_4767_AB96_8E7809F2DB44__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CFM1208App:
// See FM1208.cpp for the implementation of this class
//

class CFM1208App : public CWinApp
{
public:
	CFM1208App();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CFM1208App)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CFM1208App)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_FM1208_H__B5413E80_A6ED_4767_AB96_8E7809F2DB44__INCLUDED_)
