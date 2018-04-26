#include "stdafx.h"
#include "hfrdapi.h"

//=====================================================================================
//=============================== System Function =====================================
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

int (WINAPI* Sys_Transceive)(HID_DEVICE device, 
                             BYTE *pSendData, 
                             WORD sendLen, 
                             BYTE *pReceData,
                             WORD *receLen);


//=====================================================================================
//================================ Auxiliary Function =================================
//=====================================================================================
int (WINAPI* Aux_SingleDES)(BYTE desType,
                            BYTE *key,
                            BYTE *srcData,
                            DWORD srcDataLen,
                            BYTE *destData,
                            DWORD *destDataLen);


int (WINAPI* Aux_TripleDES)(BYTE desType,
                            BYTE *key,
                            BYTE *srcData,
                            DWORD srcDataLen,
                            BYTE *destData,
                            DWORD *destDataLen);


int (WINAPI* Aux_SingleMAC)(BYTE *key,
                            BYTE *initData,
                            BYTE *srcData, 
                            DWORD srcDataLen, 
                            BYTE *macData);


int (WINAPI* Aux_TripleMAC)(BYTE *key,
                            BYTE *initData,
                            BYTE *srcData, 
                            DWORD srcDataLen, 
                            BYTE *macData);


//=====================================================================================
//================================ ISO14443A Function =================================
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


//=================================== NTAG ====================================
int (WINAPI* TyA_NTAG_AnticollSelect)(HID_DEVICE device, BYTE *pSnr, BYTE *pLen);

int (WINAPI* TyA_NTAG_GetVersion)(HID_DEVICE device, BYTE *pData, BYTE* pLen);

int (WINAPI* TyA_NTAG_Read)(HID_DEVICE device, 
                            BYTE addr, 
                            BYTE *pData, 
                            BYTE *pLen);

int (WINAPI* TyA_NTAG_FastRead)(HID_DEVICE device, 
                                BYTE startAddr, 
                                BYTE endAddr, 
                                BYTE *pData, 
                                BYTE *pLen);

int (WINAPI* TyA_NTAG_Write)(HID_DEVICE device, BYTE addr, BYTE *pdata);

int (WINAPI* TyA_NTAG_CompWrite)(HID_DEVICE device, BYTE addr, BYTE *pData);

int (WINAPI* TyA_NTAG_ReadCnt)(HID_DEVICE device, 
                               BYTE addr, 
                               BYTE *pData, 
                               BYTE *pLen);

int (WINAPI* TyA_NTAG_PwdAuth)(HID_DEVICE device, 
                               BYTE *pPwd, 
                               BYTE *pData, 
                               BYTE *pLen);

int (WINAPI* TyA_NTAG_ReadSig)(HID_DEVICE device, 
                               BYTE addr, 
                               BYTE *pData, 
                               BYTE *pLen);


//============================= ISO14443A-4 ===================================
int (WINAPI* TyA_Reset)(HID_DEVICE device,
                        BYTE mode,
                        BYTE *pData,
                        BYTE *pMsgLg);

int (WINAPI* TyA_CosCommand)(HID_DEVICE device,
                             BYTE *pCommand,
                             BYTE cmdLen,
                             BYTE *pData,
                             BYTE *pMsgLg);

int (WINAPI* TyA_Deselect)(HID_DEVICE device);
