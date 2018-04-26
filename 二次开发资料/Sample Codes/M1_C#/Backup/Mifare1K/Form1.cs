using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using System.Runtime.InteropServices;


namespace Mifare1K
{     

    public partial class Mifare_1K : Form
    {
        [DllImport("kernel32.dll")]
        static extern void Sleep(int dwMilliseconds);


        //=========================== System Function =============================
        [DllImport("hfrdapi.dll")]
        static extern int Sys_GetDeviceNum(UInt16 vid, UInt16 pid, ref UInt32 pNum);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_GetHidSerialNumberStr(UInt32 deviceIndex,
                                                    UInt16 vid,
                                                    UInt16 pid,
                                                    [Out]StringBuilder deviceString,
                                                    UInt32 deviceStringLength);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_Open(ref IntPtr device,
                                   UInt32 index,
                                   UInt16 vid,
                                   UInt16 pid);

        [DllImport("hfrdapi.dll")]
        static extern bool Sys_IsOpen(IntPtr device);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_Close(ref IntPtr device);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_SetLight(IntPtr device, byte color);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_SetBuzzer(IntPtr device, byte msec);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_SetAntenna(IntPtr device, byte mode);

        [DllImport("hfrdapi.dll")]
        static extern int Sys_InitType(IntPtr device, byte type);


        //=========================== M1 Card Function =============================
        [DllImport("hfrdapi.dll")]
        static extern int TyA_Request(IntPtr device, byte mode, ref UInt16 pTagType);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_Anticollision(IntPtr device,
                                            byte bcnt,
                                            byte[] pSnr,
                                            ref byte pLen);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_Select(IntPtr device,
                                     byte[] pSnr,
                                     byte snrLen,
                                     ref byte pSak);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_Halt(IntPtr device);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Authentication2(IntPtr device,
                                                 byte mode,
                                                 byte block,
                                                 byte[] pKey);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Read(IntPtr device,
                                      byte block,
                                      byte[] pData,
                                      ref byte pLen);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Write(IntPtr device, byte block, byte[] pData);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_InitValue(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_ReadValue(IntPtr device, byte block, ref Int32 pValue);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Decrement(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Increment(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Restore(IntPtr device, byte block);  

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CS_Transfer(IntPtr device, byte block);
       


        //==========================================================================
        IntPtr g_hDevice = (IntPtr)(-1); //g_hDevice must init as -1

        static char[] hexDigits = { 
            '0','1','2','3','4','5','6','7',
            '8','9','A','B','C','D','E','F'};

        public static byte GetHexBitsValue(byte ch)
        {
            byte sz= 0;
            if (ch <= '9' && ch >= '0')
                sz = (byte)(ch - 0x30);
            if (ch <= 'F' && ch >= 'A')
                sz = (byte)(ch - 0x37);
            if (ch <= 'f' && ch >= 'a')
                sz = (byte)(ch - 0x57);

            return sz;
        }
        //

        #region byteHEX
        /// <summary>
        /// 单个字节转字字符.
        /// </summary>
        /// <param name="ib">字节.</param>
        /// <returns>转换好的字符.</returns>
        public static String byteHEX(Byte ib)
        {
            String _str = String.Empty;
            try
            {
                char[] Digit = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A',
			    'B', 'C', 'D', 'E', 'F' };
                char[] ob = new char[2];
                ob[0] = Digit[(ib >> 4) & 0X0F];
                ob[1] = Digit[ib & 0X0F];
                _str = new String(ob);
            }
            catch (Exception)
            {
                new Exception("对不起有错。");
            }
            return _str;

        }
        #endregion

        public static string ToHexString(byte[] bytes)
        {
            String hexString=String.Empty;
            for(int i = 0 ;i < bytes.Length;i++)
                hexString += byteHEX(bytes[i]);

            return hexString;
        }



        public static byte[] ToDigitsBytes(string theHex)
        {
            byte[] bytes = new byte[theHex.Length / 2 + (((theHex.Length % 2) > 0)?1:0)];
            for (int i = 0; i < bytes.Length;i++ )
            {
                char lowbits = theHex[i * 2];
                char highbits;

                if ((i * 2 + 1) < theHex.Length)
                    highbits = theHex[i * 2 + 1];
                else
                    highbits = '0';

                int a = (int)GetHexBitsValue((byte)lowbits);
                int b = (int)GetHexBitsValue((byte)highbits);
                bytes[i] = (byte)((a << 4) + b);
            }

            return bytes;
        }

        /**/
        public Mifare_1K()
        {
            InitializeComponent();
        }

        private void Mifare_1K_Load(object sender, EventArgs e)
        {
            cbxSubmass1.Items.Clear();
            cbxSubmass1.Items.Add("1");
            cbxSubmass1.Items.Add("2");
            
            cbxMass1.SelectedIndex = 0;
            cbxSubmass1.SelectedIndex = 0;
            cbxMass2.SelectedIndex = 0;
            cbxSubmass2.SelectedIndex = 0;
            cbxMass3.SelectedIndex = 0;
            cbxSubmass3.SelectedIndex = 0;
            cbxMass4.SelectedIndex = 0;
            cbxSubmass4.SelectedIndex = 0;

            txtSearchPurse.MaxLength = 12;
            textBoxPurseKey.MaxLength = 12;

            txtBoxDataOne2.MaxLength = 32;
            txtBoxDataTwo2.MaxLength = 32;
            txtDataThree2.MaxLength = 32;
            txtKeyA2.MaxLength = 12;
            txtKey2.MaxLength = 8;
            txtKeyB2.MaxLength = 12;
            txtInputKey2.MaxLength = 12;

            txtDataOne3.MaxLength = 32;
            txtDataTwo3.MaxLength = 32;
            txtDataThree3.MaxLength = 32;
            txtKeyA3.MaxLength = 12;
            txtKey3.MaxLength = 8;
            txtKeyB3.MaxLength = 12;
            txtInputKey3.MaxLength = 12;

            txtDataOne4.MaxLength = 32;
            txtDataTwo4.MaxLength = 32;
            txtDataThree4.MaxLength = 32;
            txtKeyA4.MaxLength = 12;
            txtKey4.MaxLength = 8;
            txtKeyB4.MaxLength = 12;
            txtInputKey4.MaxLength = 12;
        }

        

        private void btnRequest_Click(object sender, EventArgs e)
        {            
            int status;
            byte mode = 0x52;
            ushort TagType = 0;
            byte bcnt = 0;
            byte[] dataBuffer = new byte[256];
            byte len = 255;
            byte sak = 0;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < 2 ;i++ )
            {
                status = TyA_Request(g_hDevice, mode, ref TagType);//搜寻所有的卡
                if (status != 0)
                    continue;

                status = TyA_Anticollision(g_hDevice, bcnt, dataBuffer, ref len);//返回卡的序列号
                if (status != 0)
                    continue;

                status = TyA_Select(g_hDevice, dataBuffer, len, ref sak);//锁定一张ISO14443-3 TYPE_A 卡
                if (status != 0)
                    continue;

                String m_cardNo = String.Empty;

                for (int q = 0; q < len; q++)
                {
                    m_cardNo += byteHEX(dataBuffer[q]);
                }
                txtSearchPurse.Text = m_cardNo;                

                break;
            }
        }

        private void btnReqIDL_Click(object sender, EventArgs e)
        {
            byte mode = 0x26;
            ushort TagType = 0;
            int status;
            byte bcnt = 0;
            byte[] dataBuffer = new byte[256];
            //IntPtr pSnr;
            byte len = 255;
            byte sak = 0;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            status = TyA_Request(g_hDevice, mode, ref TagType);//搜寻所有的卡
            if (status != 0)
            {
                MessageBox.Show("TyA_Request failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            status = TyA_Anticollision(g_hDevice, bcnt, dataBuffer, ref len);//返回卡的序列号
            if (status != 0)
            {
                MessageBox.Show("TyA_Anticollision failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            status = TyA_Select(g_hDevice, dataBuffer, len, ref sak);//锁定一张ISO14443-3 TYPE_A 卡
            if (status != 0)
            {
                MessageBox.Show("TyA_Select failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String m_cardNo = String.Empty;

            for (int q = 0; q < len; q++)
            {
                m_cardNo += byteHEX(dataBuffer[q]);
            }
            txtSearchPurse.Text = m_cardNo; 
        }

        private void btnHalt_Click(object sender, EventArgs e)
        {
            int status;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            status = TyA_Halt(g_hDevice);
            if (status != 0)
            {
                MessageBox.Show("TyA_Halt failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void btnPurseInit_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr;
	        byte adr;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbtPurseKeyB.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass1.Text);
            adr = (byte)(Convert.ToByte(cbxSubmass1.Text) + secnr * 4);
         

            byte[] bytesKey = ToDigitsBytes(textBoxPurseKey.Text);

            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if(status != 0)
            {                
                MessageBox.Show("TyA_CS_Authentication2 failed!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (((string)txtPurseDec.Text).Length <= 0)
            {
                MessageBox.Show("not value in purse edit !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }

            Int32 nDecValue = Convert.ToInt32(txtPurseDec.Text);
            status = TyA_CS_InitValue(g_hDevice, adr, nDecValue);
            if (status != 0)
            {                
                MessageBox.Show("TyA_CS_InitValue failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);              
            }            
        }

        private void btnCreditCardIncrement_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr;
            byte adr;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbtPurseKeyB.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass1.Text);
            adr = (byte)(Convert.ToByte(cbxSubmass1.Text) + secnr * 4);

            byte[] bytesKey = ToDigitsBytes(textBoxPurseKey.Text);
            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (((string)txtPurseDec.Text).Length <= 0)
            {
                MessageBox.Show("not value in purse edit !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Int32 nDecValue = Convert.ToInt32(txtPurseDec.Text);

            status = TyA_CS_Increment(g_hDevice, adr, nDecValue);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Increment failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void btnCreditCardDecrement_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr;
            byte adr;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbtPurseKeyB.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass1.Text);
            adr = (byte)(Convert.ToByte(cbxSubmass1.Text) + secnr * 4);

            byte[] bytesKey = ToDigitsBytes(textBoxPurseKey.Text);

            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (((string)txtPurseDec.Text).Length <= 0)
            {
                MessageBox.Show("not value in purse edit !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Int32 nDecValue = Convert.ToInt32(txtPurseDec.Text);

            status = TyA_CS_Decrement(g_hDevice, adr, nDecValue);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Decrement failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreditCardBalance_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr;
            byte adr;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbtPurseKeyB.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass1.Text);
            adr = (byte)(Convert.ToByte(cbxSubmass1.Text) + secnr * 4);

            byte[] bytesKey = ToDigitsBytes(textBoxPurseKey.Text);

            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Int32 nDecValue = 0;
            status = TyA_CS_ReadValue(g_hDevice, adr, ref nDecValue);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_ReadValue failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtPurseDec.Text = nDecValue.ToString();
            byte[] byteHex = new byte[4];
            short lowWordbits = (short)((short)nDecValue & 0xFFFF);
            short highWordbits = (short)((nDecValue >> 16) & 0xFFFF);
            byteHex[0] = (byte)(lowWordbits & 0xFF);
            byteHex[1] = (byte)((lowWordbits >> 8) & 0xFF);
            byteHex[2] = (byte)(highWordbits & 0xFF);
            byteHex[3] = (byte)((highWordbits >> 8) & 0xFF);
            txtBoxPurseHex.Text = ToHexString(byteHex);
        }

        private void btnReadSector2_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte[] dataBuffer = new byte[256];

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtBoxDataOne2.Text = "";
            txtBoxDataTwo2.Text = "";
            txtDataThree2.Text = "";
            txtKeyA2.Text = "";
            txtKey2.Text = "";
            txtKeyB2.Text = "";

            if (rbtKeyB2.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass2.Text);
            byte[] bytesKey = ToDigitsBytes(txtInputKey2.Text);

            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int j;
                byte cLen = 0;
                status = TyA_CS_Read(g_hDevice, (byte)((secnr * 4) + i), dataBuffer, ref cLen);

                if (status != 0 || cLen != 16)
                {
                    MessageBox.Show("TyA_CS_Read failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] bytesData = new byte[16];
                for (j = 0; j < bytesData.Length; j++)
                    bytesData[j] = Marshal.ReadByte(dataBuffer, j);
              
                if (i == 0)
                    txtBoxDataOne2.Text = ToHexString(bytesData);
                else if (i == 1)
                    txtBoxDataTwo2.Text = ToHexString(bytesData);
                else if (i == 2)
                    txtDataThree2.Text = ToHexString(bytesData);
                else if (i == 3)
                {
                    byte[] byteskeyA = new byte[6];
                    byte[] byteskey = new byte[4];
                    byte[] byteskeyB = new byte[6];

                    for (j = 0; j < 16; j++)
                    {
                        if(j < 6)
                            byteskeyA[j] = bytesData[j];
                        else if(j >= 6 && j < 10)
                            byteskey[j - 6] = bytesData[j];
                        else
                            byteskeyB[j - 10] = bytesData[j];
                    }

                    txtKeyA2.Text = ToHexString(byteskeyA);
                    txtKey2.Text = ToHexString(byteskey);
                    txtKeyB2.Text = ToHexString(byteskeyB);
                }
            }
        }

        private void btnWriteBlock2_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte adr;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbtKeyB2.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass2.Text);
            adr = (byte)(Convert.ToByte(cbxSubmass2.Text) + secnr * 4);

            if(cbxSubmass2.SelectedIndex == 3)
            {
                if (DialogResult.Cancel == MessageBox.Show("Be sure to write block3 !", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    return;
            }


            byte[] bytesKey = ToDigitsBytes(txtInputKey2.Text);

            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] bytesBlock;
            if(cbxSubmass2.SelectedIndex == 0)
            {
                bytesBlock = ToDigitsBytes(txtBoxDataOne2.Text);
            }
            else if(cbxSubmass2.SelectedIndex == 1)
            {
                bytesBlock = ToDigitsBytes(txtBoxDataTwo2.Text);
            }
            else if(cbxSubmass2.SelectedIndex == 2)
            {
                bytesBlock = ToDigitsBytes(txtDataThree2.Text);
            }
            else
            {                
                String strCompont = txtKeyA2.Text;
                strCompont += txtKey2.Text;
                strCompont += txtKeyB2.Text;
                bytesBlock = ToDigitsBytes(strCompont);
            }

            status = TyA_CS_Write(g_hDevice, adr, bytesBlock);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Write failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnReadSector3_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte[] dataBuffer = new byte[256];

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtDataOne3.Text = "";
            txtDataTwo3.Text = "";
            txtDataThree3.Text = "";
            txtKeyA3.Text = "";
            txtKey3.Text = "";
            txtKeyB3.Text = "";

            if (rbtKeyB3.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass3.Text);
            byte[] bytesKey = ToDigitsBytes(txtInputKey3.Text);

            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int j;
                byte cLen = 0;

                status = TyA_CS_Read(g_hDevice, (byte)((secnr * 4) + i), dataBuffer, ref cLen);
                if (status != 0 || cLen != 16)
                {
                    MessageBox.Show("TyA_CS_Read failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] bytesData = new byte[16];
                for (j = 0; j < bytesData.Length; j++)
                    bytesData[j] = Marshal.ReadByte(dataBuffer, j);

                if (i == 0)
                    txtDataOne3.Text = ToHexString(bytesData);
                else if (i == 1)
                    txtDataTwo3.Text = ToHexString(bytesData);
                else if (i == 2)
                    txtDataThree3.Text = ToHexString(bytesData);
                else if (i == 3)
                {
                    byte[] byteskeyA = new byte[6];
                    byte[] byteskey = new byte[4];
                    byte[] byteskeyB = new byte[6];

                    for (j = 0; j < 16; j++)
                    {
                        if (j < 6)
                            byteskeyA[j] = bytesData[j];
                        else if (j >= 6 && j < 10)
                            byteskey[j - 6] = bytesData[j];
                        else
                            byteskeyB[j - 10] = bytesData[j];
                    }

                    txtKeyA3.Text = ToHexString(byteskeyA);
                    txtKey3.Text = ToHexString(byteskey);
                    txtKeyB3.Text = ToHexString(byteskeyB);
                }
            }           
        }

        private void btnWriteBlock3_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte adr;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbtKeyB3.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass3.Text);
            adr = (byte)(Convert.ToByte(cbxSubmass3.Text) + secnr * 4);

            if (cbxSubmass3.SelectedIndex == 3)
            {
                if (DialogResult.Cancel == MessageBox.Show("Be sure to write block3 !", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    return;
            }

            byte[] bytesKey = ToDigitsBytes(txtInputKey3.Text);
  
            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            byte[] bytesBlock;
            if (cbxSubmass3.SelectedIndex == 0)
            {
                bytesBlock = ToDigitsBytes(txtDataOne3.Text);
            }
            else if (cbxSubmass3.SelectedIndex == 1)
            {
                bytesBlock = ToDigitsBytes(txtDataTwo3.Text);
            }
            else if (cbxSubmass3.SelectedIndex == 2)
            {
                bytesBlock = ToDigitsBytes(txtDataThree3.Text);
            }
            else
            {
                String strCompont = txtKeyA3.Text;
                strCompont += txtKey3.Text;
                strCompont += txtKeyB3.Text;
                bytesBlock = ToDigitsBytes(strCompont);
            }

            status = TyA_CS_Write(g_hDevice, adr, bytesBlock);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Write failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnReadSector4_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte[] dataBuffer = new byte[256];

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtDataOne4.Text = "";
            txtDataTwo4.Text = "";
            txtDataThree4.Text = "";
            txtKeyA4.Text = "";
            txtKey4.Text = "";
            txtKeyB4.Text = "";

            if (rbtKeyB4.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass4.Text);
            byte[] bytesKey = ToDigitsBytes(txtInputKey4.Text);

            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int j;
                byte cLen = 0;
                status = TyA_CS_Read(g_hDevice, (byte)((secnr * 4) + i), dataBuffer, ref cLen);

                if (status != 0 || cLen != 16)
                {
                    MessageBox.Show("TyA_CS_Read failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] bytesData = new byte[16];
                for (j = 0; j < bytesData.Length; j++)
                    bytesData[j] = Marshal.ReadByte(dataBuffer, j);

                if (i == 0)
                    txtDataOne4.Text = ToHexString(bytesData);
                else if (i == 1)
                    txtDataTwo4.Text = ToHexString(bytesData);
                else if (i == 2)
                    txtDataThree4.Text = ToHexString(bytesData);
                else if (i == 3)
                {
                    byte[] byteskeyA = new byte[6];
                    byte[] byteskey = new byte[4];
                    byte[] byteskeyB = new byte[6];

                    for (j = 0; j < 16; j++)
                    {
                        if (j < 6)
                            byteskeyA[j] = bytesData[j];
                        else if (j >= 6 && j < 10)
                            byteskey[j - 6] = bytesData[j];
                        else
                            byteskeyB[j - 10] = bytesData[j];
                    }

                    txtKeyA4.Text = ToHexString(byteskeyA);
                    txtKey4.Text = ToHexString(byteskey);
                    txtKeyB4.Text = ToHexString(byteskeyB);
                }
            }         
        }

        private void btnWriteBlock4_Click(object sender, EventArgs e)
        {
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte adr;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbtKeyB4.Checked)
                mode = 0x61; //密钥

            secnr = Convert.ToByte(cbxMass4.Text);
            adr = (byte)(Convert.ToByte(cbxSubmass4.Text) + secnr * 4);

            if (cbxSubmass4.SelectedIndex == 3)
            {
                if (DialogResult.Cancel == MessageBox.Show("Be sure to write block3!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    return;
            }

            byte[] bytesKey = ToDigitsBytes(txtInputKey4.Text);

            status = TyA_CS_Authentication2(g_hDevice, mode, (byte)(secnr * 4), bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Authentication2 failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            byte[] bytesBlock;
            if (cbxSubmass4.SelectedIndex == 0)
            {
                bytesBlock = ToDigitsBytes(txtDataOne4.Text);
            }
            else if (cbxSubmass4.SelectedIndex == 1)
            {
                bytesBlock = ToDigitsBytes(txtDataTwo4.Text);
            }
            else if (cbxSubmass4.SelectedIndex == 2)
            {
                bytesBlock = ToDigitsBytes(txtDataThree4.Text);
            }
            else
            {
                String strCompont = txtKeyA4.Text;
                strCompont += txtKey4.Text;
                strCompont += txtKeyB4.Text;
                bytesBlock = ToDigitsBytes(strCompont);
            }

            status = TyA_CS_Write(g_hDevice, adr, bytesBlock);
            if (status != 0)
            {
                MessageBox.Show("TyA_CS_Write failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsbtnConnect_Click(object sender, EventArgs e)
        {/*toolbar button[connect] clicked*/

            int status;
            string strError;

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice))
            {
                //If the reader is already open , close it firstly
                status = Sys_Close(ref g_hDevice);
                if (0 != status)
                {
                    strError = "Sys_Close failed !";
                    MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Connect
            status = Sys_Open(ref g_hDevice, 0, 0x0416, 0x8020);
            if(0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //============= Init the reader before operating the card ============
            //Close antenna of the reader
            status = Sys_SetAntenna(g_hDevice, 0);
            if(0 != status)
            {
                strError = "Sys_SetAntenna failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating 

            //Set the reader's working mode
            status = Sys_InitType(g_hDevice, (byte)'A');
            if (0 != status)
            {
                strError = "Sys_InitType failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_InitType operating

            //Open antenna of the reader
            status = Sys_SetAntenna(g_hDevice, 1);
            if (0 != status)
            {
                strError = "Sys_SetAntenna failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating


            //============================ Success Tips ==========================
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice, 20); 
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cbxMass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cbxtext = cbxSubmass1.Text;
            cbxSubmass1.Items.Clear();
           
            if(0 == cbxMass1.SelectedIndex)
            {
                cbxSubmass1.Items.Add("1");
                cbxSubmass1.Items.Add("2");
                if (cbxtext == "2")
                    cbxSubmass1.SelectedIndex = 1;
                else
                    cbxSubmass1.SelectedIndex = 0;
            }
            else
            {
                cbxSubmass1.Items.Add("0");
                cbxSubmass1.Items.Add("1");
                cbxSubmass1.Items.Add("2");   
             
                if(cbxtext == "1")
                    cbxSubmass1.SelectedIndex = 1;
                else if(cbxtext == "2")
                    cbxSubmass1.SelectedIndex = 2;
                else
                    cbxSubmass1.SelectedIndex = 0;
            }
           
        }
    }
}