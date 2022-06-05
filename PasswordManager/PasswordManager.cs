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
using Newtonsoft.Json;              // JsonConvert class
using Newtonsoft.Json.Schema;       // JSchema class
using Newtonsoft.Json.Linq;         // JObject class


namespace PasswordManager
{
    class Program
    {
        //file path names
        private const string ACCOUNTS_FILE = "account_data.json";
        private const string SCHEMA_FILE = "password_schema.json";

        static void Main(string[] args)
        {
            string json_schema;
            if (ReadFile(SCHEMA_FILE, out json_schema))
            {
                List<Account> accounts_list = new List<Account>();
                string json_data;
                if(ReadFile(ACCOUNTS_FILE, out json_data))
                {
                    //a. If the application’s JSON file already exists, reads this and deserializes it to create
                    //a collection(such as a List<Account>) of account objects in memory.
                }


            }
            else
            {
                // Read operation for schema failed
                Console.WriteLine("\nERROR:\tUnable to read the schema file.");
            }

            {

            }
            //If the file doesn’t exist, creates an empty collection.



            //UI
            DateTime dateNow = DateTime.Now;
            Console.WriteLine(dateNow.ToShortDateString() + "\t\t\t PASSWORD MANAGEMENT SYSTEM");
            Console.WriteLine("****************************************************************************************************");
            Console.WriteLine("\tAccount Entries:");
            Console.WriteLine("****************************************************************************************************");








            //b.Shows the user a menu of the following command options:


            //i.List all fields for a selected account element


            //ii.Add a new account object to the collection with the user providing values for
            //all required fields, except the StrengthNum and the StrengthText fields
            //(which will be assigned values using the PasswordTester class included with the
            //starting code) and the LastReset field(which should be assigned the system date).


            //iii.Delete any account object from the collection.


            //iv.Change the password stored within a selected account element and simultaneously
            //update the StrengthNum, StrengthText and LastReset values
            //(again, this last value should be assigned the current system date)


            //c.If the user adds an account or modifies the password of an existing account,
            //the account object should be validated against your schema.If the new account
            //information fails the validation test, the user should validation informed that the
            //information entered is invalid and they should be prompted to re-input the required
            //information.


            //d.The updated collection of account objects should be written to the JSON file.






            //// System date demonstration
            //DateTime dateNow = DateTime.Now;
            //Console.Write("PASSWORD MANAGEMENT SYSTEM (STARTING CODE), " + dateNow.ToShortDateString());

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
        }//end main

        private static bool ReadFile(string path, out string json)
        {
            try
            {
                // Read JSON file data 
                json = File.ReadAllText(path);
                return true;
            }
            catch
            {
                json = null;
                return false;
            }
        } // end ReadFile()

          // TODO Validates an account object against a schema (incomplete)
          //TODO move into account class
        private static bool ValidateItem(Account account, string json_schema)
        {
            // Convert account object to a JSON string 
            string json_data = JsonConvert.SerializeObject(account);

            // Validate the data string against the schema contained in the 
            // json_schema parameter. Also, modify or replace the following 
            // return statement to return 'true' if item is valid, or 'false' 
            // if invalid.
            JSchema schema = JSchema.Parse(json_schema);
            JObject itemObj = JObject.Parse(json_data);
            return itemObj.IsValid(schema);

        } // end ValidateItem()

    } // end class Program




    // Definition of the Account class
    //TODO move into own file
    class Account
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public string LoginUrl { get; set; }
        public string AccountNum { get; set; }
        public Password Password { get; set; }

    } // end class Account

      // Definition of the Password class
      //TODO move into own file
    class Password
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public int StrengthNum { get; set; }
        public string StrengthText { get; set; }
        public DateTime LastReset { get; set; }

    } // end class Password
}
