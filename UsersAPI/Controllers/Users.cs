using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        SqlConnection connection = new SqlConnection("server=DESKTOP-M9HIA84\\TEW_SQLEXPRESS; database=Users; Integrated Security=true;");

        // GET: api/<Users>
        //used for listing every record
        [HttpGet]
        public string Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM Users", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
                return "No data found!!";
        }
        
        // GET api/<Users>/5
        //used for listing a specific record by an ID
        [HttpGet("{id}")]
        public string Get(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM Users WHERE id = '"+id+"' ", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
                return "No user found with this ID!!";
        }

        
        // POST api/<Users>
        //used for adding a new user
        [HttpPost]
        public string Post(string Username,string FirstName,string LastName, string Email, string Password)
        {
            SqlCommand cmd = new SqlCommand($"INSERT into Users(Username, FirstName, LastName, Email, Password) VALUES('{Username}', '{FirstName}', '{LastName}', '{Email}', '{Password}')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i == 1)
            {
                return $"Record inserted with the values: Username: {Username}, FirstName: {FirstName}, Lastname: {LastName}, Email: {Email}, Password: {Password} ";
            }
            else
            {
                return "The record could not be inserted!";
            }
        }


        // PUT api/<Users>/5
        //not started
        //used for updating the records
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
            
        }

        // DELETE api/<Users>/5
        //used for deleting a record using an ID
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID = '" + id + "' ", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i == 1)
            {
                return "Record with id " + id + " has been deleted";
            }
            else
            {
                return "No data deleted";
            }
        }
    }
}





