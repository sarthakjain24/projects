package currencyConverter;

import javax.swing.JFrame;
import javax.swing.JPanel;
import java.awt.BorderLayout;
import javax.swing.JLabel;
import javax.swing.JTextField;
import javax.swing.SwingUtilities;
import javax.swing.JOptionPane;
import javax.swing.JComboBox;
import javax.swing.JButton;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

/**
 * A currency converter using GUI
 * 
 * @author Sarthak Jain
 */
@SuppressWarnings("serial")
public class CurrencyConverter extends JFrame implements ActionListener {
	/**
	 * The frame in which the application exists
	 */
	private JFrame frame;
	/**
	 * The text box in which the user enters the amount needed to be changed
	 */
	private JTextField amountChange;
	/**
	 * The list of options in which the user converts the money from
	 */
	private JComboBox<String> fromComboBox;
	/**
	 * The list of options in which the user converts the money to
	 */
	private JComboBox<String> toComboBox;

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		// Runs the CurrencyConverter
		SwingUtilities.invokeLater(() -> new CurrencyConverter());

	}

	/**
	 * Create the application.
	 */
	public CurrencyConverter() {
		// Initializes the frame
		frame = new JFrame();
		frame.setBounds(100, 100, 689, 368);
		// Sets the frame to visible and close on the exit operation
		frame.setVisible(true);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		// Initializes the panel, and adds a BorderLayout
		JPanel panel = new JPanel();
		frame.getContentPane().add(panel, BorderLayout.CENTER);
		panel.setLayout(null);

		// Creates a label for the amount to convert and sets its bounds, and adds it to
		// the panel
		JLabel lblAmountToConvert = new JLabel("Enter Amount:");
		lblAmountToConvert.setBounds(160, 60, 138, 42);
		panel.add(lblAmountToConvert);

		// Initializes the amoutChange text field, sets its bounds, while adding to the
		// panel
		amountChange = new JTextField();
		amountChange.setBounds(283, 69, 96, 25);
		panel.add(amountChange);
		amountChange.setColumns(10);

		// Initializes the label for the currency from setting the bounds and adding to
		// the panel
		JLabel currencyFrom = new JLabel("Currency From:");
		currencyFrom.setBounds(160, 112, 138, 42);
		panel.add(currencyFrom);

		// Initializes the label for the currency to setting the bounds and adding to
		// the panel
		JLabel currencyTo = new JLabel("Currency To:");
		currencyTo.setBounds(160, 162, 138, 42);
		panel.add(currencyTo);

		// An array of the currency possible to convert
		String[] currencyPossible = { "AED", "ARS", "AUD", "AWG", "BAM", "BBD", "BDT", "BGN", "BHD", "BMD", "BOB",
				"BRL", "BSD", "CAD", "CHF", "CLP", "CNY", "COP", "CZK", "DKK", "DOP", "EGP", "EUR", "FJD", "GBP", "GHS",
				"GMD", "GTQ", "HKD", "HRK", "HUF", "IDR", "ILS", "INR", "ISK", "JMD", "JOD", "JPY", "KES", "LAK", "LBP",
				"LKR", "LTL", "MAD", "MDL", "MGA", "MKD", "MUR", "MXN", "MYR", "NAD", "NGN", "NOK", "NPR", "NZD", "OMR",
				"PAB", "PEN", "PHP", "PKR", "PLN", "PYG", "QAR", "RON", "RSD", "RUB", "SAR", "SCR", "SEK", "SGD", "SYP",
				"THB", "TND", "TRY", "TWD", "UAH", "USD", "UGX", "UYU", "VEF", "VND", "XAF", "XCD", "XOF", "XPF",
				"ZAR" };

		// Adds the currency possible array to the from combo box list and adds it to
		// the panel, whilst setting the bounds
		fromComboBox = new JComboBox<>(currencyPossible);
		fromComboBox.setBounds(283, 121, 96, 25);
		panel.add(fromComboBox);

		// Adds the currency possible array to the to combo box list and adds it to the
		// panel, whilst setting the bounds
		toComboBox = new JComboBox<>(currencyPossible);
		toComboBox.setBounds(283, 171, 96, 25);
		panel.add(toComboBox);

		// Creates a button that converts the money, sets the bounds, and adds an
		// ActionListener to it, and adds it to the panel
		JButton btnConvert = new JButton("Convert");
		btnConvert.addActionListener(this);
		btnConvert.setBounds(185, 228, 85, 21);
		panel.add(btnConvert);

		// Creates a button that quits the application, sets the bounds, and adds an
		// ActionListener to it, and adds it to the panel
		JButton quitButton = new JButton("Quit");
		quitButton.addActionListener(this);
		quitButton.setBounds(294, 228, 85, 21);
		panel.add(quitButton);

		// Creates a button that resets the application, sets the bounds, and adds an
		// ActionListener to it, and adds it to the panel
		JButton btnReset = new JButton("Reset");
		btnReset.addActionListener(this);
		btnReset.setBounds(419, 228, 85, 21);
		panel.add(btnReset);
	}

	/**
	 * Overrides the actionPerformed method from the ActionListener class
	 */
	@Override
	public void actionPerformed(ActionEvent e) {

		if (e.getActionCommand().equals("Convert")) {
			// Gets the text from the amount change box, and parses it to a double
			double amt = 0;
			try {
				amt = Double.parseDouble(amountChange.getText());

				// Throws an IllegalArgumentException if the amount is negative
				if (amt < 0) {
					throw new IllegalArgumentException();
				}
				// If a NumberFormatException is found, then it catches the
				// NumberFormatException and shows a message asking for a numeric value to be
				// converted
			} catch (NumberFormatException nfe) {
				JOptionPane.showMessageDialog(null, "Error. Enter a numeric value to convert");

				// If a IllegalArgumentException is found, then it catches the
				// IllegalArgumentException and shows a message asking for a positive value to
				// be entered
			} catch (IllegalArgumentException iae) {
				JOptionPane.showMessageDialog(null, "Error. Enter a positive value to convert");
			}
			// Converts the amt to usd
			double usd = 0;
			/*
			 * Based on the currency selected from the fromComboBox, it converts the
			 * currency to USD
			 */
			if (fromComboBox.getSelectedItem().equals("AED")) {
				usd = amt * 0.27;
			}
			if (fromComboBox.getSelectedItem().equals("ARS")) {
				usd = amt * 0.014;
			}
			if (fromComboBox.getSelectedItem().equals("AUD")) {
				usd = amt * 0.71;
			}
			if (fromComboBox.getSelectedItem().equals("AWG")) {
				usd = amt * 0.56;
			}
			if (fromComboBox.getSelectedItem().equals("BAM")) {
				usd = amt * 0.60;
			}
			if (fromComboBox.getSelectedItem().equals("BBD")) {
				usd = amt * 0.50;
			}
			if (fromComboBox.getSelectedItem().equals("BDT")) {
				usd = amt * 0.012;
			}
			if (fromComboBox.getSelectedItem().equals("BGN")) {
				usd = amt * 0.014;
			}
			if (fromComboBox.getSelectedItem().equals("BHD")) {
				usd = amt * 0.60;
			}
			if (fromComboBox.getSelectedItem().equals("BMD")) {
				usd = amt * 1.00;
			}
			if (fromComboBox.getSelectedItem().equals("BOB")) {
				usd = amt * 0.14;
			}
			if (fromComboBox.getSelectedItem().equals("BRL")) {
				usd = amt * 0.19;
			}
			if (fromComboBox.getSelectedItem().equals("BSD")) {
				usd = amt * 1.00;
			}
			if (fromComboBox.getSelectedItem().equals("CAD")) {
				usd = amt * 0.75;
			}
			if (fromComboBox.getSelectedItem().equals("CHF")) {
				usd = amt * 1.09;
			}
			if (fromComboBox.getSelectedItem().equals("CLP")) {
				usd = amt * 0.0013;
			}
			if (fromComboBox.getSelectedItem().equals("CNY")) {
				usd = amt * 0.14;
			}
			if (fromComboBox.getSelectedItem().equals("COP")) {
				usd = amt * 0.00026;
			}
			if (fromComboBox.getSelectedItem().equals("CZK")) {
				usd = amt * 0.045;
			}
			if (fromComboBox.getSelectedItem().equals("DKK")) {
				usd = amt * 0.16;
			}
			if (fromComboBox.getSelectedItem().equals("DOP")) {
				usd = amt * 0.017;
			}
			if (fromComboBox.getSelectedItem().equals("EGP")) {
				usd = amt * 0.063;
			}
			if (fromComboBox.getSelectedItem().equals("EUR")) {
				usd = amt * 1.17;
			}
			if (fromComboBox.getSelectedItem().equals("FJD")) {
				usd = amt * 0.47;
			}
			if (fromComboBox.getSelectedItem().equals("GBP")) {
				usd = amt * 1.30;
			}
			if (fromComboBox.getSelectedItem().equals("GHS")) {
				usd = amt * 0.17;
			}
			if (fromComboBox.getSelectedItem().equals("GMD")) {
				usd = amt * 0.019;
			}
			if (fromComboBox.getSelectedItem().equals("GTQ")) {
				usd = amt * 0.13;
			}
			if (fromComboBox.getSelectedItem().equals("HKD")) {
				usd = amt * 0.13;
			}
			if (fromComboBox.getSelectedItem().equals("HRK")) {
				usd = amt * 0.16;
			}
			if (fromComboBox.getSelectedItem().equals("HUF")) {
				usd = amt * 0.0034;
			}
			if (fromComboBox.getSelectedItem().equals("IDR")) {
				usd = amt * 0.000068;
			}
			if (fromComboBox.getSelectedItem().equals("ILS")) {
				usd = amt * 0.29;
			}
			if (fromComboBox.getSelectedItem().equals("INR")) {
				usd = amt * 0.013;
			}
			if (fromComboBox.getSelectedItem().equals("ISK")) {
				usd = amt * 0.0073;
			}
			if (fromComboBox.getSelectedItem().equals("JMD")) {
				usd = amt * 0.0067;
			}

			if (fromComboBox.getSelectedItem().equals("JOD")) {
				usd = amt * 1.41;
			}
			if (fromComboBox.getSelectedItem().equals("JPY")) {
				usd = amt * 0.0094;
			}
			if (fromComboBox.getSelectedItem().equals("KES")) {
				usd = amt * 0.0092;
			}
			if (fromComboBox.getSelectedItem().equals("LAK")) {
				usd = amt * 0.00011;
			}
			if (fromComboBox.getSelectedItem().equals("LBP")) {
				usd = amt * 0.00066;
			}
			if (fromComboBox.getSelectedItem().equals("LKR")) {
				usd = amt * 0.0054;
			}
			if (fromComboBox.getSelectedItem().equals("LTL")) {
				usd = amt * 0.34;
			}
			if (fromComboBox.getSelectedItem().equals("MAD")) {
				usd = amt * 0.11;
			}
			if (fromComboBox.getSelectedItem().equals("MDL")) {
				usd = amt * 0.060;
			}
			if (fromComboBox.getSelectedItem().equals("MGA")) {
				usd = amt * 0.00026;
			}
			if (fromComboBox.getSelectedItem().equals("MKD")) {
				usd = amt * 0.019;
			}
			if (fromComboBox.getSelectedItem().equals("MUR")) {
				usd = amt * 0.025;
			}
			if (fromComboBox.getSelectedItem().equals("MXN")) {
				usd = amt * 0.045;
			}
			if (fromComboBox.getSelectedItem().equals("MYR")) {
				usd = amt * 0.24;
			}
			if (fromComboBox.getSelectedItem().equals("NAD")) {
				usd = amt * 0.067;
			}
			if (fromComboBox.getSelectedItem().equals("NGN")) {
				usd = amt * 0.0026;
			}
			if (fromComboBox.getSelectedItem().equals("NOK")) {
				usd = amt * 0.11;
			}
			if (fromComboBox.getSelectedItem().equals("NPR")) {
				usd = amt * 0.0083;
			}
			if (fromComboBox.getSelectedItem().equals("NZD")) {
				usd = amt * 0.65;
			}
			if (fromComboBox.getSelectedItem().equals("OMR")) {
				usd = amt * 2.60;
			}
			if (fromComboBox.getSelectedItem().equals("PAB")) {
				usd = amt * 1.00;
			}
			if (fromComboBox.getSelectedItem().equals("PEN")) {
				usd = amt * 0.28;
			}
			if (fromComboBox.getSelectedItem().equals("PHP")) {
				usd = amt * 0.020;
			}
			if (fromComboBox.getSelectedItem().equals("PKR")) {
				usd = amt * 0.0059;
			}
			if (fromComboBox.getSelectedItem().equals("PLN")) {
				usd = amt * 0.27;
			}
			if (fromComboBox.getSelectedItem().equals("PYG")) {
				usd = amt * 0.00014;
			}
			if (fromComboBox.getSelectedItem().equals("QAR")) {
				usd = amt * 0.27;
			}
			if (fromComboBox.getSelectedItem().equals("RON")) {
				usd = amt * 0.24;
			}
			if (fromComboBox.getSelectedItem().equals("RSD")) {
				usd = amt * 0.0100;
			}
			if (fromComboBox.getSelectedItem().equals("RUB")) {
				usd = amt * 0.014;
			}
			if (fromComboBox.getSelectedItem().equals("SAR")) {
				usd = amt * 0.27;
			}
			if (fromComboBox.getSelectedItem().equals("SCR")) {
				usd = amt * 0.056;
			}
			if (fromComboBox.getSelectedItem().equals("SEK")) {
				usd = amt * 0.11;
			}
			if (fromComboBox.getSelectedItem().equals("SGD")) {
				usd = amt * 0.727552;
			}
			if (fromComboBox.getSelectedItem().equals("SYP")) {
				usd = amt * 0.00195345;
			}
			if (fromComboBox.getSelectedItem().equals("THB")) {
				usd = amt * 0.032;
			}
			if (fromComboBox.getSelectedItem().equals("TND")) {
				usd = amt * 0.36;
			}
			if (fromComboBox.getSelectedItem().equals("TRY")) {
				usd = amt * 0.14;
			}
			if (fromComboBox.getSelectedItem().equals("TWD")) {
				usd = amt * 0.034;
			}
			if (fromComboBox.getSelectedItem().equals("UAH")) {
				usd = amt * 0.036;
			}
			if (fromComboBox.getSelectedItem().equals("USD")) {
				usd = amt;
			}
			if (fromComboBox.getSelectedItem().equals("UGX")) {
				usd = amt * 0.00027;
			}
			if (fromComboBox.getSelectedItem().equals("UYU")) {
				usd = amt * 0.024;
			}
			if (fromComboBox.getSelectedItem().equals("VEF")) {
				usd = amt * 0.100125;
			}
			if (fromComboBox.getSelectedItem().equals("VND")) {
				usd = amt * 0.000043;
			}
			if (fromComboBox.getSelectedItem().equals("XAF")) {
				usd = amt * 0.0018;
			}
			if (fromComboBox.getSelectedItem().equals("XCD")) {
				usd = amt * 0.37;
			}
			if (fromComboBox.getSelectedItem().equals("XOF")) {
				usd = amt * 0.0018;
			}
			if (fromComboBox.getSelectedItem().equals("XPF")) {
				usd = amt * 0.0098;
			}
			if (fromComboBox.getSelectedItem().equals("ZAR")) {
				usd = amt * 0.057;
			}

			// Converts the amount from usd to the requested currency
			double convertedAmt = 0;
			/*
			 * Based on the currency selected from the toComboBox, it converts the currency
			 * from USD
			 */
			if (toComboBox.getSelectedItem().equals("AED")) {
				convertedAmt = usd / 0.27;
			}
			if (toComboBox.getSelectedItem().equals("ARS")) {
				convertedAmt = usd / 0.014;
			}
			if (toComboBox.getSelectedItem().equals("AUD")) {
				convertedAmt = usd / 0.71;
			}
			if (toComboBox.getSelectedItem().equals("AWG")) {
				convertedAmt = usd / 0.56;
			}
			if (toComboBox.getSelectedItem().equals("BAM")) {
				convertedAmt = usd / 0.60;
			}
			if (toComboBox.getSelectedItem().equals("BBD")) {
				convertedAmt = usd / 0.50;
			}
			if (toComboBox.getSelectedItem().equals("BDT")) {
				convertedAmt = usd / 0.012;
			}
			if (toComboBox.getSelectedItem().equals("BGN")) {
				convertedAmt = usd / 0.014;
			}
			if (toComboBox.getSelectedItem().equals("BHD")) {
				convertedAmt = usd / 0.60;
			}
			if (toComboBox.getSelectedItem().equals("BMD")) {
				convertedAmt = usd / 1.00;
			}
			if (toComboBox.getSelectedItem().equals("BOB")) {
				convertedAmt = usd / 0.14;
			}
			if (toComboBox.getSelectedItem().equals("BRL")) {
				convertedAmt = usd / 0.19;
			}
			if (toComboBox.getSelectedItem().equals("BSD")) {
				convertedAmt = usd / 1.00;
			}
			if (toComboBox.getSelectedItem().equals("CAD")) {
				convertedAmt = usd / 0.75;
			}
			if (toComboBox.getSelectedItem().equals("CHF")) {
				convertedAmt = usd / 1.09;
			}
			if (toComboBox.getSelectedItem().equals("CLP")) {
				convertedAmt = usd / 0.0013;
			}
			if (toComboBox.getSelectedItem().equals("CNY")) {
				convertedAmt = usd / 0.14;
			}
			if (toComboBox.getSelectedItem().equals("COP")) {
				convertedAmt = usd / 0.00026;
			}
			if (toComboBox.getSelectedItem().equals("CZK")) {
				convertedAmt = usd / 0.045;
			}
			if (toComboBox.getSelectedItem().equals("DKK")) {
				convertedAmt = usd / 0.16;
			}
			if (toComboBox.getSelectedItem().equals("DOP")) {
				convertedAmt = usd / 0.017;
			}
			if (toComboBox.getSelectedItem().equals("EGP")) {
				convertedAmt = usd / 0.063;
			}
			if (toComboBox.getSelectedItem().equals("EUR")) {
				convertedAmt = usd / 1.17;
			}
			if (toComboBox.getSelectedItem().equals("FJD")) {
				convertedAmt = usd / 0.47;
			}
			if (toComboBox.getSelectedItem().equals("GBP")) {
				convertedAmt = usd / 1.30;
			}
			if (toComboBox.getSelectedItem().equals("GHS")) {
				convertedAmt = usd / 0.17;
			}
			if (toComboBox.getSelectedItem().equals("GMD")) {
				convertedAmt = usd / 0.019;
			}
			if (toComboBox.getSelectedItem().equals("GTQ")) {
				convertedAmt = usd / 0.13;
			}
			if (toComboBox.getSelectedItem().equals("HKD")) {
				convertedAmt = usd / 0.13;
			}
			if (toComboBox.getSelectedItem().equals("HRK")) {
				convertedAmt = usd / 0.16;
			}
			if (toComboBox.getSelectedItem().equals("HUF")) {
				convertedAmt = usd / 0.0034;
			}
			if (toComboBox.getSelectedItem().equals("IDR")) {
				convertedAmt = usd / 0.000068;
			}
			if (toComboBox.getSelectedItem().equals("ILS")) {
				convertedAmt = usd / 0.29;
			}
			if (toComboBox.getSelectedItem().equals("INR")) {
				convertedAmt = usd / 0.013;
			}
			if (toComboBox.getSelectedItem().equals("ISK")) {
				convertedAmt = usd / 0.0073;
			}
			if (toComboBox.getSelectedItem().equals("JMD")) {
				convertedAmt = usd / 0.0067;
			}

			if (toComboBox.getSelectedItem().equals("JOD")) {
				convertedAmt = usd / 1.41;
			}
			if (toComboBox.getSelectedItem().equals("JPY")) {
				convertedAmt = usd / 0.0094;
			}
			if (toComboBox.getSelectedItem().equals("KES")) {
				convertedAmt = usd / 0.0092;
			}
			if (toComboBox.getSelectedItem().equals("LAK")) {
				convertedAmt = usd / 0.00011;
			}
			if (toComboBox.getSelectedItem().equals("LBP")) {
				convertedAmt = usd / 0.00066;
			}
			if (toComboBox.getSelectedItem().equals("LKR")) {
				convertedAmt = usd / 0.0054;
			}
			if (toComboBox.getSelectedItem().equals("LTL")) {
				convertedAmt = usd / 0.34;
			}
			if (toComboBox.getSelectedItem().equals("MAD")) {
				convertedAmt = usd / 0.11;
			}
			if (toComboBox.getSelectedItem().equals("MDL")) {
				convertedAmt = usd / 0.060;
			}
			if (toComboBox.getSelectedItem().equals("MGA")) {
				convertedAmt = usd / 0.00026;
			}
			if (toComboBox.getSelectedItem().equals("MKD")) {
				convertedAmt = usd / 0.019;
			}
			if (toComboBox.getSelectedItem().equals("MUR")) {
				convertedAmt = usd / 0.025;
			}
			if (toComboBox.getSelectedItem().equals("MXN")) {
				convertedAmt = usd / 0.045;
			}
			if (toComboBox.getSelectedItem().equals("MYR")) {
				convertedAmt = usd / 0.24;
			}
			if (toComboBox.getSelectedItem().equals("NAD")) {
				convertedAmt = usd / 0.067;
			}
			if (toComboBox.getSelectedItem().equals("NGN")) {
				convertedAmt = usd / 0.0026;
			}
			if (toComboBox.getSelectedItem().equals("NOK")) {
				convertedAmt = usd / 0.11;
			}
			if (toComboBox.getSelectedItem().equals("NPR")) {
				convertedAmt = usd / 0.0083;
			}
			if (toComboBox.getSelectedItem().equals("NZD")) {
				convertedAmt = usd / 0.65;
			}
			if (toComboBox.getSelectedItem().equals("OMR")) {
				convertedAmt = usd / 2.60;
			}
			if (toComboBox.getSelectedItem().equals("PAB")) {
				convertedAmt = usd / 1.00;
			}
			if (toComboBox.getSelectedItem().equals("PEN")) {
				convertedAmt = usd / 0.28;
			}
			if (toComboBox.getSelectedItem().equals("PHP")) {
				convertedAmt = usd / 0.020;
			}
			if (toComboBox.getSelectedItem().equals("PKR")) {
				convertedAmt = usd / 0.0059;
			}
			if (toComboBox.getSelectedItem().equals("PLN")) {
				convertedAmt = usd / 0.27;
			}
			if (toComboBox.getSelectedItem().equals("PYG")) {
				convertedAmt = usd / 0.00014;
			}
			if (toComboBox.getSelectedItem().equals("QAR")) {
				convertedAmt = usd / 0.27;
			}
			if (toComboBox.getSelectedItem().equals("RON")) {
				convertedAmt = usd / 0.24;
			}
			if (toComboBox.getSelectedItem().equals("RSD")) {
				convertedAmt = usd / 0.0100;
			}
			if (toComboBox.getSelectedItem().equals("RUB")) {
				convertedAmt = usd / 0.014;
			}
			if (toComboBox.getSelectedItem().equals("SAR")) {
				convertedAmt = usd / 0.27;
			}
			if (toComboBox.getSelectedItem().equals("SCR")) {
				convertedAmt = usd / 0.056;
			}
			if (toComboBox.getSelectedItem().equals("SEK")) {
				convertedAmt = usd / 0.11;
			}
			if (toComboBox.getSelectedItem().equals("SGD")) {
				convertedAmt = usd / 0.727552;
			}
			if (toComboBox.getSelectedItem().equals("SYP")) {
				convertedAmt = usd / 0.00195345;
			}
			if (toComboBox.getSelectedItem().equals("THB")) {
				convertedAmt = usd / 0.032;
			}
			if (toComboBox.getSelectedItem().equals("TND")) {
				convertedAmt = usd / 0.36;
			}
			if (toComboBox.getSelectedItem().equals("TRY")) {
				convertedAmt = usd / 0.14;
			}
			if (toComboBox.getSelectedItem().equals("TWD")) {
				convertedAmt = usd / 0.034;
			}
			if (toComboBox.getSelectedItem().equals("UAH")) {
				convertedAmt = usd / 0.036;
			}
			if (toComboBox.getSelectedItem().equals("USD")) {
				convertedAmt = usd;
			}
			if (toComboBox.getSelectedItem().equals("UGX")) {
				convertedAmt = usd / 0.00027;
			}
			if (toComboBox.getSelectedItem().equals("UYU")) {
				convertedAmt = usd / 0.024;
			}
			if (toComboBox.getSelectedItem().equals("VEF")) {
				convertedAmt = usd / 0.100125;
			}
			if (toComboBox.getSelectedItem().equals("VND")) {
				convertedAmt = usd / 0.000043;
			}
			if (toComboBox.getSelectedItem().equals("XAF")) {
				convertedAmt = usd / 0.0018;
			}
			if (toComboBox.getSelectedItem().equals("XCD")) {
				convertedAmt = usd / 0.37;
			}
			if (toComboBox.getSelectedItem().equals("XOF")) {
				convertedAmt = usd / 0.0018;
			}
			if (toComboBox.getSelectedItem().equals("XPF")) {
				convertedAmt = usd / 0.0098;
			}
			if (toComboBox.getSelectedItem().equals("ZAR")) {
				convertedAmt = usd / 0.057;
			}

			// Prints the amount being converted to two decimal places and the currency
			// converted to trailing behind only if the original amt is greater than 0, and
			// the converted amt is not 0
			if (convertedAmt != 0 && amt > 0) {
				JOptionPane.showMessageDialog(null, "Amount converted is: " + String.format("%.2f", convertedAmt) + " "
						+ toComboBox.getSelectedItem().toString());
			}
		}

		// Exits the program if user clicks on Quit
		if (e.getActionCommand().equals("Quit")) {
			System.exit(0);
		}

		// If user clicks on Reset, then sets clears the text box where the user adds
		// their amount and resets the two and
		// from currency option
		if (e.getActionCommand().equals("Reset")) {
			amountChange.setText("");
			fromComboBox.setSelectedIndex(0);
			toComboBox.setSelectedIndex(0);
		}
	}
}
