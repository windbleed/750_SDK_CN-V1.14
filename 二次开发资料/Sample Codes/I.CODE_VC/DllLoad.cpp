#include "stdafx.h"
#include "DllLoad.h"

//=============================================== 
HINSTANCE m_hInstMaster;

//=====================================================================================
//================================ Sys Function =======================================
//=====================================================================================

int (WINAPI* Sys_GetDeviceNum)(WORD vid, WORD pid, DWORD *pNum);

int (WINAPI* Sys_GetHidSerialNumberStr)(DWORD deviceIndex,
                                        WORD vid,
                                        WORD pid,
                                        char *deviceString,
                                        DWORD deviceStringLength);

int (WINAPI* Sys_Open)(HID_DEVICE *device,
		               DWORD index,
		               WORD vid,
		               WORD pid);

BOOL (WINAPI* Sys_IsOpen)(HID_DEVICE device);

int (WINAPI* Sys_Close)(HID_DEVICE *device);

int (WINAPI* Sys_SetLight)(HID_DEVICE device, BYTE color);

int (WINAPI* Sys_SetBuzzer)(HID_DEVICE device, BYTE msec);

int (WINAPI* Sys_SetAntenna)(HID_DEVICE device, BYTE mode);

int (WINAPI* Sys_InitType)(HID_DEVICE device, BYTE type);


//=====================================================================================
//============================= ISO15693 Function =====================================
//=====================================================================================
int (WINAPI* I15693_Inventory)(HID_DEVICE device, BYTE *pData, BYTE *pLen);

int (WINAPI* I15693_Inventorys)(HID_DEVICE device, BYTE *pData, BYTE *pLen);

int (WINAPI* I15693_StayQuiet)(HID_DEVICE device, BYTE *pUID);

int (WINAPI* I15693_Select)(HID_DEVICE device, BYTE *pUID);

int (WINAPI* I15693_ResetToReady)(HID_DEVICE device, BYTE mode, BYTE *pUID);

int (WINAPI* I15693_Read)(HID_DEVICE device,
                          BYTE mode,
                          BYTE *pUID,
                          BYTE block,
                          BYTE number,
                          BYTE *pData,
                          BYTE *pLen);

int (WINAPI* I15693_Write)(HID_DEVICE device,
                           BYTE mode,
                           BYTE *pUID,
                           BYTE block,
                           BYTE *pData);
			      
int (WINAPI* I15693_LockBlock)(HID_DEVICE device,
                               BYTE mode,
                               BYTE *pUID,
                               BYTE block);

int (WINAPI* I15693_WriteAFI)(HID_DEVICE device, 
                              BYTE mode,
                              BYTE *pUID,
                              BYTE AFI);

int (WINAPI* I15693_LockAFI)(HID_DEVICE device, BYTE mode, BYTE *pUID);

int (WINAPI* I15693_WriteDSFID)(HID_DEVICE device, 
                                BYTE mode,
                                BYTE *pUID,
                                BYTE DSFID);

int (WINAPI* I15693_LockDSFID)(HID_DEVICE device, BYTE mode, BYTE *pUID);

int (WINAPI* I15693_GetSystemInformation)(HID_DEVICE device,
                                          BYTE  mode,
                                          BYTE  *pUID,
                                          BYTE  *pData,
                                          BYTE  *pLen);
					       
int (WINAPI* I15693_GetBlockSecurity)(HID_DEVICE device,
                                      BYTE  mode,
                                      BYTE  *pUID,
                                      BYTE  block,
                                      BYTE  number,
                                      BYTE  *pData,
                                      BYTE  *pLen);

int (WINAPI* ICODE_SetEAS)(HID_DEVICE device,
                           BYTE mode,
                           BYTE mfgCode,
                           BYTE *pUID);

int (WINAPI* ICODE_ResetEAS)(HID_DEVICE device,
                             BYTE mode,
                             BYTE mfgCode,
                             BYTE *pUID);

int (WINAPI* ICODE_LockEAS)(HID_DEVICE device,
                            BYTE mode,
                            BYTE mfgCode,
                            BYTE *pUID);

int (WINAPI* ICODE_EasAlarm)(HID_DEVICE device,
                             BYTE mode,
                             BYTE mfgCode,
                             BYTE *pUID,
                             BYTE maskLen,
                             BYTE *pEasId,
                             BYTE *pData,
                             BYTE *pLen);


//=====================================================================================
//=============================== Load and Release DLL ================================
//=====================================================================================
BOOL LoadDllLib()
{
    TCHAR szBuf[MAX_PATH];	
    GetModuleFileName(NULL, (LPTSTR)szBuf, MAX_PATH);
    *strrchr( szBuf, '\\' ) = 0;    
    strcat(szBuf, _T("\\hfrdapi.dll"));
    
    m_hInstMaster = LoadLibrary(szBuf);	 
    
    if(m_hInstMaster)
    {
        (FARPROC&)Sys_GetDeviceNum             = GetProcAddress(m_hInstMaster,_T("Sys_GetDeviceNum"));
        (FARPROC&)Sys_GetHidSerialNumberStr    = GetProcAddress(m_hInstMaster,_T("Sys_GetHidSerialNumberStr"));       
        (FARPROC&)Sys_Open                     = GetProcAddress(m_hInstMaster,_T("Sys_Open"));
        (FARPROC&)Sys_IsOpen                   = GetProcAddress(m_hInstMaster,_T("Sys_IsOpen"));
        (FARPROC&)Sys_Close                    = GetProcAddress(m_hInstMaster,_T("Sys_Close"));
        (FARPROC&)Sys_SetLight                 = GetProcAddress(m_hInstMaster,_T("Sys_SetLight"));
        (FARPROC&)Sys_SetBuzzer                = GetProcAddress(m_hInstMaster,_T("Sys_SetBuzzer"));       
        (FARPROC&)Sys_SetAntenna               = GetProcAddress(m_hInstMaster,_T("Sys_SetAntenna"));
        (FARPROC&)Sys_InitType                 = GetProcAddress(m_hInstMaster,_T("Sys_InitType"));

        (FARPROC&)I15693_Inventory             = GetProcAddress(m_hInstMaster,_T("I15693_Inventory"));       
        (FARPROC&)I15693_Inventorys            = GetProcAddress(m_hInstMaster,_T("I15693_Inventorys"));        
        (FARPROC&)I15693_StayQuiet             = GetProcAddress(m_hInstMaster,_T("I15693_StayQuiet"));
        (FARPROC&)I15693_Select                = GetProcAddress(m_hInstMaster,_T("I15693_Select"));
        (FARPROC&)I15693_ResetToReady          = GetProcAddress(m_hInstMaster,_T("I15693_ResetToReady"));
        (FARPROC&)I15693_Read                  = GetProcAddress(m_hInstMaster,_T("I15693_Read"));
        (FARPROC&)I15693_Write                 = GetProcAddress(m_hInstMaster,_T("I15693_Write"));
        (FARPROC&)I15693_LockBlock             = GetProcAddress(m_hInstMaster,_T("I15693_LockBlock"));
        (FARPROC&)I15693_WriteAFI              = GetProcAddress(m_hInstMaster,_T("I15693_WriteAFI"));
        (FARPROC&)I15693_LockAFI               = GetProcAddress(m_hInstMaster,_T("I15693_LockAFI"));
        (FARPROC&)I15693_WriteDSFID            = GetProcAddress(m_hInstMaster,_T("I15693_WriteDSFID"));
        (FARPROC&)I15693_LockDSFID             = GetProcAddress(m_hInstMaster,_T("I15693_LockDSFID"));
        (FARPROC&)I15693_GetSystemInformation  = GetProcAddress(m_hInstMaster,_T("I15693_GetSystemInformation"));
        (FARPROC&)I15693_GetBlockSecurity      = GetProcAddress(m_hInstMaster,_T("I15693_GetBlockSecurity"));
        (FARPROC&)ICODE_SetEAS                 = GetProcAddress(m_hInstMaster,_T("ICODE_SetEAS"));
        (FARPROC&)ICODE_ResetEAS               = GetProcAddress(m_hInstMaster,_T("ICODE_ResetEAS"));
        (FARPROC&)ICODE_LockEAS                = GetProcAddress(m_hInstMaster,_T("ICODE_LockEAS"));
        (FARPROC&)ICODE_EasAlarm               = GetProcAddress(m_hInstMaster,_T("ICODE_EasAlarm"));
        
        if(
            NULL == Sys_GetDeviceNum ||
            NULL == Sys_GetHidSerialNumberStr ||
            NULL == Sys_Open ||
            NULL == Sys_IsOpen ||
            NULL == Sys_Close ||
            NULL == Sys_SetLight ||
            NULL == Sys_SetBuzzer ||
            NULL == Sys_SetAntenna ||
            NULL == Sys_InitType ||

            NULL == I15693_Inventory  ||
            NULL == I15693_Inventorys ||
            NULL == I15693_StayQuiet ||
            NULL == I15693_Select ||
            NULL == I15693_ResetToReady ||
            NULL == I15693_Read ||
            NULL == I15693_Write ||
            NULL == I15693_LockBlock ||
            NULL == I15693_WriteAFI ||
            NULL == I15693_LockAFI ||
            NULL == I15693_WriteDSFID ||
            NULL == I15693_LockDSFID ||
            NULL == I15693_GetSystemInformation ||
            NULL == I15693_GetBlockSecurity ||
            NULL == ICODE_SetEAS ||
            NULL == ICODE_ResetEAS ||
            NULL == ICODE_LockEAS ||
            NULL == ICODE_EasAlarm
           )
        {			
            AfxMessageBox(_T("Load hfrdapi.dll interface failed !"));
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

void ReleaseDllLib()
{
    if(m_hInstMaster) 
    {
        FreeLibrary(m_hInstMaster);
    }
}

