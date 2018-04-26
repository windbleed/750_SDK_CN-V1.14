unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls,declaredll,strutils;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    le_xlh: TLabeledEdit;
    le_akey: TLabeledEdit;
    cb_kh: TComboBox;
    Label2: TLabel;
    le_sj: TLabeledEdit;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    lb_info: TLabel;
    rg_key: TRadioGroup;
    Button5: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  g_hDevice:Pointer = Pointer(-1);  //Must init as -1
implementation

{$R *.dfm}

//===== Get card serial number and select card =====
procedure TForm1.Button1Click(Sender: TObject);
var
   i,status:integer;
   w1:word;
   b1:byte;
   buf1:array[0..200] of byte;
   s1:string;
begin
  //Check whether the reader is connected or not
  if(true<>Sys_IsOpen(g_hDevice))then
  begin
      lb_info.Caption := 'Please connect the device firstly !';
      exit;
  end;

   //Request
   status := TyA_Request(g_hDevice, $52, w1);
   if(status<>0)then
   begin
       lb_info.Caption := 'TyA_Request failed !';
       exit;
   end;

   //Anticollision
   status := TyA_Anticollision(g_hDevice, 0, buf1[0], b1);
   if(status<>0)then
   begin
       lb_info.Caption := 'TyA_Anticollision failed !';
       exit;
   end;

   s1 := '';
   for i:=0 to b1-1 do begin
       s1 := s1+inttohex(buf1[i], 2);
   end;
   le_xlh.Text := s1;

   //Select card
   status := TyA_Select(g_hDevice, buf1[0], 4, b1);
   if(status<>0)then
   begin
       lb_info.Caption := 'TyA_Select failed !';
       exit;
   end;

   lb_info.Caption := 'Select card succeed !';
end;

//================= Halt card ==================
procedure TForm1.Button4Click(Sender: TObject);
var
    status:integer;
begin
   //Check whether the reader is connected or not
   if(true<>Sys_IsOpen(g_hDevice))then
   begin
       lb_info.Caption := 'Please connect the device firstly !';
       exit;
   end;

  //Halt
   status := TyA_Halt(g_hDevice);
   if(status<>0)then
   begin
       lb_info.Caption := 'TyA_Halt failed !';
       exit;
   end;

   lb_info.Caption := 'Halt succeed !';
end;

//================= Read card ==================
procedure TForm1.Button2Click(Sender: TObject);
var
   i,j:integer;
   b1,b_kh:byte;
   buf1,buf2:array[0..200] of byte;
   s1:string;
begin
   //Check whether the reader is connected or not
   if(true<>Sys_IsOpen(g_hDevice)) then
   begin
       lb_info.Caption := 'Please connect the device firstly !';
       exit;
   end;

   //Get block addr
   b_kh:=cb_kh.ItemIndex;

   //Get key
   s1:=trim(le_akey.Text );
   if(length(s1)<>12)then
   begin
       lb_info.Caption := 'Key length error !';
       le_akey.SetFocus;
       exit;
   end;
   for i:=0 to 5 do begin
       val('$'+midstr(s1,i*2+1,2),buf2[i],j);
   end;

   //Get key type
   if(rg_key.ItemIndex = 0) then
   begin
      b1 := $60;
   end
   else
   begin
      b1 := $61;
   end;

   //Authentication
   i:=TyA_CS_Authentication2(g_hDevice, b1, b_kh, buf2[0]);
   if(i<>0)then
   begin
       lb_info.Caption := 'TyA_CS_Authentication2 failed !';
       exit;
   end;

   //Read card
   i := TyA_CS_Read(g_hDevice, b_kh, buf1[0], b1);
   if(i<>0)then
   begin
       lb_info.Caption := 'TyA_CS_Read failed !';
       exit;
   end;

   s1 := '';
   for i:=0 to b1-1 do begin
       s1 := s1+inttohex(buf1[i],2);
   end;
   le_sj.Text := s1;

   lb_info.Caption := 'Read succeed !';
end;

//================= Write card ==================
procedure TForm1.Button3Click(Sender: TObject);
var
   i,j,status:integer;
   b1,b_kh:byte;
   buf1,buf2:array[0..200] of byte;
   s1:string;
begin
  //Check whether the reader is connected or not
  if(true<>Sys_IsOpen(g_hDevice))then
  begin
      lb_info.Caption := 'Please connect the device firstly !';
      exit;
  end;

  //Get block addr
   b_kh := cb_kh.ItemIndex;

   //Get key
   s1 := trim(le_akey.Text );
   if(length(s1)<>12)then
   begin
       lb_info.Caption := 'Key length error !';
       le_akey.SetFocus;
       exit;
   end;
   for i:=0 to 5 do begin
       val('$'+midstr(s1,i*2+1,2),buf2[i],j);
   end;

   //Get the data to be written
   s1 := trim(le_sj.Text );
   if(length(s1)<>32)then
   begin
       lb_info.Caption:='Data length error !';
       le_sj.SetFocus;
       exit;
   end;
   for i:=0 to 15 do begin
       val('$'+midstr(s1,i*2+1,2),buf1[i],j);
   end;

   //Get key type
   if(rg_key.ItemIndex =0)  then
   begin
      b1 := $60;
   end
   else
   begin
      b1 := $61;
   end;

   //Authentication
   status := TyA_CS_Authentication2(g_hDevice, b1, b_kh, buf2[0]);
   if(status<>0)then
   begin
       lb_info.Caption := 'TyA_CS_Authentication2 failed !';
       exit;
   end;

   // Write card
   status := TyA_CS_Write(g_hDevice, b_kh, buf1[0]);
   if(status<>0)then
   begin
       lb_info.Caption := 'TyA_CS_Write failed !';
       exit;
   end;

   lb_info.Caption := 'Write succeed !';

end;


//================= Connect reader ==================
procedure TForm1.Button5Click(Sender: TObject);
var
  status:integer;
begin
   //-------------------------- Connect reader -------------------------
  //Check whether the reader is connected or not
  //If the reader is already open , close it firstly
  if(true=Sys_IsOpen(g_hDevice))then
  begin
      status := Sys_Close(g_hDevice);
      if(status<>0)then
      begin
          lb_info.Caption := 'Sys_Close failed !';
          exit;
      end;
  end;

  //Connect
  status := Sys_Open(g_hDevice, 0, $0416, $8020);
  if(status<>0)then
  begin
      lb_info.Caption := 'Sys_Open failed !';
      exit;
  end;

  //------------- Init the reader before operating the card -------------
  //Close antenna of the reader
  status := Sys_SetAntenna(g_hDevice, 0);
  if(status<>0)then
  begin
      lb_info.Caption := 'Sys_SetAntenna failed !';
      exit;
  end;
  Sleep(5); //Appropriate delay after Sys_SetAntenna operating

  //Set the reader's working mode
  status := Sys_InitType(g_hDevice, Byte('A'));
  if(status<>0)then
  begin
      lb_info.Caption := 'Sys_InitType failed !';
      exit;
  end;
  Sleep(5); //Appropriate delay after Sys_InitType operating

  //Open antenna of the reader
  status := Sys_SetAntenna(g_hDevice, 1);
  if(status<>0)then
  begin
      lb_info.Caption := 'Sys_SetAntenna failed !';
      exit;
  end;
  Sleep(5); //Appropriate delay after Sys_SetAntenna operating

  //---------------------------- Success Tips ----------------------------
  //Beep 200 ms
  status := Sys_SetBuzzer(g_hDevice, 20);
  if(status<>0)then
  begin
      lb_info.Caption := 'Sys_SetBuzzer failed !';
      exit;
  end;

  lb_info.Caption := 'Connect succeed !';
end;

end.
