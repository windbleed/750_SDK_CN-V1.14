object frm_main: Tfrm_main
  Left = 148
  Top = 210
  Width = 465
  Height = 443
  Caption = 'frm_main'
  Color = clBtnFace
  Font.Charset = GB2312_CHARSET
  Font.Color = clWindowText
  Font.Height = -16
  Font.Name = 'ËÎÌו'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 16
  object Label1: TLabel
    Left = 118
    Top = 12
    Width = 225
    Height = 29
    Caption = 'MIFARE ONE DEMO'
    Font.Charset = GB2312_CHARSET
    Font.Color = clWindowText
    Font.Height = -29
    Font.Name = 'ËÎÌו'
    Font.Style = []
    ParentFont = False
  end
  object Label4: TLabel
    Left = 50
    Top = 208
    Width = 48
    Height = 16
    Caption = 'Block:'
  end
  object lb_info: TLabel
    Left = 50
    Top = 374
    Width = 8
    Height = 16
  end
  object le_kh: TLabeledEdit
    Left = 80
    Top = 64
    Width = 137
    Height = 24
    EditLabel.Width = 32
    EditLabel.Height = 16
    EditLabel.Caption = 'UID:'
    LabelPosition = lpLeft
    LabelSpacing = 3
    TabOrder = 0
  end
  object rg_key: TRadioGroup
    Left = 48
    Top = 112
    Width = 361
    Height = 69
    Caption = 'Key Type'
    ItemIndex = 0
    Items.Strings = (
      'Key A'
      'Key B')
    TabOrder = 1
  end
  object ed_key: TEdit
    Left = 226
    Top = 136
    Width = 157
    Height = 24
    TabOrder = 2
    Text = 'FFFFFFFFFFFF'
  end
  object cb_kh: TComboBox
    Left = 116
    Top = 204
    Width = 93
    Height = 24
    ItemHeight = 16
    ItemIndex = 0
    TabOrder = 3
    Text = '0'
    Items.Strings = (
      '0'
      '1')
  end
  object le_sj: TLabeledEdit
    Left = 50
    Top = 266
    Width = 359
    Height = 24
    EditLabel.Width = 40
    EditLabel.Height = 16
    EditLabel.Caption = 'Data:'
    LabelPosition = lpAbove
    LabelSpacing = 3
    TabOrder = 4
  end
  object Button1: TButton
    Left = 122
    Top = 318
    Width = 63
    Height = 27
    Caption = 'Request'
    TabOrder = 5
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 344
    Top = 318
    Width = 65
    Height = 27
    Caption = 'Halt'
    TabOrder = 6
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 270
    Top = 318
    Width = 67
    Height = 27
    Caption = 'Write'
    TabOrder = 7
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 196
    Top = 318
    Width = 61
    Height = 27
    Caption = 'Read'
    TabOrder = 8
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 48
    Top = 318
    Width = 65
    Height = 27
    Caption = 'Connect'
    TabOrder = 9
    OnClick = Button5Click
  end
end
