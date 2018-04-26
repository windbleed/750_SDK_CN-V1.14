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
        static extern int Sys_GetSnr(IntPtr device, byte[] pSnr);

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
        IntPtr[] g_hDevice = new IntPtr[50];

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

            for (int i=0; i < 50; i++)
            {
                g_hDevice[i] = (IntPtr)(-1); //g_hDevice must init as -1
            }

            textDeviceSN1.Text = "";
            textDeviceSN2.Text = "";
            textDeviceSN3.Text = "";
            textDeviceSN4.Text = "";
            textDeviceSN5.Text = "";
            textDeviceSN6.Text = "";
            textDeviceSN7.Text = "";
            textDeviceSN8.Text = "";

            textConnStatus1.Text = "Not connected !";
            textConnStatus2.Text = "Not connected !";
            textConnStatus3.Text = "Not connected !";
            textConnStatus4.Text = "Not connected !";
            textConnStatus5.Text = "Not connected !";
            textConnStatus6.Text = "Not connected !";
            textConnStatus7.Text = "Not connected !";
            textConnStatus8.Text = "Not connected !";
        }



        private void tsbtnConnect_Click(object sender, EventArgs e)
        {/*toolbar button[connect] clicked*/

            int status;
            string strError;
            UInt32 i=0;
            UInt32 devNum = 0;
            UInt32 devSnStrLen = 256;
            StringBuilder devSnStr = new StringBuilder((int)devSnStrLen);

            //Close all devices that have been opened
            for (i = 0; i < 50; i++)
            {
                if (true == Sys_IsOpen(g_hDevice[i]))
                {
                    Sys_Close(ref g_hDevice[i]);
                }
            }

            textDeviceSN1.Text = "";
            textDeviceSN2.Text = "";
            textDeviceSN3.Text = "";
            textDeviceSN4.Text = "";
            textDeviceSN5.Text = "";
            textDeviceSN6.Text = "";
            textDeviceSN7.Text = "";
            textDeviceSN8.Text = "";

            textConnStatus1.Text = "Not connected !";
            textConnStatus2.Text = "Not connected !";
            textConnStatus3.Text = "Not connected !";
            textConnStatus4.Text = "Not connected !";
            textConnStatus5.Text = "Not connected !";
            textConnStatus6.Text = "Not connected !";
            textConnStatus7.Text = "Not connected !";
            textConnStatus8.Text = "Not connected !";

            //Gets the number of devices connected to the PC
            status = Sys_GetDeviceNum(0x0416, 0x8020, ref devNum);
            if (status != 0)
            {
                strError = "Sys_GetDeviceNum failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Displays the device serial number string
            for(i=0; i<devNum; i++)
            {
                status = Sys_GetHidSerialNumberStr(i, 0x0416, 0x8020, devSnStr, devSnStrLen);
                if (status != 0)
                {
                    strError = "Sys_GetHidSerialNumberStr failed !";
                    MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                switch (i)
                {
                    case 0:
                        textDeviceSN1.Text = devSnStr.ToString();
                        break;
                    case 1:
                        textDeviceSN2.Text = devSnStr.ToString();
                        break;
                    case 2:
                        textDeviceSN3.Text = devSnStr.ToString();
                        break;
                    case 3:
                        textDeviceSN4.Text = devSnStr.ToString();
                        break;
                    case 4:
                        textDeviceSN5.Text = devSnStr.ToString();
                        break;
                    case 5:
                        textDeviceSN6.Text = devSnStr.ToString();
                        break;
                    case 6:
                        textDeviceSN7.Text = devSnStr.ToString();
                        break;
                    case 7:
                        textDeviceSN8.Text = devSnStr.ToString();
                        break;
                    default:
                        break;
                }

                if (i == 7) //Display up to 8 devices
                {
                    break;
                }
            }
        }

        private void btnConn1_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 0;

            //
            if (textDeviceSN1.Text == "")
            {
                strError = "No device !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice[index]))
            {
                //If the reader is already open
                strError = "The device is connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Connect
            status = Sys_Open(ref g_hDevice[index], index, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textConnStatus1.Text = "Connected !";

            //============================ Success Tips ==========================
            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200); 
            
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnConn2_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 1;

            //
            if (textDeviceSN2.Text == "")
            {
                strError = "No device !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice[index]))
            {
                //If the reader is already open
                strError = "The device is connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Connect
            status = Sys_Open(ref g_hDevice[index], index, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textConnStatus2.Text = "Connected !";

            //============================ Success Tips ==========================
            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);

            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnConn3_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 2;

            //
            if (textDeviceSN3.Text == "")
            {
                strError = "No device !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice[index]))
            {
                //If the reader is already open
                strError = "The device is connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Connect
            status = Sys_Open(ref g_hDevice[index], index, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textConnStatus3.Text = "Connected !";

            //============================ Success Tips ==========================
            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);

            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnConn4_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 3;

            //
            if (textDeviceSN4.Text == "")
            {
                strError = "No device !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice[index]))
            {
                //If the reader is already open
                strError = "The device is connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Connect
            status = Sys_Open(ref g_hDevice[index], index, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textConnStatus4.Text = "Connected !";

            //============================ Success Tips ==========================
            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);

            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnConn5_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 4;

            //
            if (textDeviceSN5.Text == "")
            {
                strError = "No device !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice[index]))
            {
                //If the reader is already open
                strError = "The device is connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Connect
            status = Sys_Open(ref g_hDevice[index], index, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textConnStatus5.Text = "Connected !";

            //============================ Success Tips ==========================
            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);

            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnConn6_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 5;

            //
            if (textDeviceSN6.Text == "")
            {
                strError = "No device !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice[index]))
            {
                //If the reader is already open
                strError = "The device is connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Connect
            status = Sys_Open(ref g_hDevice[index], index, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textConnStatus6.Text = "Connected !";

            //============================ Success Tips ==========================
            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);

            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnConn7_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 6;

            //
            if (textDeviceSN7.Text == "")
            {
                strError = "No device !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice[index]))
            {
                //If the reader is already open
                strError = "The device is connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Connect
            status = Sys_Open(ref g_hDevice[index], index, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textConnStatus7.Text = "Connected !";

            //============================ Success Tips ==========================
            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);

            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnConn8_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 7;

            //
            if (textDeviceSN8.Text == "")
            {
                strError = "No device !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //=========================== Connect reader =========================
            //Check whether the reader is connected or not
            if (true == Sys_IsOpen(g_hDevice[index]))
            {
                //If the reader is already open
                strError = "The device is connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Connect
            status = Sys_Open(ref g_hDevice[index], index, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "Sys_Open failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textConnStatus8.Text = "Connected !";

            //============================ Success Tips ==========================
            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);

            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBeep1_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 0;

            //Check whether the reader is connected or not
            if (false == Sys_IsOpen(g_hDevice[index]))
            {
                strError = "The device is not connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBeep2_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 1;

            //Check whether the reader is connected or not
            if (false == Sys_IsOpen(g_hDevice[index]))
            {
                strError = "The device is not connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBeep3_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 2;

            //Check whether the reader is connected or not
            if (false == Sys_IsOpen(g_hDevice[index]))
            {
                strError = "The device is not connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBeep4_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 3;

            //Check whether the reader is connected or not
            if (false == Sys_IsOpen(g_hDevice[index]))
            {
                strError = "The device is not connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBeep5_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 4;

            //Check whether the reader is connected or not
            if (false == Sys_IsOpen(g_hDevice[index]))
            {
                strError = "The device is not connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBeep6_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 5;

            //Check whether the reader is connected or not
            if (false == Sys_IsOpen(g_hDevice[index]))
            {
                strError = "The device is not connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBeep7_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 6;

            //Check whether the reader is connected or not
            if (false == Sys_IsOpen(g_hDevice[index]))
            {
                strError = "The device is not connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBeep8_Click(object sender, EventArgs e)
        {
            int status;
            string strError;
            byte index;

            index = 7;

            //Check whether the reader is connected or not
            if (false == Sys_IsOpen(g_hDevice[index]))
            {
                strError = "The device is not connected !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            status = Sys_SetLight(g_hDevice[index], 2);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Beep 200 ms
            status = Sys_SetBuzzer(g_hDevice[index], 20);
            if (0 != status)
            {
                strError = "Sys_SetBuzzer failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(200);
            //
            status = Sys_SetLight(g_hDevice[index], 1);
            if (0 != status)
            {
                strError = "Sys_SetLight failed !";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
