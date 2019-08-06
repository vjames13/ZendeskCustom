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
            Console.WriteLine("Connecting to Zendesk");

            //set up the API
            var api = new ZendeskApi("https://greenworkstools1552039260.zendesk.com/api/v2", "Vincent.James@cai.io", "713apipassword");

            Console.WriteLine("Retrieving List of Fields");

            //create an empty list of CustomField types to be populated with Custom Field IDs and Values
            IList<CustomField> customfields = new List<CustomField>();

            IList<TicketField> allfields = new List<TicketField>();

            //retrieve ticket fields from Zendesk
            GroupTicketFieldResponse fields = api.Tickets.GetTicketFields();

            //populate field list with fields from Zendesk
            foreach (TicketField fielddata in fields.TicketFields)
            {
                allfields.Add(fielddata);
            }

            Console.WriteLine("Fields retrieved succesfully");

            //print fields to make sure list of fields was retrieved succesfully
            /*foreach (TicketField fieldlistdata in allfields)
            {
                Console.WriteLine("The Ticket Field ID is: " + fieldlistdata.Id);
                Console.WriteLine("The Ticket Field Name is: " + fieldlistdata.TitleInPortal);
                Console.WriteLine("The Ticket Field Type is : " + fieldlistdata.Type);
                Console.WriteLine("This Ticket Field is Required :" + fieldlistdata.Required);
                Console.WriteLine("Field List Index of: " + allfields.IndexOf(fieldlistdata));
            }*/


            Console.WriteLine("Enter 'search' to search a ticket, or 'create' to create a ticket");
            string task = Console.ReadLine();



            if (task == "create")
            {
                while (true)
                {
                    Console.WriteLine("Enter the Subject for your ticket:");
                    string subject = Console.ReadLine();

                    Console.WriteLine("Enter the Description:");
                    string description = Console.ReadLine();

                    foreach (TicketField ticketField in allfields)
                    {
                        if (allfields.IndexOf(ticketField) > 6 && ticketField.Required == true) //testing using ONLY required custom fields
                        {
                            long? fieldid = ticketField.Id;
                            bool required = ticketField.Required;
                            string type = ticketField.Type;
                            string name = ticketField.TitleInPortal;
                            Console.WriteLine("Enter the value for " + name);
                            //commented out these lines, as we're just testing fields that are required
                            /*if (required == true)
                            {
                                Console.WriteLine("This Field is required!");
                            }
                            else
                            {
                                Console.WriteLine("This fiend is NOT required");
                            }*/
                            if (type == "tagger")
                            {
                                Console.WriteLine("This field is a Dropdown Field. Input MUST be a dropdown TAG");
                                Console.WriteLine("Example: 'tecnical_question' for custom field 'Topic'");
                            }
                            else
                            {
                                Console.WriteLine("This field takes a data type of: " + type);
                            }
                            var entry = Console.ReadLine();
                            CustomField enteredfield = new CustomField() { Id = fieldid, Value = entry };
                            customfields.Add(enteredfield);
                            continue;
                        }
                        else
                        {
                            continue;
                        }

                    }
                    //List all the entries to check if values actually got saved and match the correct IDs
                    //commented out as indexes do not currently match
                    /*foreach (CustomField fieldlistitem in customfields)
                    {
                        //increase index by 7, since the first 7 fields are Zendesk system fields, and unimportant to testing
                        int index = customfields.IndexOf(fieldlistitem) + 7;
                        if (fieldlistitem.Value != null)
                        {

                            Console.WriteLine("Custom Field: " + allfields[index].Title + "  Value: " + fieldlistitem.Value.ToString() + "  Field ID: " + allfields[index].Id);
                        }
                        else
                        {
                            Console.WriteLine("Custom Field: " + allfields[index].Title + "  VALUE IS NULL FOR THIS ENTRY.  Field ID: " + allfields[index].Id);
                        }
                    }*/
                    Console.WriteLine("Testing complete, enter 'q' to quit without uploading or 'upload' to upload ticket");
                    string quitentry = Console.ReadLine();

                    if (quitentry == "q")
                    {
                        break;
                    }
                    if (quitentry == "upload")
                    {
                        var ticket = new Ticket()
                        {
                            Subject = subject,
                            TicketFormId = 360000437791,
                            Comment = new Comment() { Body = description },
                            Priority = "Normal",
                            Requester = new Requester() { Email = "Vincent.James@cai.io" },
                            CustomFields = customfields
                        };
                        //create the new ticket
                        var res = api.Tickets.CreateTicket(ticket).Ticket;
                        Console.WriteLine("Uploaded Ticket. Program quits after 5 second delay");
                        Thread.Sleep(5000);
                        break;
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
                        foreach (CustomField returnedfields in returnedticket.Ticket.CustomFields)
                        {
                            Console.WriteLine("Ticket Custom Field ID: " + returnedfields.Id.ToString());
                            if (returnedfields.Value == null)
                            {
                                Console.WriteLine("This value is null");
                            }
                            else
                            {
                                Console.WriteLine("Ticket Custom Field Value: " + returnedfields.Value.ToString());
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
}
