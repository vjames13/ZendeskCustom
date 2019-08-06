using System;
using ZendeskCustom.Models.Ticket;
using System.Collections.Generic;
using System.Threading;
namespace ZendeskCustom
{
    class Program
    {
        static void Main()
        {
            var api = new ZendeskApi("https://greenworkstools1552039260.zendesk.com/api/v2", "Vincent.James@cai.io", "re78ytjb");

            IList<CustomField> test = new List<CustomField>();

            //create the user if they don't already exist
            //var user = api.Users.SearchByEmail(email);
            //if (user == null || user.Users.Count < 1)
            //    api.Users.CreateUser(new User()
            //   {
            //        Name = name,
            //        Email = email
            //    });


            Console.WriteLine("Enter 'search' to search a ticket, or 'create' to create a ticket");
            string task = Console.ReadLine();



            if (task == "create")
            {
                Console.WriteLine("Enter the Subject for your ticket:");
                string subject = Console.ReadLine();

                Console.WriteLine("Enter the Description:");
                string description = Console.ReadLine();

                //Console.WriteLine("Enter the Unit ERP");
                //string ERP = Console.ReadLine();

                //long IDvalue = new long();
                FieldOptions options = new FieldOptions();

                //bool quit = false;
                Console.WriteLine("Enter 'quit' at any entry to exit program");

                while (true)
                {
                    foreach (string fieldname in options.fieldnames)
                    {

                        int index1 = Array.IndexOf(options.fieldnames, fieldname);
                        long fieldid = options.idvals[index1];

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

                        if (index1 == 5 || index1 == 7)
                        {
                            Console.WriteLine("This Entry takes a datetime value. Using the current date & time. Press Enter to continue");
                            Console.WriteLine("Enter the value for " + fieldname + "(No Value Necessary, 5 second delay, then moving to next entry)");
                            //var fieldvalue = Console.ReadLine();
                            //var decvalue = float.Parse(fieldvalue);
                            string dateval = DateTime.Today.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                            Console.WriteLine("The value is:" + dateval);
                            CustomField enteredfield = new CustomField() { Id = fieldid, Value = dateval };
                            test.Add(enteredfield);
                            Thread.Sleep(5000);
                            continue;


                        }
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
                            //bool boolvalue = bool.Parse(fieldvalue);
                            CustomField enteredfield = new CustomField() { Id = fieldid, Value = boolvalue };
                            test.Add(enteredfield);
                            continue;


                        }
                        else
                        {

                            Console.WriteLine("Enter the value for " + fieldname);
                            var fieldvalue = Console.ReadLine();

                            if (fieldvalue == "quit")
                            {
                                break;
                            }



                            //Console.WriteLine("Testing correct ID Value: " + options.idvals[index1].ToString());

                            CustomField enteredfield = new CustomField() { Id = fieldid, Value = fieldvalue };

                            test.Add(enteredfield);
                        }

                        //Console.WriteLine("Current Index Test:" + index1.ToString());
                    }

                    //360022775753

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

                    //try
                    //{
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
                            Requester = new Requester() { Email = "v.james713@gmail.com" },
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
            else
            {
                while (true)
                {
                    Console.WriteLine("Enter the ID to search");
                    string inputid = Console.ReadLine();
                    long ticketid = Convert.ToInt64(inputid);

                    IndividualTicketResponse returnedticket = api.Tickets.GetTicket(ticketid);

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
