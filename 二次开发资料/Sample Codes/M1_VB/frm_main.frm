VERSION 5.00
Begin VB.Form frm_main 
   Caption         =   "VB DEMO"
   ClientHeight    =   6075
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7575
   LinkTopic       =   "Form1"
   ScaleHeight     =   6075
   ScaleWidth      =   7575
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton Command5 
      Caption         =   "Connect"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   840
      TabIndex        =   16
      Top             =   4560
      Width           =   1095
   End
   Begin VB.CommandButton Command4 
      Caption         =   "Halt"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   5640
      TabIndex        =   14
      Top             =   4560
      Width           =   1035
   End
   Begin VB.CommandButton Command3 
      Caption         =   "Write"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   4440
      TabIndex        =   13
      Top             =   4560
      Width           =   1035
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Read"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   3240
      TabIndex        =   12
      Top             =   4560
      Width           =   1035
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Request"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2040
      TabIndex        =   11
      Top             =   4560
      Width           =   1035
   End
   Begin VB.TextBox tx_sj 
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   1980
      TabIndex        =   10
      Top             =   3870
      Width           =   4515
   End
   Begin VB.ComboBox cb_kh 
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      ItemData        =   "frm_main.frx":0000
      Left            =   2580
      List            =   "frm_main.frx":0002
      Style           =   2  'Dropdown List
      TabIndex        =   8
      Top             =   3150
      Width           =   1515
   End
   Begin VB.Frame Frame1 
      Caption         =   "Key Type"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   975
      Left            =   930
      TabIndex        =   3
      Top             =   1800
      Width           =   5625
      Begin VB.TextBox tx_key 
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   2880
         TabIndex        =   6
         Text            =   "FFFFFFFFFFFF"
         Top             =   360
         Width           =   2445
      End
      Begin VB.OptionButton op_b 
         Caption         =   "Key B"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   1560
         TabIndex        =   5
         Top             =   360
         Width           =   1035
      End
      Begin VB.OptionButton op_a 
         Caption         =   "Key A"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   210
         TabIndex        =   4
         Top             =   390
         Value           =   -1  'True
         Width           =   1005
      End
   End
   Begin VB.TextBox tx_kh 
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   360
      Left            =   2880
      TabIndex        =   2
      Top             =   1200
      Width           =   2205
   End
   Begin VB.Label lb_info 
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   465
      Left            =   840
      TabIndex        =   15
      Top             =   5400
      Width           =   6135
   End
   Begin VB.Label Label6 
      Caption         =   "Data:"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   990
      TabIndex        =   9
      Top             =   3900
      Width           =   855
   End
   Begin VB.Label Label5 
      Caption         =   "Block(0-63)"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   345
      Left            =   990
      TabIndex        =   7
      Top             =   3180
      Width           =   1395
   End
   Begin VB.Label Label4 
      Caption         =   "Serial Number:"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   285
      Left            =   1020
      TabIndex        =   1
      Top             =   1230
      Width           =   1815
   End
   Begin VB.Label Label3 
      Caption         =   "MIFARE ONE DEMO"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   13.5
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   2520
      TabIndex        =   0
      Top             =   360
      Width           =   3345
   End
End
Attribute VB_Name = "frm_main"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

'===== Get card serial number and select card =====
Private Sub Command1_Click()

Dim status As Long
Dim i As Integer
Dim j As Integer
Dim buf1(200) As Byte
Dim b1 As Byte
Dim s1 As String

'Check whether the reader is connected or not
If (False = Sys_IsOpen(g_hDevice)) Then
    lb_info.Caption = "Please connect the device firstly !"
    Exit Sub
End If

'Request
status = TyA_Request(g_hDevice, &H52, j)
If (status <> 0) Then
    lb_info.Caption = "TyA_Request failed !"
    Exit Sub
End If

'Anticollision
status = TyA_Anticollision(g_hDevice, 0, buf1(0), b1)
If (status <> 0) Then
    lb_info.Caption = "TyA_Anticollision failed !"
    Exit Sub
End If
s1 = ""
For i = 0 To b1 - 1
    s1 = s1 & Right("00" & Hex(buf1(i)), 2)
Next i
tx_kh.Text = s1

'Select card
status = TyA_Select(g_hDevice, buf1(0), 4, b1)
If (status <> 0) Then
    lb_info.Caption = "TyA_Select failed !"
    Exit Sub
End If

lb_info.Caption = "Select card succeed !"

End Sub


'================= Read card ==================
Private Sub Command2_Click()

Dim status As Long
Dim i As Integer
Dim m As Long
Dim buf1(200) As Byte
Dim buf2(200) As Byte
Dim s1 As String
Dim b1 As Byte
Dim b2 As Byte
Dim b3 As Byte

'Get key
s1 = Trim(tx_key.Text)
If (Len(s1) <> 12) Then
    lb_info.Caption = "Wrong Key Length!"
    tx_key.SetFocus
    Exit Sub
End If
For i = 0 To 5
    buf1(i) = Val("&H" & Mid(s1, i * 2 + 1, 2))
Next i

'Get block addr
m = cb_kh.ListIndex
If (m = -1) Then
    lb_info.Caption = "Select Block Please !"
    Exit Sub
End If
b3 = CByte(m)

'Get key type
If (op_a.Value) Then
   b1 = &H60
End If
If (op_b.Value) Then
   b1 = &H61
End If

'Check whether the reader is connected or not
If (False = Sys_IsOpen(g_hDevice)) Then
    lb_info.Caption = "Please connect the device firstly !"
    Exit Sub
End If

'Authentication
status = TyA_CS_Authentication2(g_hDevice, b1, b3, buf1(0))
If (status <> 0) Then
    lb_info.Caption = "TyA_CS_Authentication2 failed ！"
    Exit Sub
End If

'Read card
status = TyA_CS_Read(g_hDevice, b3, buf2(0), b2)
If (status <> 0) Then
    lb_info.Caption = "TyA_CS_Read failed !"
    Exit Sub
End If
s1 = ""
For i = 0 To b2 - 1
    s1 = s1 & Right("00" & Hex(buf2(i)), 2)
Next i
tx_sj.Text = s1

lb_info.Caption = "Read succeed !"

End Sub


'================= Write card ==================
Private Sub Command3_Click()

Dim status As Long
Dim i As Integer
Dim m As Long
Dim buf1(200) As Byte
Dim buf2(200) As Byte
Dim s1 As String
Dim b1 As Byte
Dim b2 As Byte
Dim b3 As Byte

'Get key
s1 = Trim(tx_key.Text)
If (Len(s1) <> 12) Then
    lb_info.Caption = "Wrong Key Length!"
    tx_key.SetFocus
    Exit Sub
End If
For i = 0 To 5
    buf1(i) = Val("&H" & Mid(s1, i * 2 + 1, 2))
Next i

'Get block addr
m = cb_kh.ListIndex
If (m = -1) Then
    lb_info.Caption = "Select Block Please !"
    Exit Sub
End If
b3 = CByte(m)

'Get key type
If (op_a.Value) Then
   b1 = &H60
End If
If (op_b.Value) Then
   b1 = &H61
End If

'Get data
s1 = Trim(tx_sj.Text)
If (Len(s1) <> 32) Then
    lb_info.Caption = "Wrong Data length !"
    tx_sj.SetFocus
    Exit Sub
End If
For i = 0 To 15
    buf2(i) = Val("&H" & Mid(s1, i * 2 + 1, 2))
Next i

'Check whether the reader is connected or not
If (False = Sys_IsOpen(g_hDevice)) Then
    lb_info.Caption = "Please connect the device firstly !"
    Exit Sub
End If

'Authentication
status = TyA_CS_Authentication2(g_hDevice, b1, b3, buf1(0))
If (status <> 0) Then
    lb_info.Caption = "TyA_CS_Authentication2 failed !"
    Exit Sub
End If

'Write card
status = TyA_CS_Write(g_hDevice, b3, buf2(0))
If (status <> 0) Then
    lb_info.Caption = "TyA_CS_Write failed ！"
    Exit Sub
End If

lb_info.Caption = "Write succeed !"

End Sub


'=================== Halt card =====================
Private Sub Command4_Click()
Dim status As Integer

'Check whether the reader is connected or not
If (False = Sys_IsOpen(g_hDevice)) Then
    lb_info.Caption = "Please connect the device firstly !"
    Exit Sub
End If

'Halt card
status = TyA_Halt(g_hDevice)
If (status <> 0) Then
    lb_info.Caption = "TyA_Halt failed !"
    Exit Sub
End If

lb_info.Caption = "Halt succeed !"

End Sub


'============== Connect and init reader ============
Private Sub Command5_Click()
Dim status As Long

'------------ Connect reader --------------
'Check whether the reader is connected or not
'If the reader is already open , close it firstly
If (False <> Sys_IsOpen(g_hDevice)) Then
    status = Sys_Close(g_hDevice)
    If (status <> 0) Then
        lb_info.Caption = "Sys_Close failed !"
        Exit Sub
    End If
End If

'Connect the reader
status = Sys_Open(g_hDevice, 0, &H416, &H8020)
If (status <> 0) Then
    lb_info.Caption = "Sys_Open failed !"
    Exit Sub
End If

'---------- Init the reader before operating the card ------------
'Close antenna of the reader
status = Sys_SetAntenna(g_hDevice, 0)
If (status <> 0) Then
    lb_info.Caption = "Sys_SetAntenna failed !"
    Exit Sub
End If
Sleep (5) 'Appropriate delay after Sys_SetAntenna operating

'Set the reader's working mode, type = 'A'
status = Sys_InitType(g_hDevice, &H41)
If (status <> 0) Then
    lb_info.Caption = "Sys_InitType failed !"
    Exit Sub
End If
Sleep (5) 'Appropriate delay after Sys_InitType operating

'Open antenna of the reader
status = Sys_SetAntenna(g_hDevice, 1)
If (status <> 0) Then
    lb_info.Caption = "Sys_SetAntenna failed !"
    Exit Sub
End If
Sleep (5) 'Appropriate delay after Sys_SetAntenna operating

'------------ Success Tips ------------
'Beep 200 ms
status = Sys_SetBuzzer(g_hDevice, 20)
If (status <> 0) Then
    lb_info.Caption = "Sys_SetBuzzer failed !"
    Exit Sub
End If

lb_info.Caption = "Connect succeed !"

End Sub


'===================================================
Private Sub Form_Load()

Dim i As Integer

'g_hDevice must be initialized as -1 before use
g_hDevice = -1

For i = 0 To 63
    cb_kh.AddItem CStr(i), i
Next i
cb_kh.ListIndex = 1

End Sub

