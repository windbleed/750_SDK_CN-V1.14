package mainpackage;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.AbstractButton;
import javax.swing.GroupLayout;
import javax.swing.GroupLayout.Alignment;
import javax.swing.JButton;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

import com.sun.jna.win32.StdCallLibrary;
import com.sun.jna.Native;
import com.sun.jna.Platform;

import javax.swing.JTextField;
import javax.swing.LayoutStyle.ComponentPlacement;

import javax.swing.JLabel;
import javax.swing.JComboBox;
import javax.swing.JRadioButton;
import javax.swing.DefaultComboBoxModel;
import javax.swing.ButtonGroup;
import javax.swing.SwingConstants;
import java.awt.Component;



public class MifareOneDemo
{

	private JFrame frmMifareOneDemo;

	/**
	 * Launch the application.
	 */
	
	int g_hDevice[] = new int[]{-1}; //hDevice must init as -1
	private JTextField textUID;
	private JTextField textKey;
	private JTextField textData;
	private final ButtonGroup buttonGroup = new ButtonGroup();
	
	//Load DLL Library
    public interface ReaderLib extends StdCallLibrary 
    {
    	ReaderLib INSTANCE = (ReaderLib)Native.loadLibrary("hfrdapi",ReaderLib.class);

        int Sys_Open(int[] device, int index, short vid, short pid);
        int Sys_Close(int[] device);
        boolean Sys_IsOpen(int device);
        int Sys_SetLight(int device, byte color);
        int Sys_SetBuzzer(int device, byte msec);
        int Sys_SetAntenna(int device, byte mode);
        int Sys_InitType(int device, byte type);
        
        int TyA_Request(int device, byte mode , short[] pTagType);
        int TyA_Anticollision(int device, byte bcnt, byte[] pSnr, byte[] pLen);
        int TyA_Select(int device, byte[] pSnr, byte snrLen, byte[] pSak);
        int TyA_Halt(int device);
        int TyA_CS_Authentication2(int device, byte mode, byte block, byte[] pKey);
        int TyA_CS_Read(int device, byte block, byte[] pData, byte[] pLen);
        int TyA_CS_Write(int device, byte block, byte[] pData);
        int TyA_UID_Write(int device, byte[] pData);
    }
    
	public static void main(String[] args) throws InterruptedException
	{
		EventQueue.invokeLater(new Runnable()
		{
			public void run()
			{
				try
				{
					MifareOneDemo window = new MifareOneDemo();
					window.frmMifareOneDemo.setVisible(true);
				} catch (Exception e)
				{
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the application.
	 */
	public MifareOneDemo()
	{
		initialize();
	}

	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize()
	{
		frmMifareOneDemo = new JFrame();
		frmMifareOneDemo.setTitle("Mifare One Demo");
		frmMifareOneDemo.setBounds(100, 100, 535, 396);
		frmMifareOneDemo.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		textUID = new JTextField();
		textUID.setBounds(96, 36, 186, 24);
		textUID.setColumns(10);
		
		final JLabel lblTips = new JLabel("");
		lblTips.setBounds(93, 304, 367, 18);
		
		JLabel lblTips_1 = new JLabel("Tips:");
		lblTips_1.setBounds(50, 304, 40, 18);
		
		textKey = new JTextField();
		textKey.setBounds(96, 93, 186, 24);
		textKey.setText("FFFFFFFFFFFF");
		textKey.setColumns(10);
		
		textData = new JTextField();
		textData.setBounds(96, 193, 347, 24);
		textData.setColumns(10);
		
		final JComboBox comboBoxBlock = new JComboBox();
		comboBoxBlock.setBounds(96, 144, 186, 24);
		comboBoxBlock.setModel(new DefaultComboBoxModel(new String[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63"}));
		comboBoxBlock.setSelectedIndex(1);
		
		final JRadioButton rdbtnKeyA = new JRadioButton("key A");
		rdbtnKeyA.setBounds(299, 92, 69, 27);
		buttonGroup.add(rdbtnKeyA);
		rdbtnKeyA.setSelected(true);
		JRadioButton rdbtnKeyB = new JRadioButton("Key B");
		rdbtnKeyB.setBounds(374, 92, 69, 27);
		buttonGroup.add(rdbtnKeyB);

		
		JLabel lblUid = new JLabel("UID:");
		lblUid.setBounds(58, 42, 32, 18);
		
		JLabel lblKey = new JLabel("Key:");
		lblKey.setBounds(58, 96, 32, 18);
		
		JLabel lblKey_1 = new JLabel("Block:");
		lblKey_1.setBounds(42, 147, 48, 18);
		
		JLabel lblData = new JLabel("Data:");
		lblData.setBounds(50, 199, 40, 18);
		
		JButton btnOpen = new JButton("Open");
		btnOpen.setBounds(32, 252, 89, 27);
		btnOpen.addActionListener(new ActionListener() 
		{
			public void actionPerformed(ActionEvent arg0) 
			{
		    	int status;
		    	boolean bStatus;
		    	
		    	//=================== Connect the reader ===================
		        //Check whether the reader is connected or not
		        //If the reader is already open , close it firstly
		    	bStatus = ReaderLib.INSTANCE.Sys_IsOpen(g_hDevice[0]);
		    	if(bStatus == true)
		    	{
		    		status = ReaderLib.INSTANCE.Sys_Close(g_hDevice);
		    		if(status != 0)
		    		{
			    		lblTips.setText("Sys_Close failed !");
			    		return;
		    		}
		    	}
		    	
		        //Connect
		        status = ReaderLib.INSTANCE.Sys_Open(g_hDevice, 0, (short)0x0416, (short)0x8020);
		        if(status != 0)
		        {
		        	lblTips.setText("Sys_Open failed !");
		        	return;
		        }
		        
		        //========== Init the reader before operating the card ==========
		        //Close antenna of the reader
		        status = ReaderLib.INSTANCE.Sys_SetAntenna(g_hDevice[0], (byte)0);
		        if(status != 0)
		        {
		        	lblTips.setText("Sys_SetAntenna failed !");
		            return;
		        }
		        //Appropriate delay after Sys_SetAntenna operating
			    try  {  Thread.sleep(5);  }  
		        	catch (InterruptedException e)  {  } 
		        
		        //Set the reader's working mode
		        status = ReaderLib.INSTANCE.Sys_InitType(g_hDevice[0], (byte)'A');
		        if(status != 0)
		        {
		        	lblTips.setText("Sys_InitType failed !");
		            return;
		        }
		        //Appropriate delay after Sys_SetAntenna operating
			    try  {  Thread.sleep(5);  }  
		        	catch (InterruptedException e)  {  } 
		        
		        //Open antenna of the reader
		        status = ReaderLib.INSTANCE.Sys_SetAntenna(g_hDevice[0], (byte)1);
		        if(status != 0)
		        {
		        	lblTips.setText("Sys_SetAntenna failed !");
		            return;
		        }
		        //Appropriate delay after Sys_SetAntenna operating
			    try  {  Thread.sleep(5);  }  
		        	catch (InterruptedException e)  {  } 
		        
		        //==================== Success Tips ====================
		        //Beep 200 ms
		        status = ReaderLib.INSTANCE.Sys_SetBuzzer(g_hDevice[0], (byte)20);
		        if(status != 0)
		        {
		        	lblTips.setText("Sys_SetBuzzer failed !");
		            return;
		        }
		        
		        //Tips
		        lblTips.setText("Connect reader succeed !");
			}
		});
		
		JButton btnRequest = new JButton("Request");
		btnRequest.setBounds(153, 252, 89, 27);
		btnRequest.addActionListener(new ActionListener() 
		{
			public void actionPerformed(ActionEvent e) 
			{
				int status;
				byte mode = 0x52;
				short[] TagType = new short[1];
				byte bcnt = 0;
				byte snr[] = new byte[16];
				byte len[] = new byte[1];
				byte sak[] = new byte[1];
				
			    //Check whether the reader is connected or not
			    if(false == ReaderLib.INSTANCE.Sys_IsOpen(g_hDevice[0]))
			    {
			    	lblTips.setText("Please connect the device firstly !");
					return ;        
			    }
			    
			    textUID.setText("");
			    
			    //Request card
				status = ReaderLib.INSTANCE.TyA_Request(g_hDevice[0], mode, TagType);//search all card
				if(status != 0) 
			    {
					lblTips.setText("TyA_Request failed !");
					return ;
				}
				
			    //Anticollision
				status = ReaderLib.INSTANCE.TyA_Anticollision(g_hDevice[0], bcnt, snr, len);//return serial number of card
				if(status != 0 || len[0] != 4) 
			    { 
					lblTips.setText("TyA_Anticollision failed !");
					return ;
				}
				
				String str="";
				for(int i=0; i<4; i++) 
				{
					str = str + String.format("%02X", snr[i]);
				}
				textUID.setText(str);
				
			    //Select card
				status = ReaderLib.INSTANCE.TyA_Select(g_hDevice[0], snr, len[0], sak);//lock ISO14443-3 TYPE_A 
				if(status != 0) 
			    {
					lblTips.setText("TyA_Select failed !");
					return ;
				}
				
			    //Tips
				lblTips.setText("Request card succeed !");
			}
		});
		
		JButton btnRead = new JButton("Read");
		btnRead.setBounds(274, 252, 89, 27);
		btnRead.addActionListener(new ActionListener() 
		{
			public void actionPerformed(ActionEvent e) 
			{
			    int status;
				byte mode = 0x60;
				byte secnr = 0x00;
				byte cLen[] = new byte[1];
				byte pData[] = new byte[64];
				byte key[] = new byte[6];
				
				//Get key type
				if(rdbtnKeyA.isSelected())
				{
					mode = 0x60;
				}
				else
				{
					mode = 0x61;
				}
				
				//Get key
				String strKey = (String) textKey.getText();
				if(strKey.length() != 12)
				{
					lblTips.setText("Please input 6 bytes data in key area !");
					return;
				}
				for(int i=0; i<6; i++)
				{
					byte value = (byte)Integer.parseInt(strKey.substring(2*i, 2*i+2), 16);
					key[i] = value;
				}
				
				//Get block address
				secnr = (byte) Integer.parseInt((String) comboBoxBlock.getSelectedItem(),10);
				
			    //Check whether the reader is connected or not
			    if(false == ReaderLib.INSTANCE.Sys_IsOpen(g_hDevice[0]))
			    {
			    	lblTips.setText("Please connect the device firstly !");
					return ;        
			    }
			    
			    //
			    textData.setText("");
			    
				//Authentication
				status = ReaderLib.INSTANCE.TyA_CS_Authentication2(g_hDevice[0], mode, (byte)((secnr/4)*4), key);
				if(status != 0)
			    {
					lblTips.setText("TyA_CS_Authentication2 failed !");
					return ;
				}
				
			    //Read card
				status = ReaderLib.INSTANCE.TyA_CS_Read(g_hDevice[0], secnr, pData, cLen);
				if(status != 0 || cLen[0] != 16)
			    {
					lblTips.setText("TyA_CS_Read failed !");
					return ;
				}
				
				String str="";
				for(int i=0; i<16; i++) 
				{
					str = str + String.format("%02X", pData[i]);
				}
				textData.setText(str);
			    
				//Tips
				lblTips.setText("Read card succeed !");
				
				//System.out.println(secnr);
			}
		});
		
		JButton btnWrite = new JButton("Write");
		btnWrite.setBounds(395, 252, 89, 27);
		btnWrite.addActionListener(new ActionListener() 
		{
			public void actionPerformed(ActionEvent arg0) 
			{
			    int status;
				byte mode = 0x60;
				byte secnr = 0x00;
				byte cLen[] = new byte[1];
				byte arrData[] = new byte[64];
				byte key[] = new byte[6];
				
				//Get key type
				if(rdbtnKeyA.isSelected())
				{
					mode = 0x60;
				}
				else
				{
					mode = 0x61;
				}
				
				//Get key
				String strKey = (String) textKey.getText();
				if(strKey.length() != 12)
				{
					lblTips.setText("Please input 6 bytes data in key area !");
					return;
				}
				for(int i=0; i<6; i++)
				{
					byte value = (byte)Integer.parseInt(strKey.substring(2*i, 2*i+2), 16);
					key[i] = value;
				}
				
				//Get block address
				secnr = (byte) Integer.parseInt((String) comboBoxBlock.getSelectedItem(),10);
				
				//Get block data
				String strData = (String) textData.getText();
				if(strData.length() != 32)
				{
					lblTips.setText("Please input 16 bytes data in data area !");
					return;
				}
				for(int i=0; i<16; i++)
				{
					byte value = (byte)Integer.parseInt(strData.substring(2*i, 2*i+2), 16);
					arrData[i] = value;
				}			
				
			    //Check whether the reader is connected or not
			    if(false == ReaderLib.INSTANCE.Sys_IsOpen(g_hDevice[0]))
			    {
			    	lblTips.setText("Please connect the device firstly !");
					return ;        
			    }
			    
				//Authentication
				status = ReaderLib.INSTANCE.TyA_CS_Authentication2(g_hDevice[0], mode, (byte)((secnr/4)*4), key);
				if(status != 0)
			    {
					lblTips.setText("TyA_CS_Authentication2 failed !");
					return ;
				}
				
			    //Write card
				status = ReaderLib.INSTANCE.TyA_CS_Write(g_hDevice[0], secnr, arrData);
				if(status != 0)
			    {
					lblTips.setText("TyA_CS_Write failed !");
					return ;
				}
			    
				//Tips
				lblTips.setText("Write card succeed !");			
			}
		});
		frmMifareOneDemo.getContentPane().setLayout(null);
		frmMifareOneDemo.getContentPane().add(lblTips);
		frmMifareOneDemo.getContentPane().add(lblKey);
		frmMifareOneDemo.getContentPane().add(lblUid);
		frmMifareOneDemo.getContentPane().add(lblKey_1);
		frmMifareOneDemo.getContentPane().add(lblTips_1);
		frmMifareOneDemo.getContentPane().add(lblData);
		frmMifareOneDemo.getContentPane().add(textUID);
		frmMifareOneDemo.getContentPane().add(textKey);
		frmMifareOneDemo.getContentPane().add(comboBoxBlock);
		frmMifareOneDemo.getContentPane().add(rdbtnKeyA);
		frmMifareOneDemo.getContentPane().add(rdbtnKeyB);
		frmMifareOneDemo.getContentPane().add(textData);
		frmMifareOneDemo.getContentPane().add(btnRead);
		frmMifareOneDemo.getContentPane().add(btnWrite);
		frmMifareOneDemo.getContentPane().add(btnOpen);
		frmMifareOneDemo.getContentPane().add(btnRequest);
	}
}
