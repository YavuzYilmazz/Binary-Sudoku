using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PROJECT-2
{
    class Program
    {
        static void Main(string[] args)

        {
            bool game_over = false, inserted = true;//these flags are used to determine whether game is over and a piece is inserted by the user
            int shape_number = 0;//shape number holds the current piece number (1-11)
            int[,] points = new int[3, 9];//for each row, column and block in the board this array can hold points 
            int length_x = 0, length_y = 0, max_lenght_x = 0, max_lenght_y = 0;//these variables holds the current piece's length in two dimensions, so piece don't get across borders
            int cursorx = 8, cursory = 4;//initial cursorx, and cursory positions
            int total_score = 0, piece = 0;//total score and total piece number 
            ConsoleKeyInfo cki;//to determine keys entered by the user
            int i, j, k, l;//counters for nested loops
            char[,] numbers_in_board = new char[9, 9];//numbers in board, '.' means slot is empty 
            for (i = 0; i < numbers_in_board.GetLength(0); i++)
                for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    numbers_in_board[i, j] = '.';//making the numbers_in_board empty at the beginning
            char[,] shape = new char[3, 3];//piece
            for (i = 0; i < shape.GetLength(0); i++)
                for (j = 0; j < shape.GetLength(1); j++)
                    shape[i, j] = '.';//making piece empty at the beginning
            while (true)
            {//Game is played in this loop, program breaks the loop when the game is over 
                for (i = 0; i < points.GetLength(0); i++)
                    for (j = 0; j < points.GetLength(1); j++)
                        points[i, j] = 0;//at beginning of each loop, points are zeroed
                int number_of_elements = 0, empty_slot = 0;//number_of_elements means filled field in shape, and empty_slot means how many of them can be inserted into current position
                //these variables is to detect whether a piece can be inserted or not
                for (i = 0; i < points.GetLength(0); i++)
                    for (j = 0; j < points.GetLength(1); j++)
                        points[i, j] = 0;
                char[] tobinary = new char[9];//The array to which the binary number is assigned
                int countr1 = 0, countr2 = 0, countr3 = 0, countr4 = 0, countr5 = 0, countr6 = 0, countr7 = 0, countr8 = 0, countr9 = 0;//row occupancy control element
                int countc1 = 0, countc2 = 0, countc3 = 0, countc4 = 0, countc5 = 0, countc6 = 0, countc7 = 0, countc8 = 0, countc9 = 0;//column occupancy control element
                int counts1 = 0, counts2 = 0, counts3 = 0, counts4 = 0, counts5 = 0, counts6 = 0, counts7 = 0, counts8 = 0, counts9 = 0;//block occupancy control element
                // Board 
                Console.SetCursorPosition(3, 2);
                Console.WriteLine("     1   2   3   4   5   6   7   8   9");
                for (i = 0; i < 19; i++)
                {
                    if (i == 0 || i == 6 || i == 12 || i == 18)
                    {
                        Console.SetCursorPosition(3, i + 3);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("   +-----------+-----------+-----------+");
                        Console.ResetColor();
                    }
                    else if (i % 2 == 0)
                    {
                        Console.SetCursorPosition(3, i + 3);
                        Console.WriteLine("   |-----------|-----------|-----------|");
                    }
                    else
                    {
                        Console.SetCursorPosition(3, i + 3);
                        Console.Write(i / 2 + 1 + "  |");
                        for (j = 0; j < numbers_in_board.GetLength(1); j++)
                            Console.Write(" {0} |", numbers_in_board[i / 2, j]);
                    }
                }
                Console.SetCursorPosition(55, 3);
                Console.Write("Piece : {0}           Score : {1}", piece, total_score);
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.RightArrow && cursorx + 4 * (max_lenght_x - 1) < 38)
                        cursorx += 4;
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 8)
                        cursorx -= 4;
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 4)
                        cursory -= 2;
                    if (cki.Key == ConsoleKey.DownArrow && cursory + 2 * (max_lenght_y - 1) < 20)
                        cursory += 2;//changing the position of cursor, but lengths of the piece in each dimensions are important here 
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        Console.SetCursorPosition(55, 10);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(55, 12);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(55, 14);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(59, 10);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(59, 12);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(59, 14);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(63, 10);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(55, 7);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your Piece Is Ready");
                        Console.ResetColor();
                        Random rand = new Random();
                        for (i = 0; i < shape.GetLength(0); i++)
                            for (j = 0; j < shape.GetLength(1); j++)
                                if (shape[i, j] != '.') number_of_elements++;
                        empty_slot = 0;
                        for (k = 0; k < max_lenght_x; k++)
                            for (l = 0; l < max_lenght_y; l++)
                                if (numbers_in_board[(cursory - 4) / 2 + l, (cursorx - 8) / 4 + k] == '.' && shape[l, k] != '.')
                                    empty_slot++;//checking if the piece can be inserted into the current position
                        if (empty_slot == number_of_elements)
                        {
                            inserted = true;
                            for (k = 0; k < max_lenght_x; k++)
                                for (l = 0; l < max_lenght_y; l++)
                                    if (numbers_in_board[(cursory - 4) / 2 + l, (cursorx - 8) / 4 + k] == '.' && shape[l, k] != '.')
                                        numbers_in_board[(cursory - 4) / 2 + l, (cursorx - 8) / 4 + k] = shape[l, k];
                        }//if piece can be inserted, this nested loop inserts the piece
                        if (inserted) shape_number = rand.Next(1, 11);
                        if (inserted)
                            for (i = 16; i <= 20; i++)
                            {
                                Console.SetCursorPosition(55, i);
                                Console.Write("                                                 ");
                            }
                        if (inserted)
                        {
                            for (k = 0; k < shape.GetLength(0); k++)
                                for (l = 0; l < shape.GetLength(1); l++)
                                    shape[k, l] = '.';
                        }
                        if (inserted && shape_number == 1)
                        {
                            char[] array = { (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                Console.SetCursorPosition(55, 10);
                                Console.WriteLine(array[t - 1]);
                            }
                            shape[0, 0] = array[0];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 2)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                Console.SetCursorPosition(4 * t + 51, 10);
                                Console.WriteLine(array[t - 1]);
                            }
                            shape[0, 0] = array[0];
                            shape[0, 1] = array[1];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 3)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                Console.SetCursorPosition(55, 2 * t + 8);
                                Console.WriteLine(array[t - 1]);
                            }
                            shape[0, 0] = array[0];
                            shape[1, 0] = array[1];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 4)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                Console.SetCursorPosition(4 * t + 51, 10);
                                Console.WriteLine(array[t - 1]);
                            }
                            shape[0, 0] = array[0];
                            shape[0, 1] = array[1];
                            shape[0, 2] = array[2];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 5)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                Console.SetCursorPosition(55, 2 * t + 8);
                                Console.WriteLine(array[t - 1]);
                            }
                            shape[0, 0] = array[0];
                            shape[1, 0] = array[1];
                            shape[2, 0] = array[2];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 6)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                if (t == 1 || t == 2)
                                {
                                    Console.SetCursorPosition(4 * t + 51, 10);
                                    Console.WriteLine(array[t - 1]);
                                }
                                else
                                {
                                    Console.SetCursorPosition(55, 12);
                                    Console.WriteLine(array[t - 1]);
                                }
                            }
                            shape[0, 0] = array[0];
                            shape[0, 1] = array[1];
                            shape[1, 0] = array[2];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 7)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                if (t == 1 || t == 2)
                                {
                                    Console.SetCursorPosition(4 * t + 51, 10);
                                    Console.WriteLine(array[t - 1]);
                                }
                                else
                                {
                                    Console.SetCursorPosition(59, 12);
                                    Console.WriteLine(array[t - 1]);
                                }
                            }
                            shape[0, 0] = array[0];
                            shape[0, 1] = array[1];
                            shape[1, 1] = array[2];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 8)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                if (t == 1 || t == 2)
                                {
                                    Console.SetCursorPosition(55, 2 * t + 8);
                                    Console.WriteLine(array[t - 1]);
                                }
                                else
                                {
                                    Console.SetCursorPosition(59, 12);
                                    Console.WriteLine(array[t - 1]);
                                }
                            }
                            shape[0, 0] = array[0];
                            shape[1, 0] = array[1];
                            shape[1, 1] = array[2];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 9)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                if (t == 1 || t == 2)
                                {
                                    Console.SetCursorPosition(59, 2 * t + 8);
                                    Console.WriteLine(array[t - 1]);
                                }
                                else
                                {
                                    Console.SetCursorPosition(55, 12);
                                    Console.WriteLine(array[t - 1]);
                                }
                            }
                            shape[0, 1] = array[0];
                            shape[1, 0] = array[2];
                            shape[1, 1] = array[1];
                            piece++;
                            inserted = false;
                        }
                        else if (inserted && shape_number == 10)
                        {
                            char[] array = { (char)rand.Next(48, 50), (char)rand.Next(48, 50), (char)rand.Next(48, 50), (char)rand.Next(48, 50) };
                            for (int t = 1; t <= array.Length; t++)
                            {
                                if (t == 1)
                                {
                                    Console.SetCursorPosition(55, 10);
                                    Console.WriteLine(array[t - 1]);
                                }
                                else if (t == 2)
                                {
                                    Console.SetCursorPosition(59, 10);
                                    Console.WriteLine(array[t - 1]);
                                }
                                else if (t == 3)
                                {
                                    Console.SetCursorPosition(55, 12);
                                    Console.WriteLine(array[t - 1]);
                                }
                                else
                                {
                                    Console.SetCursorPosition(59, 12);
                                    Console.WriteLine(array[t - 1]);
                                }
                            }
                            shape[0, 0] = array[0];
                            shape[0, 1] = array[1];
                            shape[1, 0] = array[2];
                            shape[1, 1] = array[3];
                            piece++;
                            inserted = false;
                        }
                        game_over = true;//it is assumed as game is over when a new piece is generated, if the ppiece can be inserted then game_over is made false
                        max_lenght_x = 0;
                        max_lenght_y = 0;
                        for (k = 0; k < shape.GetLength(0); k++)
                        {
                            length_x = 0;
                            for (l = 0; l < shape.GetLength(1); l++)
                                if (shape[k, l] != '.') length_x++;
                            max_lenght_x = max_lenght_x < length_x ? length_x : max_lenght_x;
                        }
                        for (k = 0; k < shape.GetLength(1); k++)
                        {
                            length_y = 0;
                            for (l = 0; l < shape.GetLength(0); l++)
                                if (shape[l, k] != '.') length_y++;
                            max_lenght_y = max_lenght_y < length_y ? length_y : max_lenght_y;
                        }//finding lengths for new piece
                    }
                }
                number_of_elements = 0;
                for (i = 0; i < shape.GetLength(0); i++)
                    for (j = 0; j < shape.GetLength(1); j++)
                        if (shape[i, j] != '.') number_of_elements++;
                for (i = 0; i <= numbers_in_board.GetLength(1) - max_lenght_x; i++)
                    for (j = 0; j <= numbers_in_board.GetLength(0) - max_lenght_y; j++)
                    {
                        empty_slot = 0;
                        for (k = 0; k < max_lenght_x; k++)
                            for (l = 0; l < max_lenght_y; l++)
                            {
                                if (numbers_in_board[j + l, i + k] == '.' && shape[l, k] != '.') empty_slot++;
                                if (empty_slot == number_of_elements)
                                {
                                    game_over = false;
                                    break;
                                }
                            }
                    }//program tries each combination to determine the piece can be inserted or not
                if (cursory + 2 * (max_lenght_y - 1) >= 22) cursory -= 2 * (max_lenght_y - 1);
                if (cursorx + 4 * (max_lenght_x - 1) >= 44) cursorx -= 4 * (max_lenght_x - 1);//if new piece's lengths get across the borders cursor position is reduced
                Console.SetCursorPosition(cursorx, cursory);//
                int junction = 0;//this is used to if there is a junction of a number in board and number in shape, if there is shape is displayed red
                for (k = 0; k < max_lenght_x; k++)
                    for (l = 0; l < max_lenght_y; l++)
                        if (numbers_in_board[(cursory - 4) / 2 + l, (cursorx - 8) / 4 + k] != '.' && shape[l, k] != '.') junction++;
                if (junction > 0) Console.ForegroundColor = ConsoleColor.Red;
                for (k = 0; k < shape.GetLength(0); k++)
                    for (l = 0; l < shape.GetLength(1); l++)
                        if (shape[k, l] != '.')
                        {
                            Console.SetCursorPosition(cursorx + l * 4, cursory + k * 2);
                            Console.Write(shape[k, l]);
                        }//displaying shape
                Console.ResetColor();
                for (i = 0; i < numbers_in_board.GetLength(0); i++)//occupancy checks
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        if (i == 0 && numbers_in_board[i, j] != '.')
                            countr1++;
                        else if (i == 1 && numbers_in_board[i, j] != '.')
                            countr2++;
                        else if (i == 2 && numbers_in_board[i, j] != '.')
                            countr3++;
                        else if (i == 3 && numbers_in_board[i, j] != '.')
                            countr4++;
                        else if (i == 4 && numbers_in_board[i, j] != '.')
                            countr5++;
                        else if (i == 5 && numbers_in_board[i, j] != '.')
                            countr6++;
                        else if (i == 6 && numbers_in_board[i, j] != '.')
                            countr7++;
                        else if (i == 7 && numbers_in_board[i, j] != '.')
                            countr8++;
                        else if (i == 8 && numbers_in_board[i, j] != '.')
                            countr9++;
                        if (j == 0 && numbers_in_board[i, j] != '.')
                            countc1++;
                        else if (j == 1 && numbers_in_board[i, j] != '.')
                            countc2++;
                        else if (j == 2 && numbers_in_board[i, j] != '.')
                            countc3++;
                        else if (j == 3 && numbers_in_board[i, j] != '.')
                            countc4++;
                        else if (j == 4 && numbers_in_board[i, j] != '.')
                            countc5++;
                        else if (j == 5 && numbers_in_board[i, j] != '.')
                            countc6++;
                        else if (j == 6 && numbers_in_board[i, j] != '.')
                            countc7++;
                        else if (j == 7 && numbers_in_board[i, j] != '.')
                            countc8++;
                        else if (j == 8 && numbers_in_board[i, j] != '.')
                            countc9++;
                        if ((i == 0 || i == 1 || i == 2) && (j == 0 || j == 1 || j == 2) && numbers_in_board[i, j] != '.')
                            counts1++;
                        else if ((i == 0 || i == 1 || i == 2) && (j == 3 || j == 4 || j == 5) && numbers_in_board[i, j] != '.')
                            counts2++;
                        else if ((i == 0 || i == 1 || i == 2) && (j == 6 || j == 7 || j == 8) && numbers_in_board[i, j] != '.')
                            counts3++;
                        else if ((i == 3 || i == 4 || i == 5) && (j == 0 || j == 1 || j == 2) && numbers_in_board[i, j] != '.')
                            counts4++;
                        else if ((i == 3 || i == 4 || i == 5) && (j == 3 || j == 4 || j == 5) && numbers_in_board[i, j] != '.')
                            counts5++;
                        else if ((i == 3 || i == 4 || i == 5) && (j == 6 || j == 7 || j == 8) && numbers_in_board[i, j] != '.')
                            counts6++;
                        else if ((i == 6 || i == 7 || i == 8) && (j == 0 || j == 1 || j == 2) && numbers_in_board[i, j] != '.')
                            counts7++;
                        else if ((i == 6 || i == 7 || i == 8) && (j == 3 || j == 4 || j == 5) && numbers_in_board[i, j] != '.')
                            counts8++;
                        else if ((i == 6 || i == 7 || i == 8) && (j == 6 || j == 7 || j == 8) && numbers_in_board[i, j] != '.')
                            counts9++;
                    }
                int pointy = 0;
                //Converting the filled part to decimal and printing 
                if (countr1 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    i = 0;
                    Console.Write("(");
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 0] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 0]);

                }
                if (countr2 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    i = 1;
                    Console.Write("(");
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 1] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 1]);
                }
                if (countr3 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    i = 2;
                    Console.Write("(");
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 2] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 2]);
                }
                if (countr4 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    i = 3;
                    Console.Write("(");
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 3] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 3]);
                }
                if (countr5 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    i = 4;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 4] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 4]);
                }
                if (countr6 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    i = 5;
                    Console.Write("(");
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 5] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 5]);
                }
                if (countr7 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    i = 6;
                    Console.Write("(");
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 6] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 6]);
                }
                if (countr8 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    i = 7;
                    Console.Write("(");
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 7] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 7]);
                }
                if (countr9 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    i = 8;
                    Console.Write("(");
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[j] = numbers_in_board[i, j];
                    }
                    points[0, 8] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[0, 8]);
                }
                if (countc1 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 0;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 0] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 0]);
                }
                if (countc2 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 1;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 1] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 1]);

                }
                if (countc3 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 2;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 2] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 2]);
                }
                if (countc4 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 3;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 3] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 3]);
                }
                if (countc5 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 4;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 4] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 4]);
                }
                if (countc6 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 5;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 5] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 5]);
                }
                if (countc7 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 6;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 6] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 6]);
                }
                if (countc8 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 7;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 7] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 7]);
                }
                if (countc9 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    j = 8;
                    Console.Write("(");
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                    {
                        Console.Write(numbers_in_board[i, j]);
                        tobinary[i] = numbers_in_board[i, j];
                    }
                    points[1, 8] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[1, 8]);
                }
                if (counts1 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 0; i < 3; i++)
                        for (j = 0; j < 3; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * i + j] = numbers_in_board[i, j];
                        }
                    points[2, 0] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 0]);
                }
                if (counts2 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 0; i < 3; i++)
                        for (j = 3; j < 6; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * i + j - 3] = numbers_in_board[i, j];
                        }
                    points[2, 1] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 1]);
                }
                if (counts3 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 0; i < 3; i++)
                        for (j = 6; j < 9; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * i + j - 6] = numbers_in_board[i, j];
                        }
                    points[2, 2] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 2]);
                }
                if (counts4 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 3; i < 6; i++)
                        for (j = 0; j < 3; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * (i - 3) + j] = numbers_in_board[i, j];
                        }
                    points[2, 3] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 3]);
                }
                if (counts5 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 3; i < 6; i++)
                        for (j = 3; j < 6; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * (i - 3) + j - 3] = numbers_in_board[i, j];
                        }
                    points[2, 4] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 4]);
                }
                if (counts6 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 3; i < 6; i++)
                        for (j = 6; j < 9; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * (i - 3) + j - 6] = numbers_in_board[i, j];
                        }
                    points[2, 5] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 5]);
                }
                if (counts7 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 6; i < 9; i++)
                        for (j = 0; j < 3; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * (i - 6) + j] = numbers_in_board[i, j];
                        }
                    points[2, 6] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 6]);
                }
                if (counts8 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 6; i < 9; i++)
                        for (j = 3; j < 6; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * (i - 6) + j - 3] = numbers_in_board[i, j];
                        }
                    points[2, 7] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 7]);
                }
                if (counts9 == 9)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    pointy++;
                    Console.Write("(");
                    for (i = 6; i < 9; i++)
                        for (j = 6; j < 9; j++)
                        {
                            Console.Write(numbers_in_board[i, j]);
                            tobinary[3 * (i - 6) + j - 6] = numbers_in_board[i, j];
                        }
                    points[2, 8] = Convert_to_decimal(tobinary);
                    Console.Write(")2 = " + points[2, 8]);
                }
                //printing score
                int sum = 0;
                int x = 0;
                for (i = 0; i < points.GetLength(0); i++)
                    for (j = 0; j < points.GetLength(1); j++)
                        if (points[i, j] != 0)
                        {
                            sum += points[i, j];
                            x++;
                        }
                sum *= x;
                total_score += sum;
                if (x > 1)
                {
                    Console.SetCursorPosition(55, 16 + pointy);
                    Console.Write("Congratulations! {0}x combo --> {1}", x, sum);
                }
                //location for erasing after printing the filled parts
                if (countr1 == 9)
                {
                    i = 0;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countr2 == 9)
                {
                    i = 1;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countr3 == 9)
                {
                    i = 2;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countr4 == 9)
                {
                    i = 3;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countr5 == 9)
                {
                    i = 4;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countr6 == 9)
                {
                    i = 5;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countr7 == 9)
                {
                    i = 6;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countr8 == 9)
                {
                    i = 7;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countr9 == 9)
                {
                    i = 8;
                    for (j = 0; j < numbers_in_board.GetLength(1); j++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc1 == 9)
                {
                    j = 0;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc2 == 9)
                {
                    j = 1;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc3 == 9)
                {
                    j = 2;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc4 == 9)
                {
                    j = 3;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc5 == 9)
                {
                    j = 4;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc6 == 9)
                {
                    j = 5;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc7 == 9)
                {
                    j = 6;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc8 == 9)
                {
                    j = 7;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (countc9 == 9)
                {
                    j = 8;
                    for (i = 0; i < numbers_in_board.GetLength(0); i++)
                        numbers_in_board[i, j] = '.';
                }
                if (counts1 == 9)
                    for (i = 0; i < 3; i++)
                        for (j = 0; j < 3; j++)
                            numbers_in_board[i, j] = '.';
                if (counts2 == 9)
                    for (i = 0; i < 3; i++)
                        for (j = 3; j < 6; j++)
                            numbers_in_board[i, j] = '.';
                if (counts3 == 9)
                    for (i = 0; i < 3; i++)
                        for (j = 6; j < 9; j++)
                            numbers_in_board[i, j] = '.';
                if (counts4 == 9)
                    for (i = 3; i < 6; i++)
                        for (j = 0; j < 3; j++)
                            numbers_in_board[i, j] = '.';
                if (counts5 == 9)
                    for (i = 3; i < 6; i++)
                        for (j = 3; j < 6; j++)
                            numbers_in_board[i, j] = '.';
                if (counts6 == 9)
                    for (i = 3; i < 6; i++)
                        for (j = 6; j < 9; j++)
                            numbers_in_board[i, j] = '.';
                if (counts7 == 9)
                    for (i = 6; i < 9; i++)
                        for (j = 0; j < 3; j++)
                            numbers_in_board[i, j] = '.';
                if (counts8 == 9)
                    for (i = 6; i < 9; i++)
                        for (j = 3; j < 6; j++)
                            numbers_in_board[i, j] = '.';
                if (counts9 == 9)
                    for (i = 6; i < 9; i++)
                        for (j = 6; j < 9; j++)
                            numbers_in_board[i, j] = '.';
                if (game_over)
                    break;//if new piece cannot be inserted then game is over and this statemend breaks the while loop
                Console.SetCursorPosition(55, 5);
                Console.WriteLine("(Please press enter for new piece)");
                Thread.Sleep(100);
            }
            Console.SetCursorPosition(75, 21);
            Console.Write("Game over, goodbye!");//Goodbye message
            Console.ReadLine();//to hold the console screen open, so user can see the message 
        }
        public static int Convert_to_decimal(char[] received)
        {
            int counter, counter2, converted_to_decimal = 0;
            for (counter = 0; counter < received.Length; counter++)
                if (received[counter] == 49)
                {
                    int exponential = 1;
                    for (counter2 = 0; counter2 < received.Length - 1 - counter; counter2++)
                        exponential *= 2;
                    converted_to_decimal += exponential;
                }
            return converted_to_decimal;//we hope that you will allow this function, this function is called when points need to be calculated
        }
    }
}