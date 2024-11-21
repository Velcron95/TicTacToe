using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{

    public partial class MainWindow : Window
    {
        private string currentPlayer = "X"; 
        private Button[] buttons;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[] { Button0, Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8 };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            
            if (string.IsNullOrEmpty(clickedButton.Content as string))
            {
                clickedButton.Content = "X";
                if (CheckWin("X"))
                {
                    MessageBox.Show("Player X Wins!");
                    ResetGame();
                    return;
                }

                
                if (buttons.All(b => !string.IsNullOrEmpty(b.Content as string)))
                {
                    MessageBox.Show("It's a Draw!");
                    ResetGame();
                    return;
                }

                
                ComputerMove();
            }
        }

        private void ComputerMove()
        {
            
            Random random = new Random();
            var availableButtons = buttons.Where(b => string.IsNullOrEmpty(b.Content as string)).ToList();
            if (availableButtons.Any())
            {
                var chosenButton = availableButtons[random.Next(availableButtons.Count)];
                chosenButton.Content = "O";

                if (CheckWin("O"))
                {
                    MessageBox.Show("Computer (O) Wins!");
                    ResetGame();
                    return;
                }

                
                if (buttons.All(b => !string.IsNullOrEmpty(b.Content as string)))
                {
                    MessageBox.Show("It's a Draw!");
                    ResetGame();
                    return;
                }
            }
        }

        private bool CheckWin(string player)
        {
            
            int[][] winningCombinations = new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            
            foreach (var combo in winningCombinations)
            {
                if (buttons[combo[0]].Content as string == player &&
                    buttons[combo[1]].Content as string == player &&
                    buttons[combo[2]].Content as string == player)
                {
                    return true;
                }
            }

            return false;
        }

        private void ResetGame()
        {
            foreach (var button in buttons)
            {
                button.Content = null;
            }
            currentPlayer = "X"; 
        }
    }
}