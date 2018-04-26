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


        //======================= Ultralight(C) Card Function ========================= 
        [DllImport("hfrdapi.dll")]
        static extern int TyA_UL_Select(IntPtr device, byte[] pSnr, ref byte pLen);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_UL_Write(IntPtr device, byte page, byte[] pdata);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_UL_Authentication(IntPtr device, byte[] pKey);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_UL_ChangeKey(IntPtr device, byte[] pKey);


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
                new Exception("byteHEX error !");
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
            cbxPage.SelectedIndex = 0;
            txtBoxData.MaxLength = 8;
            txtBoxKey.MaxLength = 32;
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
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //============= Init the reader before operating the card ============
            //Close antenna of the reader
            status = Sys_SetAntenna(g_hDevice, 0);
            if (0 != status)
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

        

        private void btnRequest_Click(object sender, EventArgs e)
        {            
            int status;
            byte mode = 0x52; //WUPA mode, request cards of all status
            ushort TagType = 0;
            byte[] dataBuffer = new byte[256];
            byte len = 255;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < 2 ;i++ )
            {
                status = TyA_Request(g_hDevice, mode, ref TagType);
                if (status != 0)
                    continue;

                status = TyA_UL_Select(g_hDevice, dataBuffer, ref len); //Return the card serial number
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
            byte mode = 0x26; //REQA mode, request non-halt cards
            ushort TagType = 0;
            int status;
            byte[] dataBuffer = new byte[256];
            byte len = 255;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            status = TyA_Request(g_hDevice, mode, ref TagType);
            if (status != 0)
            {
                MessageBox.Show("TyA_Request failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            status = TyA_UL_Select(g_hDevice, dataBuffer, ref len); //Return the card serial number
            if (status != 0)
            {
                MessageBox.Show("TyA_UL_Select failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void btnReadPage_Click(object sender, EventArgs e)
        {
            int status;
            byte pagenr = 0x00;
            byte[] dataBuffer = new byte[256];

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtBoxData.Text = "";

            pagenr = Convert.ToByte(cbxPage.Text);
            byte cLen = 0;
            status = TyA_CS_Read(g_hDevice, pagenr, dataBuffer, ref cLen);
            if (status != 0 || cLen != 16)
            {
                MessageBox.Show("TyA_CS_Read failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] bytesData = new byte[4];
            for (int j = 0; j < 4; j++)
                bytesData[j] = Marshal.ReadByte(dataBuffer, j);

            txtBoxData.Text = ToHexString(bytesData);
        }

        private void btnWritePage_Click(object sender, EventArgs e)
        {
            int status;
            byte pagenr = 0x00;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pagenr = Convert.ToByte(cbxPage.Text);
            //Page2、3、40、41、42、43 are a non-user data area, 
            //make sure you already know the region before data is written to.
            if (pagenr == 2 || pagenr == 3 || pagenr == 40 || pagenr == 41 || pagenr == 42 || pagenr == 43)
            {
                if (DialogResult.Cancel == MessageBox.Show("Be sure to write this page ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    return;
            }

            byte[] bytesPageData;
            bytesPageData = ToDigitsBytes(txtBoxData.Text);
            status = TyA_UL_Write(g_hDevice, pagenr, bytesPageData);
            if (status != 0)
            {
                MessageBox.Show("TyA_UL_Write failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            int status;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] bytesKey;
            bytesKey = ToDigitsBytes(txtBoxKey.Text);
            status = TyA_UL_Authentication(g_hDevice, bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_UL_Authentication failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            int status;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DialogResult.Cancel == MessageBox.Show("Be sure to change key ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                return;

            byte[] bytesKey;
            bytesKey = ToDigitsBytes(txtBoxKey.Text);
            status = TyA_UL_ChangeKey(g_hDevice, bytesKey);
            if (status != 0)
            {
                MessageBox.Show("TyA_UL_ChangeKey failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnEnableVerification_Click(object sender, EventArgs e)
        {
            int status;
            byte auth0Addr = 0x2A;
            byte auth1Addr = 0x2B;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DialogResult.Cancel == MessageBox.Show("Be sure to enable verification ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                return;

            //Write AUTH0
            byte[] bytesPageData= new byte[4];
            bytesPageData[0] = 0x03;  //bytesPageData[0] is AUTH0, AUTH0 defines the page address from which the 
                                      //authentication is required. Valid address range for byte AUTH0 is from 03h to 30h.
            bytesPageData[1] = 0x0;  //rfu
            bytesPageData[2] = 0x0;  //rfu
            bytesPageData[3] = 0x0;  //rfu

            status = TyA_UL_Write(g_hDevice, auth0Addr, bytesPageData);
            if (status != 0)
            {
                MessageBox.Show("TyA_UL_Write failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Write AUTH1
            bytesPageData[0] = 0x0;  //bytesPageData[0] is AUTH1,
                                     //if AUTH1 = 0x01: Write access restricted,read access allowed without authentication.
                                     //if AUTH1 = 0x0: Read and write access restricted.
            bytesPageData[1] = 0x0;  //rfu
            bytesPageData[2] = 0x0;  //rfu
            bytesPageData[3] = 0x0;  //rfu

            status = TyA_UL_Write(g_hDevice, auth1Addr, bytesPageData);
            if (status != 0)
            {
                MessageBox.Show("TyA_UL_Write failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnDisableVerification_Click(object sender, EventArgs e)
        {
            int status;
            byte auth0Addr = 0x2A;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Write AUTH0
            byte[] bytesPageData = new byte[4];
            bytesPageData[0] = 0x30;  //bytesPageData[0] is AUTH0, AUTH0 defines the page address from which the 
                                      //authentication is required. Valid address range for byte AUTH0 is from 03h to 30h.
            bytesPageData[1] = 0x0;  //rfu
            bytesPageData[2] = 0x0;  //rfu
            bytesPageData[3] = 0x0;  //rfu

            status = TyA_UL_Write(g_hDevice, auth0Addr, bytesPageData);
            if (status != 0)
            {
                MessageBox.Show("TyA_UL_Write failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
