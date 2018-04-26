Option Strict Off
Option Explicit On
Module mo_declare

    '**************************************** Global value *******************************************
    Public g_hDevice As IntPtr = -1  'Must init as -1

    '***************************************** Sys Command *******************************************
    'int WINAPI Sys_GetDeviceNum(WORD vid, WORD pid, DWORD *pNum);
    Public Declare Function Sys_GetDeviceNum Lib "hfrdapi.dll" (ByVal vid As Short, _
                                                                ByVal pid As Short, _
                                                                ByRef pNum As UInteger) As Integer

    'int WINAPI Sys_Open(HID_DEVICE *device,
    '                    DWORD index,
    '                    WORD vid,
    '                    WORD pid);
    Public Declare Function Sys_Open Lib "hfrdapi.dll" (ByRef device As IntPtr, _
                                                        ByVal index As UInteger, _
                                                        ByVal vid As Short, _
                                                        ByVal pid As Short) As Integer

    'BOOL WINAPI Sys_IsOpen(HID_DEVICE device);
    Public Declare Function Sys_IsOpen Lib "hfrdapi.dll" (ByVal device As IntPtr) As Boolean

    'int WINAPI Sys_Close(HID_DEVICE *device);
    Public Declare Function Sys_Close Lib "hfrdapi.dll" (ByRef device As IntPtr) As Integer

    'int WINAPI Sys_SetLight(HID_DEVICE device, BYTE color);
    Public Declare Function Sys_SetLight Lib "hfrdapi.dll" (ByVal device As IntPtr, ByVal color As Byte) As Integer

    'int WINAPI Sys_SetBuzzer(HID_DEVICE device, BYTE msec);
    Public Declare Function Sys_SetBuzzer Lib "hfrdapi.dll" (ByVal device As IntPtr, ByVal msec As Byte) As Integer

    'int WINAPI Sys_SetAntenna(HID_DEVICE device, BYTE mode);
    Public Declare Function Sys_SetAntenna Lib "hfrdapi.dll" (ByVal device As IntPtr, ByVal mode As Byte) As Integer

    'int WINAPI Sys_InitType(HID_DEVICE device, BYTE type);
    Public Declare Function Sys_InitType Lib "hfrdapi.dll" (ByVal device As IntPtr, ByVal type As Byte) As Integer


    '**************************************** MF1 Card Command ****************************************
    'int WINAPI TyA_Request(HID_DEVICE device, BYTE mode , WORD *pTagType);
    Public Declare Function TyA_Request Lib "hfrdapi.dll" (ByVal device As IntPtr, _
                                                           ByVal mode As Byte, _
                                                           ByRef pTagType As Short) As Integer

    'int WINAPI TyA_Anticollision(HID_DEVICE device,
    '                             BYTE bcnt,
    '                             BYTE *pSnr,
    '                             BYTE *pLen);
    Public Declare Function TyA_Anticollision Lib "hfrdapi.dll" (ByVal device As IntPtr, _
                                                                 ByVal bcnt As Byte, _
                                                                 ByRef pSnr As Byte, _
                                                                 ByRef pLen As Byte) As Integer
    'int WINAPI TyA_Select(HID_DEVICE device,
    '                      BYTE *pSnr,
    '                      BYTE snrLen,
    '                      BYTE *pSak);
    Public Declare Function TyA_Select Lib "hfrdapi.dll" (ByVal device As IntPtr, _
                                                          ByRef pSnr As Byte, _
                                                          ByVal snrLen As Byte, _
                                                          ByRef pSak As Byte) As Integer

    'int WINAPI TyA_Halt(HID_DEVICE device);
    Public Declare Function TyA_Halt Lib "hfrdapi.dll" (ByVal device As IntPtr) As Integer

    'int WINAPI TyA_CS_Authentication2(HID_DEVICE device,
    '                                  BYTE mode,
    '                                  BYTE block,
    '                                  BYTE *pKey);
    Public Declare Function TyA_CS_Authentication2 Lib "hfrdapi.dll" (ByVal device As IntPtr, _
                                                                      ByVal mode As Byte, _
                                                                      ByVal block As Byte, _
                                                                      ByRef pKey As Byte) As Integer

    'int WINAPI TyA_CS_Read(HID_DEVICE device,
    '                       BYTE block,
    '                       BYTE *pData,
    '                       BYTE *pLen);
    Public Declare Function TyA_CS_Read Lib "hfrdapi.dll" (ByVal device As IntPtr, _
                                                           ByVal block As Byte, _
                                                           ByRef pData As Byte, _
                                                           ByRef pLen As Byte) As Integer

    'int WINAPI TyA_CS_Write(HID_DEVICE device, BYTE block, BYTE *pData);
    Public Declare Function TyA_CS_Write Lib "hfrdapi.dll" (ByVal device As IntPtr, _
                                                            ByVal block As Byte, _
                                                            ByRef pData As Byte) As Integer


End Module