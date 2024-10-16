using Mentorship.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace Mentorship.Controllers
{
    public class MentorController : Controller
    {
        public string constr = "server=132.148.177.57;user=biztrontechdb26;database=BiztronTechDB;password=Rama26@mysql;";
        //public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        //public  static MySqlConnection conn;

        public IActionResult Index(int PAGE2,string SORTDIR2,string SORTCOL2)
        {
            if(PAGE2 == 0)
            {
                PAGE2 = 1;
            }
            if (SORTCOL2 == null)
            {
                SORTCOL2 = "EMPLOYEEMAIL";
            }
            if(SORTDIR2 == null)
            {
                SORTDIR2 = "DESC";
            }
            List<Mentor> mentors = new List<Mentor>();
            List<Mentor> specificmentors = new List<Mentor>();
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
              
               MySqlCommand cmd = new MySqlCommand("select *from Mentor");
                cmd.Connection = conn;
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mentors.Add(
                        new Mentor()
                        {
                            EmployeeId = (int)reader["EmployeeId"],
                            EmployeeName = (string)reader["EmployeeName"],
                            EmployeeEmail = (string)reader["EmployeeEmail"],
                            Unit = (string)reader["Unit"],
                            CurrentCity = (string)reader["CurrentCity"],
                            PrimaryTechSkills = (string)reader["PrimaryTechSkills"]
                        });
                }

                switch (SORTCOL2.ToUpper())
                {
                    case "EMPLOYEEMAIL":
                        if (SORTDIR2 == "DESC")
                            mentors=mentors.OrderByDescending(x => x.EmployeeEmail).ToList();
                        else
                           mentors=mentors.OrderBy(x => x.EmployeeEmail).ToList();
                        break;
                    case "EMPLOYEEID":
                        if (SORTDIR2 == "DESC")
                            mentors=mentors.OrderByDescending(x => x.EmployeeId).ToList();
                        else
                           mentors= mentors.OrderBy(x => x.EmployeeId).ToList();
                        break;
                    default:

                        break;
                }
                var totalPage = (int)Math.Ceiling((double)mentors.Count / 4);

               specificmentors = mentors.Skip(4 * (PAGE2 - 1)).Take(4).ToList();

                ViewBag.TotalPages = totalPage;


                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
            return View(specificmentors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Mentor());
        }

        [HttpPost]
        public IActionResult Create(Mentor mentor)
        {
           MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                
                MySqlCommand cmd = new MySqlCommand("insert into Mentor value(@Empid,@Empname,@Empemail,@unit,@Primarytechskills,@currentcity)");
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@Empid",mentor.EmployeeId);
                cmd.Parameters.AddWithValue("@Empname", mentor.EmployeeName);
                cmd.Parameters.AddWithValue("@Empemail", mentor.EmployeeEmail);
                cmd.Parameters.AddWithValue("@unit", mentor.Unit);
                cmd.Parameters.AddWithValue("@Primarytechskills", mentor.PrimaryTechSkills);
                cmd.Parameters.AddWithValue("@currentcity", mentor.CurrentCity);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
            finally
            {
                conn.Close();
            }
           
        }
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
                Mentor mentor = null;
                MySqlCommand cmd = new MySqlCommand("Select *from Mentor where EmployeeId=@id");
                cmd.Parameters.AddWithValue("@id", id);

                cmd.Connection = conn;
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    mentor = new Mentor
                    {
                        EmployeeId = (int)reader["EmployeeId"],
                        EmployeeName = (string)reader["EmployeeName"],
                        EmployeeEmail = (string)reader["EmployeeEmail"],
                        Unit = (string)reader["Unit"],
                        CurrentCity = (string)reader["CurrentCity"],
                        PrimaryTechSkills = (string)reader["PrimaryTechSkills"]
                    };
                }
                return View(mentor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View();

        }

        [HttpPost]
        public IActionResult Edit(Mentor mentor)
        {
            MySqlConnection conn = new MySqlConnection(constr);
            try
            {
               
                MySqlCommand cmd = new MySqlCommand("update Mentor set EmployeeName=@Emp_name,EmployeeEmail=@Emp_mail,Unit=@unit,CurrentCity=@city,PrimaryTechSkills=@skills where EmployeeId=@id");
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@id",mentor.EmployeeId);
                cmd.Parameters.AddWithValue("@Emp_name", mentor.EmployeeName);
                cmd.Parameters.AddWithValue("@Emp_mail", mentor.EmployeeEmail);
                cmd.Parameters.AddWithValue("@unit", mentor.Unit);
                cmd.Parameters.AddWithValue("@city", mentor.CurrentCity);
                cmd.Parameters.AddWithValue("@skills", mentor.PrimaryTechSkills);

                cmd.ExecuteNonQuery();
                
                return RedirectToAction("Index");
            }
            catch(MySqlException ex) 
            {
                Console.WriteLine(ex.Message);
                return View();
            }
            finally
            {
                conn.Close();
            }

           
        }

        public IActionResult Delete(int id) 
        {
            
            using (MySqlConnection conn= new MySqlConnection(constr))
            {
                conn.Open();
                string q = "Delete from Mentor where EmployeeId=@id";
                MySqlCommand cmd = new MySqlCommand(q,conn);
                cmd.Parameters.AddWithValue("@id",id);
                cmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
          
           
        }
    }
}
