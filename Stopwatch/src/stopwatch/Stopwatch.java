package stopwatch;

import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.SwingUtilities;
import javax.swing.border.EmptyBorder;

/**
 * A Stopwatch using GUI
 * 
 * @author Sarthak Jain
 *
 */
public class Stopwatch extends JFrame implements ActionListener {
	// Runs the Stopwatch
	public static void main(String[] args) {
		SwingUtilities.invokeLater(() -> new Stopwatch());
	}

	/**
	 * Represents the milliseconds on the stopwatch
	 */
	public static int millisecond = 0;
	/**
	 * Represents the seconds on the stopwatch
	 */
	public static int second = 0;
	/**
	 * Represents the minutes on the stopwatch
	 */
	public static int minute = 0;
	/**
	 * Represents the hours on the stopwatch
	 */
	public static int hour = 0;
	/**
	 * Represents whether the stopwatch is running or not
	 */
	public static boolean running = true;
	/**
	 * Represents the panel
	 */
	private JPanel contentPane;
	/**
	 * Represents the text area that shows the hrs
	 */
	private JTextField txtHrs;
	/**
	 * Represents the text area that shows the minutes
	 */
	private JTextField txtMinutes;
	/**
	 * Represents the text area that shows the seconds
	 */
	private JTextField txtSeconds;
	/**
	 * Represents the text area that shows the milliseconds
	 */
	private final JTextField txtMilliseconds;
	/**
	 * Represents the button that starts the stopwatch
	 */
	private JButton startButton;
	/**
	 * Represents the button that stops the stopwatch
	 */
	private JButton stopButton;
	/**
	 * Represents the button that resets the stopwatch
	 */
	private JButton resetButton;

	/**
	 * Create the frame.
	 */
	public Stopwatch() {
		// Creates the layout of the Background, and makes the program end when closed,
		// visible, and adds a Layout
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setTitle("Stopwatch");
		setVisible(true);
		setBounds(100, 100, 1100, 330);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);

		// Initializes the txtHrs text field, and sets the font, text, bounds, makes it
		// editable by the user, and sets it on the content pane
		txtHrs = new JTextField();
		txtHrs.setFont(new Font("Tahoma", Font.PLAIN, 40));
		txtHrs.setBounds(10, 38, 260, 120);
		txtHrs.setText(" 00 ");
		txtHrs.setEditable(false);
		contentPane.add(txtHrs);
		txtHrs.setColumns(10);

		// Initializes the txtMinutes text field, and sets the font, text, bounds, makes
		// it editable by the user, and sets it on the content pane
		txtMinutes = new JTextField();
		txtMinutes.setFont(new Font("Tahoma", Font.PLAIN, 40));
		txtMinutes.setBounds(280, 38, 260, 120);
		txtMinutes.setText(" : 00");
		txtMinutes.setEditable(false);
		txtMinutes.setColumns(10);
		contentPane.add(txtMinutes);

		// Initializes the txtSeconds text field, and sets the font, text, bounds, makes
		// it editable by the user, and sets it on the content pane
		txtSeconds = new JTextField();
		txtSeconds.setFont(new Font("Tahoma", Font.PLAIN, 40));
		txtSeconds.setBounds(550, 38, 260, 120);
		txtSeconds.setText(" : 00");
		txtSeconds.setEditable(false);
		txtSeconds.setColumns(10);
		contentPane.add(txtSeconds);

		// Initializes the txtMilliseconds text field, and sets the font, text, bounds,
		// makes it editable by the user, and sets it on the content pane
		txtMilliseconds = new JTextField();
		txtMilliseconds.setFont(new Font("Tahoma", Font.PLAIN, 40));
		txtMilliseconds.setBounds(820, 38, 260, 120);
		txtMilliseconds.setText(" : 0000");
		contentPane.add(txtMilliseconds);
		txtMilliseconds.setEditable(false);
		txtMilliseconds.setColumns(10);

		// Initializes the start button, and sets the font, text, bounds, and adds an
		// ActionListener, and adds it to the content pane
		startButton = new JButton("Start");
		startButton.setFont(new Font("Tahoma", Font.PLAIN, 25));
		startButton.addActionListener(this);
		startButton.setBounds(208, 174, 140, 45);
		contentPane.add(startButton);

		// Initializes the stop button, and sets the font, text, bounds, and adds an
		// ActionListener, and adds it to the content pane
		stopButton = new JButton("Stop");
		stopButton.addActionListener(this);
		stopButton.setFont(new Font("Tahoma", Font.PLAIN, 25));
		stopButton.setBounds(478, 174, 140, 45);
		contentPane.add(stopButton);

		// Initializes the reset button, and sets the font, text, bounds, and adds an
		// ActionListener, and adds it to the content pane
		resetButton = new JButton("Reset");
		resetButton.addActionListener(this);
		resetButton.setFont(new Font("Tahoma", Font.PLAIN, 25));
		resetButton.setBounds(757, 174, 140, 45);
		contentPane.add(resetButton);
	}

	/**
	 * Overrides the actionPerformed method from the ActionListener class
	 */
	@Override
	public void actionPerformed(ActionEvent e) {
		/*
		 * If the Start Button is clicked, it goes into this condition
		 */
		if (e.getActionCommand().equals("Start")) {
			// Sets running to true
			running = true;

			// Initializes a new thread
			Thread t = new Thread() {
				public void run() {

					// Loop that runs until running is true(Until stop or reset is clicked)
					while (running == true) {

						// Disables the start button
						startButton.setEnabled(false);
						if (running == true) {
							try {

								// Sleeps the thread for a millisecond
								sleep(1);

								// Increments the seconds for every thousand milliseconds
								if (millisecond > 1000) {
									millisecond = 0;
									second++;
								}

								// Increments a minute for every sixty seconds
								else if (second > 60) {
									millisecond = 0;
									second = 0;
									minute++;
								}

								// Increments an hour for every sixty minutes
								else if (minute > 60) {
									millisecond = 0;
									second = 0;
									minute = 0;
									hour++;
								}

								// Increments by a millisecond
								millisecond++;

								// Sets the text with the milliseconds, seconds, minutes, and hrs
								txtMilliseconds.setText(" : " + millisecond);
								txtSeconds.setText(" : " + second);
								txtMinutes.setText(" : " + minute);
								txtHrs.setText(" " + hour);
							} catch (Exception e) {
							}
						} else {
							break;
						}
					}
				}
			};
			// Starts the thread
			t.start();
		}

		/*
		 * If the Stop Button is clicked, it goes into this condition
		 */
		if (e.getActionCommand().equals("Stop")) {
			// Sets running to false, and enables the start button
			running = false;
			startButton.setEnabled(true);
		}
		/*
		 * If the Reset Button is clicked, it goes into this condition
		 */
		if (e.getActionCommand().equals("Reset")) {
			// Sets runing to false, and enables the start button
			running = false;
			startButton.setEnabled(true);

			// Resets the hr, minutes, second and millisecond values
			hour = 0;
			minute = 0;
			second = 0;
			millisecond = 0;

			// Resets the text representing the time
			txtHrs.setText(" 00 ");
			txtMinutes.setText(" : 00 ");
			txtSeconds.setText(" : 00 ");
			txtMilliseconds.setText(" : 0000");
		}
	}

}
