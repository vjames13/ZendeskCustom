﻿using System;
using ZendeskCustom.Models.Ticket;
using System.Collections.Generic;
using System.Threading;
namespace ZendeskCustom
{
    class Program
    {
        static void Main()
        {

            //set up the API
            var api = new ZendeskApi("https://greenworkstools1552039260.zendesk.com/api/v2", "Vincent.James@cai.io", "713apipassword");

            //create an empty list of CustomField types to be populated with Custom Field IDs and Values
            IList<CustomField> test = new List<CustomField>();


            Console.WriteLine("Enter 'search' to search a ticket, or 'create' to create a ticket");
            string task = Console.ReadLine();



            if (task == "create")
            {
                Console.WriteLine("Enter the Subject for your ticket:");
                string subject = Console.ReadLine();

                Console.WriteLine("Enter the Description:");
                string description = Console.ReadLine();

                FieldOptions options = new FieldOptions();

                Console.WriteLine("Enter 'quit' at any entry to exit program");

                while (true)
                {
                    foreach (string fieldname in options.fieldnames)
                    {
                        //get the index of whichever Custom field is currently being entered
                        int index1 = Array.IndexOf(options.fieldnames, fieldname);

                        //get the ID of this custom field
                        long fieldid = options.idvals[index1];

                        //Standard value entry for Custom fields that take a floating point decimal value
                        if (index1 == 4)
                        {
                            Console.WriteLine("This Entry takes a floating point value. Only A number may be entered");
                            Console.WriteLine("Enter the value for " + fieldname);
                            //var fieldvalue = Console.ReadLine();
                            var decvalue = float.Parse(Console.ReadLine());
                            CustomField enteredfield = new CustomField() { Id = fieldid, Value = decvalue };
                            test.Add(enteredfield);
                            Console.WriteLine("The value you entered was: " + decvalue.ToString());
                            continue;

                        }
                        //gets current Date & Time for custom fields that take a Date value
                        if (index1 == 5 || index1 == 7)
                        {
                            Console.WriteLine("This Entry takes a datetime value. Using the current date & time. Press Enter to continue");
                            Console.WriteLine("Enter the value for " + fieldname + "(No Value Necessary, 5 second delay, then moving to next entry)");

                            //convert DateTime to required format for upload to Zendesk
                            string dateval = DateTime.Today.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                            Console.WriteLine("The value is:" + dateval);
                            CustomField enteredfield = new CustomField() { Id = fieldid, Value = dateval };
                            test.Add(enteredfield);
                            Thread.Sleep(5000);
                            continue;


                        }
                        //standard value entry for custom fields that take a boolean value (checkbox)
                        if (index1 == 10 || index1 == 11)
                        {
                            bool boolvalue = new bool();
                            Console.WriteLine("This Entry takes a boolean value. Enter 'yes' or 'no'");
                            Console.WriteLine("Enter the value for " + fieldname);
                            var fieldvalue = Console.ReadLine();

                            if (fieldvalue == "yes")
                            {
                                boolvalue = true;
                            }
                            if (fieldvalue == "no")
                            {
                                boolvalue = false;
                            }

                            CustomField enteredfield = new CustomField() { Id = fieldid, Value = boolvalue };
                            test.Add(enteredfield);
                            continue;


                        }
                        //standard value entry for custom fields that take a string Value
                        else
                        {

                            Console.WriteLine("Enter the value for " + fieldname);
                            var fieldvalue = Console.ReadLine();

                            if (fieldvalue == "quit")
                            {
                                break;
                            }

                            CustomField enteredfield = new CustomField() { Id = fieldid, Value = fieldvalue };

                            test.Add(enteredfield);
                        }

                    }

                    //List all the entries to check if values actually got saved and match the correct IDs
                    foreach (CustomField fieldlistitem in test)
                    {
                        int index2 = test.IndexOf(fieldlistitem);
                        if (fieldlistitem.Value != null)
                        {
                            Console.WriteLine("Custom Field: " + options.fieldnames[index2].ToString() + "  Value: " + fieldlistitem.Value.ToString() + "  Field ID: " + options.idvals[index2]);
                        }
                        else
                        {
                            Console.WriteLine("Custom Field: " + options.fieldnames[index2].ToString() + "  VALUE IS NULL FOR THIS ENTRY Field ID: " + options.idvals[index2]);
                        }
                    }


                    Console.WriteLine("Entry Finished, enter 'quit' to exit program without uploading or 'upload' to upload ticket to Zendesk");
                    string finalentry = Console.ReadLine();
                    if (finalentry == "quit")
                    {
                        break;
                    }
                    if (finalentry == "upload")
                    {
                        //setup the ticket
                        var ticket = new Ticket()
                        {
                            Subject = subject,
                            Comment = new Comment() { Body = description },
                            Priority = "Normal",
                            Requester = new Requester() { Email = "Vincent.James@cai.io" },
                            CustomFields = test
                        };
                        //create the new ticket
                        var res = api.Tickets.CreateTicket(ticket).Ticket;
                        Console.WriteLine("Uploaded Ticket. Program quits after 5 second delay");
                        Thread.Sleep(5000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That's not a valid entry");
                    }
                }
            }
            //Search for an existing ticket
            if (task == "search")
            {
                while (true)
                {
                    Console.WriteLine("Enter the ID to search");
                    string inputid = Console.ReadLine();
                    long ticketid = Convert.ToInt64(inputid);

                    IndividualTicketResponse returnedticket = api.Tickets.GetTicket(ticketid);

                    //print ticket fields
                    Console.WriteLine("Ticket Title: " + returnedticket.Ticket.Subject.ToString());
                    Console.WriteLine("Ticket Description: " + returnedticket.Ticket.Description.ToString());
                    foreach (CustomField fields in returnedticket.Ticket.CustomFields)
                    {
                        Console.WriteLine("Ticket Custom Field ID: " + fields.Id.ToString());
                        if (fields.Value == null)
                        {
                            Console.WriteLine("This value is null");
                        }
                        else
                        {
                            Console.WriteLine("Ticket Custom Field Value: " + fields.Value.ToString());
                        }

                    }

                    Console.WriteLine("Press 'q' to quit");
                    string entry = Console.ReadLine();

                    if (entry == "q")
                    {
                        break;
                    }
                }

            }
        }
    }
}
