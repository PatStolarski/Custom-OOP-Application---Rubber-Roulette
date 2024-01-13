using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubber_Roulette
{
    class FillAction
    {
        public int[] FillClip(int shells, int[] tab)
        {
            Random rnd = new Random();
            for (int i = 0; i < shells; i++)
            {
                tab[i] = rnd.Next(2);
            }
            return tab;
        }
    }
    class CountAction
    {
        public int CountShells(int shells, int[] tab)
        {
            int rubber = 0;

            while (rubber < shells / 2)
            {
                for (int i = 0; i < shells; i++)
                {
                    if (tab[i] == 1)
                    {
                        rubber++;
                    }
                }
            }

            return rubber;

        }
    }

    internal class Program
    {
        const int nameLength = 8;






        static void ShowClip(int shells, int[] tab)
        {
            for(int i = 0;i < shells; i++)
            {
                Console.Write("{0} ",tab[i]);
            }
        }


        static void Main(string[] args)
        {
            int clip_size = 0;
            int charges = 0;
            int shCheck = 0;

            FillAction fill = new FillAction();
            CountAction count = new CountAction();

            Console.WriteLine("Welcome to Rubber Roulette!");
            Console.WriteLine("Please type your name below: (please do not exceed length of 8 characters)");


            string name = Console.ReadLine();
            if (name.Length > nameLength)
                name = name.Substring(0, nameLength);

            Console.Clear();




            Console.WriteLine("Excellent! Welcome {0}!", name);

            Console.WriteLine("Please choose difficulty (Quickplay, Standard, All Out)");
            string mode = Console.ReadLine();
            bool rightmode = false;
            while(rightmode==false) {
                if (mode.Equals("Quickplay", StringComparison.CurrentCultureIgnoreCase) || mode.Equals("Standard", StringComparison.CurrentCultureIgnoreCase) || mode.Equals("All Out", StringComparison.CurrentCultureIgnoreCase))
                {
                    rightmode = true;
                    if (mode.Equals("Quickplay", StringComparison.CurrentCultureIgnoreCase))
                    {
                        clip_size = 3;
                        charges = 1;
                        
                    }
                    else if (mode.Equals("Standard", StringComparison.CurrentCultureIgnoreCase))
                    {
                        clip_size = 5;
                        charges = 2;
                        shCheck = 1;
                        

                    }
                    else if (mode.Equals("All Out", StringComparison.CurrentCultureIgnoreCase))
                    {
                        clip_size = 8;
                        charges = 3;
                        shCheck = 2;
                        
                        
                    }
                }
                else
                {
                    Console.WriteLine("Come again?");
                    mode = Console.ReadLine();
                }
            }

            Console.Clear() ;   



            Console.WriteLine("Thank you! Your game of Rubber Roulette on {0} difficulty will begin shortly",mode);

            int playerHP = charges;
            int opponentHP = charges;
            int[] clip = new int[clip_size];
            //clip = FillClip(clip_size, clip);
            clip = fill.FillClip(clip_size, clip);
            int rubber = 0;
            rubber = count.CountShells(clip_size, clip);

            int blanks = clip_size - rubber;

            Console.WriteLine("{0} bullets in the clip, {1} rubber rounds, {2} blanks", clip_size, rubber, blanks);
            Console.WriteLine("Good Luck! Let the game begin...");
            Console.ReadKey();
            Console.Clear();



            ShowClip(clip_size, clip);
            Console.WriteLine();
            int remaining_shells = clip_size-1;
            bool turn = true;
            string decision;

            while (remaining_shells!=0)
            {
                
                if(playerHP > 0 && opponentHP > 0)
                {


                    Console.WriteLine("Opponent ({0} charges) VS ({1} charges) {2}", opponentHP, playerHP, name);

                    if (turn == true)
                    {

                        Console.WriteLine("Your Turn");


                        if (shCheck != 0)
                        {
                            Console.WriteLine("Remaining shell checks: {0}", shCheck);
                            Console.WriteLine("Type 'check' to check type of currently loaded shell or 'shoot' followed by either 'me' or 'opponent' to shoot you or your opponent");
                            Console.WriteLine("Shooting yourself with blank shell skips opponents turn");
                        }
                        else
                        {
                            Console.WriteLine("Type 'shoot' followed by either 'me' or 'opponent' to shoot you or your opponent");
                            Console.WriteLine("Shooting yourself with blank shell skips opponents turn");
                        }


                        bool rightdecision = false;
                        decision = Console.ReadLine();
                        while (rightdecision == false)
                        {

                            if (decision.Equals("check", StringComparison.CurrentCultureIgnoreCase) || decision.Equals("shoot opponent", StringComparison.CurrentCultureIgnoreCase) || decision.Equals("shoot me", StringComparison.CurrentCultureIgnoreCase))
                            {
                                rightdecision = true;
                                if (decision.Equals("check", StringComparison.CurrentCultureIgnoreCase) && shCheck != 0)
                                {
                                    if (clip[remaining_shells - 1] == 1)
                                    {
                                        Console.WriteLine("A rubber shell is loaded currently");
                                    }
                                    else
                                    {
                                        Console.WriteLine("A blank shell is loaded currently");
                                    }

                                    shCheck--;
                                }
                                else
                                {
                                    if (decision.Equals("shoot opponent", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        if (clip[remaining_shells] == 1)
                                        {
                                            Console.WriteLine("You shot opponent with rubber shell! Good job...");
                                            opponentHP--;
                                            turn = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine("It was a blank. Tough luck buddy...");
                                            turn = false;
                                        }
                                    }
                                    else if (decision.Equals("shoot me", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        if (clip[remaining_shells] == 1)
                                        {
                                            Console.WriteLine("You shot yourself with a rubber shell! You lose 1 charge...");
                                            playerHP--;
                                            turn = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine("You shot yourself with a blank shell. Good riddance...");
                                        }
                                    }
                                }
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("Come again?");
                                decision = Console.ReadLine();
                            }

                        }

                        remaining_shells--;
                    }
                    else
                    {
                        Console.WriteLine("Opponents turn. Watch out...");
                        Random shoot = new Random();
                        int choice = shoot.Next(2);

                        if (choice == 0)
                        {
                            if (clip[remaining_shells] == 1)
                            {
                                Console.WriteLine("Your opponent shot themselves with a rubber shell. Good for you...");
                                opponentHP--;
                                turn = true;
                            }
                            else
                            {
                                Console.WriteLine("Your opponent shot themselves with a blank shell.");
                                turn = true;
                            }

                        }
                        else
                        {
                            if (clip[remaining_shells] == 1)
                            {
                                Console.WriteLine("Your opponent shot you with a rubber shell. That'll leave a mark");
                                playerHP--;
                                turn = true;
                            }
                            else
                            {
                                Console.WriteLine("Your opponent shot you with a blank shell. Lucky you...");
                                turn = true;
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();

                        remaining_shells--;

                    }
                }
                else
                {
                    break;
                }
            }
            Console.Clear ();
            if(remaining_shells == 0)
            {
                if(playerHP > opponentHP)
                {
                    Console.WriteLine("Congratualions!!! You WIN!");
                    Console.WriteLine("Next time you won't be so lucky... ~ Opponent");
                }
                else if(opponentHP > playerHP)
                {
                    Console.WriteLine("You lost... Better luck next time");
                }
            }
            else
            {
                if(playerHP>0)
                {
                    Console.WriteLine("Congratualions!!! You WIN!");
                    Console.WriteLine("Next time you won't be so lucky... ~ Opponent");
                }
                else
                {
                    Console.WriteLine("You lost... Better luck next time");
                }
            }



            Console.WriteLine("Thanks for Playing");
            Console.ReadKey();
        }


    }


}
