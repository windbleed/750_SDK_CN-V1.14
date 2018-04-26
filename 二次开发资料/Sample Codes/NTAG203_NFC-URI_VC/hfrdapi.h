#ifndef __DLLLOAD_H__
#define __DLLLOAD_H__

typedef void* HID_DEVICE;

#define SUCCESS                         0

#define HID_DEVICE_FAILED               0xC0
#define HID_DEVICE_NOT_FOUND            0xC1
#define HID_DEVICE_NOT_OPENED           0xC2
#define HID_DEVICE_ALREADY_OPENED       0xC3
#define	HID_DEVICE_TRANSFER_TIMEOUT     0xC4
#define HID_DEVICE_TRANSFER_FAILED      0xC5
#define HID_DEVICE_CANNOT_GET_HID_INFO  0xC6
#define HID_DEVICE_HANDLE_ERROR         0xC7
#define HID_DEVICE_INVALID_BUFFER_SIZE  0xC8
#define HID_DEVICE_SYSTEM_CODE          0xC9
#define HID_DEVICE_UNKNOW_ERROR         0xCA

#define LIB_SUCCESS                     0
#define LIB_FAILED                      0xE0
#define LIB_CHECKDATA_FAILED            0xE1

#define MAX_RF_BUFFER                   1024

//=====================================================================================
//================================== 系统函数 =========================================
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

extern int (WINAPI* Sys_Transceive)(HID_DEVICE device, 
                                    BYTE *pSendData, 
                                    WORD sendLen, 
                                    BYTE *pReceData,
                                    WORD *receLen);


//=====================================================================================
//================================ ISO14443A函数 ======================================
//=====================================================================================
extern int (WINAPI* TyA_Request)(HID_DEVICE device, BYTE mode , WORD *pTagType);

extern int (WINAPI* TyA_Anticollision)(HID_DEVICE device,
								       BYTE bcnt,
								       BYTE *pSnr,
								       BYTE *pLen);

extern int (WINAPI* TyA_Select)(HID_DEVICE device,
						        BYTE *pSnr,
						        BYTE snrLen,
						        BYTE *pSak);

extern int (WINAPI* TyA_Halt)(HID_DEVICE device);

extern int (WINAPI* TyA_CS_Authentication2)(HID_DEVICE device,
									        BYTE mode,
									        BYTE block,
									        BYTE *pKey);

extern int (WINAPI* TyA_CS_Read)(HID_DEVICE device,
						         BYTE block,
						         BYTE *pData,
						         BYTE *pLen);

extern int (WINAPI* TyA_CS_Write)(HID_DEVICE device, BYTE block, BYTE *pData);


//================================ NTAG =======================================
extern int (WINAPI* TyA_NTAG_AnticollSelect)(HID_DEVICE device, BYTE *pSnr, BYTE *pLen);

extern int (WINAPI* TyA_NTAG_GetVersion)(HID_DEVICE device, BYTE *pData, BYTE* pLen);

extern int (WINAPI* TyA_NTAG_Read)(HID_DEVICE device, 
                                   BYTE addr, 
                                   BYTE *pData, 
                                   BYTE *pLen);

extern int (WINAPI* TyA_NTAG_FastRead)(HID_DEVICE device, 
                                       BYTE startAddr, 
                                       BYTE endAddr, 
                                       BYTE *pData, 
                                       BYTE *pLen);

extern int (WINAPI* TyA_NTAG_Write)(HID_DEVICE device, BYTE addr, BYTE *pdata);

extern int (WINAPI* TyA_NTAG_CompWrite)(HID_DEVICE device, BYTE addr, BYTE *pData);

extern int (WINAPI* TyA_NTAG_ReadCnt)(HID_DEVICE device, 
                                      BYTE addr, 
                                      BYTE *pData, 
                                      BYTE *pLen);

extern int (WINAPI* TyA_NTAG_PwdAuth)(HID_DEVICE device, 
                                      BYTE *pPwd, 
                                      BYTE *pData, 
                                      BYTE *pLen);

extern int (WINAPI* TyA_NTAG_ReadSig)(HID_DEVICE device, 
                                      BYTE addr, 
                                      BYTE *pData, 
                                      BYTE *pLen);

#endif