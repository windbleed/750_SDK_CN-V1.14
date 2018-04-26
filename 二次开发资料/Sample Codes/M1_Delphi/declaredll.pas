unit declaredll;

interface

    //============================================== System Function ====================================================
    function Sys_GetDeviceNum(vid:Word; pid:Word; var pNum:LongInt):integer;stdcall;external 'hfrdapi.dll';

    function Sys_GetHidSerialNumberStr(deviceIndex:LongInt;
                                       vid:Word;
                                       pid:Word;
                                       deviceString:PChar;
                                       deviceStringLength:LongInt):integer;stdcall;external 'hfrdapi.dll';

    function Sys_Open(var device:Pointer;
                      index:LongInt;
                      vid:Word;
                      pid:Word):integer;stdcall;external 'hfrdapi.dll';

    function Sys_IsOpen(device:Pointer):LongBool;stdcall;external 'hfrdapi.dll';

    function Sys_Close(var device:Pointer):integer;stdcall;external 'hfrdapi.dll';

    function Sys_SetLight(device:Pointer; color:Byte):integer;stdcall;external 'hfrdapi.dll';

    function Sys_SetBuzzer(device:Pointer; msec:Byte):integer;stdcall;external 'hfrdapi.dll';

    function Sys_SetAntenna(device:Pointer; mode:Byte):integer;stdcall;external 'hfrdapi.dll';

    function Sys_InitType(device:Pointer; workType:Byte):integer;stdcall;external 'hfrdapi.dll';
    

    //=============================================== M1 Function =======================================================
    function TyA_Request(device:Pointer; mode:Byte; var pTagType:Word):integer;stdcall;external 'hfrdapi.dll';

    function TyA_Anticollision(device:Pointer;
                               bcnt:Byte;
                               var pSnr:Byte;
                               var pLen:Byte):integer;stdcall;external 'hfrdapi.dll';

    function TyA_Select(device:Pointer;
                        var pSnr:Byte;
                        snrLen:Byte;
                        var pSak:Byte):integer;stdcall;external 'hfrdapi.dll';

    function TyA_Halt(device:Pointer):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_Authentication2(device:Pointer;
                                    mode:Byte;
                                    block:Byte;
                                    var pKey:Byte):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_Read(device:Pointer;
                         block:Byte;
                         var pData:Byte;
                         var pLen:Byte):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_Write(device:Pointer; block:Byte; var pData:Byte):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_InitValue(device:Pointer; block:Byte; value:LongInt):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_ReadValue(device:Pointer; block:Byte; var pValue:LongInt):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_Decrement(device:Pointer; block:Byte; value:LongInt):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_Increment(device:Pointer; block:Byte; value:LongInt):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_Restore(device:Pointer; block:Byte):integer;stdcall;external 'hfrdapi.dll';

    function TyA_CS_Transfer(device:Pointer; block:Byte):integer;stdcall;external 'hfrdapi.dll';

implementation

end.
