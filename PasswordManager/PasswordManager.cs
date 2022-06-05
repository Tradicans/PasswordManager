/*
 * Program:         PasswordManager.exe
 * Module:          PasswordManager.cs
 * Date:            <enter a date>
 * Author:          <enter your name>
 * Description:     Some free starting code for INFO-3138 project 1, the Password Manager
 *                  application. All it does so far is demonstrate how to obtain the system date 
 *                  and how to use the PasswordTester class provided.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;            // File class

namespace PasswordManager
{
    class Program
    {

        static void Main(string[] args)
        {
            // System date demonstration
            DateTime dateNow = DateTime.Now;
            Console.Write("PASSWORD MANAGEMENT SYSTEM (STARTING CODE), " + dateNow.ToShortDateString());
            
            bool done;
            do
            {
                Console.Write("\n\nEnter a password: ");
                string pwText = Console.ReadLine();

                try
                {
                    // PasswordTester class demonstration
                    PasswordTester pw = new PasswordTester(pwText);
                    Console.WriteLine("That password is " + pw.StrengthLabel);
                    Console.WriteLine("That password has a strength of " + pw.StrengthPercent + "%");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("ERROR: Invalid password format");
                }

                Console.Write("\nTest another password? (y/n): ");
                done = Console.ReadKey().KeyChar != 'y';

            } while (!done);

            Console.WriteLine("\n");
        }
        
    } // end class
}
