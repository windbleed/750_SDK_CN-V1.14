#ifndef __DLLLOAD_H__
#define __DLLLOAD_H__


typedef void* HID_DEVICE;

#define SUCCESS                         0

#define HID_DEVICE_FAILED               0xC0
#define HID_DEVICE_NOT_FOUND			0xC1
#define HID_DEVICE_NOT_OPENED			0xC2
#define HID_DEVICE_ALREADY_OPENED		0xC3
#define	HID_DEVICE_TRANSFER_TIMEOUT		0xC4
#define HID_DEVICE_TRANSFER_FAILED		0xC5
#define HID_DEVICE_CANNOT_GET_HID_INFO	0xC6
#define HID_DEVICE_HANDLE_ERROR			0xC7
#define HID_DEVICE_INVALID_BUFFER_SIZE	0xC8
#define HID_DEVICE_SYSTEM_CODE			0xC9
#define HID_DEVICE_UNKNOW_ERROR         0xCA

#define LIB_FAILED                      0xE0
#define LIB_CHECKDATA_FAILED            0xE1

#define MAX_RF_BUFFER                   1024
 

//=====================================================================================
//================================== 系统函数 =========================================
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
//================================ ISO14443A函数 ======================================
//=====================================================================================
int (WINAPI* TyA_Request)(HID_DEVICE device, BYTE mode , WORD *pTagType);

int (WINAPI* TyA_Anticollision)(HID_DEVICE device,
								BYTE bcnt,
								BYTE *pSnr,
								BYTE *pLen);

int (WINAPI* TyA_Select)(HID_DEVICE device,
						 BYTE *pSnr,
						 BYTE snrLen,
						 BYTE *pSak);

int (WINAPI* TyA_Halt)(HID_DEVICE device);

int (WINAPI* TyA_CS_Authentication2)(HID_DEVICE device,
									 BYTE mode,
									 BYTE block,
									 BYTE *pKey);

int (WINAPI* TyA_CS_Read)(HID_DEVICE device,
						  BYTE block,
						  BYTE *pData,
						  BYTE *pLen);

int (WINAPI* TyA_CS_Write)(HID_DEVICE device, BYTE block, BYTE *pData);


#endif