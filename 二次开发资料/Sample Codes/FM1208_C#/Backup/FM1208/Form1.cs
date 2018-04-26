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

    public partial class FM1208 : Form
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

        //=========================== Auxiliary Function ===========================
        [DllImport("hfrdapi.dll")]
        static extern int Aux_SingleDES(byte desType,
                                        byte[] key,
                                        byte[] srcData,
                                        UInt32 srcDataLen,
                                        byte[] destData,
                                        ref UInt32 destDataLen);

        [DllImport("hfrdapi.dll")]
        static extern int Aux_TripleDES(byte desType,
                                        byte[] key,
                                        byte[] srcData,
                                        UInt32 srcDataLen,
                                        byte[] destData,
                                        ref UInt32 destDataLen);

        [DllImport("hfrdapi.dll")]
        static extern int Aux_SingleMAC(byte[] key,
                                        byte[] initData,
                                        byte[] srcData,
                                        UInt32 srcDataLen,
                                        byte[] macData);

        [DllImport("hfrdapi.dll")]
        static extern int Aux_TripleMAC(byte[] key,
                                        byte[] initData,
                                        byte[] srcData,
                                        UInt32 srcDataLen,
                                        byte[] macData);

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

        //======================== ISO14443A-4 Card Function =======================
        [DllImport("hfrdapi.dll")]
        static extern int TyA_Reset(IntPtr device,
                                    byte mode,
                                    byte[] pData,
                                    ref byte pMsgLg);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_CosCommand(IntPtr device,
                                         byte[] pCommand,
                                         byte cmdLen,
                                         byte[] pData,
                                         ref byte pMsgLg);

        [DllImport("hfrdapi.dll")]
        static extern int TyA_Deselect(IntPtr device);

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
        public FM1208()
        {
            InitializeComponent();
        }

        private void Mifare_1K_Load(object sender, EventArgs e)
        {
            //cbxPage.SelectedIndex = 0;
            //txtBoxData.MaxLength = 8;
            //txtBoxKey.MaxLength = 32;
            
            textCommand.Text = "0084000004";
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte mode = 0x52; //WUPA mode, request cards of all status
            byte[] dataBuffer = new byte[256];
            byte len = 255;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textCSN.Text = "";
            textResetInf.Text = "";

            //Close and then open the antenna in order to achieve multiple reset card.
            //(If you need to reset the card several times, you need to increase the turn off, open the antenna function.)
            //Close antenna of the reader
            status = Sys_SetAntenna(g_hDevice, 0);
            if (0 != status)
            {
                strError = "Sys_SetAntenna failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating 

            //Open antenna of the reader
            status = Sys_SetAntenna(g_hDevice, 1);
            if (0 != status)
            {
                strError = "Sys_SetAntenna failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating

            //Reset card
            status = TyA_Reset(g_hDevice, mode, dataBuffer, ref len);
            if (status != 0)
            {
                MessageBox.Show("TyA_Reset failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Show  CSN and  Reset  information
            String str = String.Empty;
            for (int i = 0; i < 4; i++)
            {
                str += byteHEX(dataBuffer[i]);
            }
            textCSN.Text = str;

            str = String.Empty;
            for (int i = 0; i < len-4; i++)
            {
                str += byteHEX(dataBuffer[i+4]);
            }
            textResetInf.Text = str;
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            int status;
            byte[] dataBuffer = new byte[256];
            byte cmdLen = 255;
            byte msgLen = 255;

            //Check whether the reader is connected or not
            if (true != Sys_IsOpen(g_hDevice))
            {
                MessageBox.Show("Not connect to device !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textResponse.Text = "";

            //Gets the command to send
            byte[] commandData;
            commandData = ToDigitsBytes(textCommand.Text);
            cmdLen = (byte)textCommand.TextLength;
            cmdLen = (byte)(cmdLen / 2);
            status = TyA_CosCommand(g_hDevice, commandData, cmdLen, dataBuffer, ref msgLen);
            if (status != 0)
            {
                MessageBox.Show("TyA_CosCommand failed !", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //Show response data
            String str = String.Empty;
            for (int i = 0; i < msgLen; i++)
            {
                str += byteHEX(dataBuffer[i]);
            }
            textResponse.Text = str;
        }
    }
}
