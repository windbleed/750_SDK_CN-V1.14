#ifndef __DLLLOAD_H__
#define __DLLLOAD_H__

typedef void* HID_DEVICE;

#define SUCCESS                         0
#define LIB_SUCCESS                     0

#define HID_DEVICE_FAILED               0xC0
#define HID_DEVICE_NOT_FOUND	        0xC1
#define HID_DEVICE_NOT_OPENED		    0xC2
#define HID_DEVICE_ALREADY_OPENED	    0xC3
#define	HID_DEVICE_TRANSFER_TIMEOUT	    0xC4
#define HID_DEVICE_TRANSFER_FAILED	    0xC5
#define HID_DEVICE_CANNOT_GET_HID_INFO	0xC6    
#define HID_DEVICE_HANDLE_ERROR		    0xC7
#define HID_DEVICE_INVALID_BUFFER_SIZE	0xC8
#define HID_DEVICE_SYSTEM_CODE		    0xC9
#define HID_DEVICE_UNKNOW_ERROR         0xCA

#define LIB_FAILED                      0xE0
#define LIB_CHECKDATA_FAILED            0xE1

#define MAX_RF_BUFFER                   1024

//=============================================== 
BOOL LoadDllLib();
void ReleaseDllLib();

//=====================================================================================
//================================ System Function ====================================
//=====================================================================================

extern int (WINAPI* Sys_GetDeviceNum)(WORD vid, WORD pid, DWORD *pNum);

extern int (WINAPI* Sys_GetHidSerialNumberStr)(DWORD deviceIndex,
                                                WORD vid,
                                                WORD pid,
                                                char *deviceString,
                                                DWORD deviceStringLength);

extern int (WINAPI* Sys_Open)(HID_DEVICE *device,
		                       DWORD index,
		                       WORD vid,
		                       WORD pid);

extern BOOL (WINAPI* Sys_IsOpen)(HID_DEVICE device);

extern int (WINAPI* Sys_Close)(HID_DEVICE *device);

extern int (WINAPI* Sys_SetLight)(HID_DEVICE device, BYTE color);

extern int (WINAPI* Sys_SetBuzzer)(HID_DEVICE device, BYTE msec);

extern int (WINAPI* Sys_SetAntenna)(HID_DEVICE device, BYTE mode);

extern int (WINAPI* Sys_InitType)(HID_DEVICE device, BYTE type);


//=====================================================================================
//============================== ISO15693 Function ====================================
//=====================================================================================
extern int (WINAPI* I15693_Inventory)(HID_DEVICE device, BYTE *pData, BYTE *pLen);

extern int (WINAPI* I15693_Inventorys)(HID_DEVICE device, BYTE *pData, BYTE *pLen);

extern int (WINAPI* I15693_StayQuiet)(HID_DEVICE device, BYTE *pUID);

extern int (WINAPI* I15693_Select)(HID_DEVICE device, BYTE *pUID);

extern int (WINAPI* I15693_ResetToReady)(HID_DEVICE device, BYTE mode, BYTE *pUID);

extern int (WINAPI* I15693_Read)(HID_DEVICE device,
                                  BYTE mode,
                                  BYTE *pUID,
                                  BYTE block,
                                  BYTE number,
                                  BYTE *pData,
                                  BYTE *pLen);

extern int (WINAPI* I15693_Write)(HID_DEVICE device,
                                   BYTE mode,
                                   BYTE *pUID,
                                   BYTE block,
                                   BYTE *pData);
			      
extern int (WINAPI* I15693_LockBlock)(HID_DEVICE device,
                                       BYTE mode,
                                       BYTE *pUID,
                                       BYTE block);

extern int (WINAPI* I15693_WriteAFI)(HID_DEVICE device, 
                                      BYTE mode,
                                      BYTE *pUID,
                                      BYTE AFI);

extern int (WINAPI* I15693_LockAFI)(HID_DEVICE device, BYTE mode, BYTE *pUID);

extern int (WINAPI* I15693_WriteDSFID)(HID_DEVICE device, 
                                        BYTE mode,
                                        BYTE *pUID,
                                        BYTE DSFID);

extern int (WINAPI* I15693_LockDSFID)(HID_DEVICE device, BYTE mode, BYTE *pUID);

extern int (WINAPI* I15693_GetSystemInformation)(HID_DEVICE device,
                                                  BYTE  mode,
                                                  BYTE  *pUID,
                                                  BYTE  *pData,
                                                  BYTE  *pLen);
					       
extern int (WINAPI* I15693_GetBlockSecurity)(HID_DEVICE device,
                                              BYTE  mode,
                                              BYTE  *pUID,
                                              BYTE  block,
                                              BYTE  number,
                                              BYTE  *pData,
                                              BYTE  *pLen);

extern int (WINAPI* ICODE_SetEAS)(HID_DEVICE device,
                                  BYTE mode,
                                  BYTE mfgCode,
                                  BYTE *pUID);

extern int (WINAPI* ICODE_ResetEAS)(HID_DEVICE device,
                                    BYTE mode,
                                    BYTE mfgCode,
                                    BYTE *pUID);

extern int (WINAPI* ICODE_LockEAS)(HID_DEVICE device,
                                   BYTE mode,
                                   BYTE mfgCode,
                                   BYTE *pUID);

extern int (WINAPI* ICODE_EasAlarm)(HID_DEVICE device,
                                    BYTE mode,
                                    BYTE mfgCode,
                                    BYTE *pUID,
                                    BYTE maskLen,
                                    BYTE *pEasId,
                                    BYTE *pData,
                                    BYTE *pLen);


#endif