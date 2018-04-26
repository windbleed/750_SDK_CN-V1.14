Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frm_main
	Inherits System.Windows.Forms.Form

    Private Sub Command0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Command0.Click

        Dim byStatus As Byte
        Dim deviceOpen As Boolean
        Dim readerNum As UInteger

        '=========================== Connect reader =========================
        'Get reader number, USB VID = 0x0416, USB PID = 0x8020
        byStatus = Sys_GetDeviceNum(&H416S, &H8020S, readerNum)
        If (byStatus <> 0) Then
            lb_info.Text = "Sys_GetDeviceNum fail !"
            Exit Sub
        End If

        If (readerNum < 1) Then
            lb_info.Text = "No reader connected to the PC !"
            Exit Sub
        End If

        'Check whether the reader is connected or not
        deviceOpen = Sys_IsOpen(g_hDevice)
        If (True = deviceOpen) Then
            'If the reader is already open , close it firstly
            byStatus = Sys_Close(g_hDevice)
            If (byStatus <> 0) Then
                lb_info.Text = "Sys_Close fail !"
                Exit Sub
            End If

        End If

        'Connect
        byStatus = Sys_Open(g_hDevice, 0, &H416S, &H8020S)
        If (byStatus <> 0) Then
            lb_info.Text = "Sys_Open fail !"
            Exit Sub
        End If

        '============= Init the reader before operating the card ============
        'Close antenna of the reader
        byStatus = Sys_SetAntenna(g_hDevice, 0)
        If (byStatus <> 0) Then
            lb_info.Text = "Sys_SetAntenna fail !"
            Exit Sub
        End If
        System.Threading.Thread.Sleep(5)  'Appropriate delay after Sys_SetAntenna operating

        'Set the reader's working mode
        byStatus = Sys_InitType(g_hDevice, Asc("A"))
        If (byStatus <> 0) Then
            lb_info.Text = "Sys_InitType fail !"
            Exit Sub
        End If
        System.Threading.Thread.Sleep(5)  'Appropriate delay after Sys_InitType operating

        'Open antenna of the reader
        byStatus = Sys_SetAntenna(g_hDevice, 1)
        If (byStatus <> 0) Then
            lb_info.Text = "Sys_SetAntenna fail !"
            Exit Sub
        End If
        System.Threading.Thread.Sleep(5)  'Appropriate delay after Sys_SetAntenna operating

        '============================ Success Tips ==========================
        'Beep 200 ms
        byStatus = Sys_SetBuzzer(g_hDevice, 20)
        If (byStatus <> 0) Then
            lb_info.Text = "Sys_SetBuzzer fail !"
            Exit Sub
        End If

        'Tips
        lb_info.Text = "Connect succeed !"

    End Sub

	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Dim i As Integer
        Dim j As Short
        Dim buf1(200) As Byte
        Dim b1 As Byte
        Dim s1 As String
        Dim deviceOpen As Boolean
        Dim byStatus As Byte

        'Check whether the reader is connected or not
        deviceOpen = Sys_IsOpen(g_hDevice)
        If (False = deviceOpen) Then
            'If the reader is not open
            lb_info.Text = "Please connect the device firstly !"
            Exit Sub
        End If

        'Request,return the card type
        byStatus = TyA_Request(g_hDevice, &H52S, j)
        If (byStatus <> 0) Then
            lb_info.Text = "Request fail !"
            Exit Sub
        End If

        'Anticollision ,return the card serial number
        byStatus = TyA_Anticollision(g_hDevice, 0, buf1(0), b1)
        If (byStatus <> 0) Then
            lb_info.Text = "Anticollision fail !"
            Exit Sub
        End If
        s1 = ""
        For i = 0 To b1 - 1
            s1 = s1 & VB.Right("00" & Hex(buf1(i)), 2)
        Next i
        tx_kh.Text = s1

        'Select card
        byStatus = TyA_Select(g_hDevice, buf1(0), 4, b1)
        If (byStatus <> 0) Then
            lb_info.Text = "Select card fail !"
            Exit Sub
        End If

        lb_info.Text = "Select card succeed !"

    End Sub
	
	Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
        Dim i, m As Integer
        Dim buf1(200) As Byte
        Dim buf2(200) As Byte
        Dim s1 As String
        Dim b2, b1, b3 As Byte
        Dim byStatus As Byte
        Dim deviceOpen As Boolean

        s1 = Trim(tx_key.Text)
        If (Len(s1) <> 12) Then
            lb_info.Text = "Wrong key length !"
            tx_key.Focus()
            Exit Sub
        End If
        For i = 0 To 5
            buf1(i) = Val("&H" & Mid(s1, i * 2 + 1, 2))
        Next i
        m = cb_kh.SelectedIndex
        If (m = -1) Then
            lb_info.Text = "Please select block !"
            Exit Sub
        End If
        If (op_a.Checked) Then
            b1 = &H60S
        End If
        If (op_b.Checked) Then
            b1 = &H61S
        End If
        b3 = CByte(m)

        'Check whether the reader is connected or not
        deviceOpen = Sys_IsOpen(g_hDevice)
        If (False = deviceOpen) Then
            'If the reader is not open
            lb_info.Text = "Please connect the device firstly !"
            Exit Sub
        End If

        'Authentication
        byStatus = TyA_CS_Authentication2(g_hDevice, b1, b3, buf1(0))
        If (byStatus <> 0) Then
            lb_info.Text = "Authentication fail !"
            Exit Sub
        End If

        'Read card
        byStatus = TyA_CS_Read(g_hDevice, b3, buf2(0), b2)
        If (byStatus <> 0) Then
            lb_info.Text = "Read card fail !"
            Exit Sub
        End If
        s1 = ""
        For i = 0 To b2 - 1
            s1 = s1 & VB.Right("00" & Hex(buf2(i)), 2)
        Next i
        tx_sj.Text = s1
        lb_info.Text = "Read succeed !"

    End Sub
	
	Private Sub Command3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command3.Click
		Dim i, m As Integer
		Dim buf1(200) As Byte
		Dim buf2(200) As Byte
		Dim s1 As String
        Dim b1, b3 As Byte
        Dim deviceOpen As Boolean

		s1 = Trim(tx_key.Text)
		If (Len(s1) <> 12) Then
            lb_info.Text = "Wrong key length!"
			tx_key.Focus()
			Exit Sub
		End If
		For i = 0 To 5
			buf1(i) = Val("&H" & Mid(s1, i * 2 + 1, 2))
		Next i
		m = cb_kh.SelectedIndex
		If (m = -1) Then
            lb_info.Text = "Please select block !"
			Exit Sub
		End If
		If (op_a.Checked) Then
			b1 = &H60s
		End If
		If (op_b.Checked) Then
			b1 = &H61s
		End If
		
		s1 = Trim(tx_sj.Text)
		If (Len(s1) <> 32) Then
            lb_info.Text = "Wrong data length "
			tx_sj.Focus()
			Exit Sub
		End If
		For i = 0 To 15
			buf2(i) = Val("&H" & Mid(s1, i * 2 + 1, 2))
        Next i

        'Check whether the reader is connected or not
        deviceOpen = Sys_IsOpen(g_hDevice)
        If (False = deviceOpen) Then
            'If the reader is not open
            lb_info.Text = "Please connect the device firstly !"
            Exit Sub
        End If

		'Authentication
		b3 = CByte(m)
        i = TyA_CS_Authentication2(g_hDevice, b1, b3, buf1(0))
		If (i <> 0) Then
            lb_info.Text = "Authentication fail !"
			Exit Sub
        End If

		'Write card
        i = TyA_CS_Write(g_hDevice, b3, buf2(0))
		If (i <> 0) Then
            lb_info.Text = "Write card fail !"
			Exit Sub
		End If
		
        lb_info.Text = "Write succeed !"
	End Sub
	
	Private Sub Command4_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command4.Click
        Dim i As Integer
        Dim deviceOpen As Boolean

        'Check whether the reader is connected or not
        deviceOpen = Sys_IsOpen(g_hDevice)
        If (False = deviceOpen) Then
            'If the reader is not open
            lb_info.Text = "Please connect the device firstly !"
            Exit Sub
        End If

        'Halt card
        i = TyA_Halt(g_hDevice)
		If (i <> 0) Then
            lb_info.Text = "Halt fail !"
			Exit Sub
		End If
        lb_info.Text = "Halt succeed !"
	End Sub
	
	Private Sub frm_main_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim i As Integer
		For i = 0 To 63
			cb_kh.Items.Insert(i, CStr(i))
		Next i
        cb_kh.SelectedIndex = 4
	End Sub

    Private Sub cb_ckh_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class