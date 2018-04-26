//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "p_main.h"
#include "declaredll.h"
#include "myfunc.h"

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
Tfrm_main *frm_main;

//---------------------------------------------------------------------------
HID_DEVICE g_hDevice = HID_DEVICE(-1);  //g_hDevice must be initialized as -1 before use

F_SYS_OPEN                Sys_Open;
F_SYS_ISOPEN              Sys_IsOpen;
F_SYS_CLOSE               Sys_Close;
F_SYS_SETBUZZER           Sys_SetBuzzer;
F_SYS_SETANTENNA          Sys_SetAntenna;
F_SYS_INITTYPE            Sys_InitType;
F_TYA_REQUEST             TyA_Request;
F_TYA_ANTICOLLISION       TyA_Anticollision;
F_TYA_SELECT              TyA_Select;
F_TYA_HALT                TyA_Halt;
F_TYA_CS_AUTHENTICATION2  TyA_CS_Authentication2;
F_TYA_CS_READ             TyA_CS_Read;
F_TYA_CS_WRITE            TyA_CS_Write;

//---------------------------------------------------------------------------
__fastcall Tfrm_main::Tfrm_main(TComponent* Owner)
        : TForm(Owner)
{

}

//---------------------------------------------------------------------------
void __fastcall Tfrm_main::FormCreate(TObject *Sender)
{
    int i;
    HINSTANCE hInstDll = NULL;

    for(i=2;i<64;i++)
    {
        cb_kh->AddItem(StrToInt(i), NULL);
    }

    //------------------------ Load DLL
    hInstDll = LoadLibrary("hfrdapi.dll");
    if(hInstDll==NULL)
    {
        lb_info->Caption = "Load DLL failed !";
        return;
    }

    Sys_IsOpen = (F_SYS_ISOPEN)GetProcAddress(hInstDll, "Sys_IsOpen");
    if(Sys_IsOpen == NULL)
    {
        lb_info->Caption = "Function Sys_IsOpen GetProcAddress failed !";
        return;
    }

    Sys_Open = (F_SYS_OPEN)GetProcAddress(hInstDll, "Sys_Open");
    if(Sys_Open == NULL)
    {
        lb_info->Caption = "Function Sys_Open GetProcAddress failed !";
        return;
    }

    Sys_Close = (F_SYS_CLOSE)GetProcAddress(hInstDll, "Sys_Close");
    if(Sys_Close == NULL)
    {
        lb_info->Caption = "Function Sys_Close GetProcAddress failed !";
        return;
    }

    Sys_SetBuzzer = (F_SYS_SETBUZZER)GetProcAddress(hInstDll, "Sys_SetBuzzer");
    if(Sys_SetBuzzer == NULL)
    {
        lb_info->Caption = "Function Sys_SetBuzzer GetProcAddress failed !";
        return;
    }

    Sys_SetAntenna = (F_SYS_SETANTENNA)GetProcAddress(hInstDll, "Sys_SetAntenna");
    if(Sys_SetAntenna == NULL)
    {
        lb_info->Caption = "Function Sys_SetAntenna GetProcAddress failed !";
        return;
    }

    Sys_InitType = (F_SYS_INITTYPE)GetProcAddress(hInstDll, "Sys_InitType");
    if(Sys_InitType == NULL)
    {
        lb_info->Caption = "Function Sys_InitType GetProcAddress failed !";
        return;
    }

    TyA_Request = (F_TYA_REQUEST)GetProcAddress(hInstDll, "TyA_Request");
    if(TyA_Request == NULL)
    {
        lb_info->Caption ="Function TyA_Request GetProcAddress failed !";
        return;
    }

    TyA_Anticollision = (F_TYA_ANTICOLLISION)GetProcAddress(hInstDll, "TyA_Anticollision");
    if(TyA_Anticollision == NULL)
    {
       lb_info->Caption ="Function TyA_Anticollision GetProcAddress failed !";
       return;
    }

    TyA_Select = (F_TYA_SELECT)GetProcAddress(hInstDll, "TyA_Select");
    if(TyA_Select == NULL)
    {
        lb_info->Caption ="Function TyA_Select GetProcAddress failed !";
        return;
    }

    TyA_Halt = (F_TYA_HALT)GetProcAddress(hInstDll, "TyA_Halt");
    if(TyA_Halt == NULL)
    {
        lb_info->Caption ="Function TyA_Halt GetProcAddress failed !";
        return;
    }

    TyA_CS_Authentication2 = (F_TYA_CS_AUTHENTICATION2)GetProcAddress(hInstDll, "TyA_CS_Authentication2");
    if(TyA_CS_Authentication2 == NULL)
    {
        lb_info->Caption ="Function TyA_CS_Authentication2 GetProcAddress failed !";
        return;
    }

    TyA_CS_Read = (F_TYA_CS_READ)GetProcAddress(hInstDll, "TyA_CS_Read");
    if(TyA_CS_Read == NULL)
    {
        lb_info->Caption ="Function TyA_CS_Read GetProcAddress failed !";
        return;
    }

    TyA_CS_Write = (F_TYA_CS_WRITE)GetProcAddress(hInstDll, "TyA_CS_Write");
    if(TyA_CS_Write == NULL)
    {
        lb_info->Caption ="Function TyA_CS_Write GetProcAddress failed !";
        return;
    }
}

//---------------------------------------------------------------------------
//Get card serial number and select card
void __fastcall Tfrm_main::Button1Click(TObject *Sender)
{
    int status;
    AnsiString s1;
    BYTE buf1[100];
    WORD tagType;
    BYTE len;
    BYTE sak;

    //------------------------Check whether the reader is connected or not
    if(TRUE != Sys_IsOpen(g_hDevice))
    {
        lb_info->Caption = "Please connect the device firstly !";
        return;
    }

    //------------------------Request
    status = TyA_Request(g_hDevice, 0x52, &tagType);
    if(status != 0)
    {
        lb_info->Caption ="TyA_Request failed !";
        return;
    }

    //------------------------Anticollision
    status = TyA_Anticollision(g_hDevice, 0, buf1, &len);
    if(status != 0)
    {
        lb_info->Caption ="TyA_Anticollision failed !";
        return;
    }

    s1=f_bytetohex(buf1,len);
    le_kh->Text =s1;

    //------------------------Select card
    status = TyA_Select(g_hDevice, buf1, 4, &sak);
    if(status != 0)
    {
        lb_info->Caption ="TyA_Select failed !";
        return;
    }

    //------------------------Tips
    lb_info->Caption ="Select card succeed !";
}

//---------------------------------------------------------------------------
//Halt card
void __fastcall Tfrm_main::Button2Click(TObject *Sender)
{
    int status;

    //------------------------Check whether the reader is connected or not
    if(TRUE != Sys_IsOpen(g_hDevice))
    {
        lb_info->Caption = "Please connect the device firstly !";
        return;
    }

    //------------------------Halt card
    status = TyA_Halt(g_hDevice);
    if(status != 0)
    {
        lb_info->Caption ="TyA_Halt failed !";
        return;
    }

    lb_info->Caption ="Halt succeed !";
}

//---------------------------------------------------------------------------
//Read card
void __fastcall Tfrm_main::Button4Click(TObject *Sender)
{
    int status;
    AnsiString s1;
    BYTE keyType;
    BYTE blockAddr;
    BYTE len;
    BYTE keyBuffer[100];
    BYTE dataBuffer[100];

    //Get key
    s1 = ed_key->Text;
    if(s1.Length() != 12)
    {
        lb_info->Caption ="The length of key is error !";
        ed_key->SetFocus();
        return;
    }
    f_stringtobin(s1, 12, keyBuffer);

    //Get key type
    if(rg_key->ItemIndex ==0)
        keyType = 0x60;
    else
        keyType = 0x61;

    //Get block address
    blockAddr = cb_kh->ItemIndex;

    //------------------------Check whether the reader is connected or not
    if(TRUE != Sys_IsOpen(g_hDevice))
    {
        lb_info->Caption = "Please connect the device firstly !";
        return;
    }

    //Authentication
    status = TyA_CS_Authentication2(g_hDevice, keyType, blockAddr, keyBuffer);
    if(status != 0)
    {
        lb_info->Caption ="TyA_CS_Authentication2 failed !";
        return;
    }

    //Read card
    status = TyA_CS_Read(g_hDevice, blockAddr, dataBuffer, &len);
    if(status != 0)
    {
        lb_info->Caption ="TyA_CS_read failed !";
        return;
    }
    le_sj->Text =f_bytetohex(dataBuffer, len);

    //Tips
    lb_info->Caption ="Read card succeed !";
}

//---------------------------------------------------------------------------
//Write card
void __fastcall Tfrm_main::Button3Click(TObject *Sender)
{
    int status;
    AnsiString s1;
    BYTE keyType;
    BYTE blockAddr;
    BYTE len;
    BYTE keyBuffer[100];
    BYTE dataBuffer[100];

    //Get key
    s1 = ed_key->Text;
    if(s1.Length() !=12)
    {
        lb_info->Caption ="The length of key is error!"; ed_key->SetFocus();
        return;
    }
    f_stringtobin(s1, 12, keyBuffer);

    //Get data
    s1=le_sj->Text;
    if(s1.Length() !=32)
    {
        lb_info->Caption ="The length of data is error!"; le_sj->SetFocus();
        return;
    }
    f_stringtobin(s1, 32, dataBuffer);

    //Get key type
    if(rg_key->ItemIndex == 0)
        keyType = 0x60;
    else
        keyType = 0x61;

    //Get block address
    blockAddr = cb_kh->ItemIndex;

    //------------------------Check whether the reader is connected or not
    if(TRUE != Sys_IsOpen(g_hDevice))
    {
        lb_info->Caption = "Please connect the device firstly !";
        return;
    }

    //Authentication
    status = TyA_CS_Authentication2(g_hDevice, keyType, blockAddr, keyBuffer);
    if(status != 0)
    {
        lb_info->Caption ="TyA_CS_Authentication2 failed !";
        return;
    }

    //Write card
    status = TyA_CS_Write(g_hDevice, blockAddr, dataBuffer);
    if(status != 0)
    {
        lb_info->Caption = "TyA_CS_Write failed !";
        return;
    }

    //Tips
    lb_info->Caption = "Write card succeed !";
}


//---------------------------------------------------------------------------
//Connect and init the reader
void __fastcall Tfrm_main::Button5Click(TObject *Sender)
{
    int status;

    //=========================== Connect reader =========================
    //Check whether the reader is connected or not
    //If the reader is already open , close it firstly
    if(TRUE == Sys_IsOpen(g_hDevice))
    {
        status = Sys_Close(&g_hDevice);
        if(status != 0)
        {
            lb_info->Caption = "Sys_Close failed !";
            return;
        }
    }

    //Connect the reader
    status = Sys_Open(&g_hDevice, 0, 0x0416, 0x8020);
    if(status != 0)
    {
        lb_info->Caption = "Sys_Open failed !";
        return;
    }

    //============= Init the reader before operating the card ============
    //Close antenna of the reader
    status = Sys_SetAntenna(g_hDevice, 0);
    if(status != 0)
    {
        lb_info->Caption = "Sys_SetAntenna failed !";
        return;
    }
    Sleep(5); //'Appropriate delay after Sys_SetAntenna operating

    //Set the reader's working mode
    status = Sys_InitType(g_hDevice, 'A');
    if(status != 0)
    {
        lb_info->Caption = "Sys_SetAntenna failed !";
        return;
    }
    Sleep(5); //'Appropriate delay after Sys_InitType operating

    //Open antenna of the reader
    status = Sys_SetAntenna(g_hDevice, 1);
    if(status != 0)
    {
        lb_info->Caption = "Sys_SetAntenna failed !";
        return;
    }
    Sleep(5); //'Appropriate delay after Sys_SetAntenna operating


    //============================ Success Tips ==========================
    //Beep 200 ms
    status = Sys_SetBuzzer(g_hDevice, 20);
    if(status != 0)
    {
        lb_info->Caption = "Sys_SetBuzzer failed !";
        return;
    }

    //Tips
    lb_info->Caption ="Connect reader succeed !";
}

//---------------------------------------------------------------------------

