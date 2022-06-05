/*
 * Program:         PasswordManager.exe
 * Module:          PasswordManager.cs
 * Date:            2022-06-05
 * Author:          Amber Rose
 * Description:     Password Manager application
 *                  for storage and retrieval of account data and passwords 
 */

using System;
using System.Collections.Generic;
using System.IO;                    // File class
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
            //UI
            DateTime dateNow = DateTime.Now;
            Console.WriteLine(dateNow.ToShortDateString() + "\t\t\t PASSWORD MANAGEMENT SYSTEM");
            Console.WriteLine("****************************************************************************************************");


            //read in schema file
            string json_schema;
            if (ReadFile(SCHEMA_FILE, out json_schema))
            {
                //If the file doesn’t exist, creates an empty collection.
                List<Account> accounts_list = new List<Account>();
                string json_data;
                //a. If the application’s JSON file already exists,
                if (ReadFile(ACCOUNTS_FILE, out json_data))
                {
                    Console.WriteLine("\tAccount Entries:");
                    //Console.WriteLine("****************************************************************************************************");
                    //reads json data and deserializes it to create a collection of account objects in memory.
                    accounts_list = JsonConvert.DeserializeObject<List<Account>>(json_data);
                    //show existing accounts on menu
                    int i = 0;
                    foreach (Account a in accounts_list)
                    {
                        i++;
                        Console.WriteLine("\t" + i + ".\t" + a.Description);

                    }
                    Console.WriteLine("****************************************************************************************************");
                    //b.Shows the user a menu of the following command options:
                    //i.List all fields for a selected account element
                    Console.WriteLine("\tEnter # from the above list to select an entry");
                }


                Console.WriteLine("\tPress A to add an account");
                Console.WriteLine("\tPress X to save and exit");
                Console.WriteLine("****************************************************************************************************");
                
                bool notdone = true;
                do
                {
                    Console.Write("\nEnter a command: ");
                    //string pwText = Console.ReadLine();
                    string command = Console.ReadLine();
                    if(Int32.TryParse(command, out int i))
                    {
                        //TODO show details for accounts_list[i]
                        Console.WriteLine("****************************************************************************************************");
                        Console.WriteLine("\tSelected Account Information:");
                        //account description
                        Console.WriteLine("Description:\t\t" + accounts_list[i-1].Description);
                        //user ID
                        Console.WriteLine("User ID:\t\t" + accounts_list[i-1].UserId);
                        //login webpage
                        Console.WriteLine("Login Page:\t\t" + accounts_list[i - 1].LoginUrl);
                        //account number
                        Console.WriteLine("Account #:\t\t" + accounts_list[i - 1].AccountNum);
                        //password
                        Console.WriteLine("Password:\t\t" + accounts_list[i - 1].Password.Value);
                        //password strength
                        Console.WriteLine("Password Strength:\t" + accounts_list[i - 1].Password.StrengthText + " " + accounts_list[i - 1].Password.StrengthNum + "%");
                        //change date
                        Console.WriteLine("Password Changed:\t" + accounts_list[i - 1].Password.LastReset.ToShortDateString());
                        




                        //TODO show options to edit password or delete account


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
                    }
                    else
                    {
                        switch (command)
                        {
                            case "A":
                                {
                                    bool validAccount;
                                    do
                                    {
                                    //ii.Add a new account object to the collection with the user providing values for
                                    //all required fields, except the StrengthNum and the StrengthText fields
                                    //(which will be assigned values using the PasswordTester class included with the
                                    //starting code) and the LastReset field(which should be assigned the system date).
                                    Account a = new Account();
                                    string aEntry;
                                    //account description
                                    Console.Write("\nEnter the account name/description: ");
                                    aEntry = Console.ReadLine();
                                    a.Description = aEntry;
                                    //user ID
                                    Console.Write("\nEnter the user ID: ");
                                    aEntry = Console.ReadLine();
                                    a.UserId = aEntry;
                                    //login webpage
                                    Console.Write("\nEnter the login URL: ");
                                    aEntry = Console.ReadLine();
                                    a.LoginUrl = aEntry;
                                    //account number
                                    Console.Write("\nEnter the account number: ");
                                    aEntry = Console.ReadLine();
                                    a.AccountNum = aEntry;
                                    //password
                                    Password p = new Password();
                                    a.Password = p;
                                    Console.Write("\nEnter the password: ");
                                    aEntry = Console.ReadLine();
                                    p.Value = aEntry;
                                    PasswordTester pw = new PasswordTester(aEntry);
                                    p.StrengthText = pw.StrengthLabel;
                                    p.StrengthNum = pw.StrengthPercent;
                                    dateNow = DateTime.Now;
                                    p.LastReset = dateNow;

                                    //validate using schema
                                    validAccount = ValidateAccount(a, json_schema);
                                        if(validAccount)
                                        {
                                            //once valid, add to accounts list
                                            accounts_list.Add(a);
                                            Console.WriteLine("\nAccount " + a.Description + " added");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nAccount information invalid. Please re-enter");
                                        }
                                        
                                    } while (validAccount == false);
                                    
                                    
                                    
                                }
                                break;
                            case "X":
                                {
                                    notdone = false;
                                    //d.The updated collection of account objects should be written to the JSON file.
                                    json_data = "";
                                    foreach (Account a in accounts_list)
                                    {
                                        json_data += JsonConvert.SerializeObject(a);
                                    }
                                    try
                                    {
                                        File.WriteAllText(ACCOUNTS_FILE, json_data);
                                        Console.WriteLine($"Accounts saved to {ACCOUNTS_FILE}.\n");
                                    }
                                    catch (IOException ex)
                                    {
                                        Console.WriteLine($"\n\nERROR: {ex.Message}.\n");
                                    }
                                }
                                break ;
                            default:
                                break;
                        }
                    }
                } while (notdone);
                ////d.The updated collection of account objects should be written to the JSON file.
                //json_data = "";
                //foreach (Account a in accounts_list)
                //{
                //    json_data += JsonConvert.SerializeObject(a);
                //}
                //try
                //{
                //    File.WriteAllText(ACCOUNTS_FILE, json_data);
                //    Console.WriteLine($"Accounts saved to {ACCOUNTS_FILE}.\n");
                //}
                //catch (IOException ex)
                //{
                //    Console.WriteLine($"\n\nERROR: {ex.Message}.\n");
                //}
                
            }
            else
            {
                // Read operation for schema failed
                Console.WriteLine("\nERROR:\tUnable to read the schema file.");
            }

            Console.WriteLine("\n");
        }//end main

        private static bool ReadFile(string path, out string json)
        {
            try
            {
                StreamReader r = new StreamReader(path);
                // Read JSON file data 
                //json = File.ReadAllText(path);
                json = r.ReadToEnd();
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
        private static bool ValidateAccount(Account account, string json_schema)
        {
            // Convert account object to a JSON string 
            string json_data = JsonConvert.SerializeObject(account);
            //JSchemaValidatingReader.Equals(json_schema, json_data);
            // Validate the data string against the schema contained in the 
            // json_schema parameter. Also, modify or replace the following 
            // return statement to return 'true' if item is valid, or 'false' 
            // if invalid.
            JSchema schema = JSchema.Parse(json_schema);
            JObject itemObj = JObject.Parse(json_data);
            return itemObj.IsValid(schema);

        } // end ValidateAccount()

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
        public string Value { get; set; }
        public int StrengthNum { get; set; }
        public string StrengthText { get; set; }
        public DateTime LastReset { get; set; }

    } // end class Password
}
