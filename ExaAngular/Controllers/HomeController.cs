using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;

namespace ExaAngular
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public List<string>Get()
        {
            DbConnect dbConnector = new DbConnect();
           return dbConnector.GetThemes();
        }

        [HttpPost]

        public IActionResult Post([FromBody] FeedbackModel Feedback)
        {
            DbConnect dbConnector = new DbConnect();

            String Naming = Feedback.Name;
            String Emaling = Feedback.Email;
            String Telephoneing = Feedback.Telephone;
            String Topicing = Feedback.Topic;
            String Messageing = Feedback.Message;




            String summer = Naming + Emaling + Telephoneing + Topicing + Messageing;
            

            

            
            String text;
            FeedbackModel temp = new FeedbackModel { Name = Feedback.Name, Email = Feedback.Email, Telephone = Feedback.Telephone, Topic = Feedback.Topic, Message = Feedback.Message };
           
            string dataString;
            string insertString;
            string mesString;
            string mesString1;

            String Name = Feedback.Name;
            String Email = Feedback.Email;
            String Phone = Feedback.Telephone;
            int ThemeId = dbConnector.GetThemeId(Feedback.Topic);

            Validator validator = new Validator();

            


            
                if (validator.checkEmail(Email) && validator.checkPhone(Phone))
                {
                    String Message = Feedback.Message;
                    String PhoneEmail = "Phone='" + Phone + "'" + " AND Email='" + Email + "'";
               
                    dataString = "SELECT * FROM Contacts WHERE Phone='" + Phone + "'" + " AND Email='" + Email + "'";

                    if (dbConnector.IsContact(dataString))
                    {
                        insertString = "INSERT INTO Contacts(Id, ContName, Email, Phone) VALUES (" + dbConnector.amountId("Contacts") + ", '" + Name + "', '" + Email + "', '" + Phone + "')";
                        dbConnector.InsertToSql(insertString);
                        mesString = "INSERT INTO MessagesPool(Id, Id_theme, Id_contact, MessageText) VALUES (" + dbConnector.amountId("MessagesPool") + ", '" + ThemeId + "', '" + (dbConnector.amountId("Contacts") - 1) + "', '" + Message + "')";
                        dbConnector.InsertToSql(mesString);
                    }
                    else
                    {
                        mesString1 = "INSERT INTO MessagesPool(Id, Id_theme, Id_contact, MessageText) VALUES (" + dbConnector.amountId("MessagesPool") + ", '" + ThemeId + "', '" + dbConnector.GetUserId(PhoneEmail) + "', '" + Message + "')";
                        dbConnector.InsertToSql(mesString1);
                    }
                }
                
            return Ok(temp);
        }
    }
}
