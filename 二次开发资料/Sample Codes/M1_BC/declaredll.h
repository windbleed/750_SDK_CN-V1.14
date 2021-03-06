typedef void* HID_DEVICE;

//============================== Sys function ==================================
//int WINAPI Sys_GetDeviceNum(WORD vid, WORD pid, DWORD *pNum);
typedef int (WINAPI *F_SYS_GETDEVICENUM)(WORD, WORD, DWORD *);

//int WINAPI Sys_Open(HID_DEVICE *device,
//                    DWORD index,
//                    WORD vid,
//                    WORD pid);
typedef int (WINAPI *F_SYS_OPEN)(HID_DEVICE *,
                                 DWORD,
                                 WORD,
                                 WORD);

//BOOL WINAPI Sys_IsOpen(HID_DEVICE device);
typedef BOOL (WINAPI *F_SYS_ISOPEN)(HID_DEVICE);

//int WINAPI Sys_Close(HID_DEVICE *device);
typedef int (WINAPI *F_SYS_CLOSE)(HID_DEVICE *);

//int WINAPI Sys_SetLight(HID_DEVICE device, BYTE color);
typedef int (WINAPI *F_SYS_SETLIGHT)(HID_DEVICE, BYTE);

//int WINAPI Sys_SetBuzzer(HID_DEVICE device, BYTE msec);
typedef int (WINAPI *F_SYS_SETBUZZER)(HID_DEVICE, BYTE);

//int WINAPI Sys_SetAntenna(HID_DEVICE device, BYTE mode);
typedef int (WINAPI *F_SYS_SETANTENNA)(HID_DEVICE, BYTE);

//int WINAPI Sys_InitType(HID_DEVICE device, BYTE type);
typedef int (WINAPI *F_SYS_INITTYPE)(HID_DEVICE, BYTE);


//=========================== MIFARE ONE function ==============================
//int WINAPI TyA_Request(HID_DEVICE device, BYTE mode , WORD *pTagType);
typedef int (WINAPI *F_TYA_REQUEST)(HID_DEVICE, BYTE, WORD *);

//int WINAPI TyA_Anticollision(HID_DEVICE device,
//                             BYTE bcnt,
//                             BYTE *pSnr,
//                             BYTE *pLen);
typedef int (WINAPI *F_TYA_ANTICOLLISION)(HID_DEVICE,
                                          BYTE,
                                          BYTE *,
                                          BYTE *);

//int WINAPI TyA_Select(HID_DEVICE device,
//                      BYTE *pSnr,
//                      BYTE snrLen,
//                      BYTE *pSize);
typedef int (WINAPI *F_TYA_SELECT)(HID_DEVICE,
                                   BYTE *,
                                   BYTE,
                                   BYTE *);

//int WINAPI TyA_Halt(HID_DEVICE device);
typedef int (WINAPI *F_TYA_HALT)(HID_DEVICE);

//int WINAPI TyA_CS_Authentication2(HID_DEVICE device,
//                                  BYTE mode,
//                                  BYTE block,
//                                  BYTE *pKey);
typedef int (WINAPI *F_TYA_CS_AUTHENTICATION2)(HID_DEVICE,
                                               BYTE,
                                               BYTE,
                                               BYTE *);

//int WINAPI TyA_CS_Read(HID_DEVICE device,
//                       BYTE block,
//                       BYTE *pData,
//                       BYTE *pLen);
typedef int (WINAPI *F_TYA_CS_READ)(HID_DEVICE,
                                    BYTE,
                                    BYTE *,
                                    BYTE *);

//int WINAPI TyA_CS_Write(HID_DEVICE device, BYTE block, BYTE *pData);
typedef int (WINAPI *F_TYA_CS_WRITE)(HID_DEVICE, BYTE, BYTE *);


