Attribute VB_Name = "mo_declare"

'**************************************** Global value *******************************************
Public g_hDevice As Long  'g_hDevice must be initialized as -1 before use


'***************************************** Delay function ****************************************
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)


'***************************************** Sys Command *******************************************
'int WINAPI Sys_GetDeviceNum(WORD vid, WORD pid, DWORD *pNum);
Public Declare Function Sys_GetDeviceNum Lib "hfrdapi.dll" (ByVal vid As Integer, _
                                                            ByVal pid As Integer, _
                                                            ByRef pNum As Long) As Long
                                                            
'int WINAPI Sys_Open(HID_DEVICE *device,
'                    DWORD index,
'                    WORD vid,
'                    WORD pid);
Public Declare Function Sys_Open Lib "hfrdapi.dll" (ByRef device As Long, _
                                                    ByVal index As Long, _
                                                    ByVal vid As Integer, _
                                                    ByVal pid As Integer) As Long

'BOOL WINAPI Sys_IsOpen(HID_DEVICE device);
Public Declare Function Sys_IsOpen Lib "hfrdapi.dll" (ByVal device As Long) As Long

'int WINAPI Sys_Close(HID_DEVICE *device);
Public Declare Function Sys_Close Lib "hfrdapi.dll" (ByRef device As Long) As Long

'int WINAPI Sys_SetLight(HID_DEVICE device, BYTE color);
Public Declare Function Sys_SetLight Lib "hfrdapi.dll" (ByVal device As Long, ByVal color As Byte) As Long

'int WINAPI Sys_SetBuzzer(HID_DEVICE device, BYTE msec);
Public Declare Function Sys_SetBuzzer Lib "hfrdapi.dll" (ByVal device As Long, ByVal msec As Byte) As Long

'int WINAPI Sys_SetAntenna(HID_DEVICE device, BYTE mode);
Public Declare Function Sys_SetAntenna Lib "hfrdapi.dll" (ByVal device As Long, ByVal mode As Byte) As Long

'int WINAPI Sys_InitType(HID_DEVICE device, BYTE type);
Public Declare Function Sys_InitType Lib "hfrdapi.dll" (ByVal device As Long, ByVal workType As Byte) As Long


'**************************************** MF1 Card Command ****************************************
'int WINAPI TyA_Request(HID_DEVICE device, BYTE mode , WORD *pTagType);
Public Declare Function TyA_Request Lib "hfrdapi.dll" (ByVal device As Long, _
                                                       ByVal mode As Byte, _
                                                       ByRef pTagType As Integer) As Long

'int WINAPI TyA_Anticollision(HID_DEVICE device,
'                             BYTE bcnt,
'                             BYTE *pSnr,
'                             BYTE *pLen);
Public Declare Function TyA_Anticollision Lib "hfrdapi.dll" (ByVal device As Long, _
                                                             ByVal bcnt As Byte, _
                                                             ByRef pSnr As Byte, _
                                                             ByRef pLen As Byte) As Long
'int WINAPI TyA_Select(HID_DEVICE device,
'                      BYTE *pSnr,
'                      BYTE snrLen,
'                      BYTE *pSak);
Public Declare Function TyA_Select Lib "hfrdapi.dll" (ByVal device As Long, _
                                                      ByRef pSnr As Byte, _
                                                      ByVal snrLen As Byte, _
                                                      ByRef pSak As Byte) As Long

'int WINAPI TyA_Halt(HID_DEVICE device);
Public Declare Function TyA_Halt Lib "hfrdapi.dll" (ByVal device As Long) As Long

'int WINAPI TyA_CS_Authentication2(HID_DEVICE device,
'                                  BYTE mode,
'                                  BYTE block,
'                                  BYTE *pKey);
Public Declare Function TyA_CS_Authentication2 Lib "hfrdapi.dll" (ByVal device As Long, _
                                                                  ByVal mode As Byte, _
                                                                  ByVal block As Byte, _
                                                                  ByRef pKey As Byte) As Long

'int WINAPI TyA_CS_Read(HID_DEVICE device,
'                       BYTE block,
'                       BYTE *pData,
'                       BYTE *pLen);
Public Declare Function TyA_CS_Read Lib "hfrdapi.dll" (ByVal device As Long, _
                                                       ByVal block As Byte, _
                                                       ByRef pData As Byte, _
                                                       ByRef pLen As Byte) As Long

'int WINAPI TyA_CS_Write(HID_DEVICE device, BYTE block, BYTE *pData);
Public Declare Function TyA_CS_Write Lib "hfrdapi.dll" (ByVal device As Long, _
                                                        ByVal block As Byte, _
                                                        ByRef pData As Byte) As Long
                                                    





          
























