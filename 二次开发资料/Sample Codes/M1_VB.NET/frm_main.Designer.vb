<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frm_main
#Region "Windows 窗体设计器生成的代码 "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'此调用是 Windows 窗体设计器所必需的。
		InitializeComponent()
	End Sub
	'窗体重写释放，以清理组件列表。
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Windows 窗体设计器所必需的
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents Command4 As System.Windows.Forms.Button
	Public WithEvents Command3 As System.Windows.Forms.Button
	Public WithEvents Command2 As System.Windows.Forms.Button
	Public WithEvents Command1 As System.Windows.Forms.Button
	Public WithEvents tx_sj As System.Windows.Forms.TextBox
	Public WithEvents cb_kh As System.Windows.Forms.ComboBox
	Public WithEvents tx_key As System.Windows.Forms.TextBox
	Public WithEvents op_b As System.Windows.Forms.RadioButton
	Public WithEvents op_a As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents tx_kh As System.Windows.Forms.TextBox
	Public WithEvents lb_info As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器来修改它。
    '不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Command4 = New System.Windows.Forms.Button
        Me.Command3 = New System.Windows.Forms.Button
        Me.Command2 = New System.Windows.Forms.Button
        Me.Command1 = New System.Windows.Forms.Button
        Me.tx_sj = New System.Windows.Forms.TextBox
        Me.cb_kh = New System.Windows.Forms.ComboBox
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.tx_key = New System.Windows.Forms.TextBox
        Me.op_b = New System.Windows.Forms.RadioButton
        Me.op_a = New System.Windows.Forms.RadioButton
        Me.tx_kh = New System.Windows.Forms.TextBox
        Me.lb_info = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Command0 = New System.Windows.Forms.Button
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Command4
        '
        Me.Command4.BackColor = System.Drawing.SystemColors.Control
        Me.Command4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command4.Location = New System.Drawing.Point(428, 299)
        Me.Command4.Name = "Command4"
        Me.Command4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command4.Size = New System.Drawing.Size(78, 28)
        Me.Command4.TabIndex = 16
        Me.Command4.Text = "Halt"
        Me.Command4.UseVisualStyleBackColor = False
        '
        'Command3
        '
        Me.Command3.BackColor = System.Drawing.SystemColors.Control
        Me.Command3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command3.Location = New System.Drawing.Point(340, 299)
        Me.Command3.Name = "Command3"
        Me.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command3.Size = New System.Drawing.Size(78, 28)
        Me.Command3.TabIndex = 15
        Me.Command3.Text = "Write"
        Me.Command3.UseVisualStyleBackColor = False
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Location = New System.Drawing.Point(252, 299)
        Me.Command2.Name = "Command2"
        Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command2.Size = New System.Drawing.Size(78, 28)
        Me.Command2.TabIndex = 14
        Me.Command2.Text = "Read"
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(158, 299)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(84, 28)
        Me.Command1.TabIndex = 13
        Me.Command1.Text = "Request"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'tx_sj
        '
        Me.tx_sj.AcceptsReturn = True
        Me.tx_sj.BackColor = System.Drawing.SystemColors.Window
        Me.tx_sj.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tx_sj.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tx_sj.Location = New System.Drawing.Point(148, 244)
        Me.tx_sj.MaxLength = 0
        Me.tx_sj.Name = "tx_sj"
        Me.tx_sj.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tx_sj.Size = New System.Drawing.Size(338, 27)
        Me.tx_sj.TabIndex = 12
        '
        'cb_kh
        '
        Me.cb_kh.BackColor = System.Drawing.SystemColors.Window
        Me.cb_kh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cb_kh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_kh.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cb_kh.Location = New System.Drawing.Point(196, 201)
        Me.cb_kh.Name = "cb_kh"
        Me.cb_kh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cb_kh.Size = New System.Drawing.Size(114, 24)
        Me.cb_kh.TabIndex = 10
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.tx_key)
        Me.Frame1.Controls.Add(Me.op_b)
        Me.Frame1.Controls.Add(Me.op_a)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(70, 116)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(422, 65)
        Me.Frame1.TabIndex = 5
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Key"
        '
        'tx_key
        '
        Me.tx_key.AcceptsReturn = True
        Me.tx_key.BackColor = System.Drawing.SystemColors.Window
        Me.tx_key.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tx_key.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tx_key.Location = New System.Drawing.Point(216, 24)
        Me.tx_key.MaxLength = 0
        Me.tx_key.Name = "tx_key"
        Me.tx_key.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tx_key.Size = New System.Drawing.Size(182, 27)
        Me.tx_key.TabIndex = 8
        Me.tx_key.Text = "FFFFFFFFFFFF"
        '
        'op_b
        '
        Me.op_b.BackColor = System.Drawing.SystemColors.Control
        Me.op_b.Cursor = System.Windows.Forms.Cursors.Default
        Me.op_b.ForeColor = System.Drawing.SystemColors.ControlText
        Me.op_b.Location = New System.Drawing.Point(118, 24)
        Me.op_b.Name = "op_b"
        Me.op_b.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.op_b.Size = New System.Drawing.Size(78, 25)
        Me.op_b.TabIndex = 7
        Me.op_b.TabStop = True
        Me.op_b.Text = "Key B"
        Me.op_b.UseVisualStyleBackColor = False
        '
        'op_a
        '
        Me.op_a.BackColor = System.Drawing.SystemColors.Control
        Me.op_a.Checked = True
        Me.op_a.Cursor = System.Windows.Forms.Cursors.Default
        Me.op_a.ForeColor = System.Drawing.SystemColors.ControlText
        Me.op_a.Location = New System.Drawing.Point(16, 24)
        Me.op_a.Name = "op_a"
        Me.op_a.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.op_a.Size = New System.Drawing.Size(75, 23)
        Me.op_a.TabIndex = 6
        Me.op_a.TabStop = True
        Me.op_a.Text = "Key A"
        Me.op_a.UseVisualStyleBackColor = False
        '
        'tx_kh
        '
        Me.tx_kh.AcceptsReturn = True
        Me.tx_kh.BackColor = System.Drawing.SystemColors.Window
        Me.tx_kh.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.tx_kh.ForeColor = System.Drawing.SystemColors.WindowText
        Me.tx_kh.Location = New System.Drawing.Point(216, 75)
        Me.tx_kh.MaxLength = 0
        Me.tx_kh.Name = "tx_kh"
        Me.tx_kh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tx_kh.Size = New System.Drawing.Size(164, 27)
        Me.tx_kh.TabIndex = 4
        '
        'lb_info
        '
        Me.lb_info.BackColor = System.Drawing.SystemColors.Control
        Me.lb_info.Cursor = System.Windows.Forms.Cursors.Default
        Me.lb_info.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lb_info.Location = New System.Drawing.Point(60, 352)
        Me.lb_info.Name = "lb_info"
        Me.lb_info.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lb_info.Size = New System.Drawing.Size(448, 31)
        Me.lb_info.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(66, 247)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(64, 23)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Data:"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(66, 203)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(118, 23)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Block(0-63)"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(66, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(136, 19)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Serial Number:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(202, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(192, 31)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "MIFARE ONE DEMO"
        '
        'Command0
        '
        Me.Command0.Location = New System.Drawing.Point(63, 299)
        Me.Command0.Name = "Command0"
        Me.Command0.Size = New System.Drawing.Size(84, 28)
        Me.Command0.TabIndex = 20
        Me.Command0.Text = "Connect"
        Me.Command0.UseVisualStyleBackColor = True
        '
        'frm_main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(568, 397)
        Me.Controls.Add(Me.Command0)
        Me.Controls.Add(Me.Command4)
        Me.Controls.Add(Me.Command3)
        Me.Controls.Add(Me.Command2)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.tx_sj)
        Me.Controls.Add(Me.cb_kh)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.tx_kh)
        Me.Controls.Add(Me.lb_info)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("PMingLiU", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frm_main"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "VB DEMO"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Command0 As System.Windows.Forms.Button
#End Region
End Class