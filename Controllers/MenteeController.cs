using Mentorship.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.Metrics;

namespace Mentorship.Controllers
{
    public class MenteeController : Controller
    {
        public string constr = "server=132.148.177.57;user=biztrontechdb26;database=BiztronTechDB;password=Rama26@mysql;";
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        public static MySqlConnection conn;
        // GET: MenteeController
        public ActionResult Index(int PAGE2,string SORTDIR2,string SORTCOL2)
        {
            if (PAGE2 == 0)
            {
                PAGE2 = 1;
            }
            if (SORTCOL2 == null)
            {
                SORTCOL2 = "MENTEEMAIL";
            }
            if (SORTDIR2 == null)
            {
                SORTDIR2 = "DESC";
            }
            List<Mentee> mentee = new List<Mentee>();
            List<Mentee> specificmentees = new List<Mentee>();
            // DataTable dataTable = new DataTable();
            try
            {
                /* using (conn = new MySqlConnection(constr))
                 {
                     conn.Open();
                     MySqlDataAdapter sda = new MySqlDataAdapter("Select *From Mentee", conn);
                     sda.Fill(dataTable);
                 }

                 */
                conn = new MySqlConnection(constr);
                cmd = new MySqlCommand("select *from Mentee");
                cmd.Connection = conn;
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mentee.Add(
                        new Mentee()
                        {
                            EmployeeId = (int)reader["EmployeeId"],
                            MenteeName = (string)reader["MenteeName"],
                            MenteeMail = (string)reader["MenteeMail"],
                            BenchAge = (int)reader["BenchAge"],
                            CurrentCity = (string)reader["CurrentCity"],
                            SelectedPrimTechSkills = (string)reader["SelectedPrimTechSkills"],
                            MentorEmpId = (int)reader["MentorEmpId"],
                            MentorComments = (string)reader["MentorComments"],
                            Unit = (string)reader["Unit"],
                            MentorshipStartDate = (DateTime)reader["MentorshipStartDate"],
                            MentorshipEndDate = (DateTime)reader["MentorshipEndDate"],
                            IsReleased = (bool)reader["IsReleased"],
                            ReleasedReason = (string)reader["ReleasedReason"],
                            ReleaseComments = (string)reader["ReleaseComments"]
                        });
                }

                switch (SORTCOL2.ToUpper())
                {
                    case "MENTEEMAIL":
                        if (SORTDIR2 == "DESC")
                            mentee=mentee.OrderByDescending(x => x.MenteeMail).ToList();
                        else
                            mentee = mentee.OrderBy(x => x.MenteeMail).ToList();
                        break;
                    case "EMPLOYEEID":
                        if (SORTDIR2 == "DESC")
                            mentee = mentee.OrderByDescending(x => x.EmployeeId).ToList();
                        else
                            mentee = mentee.OrderBy(x => x.EmployeeId).ToList();
                        break;
                    default:

                        break;
                }
                var totalPage = (int)Math.Ceiling((double)mentee.Count / 4);

                specificmentees = mentee.Skip(4* (PAGE2 - 1)).Take(4).ToList();

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
            return View(specificmentees);


        }

        // GET: MenteeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenteeController/Create
        public ActionResult Create()
        {
            return View(new Mentee());
        }

        // POST: MenteeController/Create
        [HttpPost]
        public ActionResult Create(Mentee men)
        {
            try
            {
                using (conn = new MySqlConnection(constr))
                {
                    conn.Open();
                    cmd = new MySqlCommand("Insert into Mentee values(@Empid,@Mname,@Mmail,@benchage,@city,@Skills,@Mempid,@Mcomments,@unit,@Mstartdate,@Menddate,@Isrelease,@Rreason,@Rcomments)");
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@Empid", men.EmployeeId);
                    cmd.Parameters.AddWithValue("@Mname", men.MenteeName);
                    cmd.Parameters.AddWithValue("@Mmail", men.MenteeMail);
                    cmd.Parameters.AddWithValue("@benchage", men.BenchAge);
                    cmd.Parameters.AddWithValue("@city", men.CurrentCity);
                    cmd.Parameters.AddWithValue("@skills", men.SelectedPrimTechSkills);
                    cmd.Parameters.AddWithValue("@Mempid", men.MentorEmpId);
                    cmd.Parameters.AddWithValue("@Mcomments", men.MentorComments);
                    cmd.Parameters.AddWithValue("@unit", men.Unit);
                   cmd.Parameters.AddWithValue("@Mstartdate", men.MentorshipStartDate);
                   cmd.Parameters.AddWithValue("@Menddate", men.MentorshipEndDate);
                    cmd.Parameters.AddWithValue("@Isrelease", men.IsReleased);
                    cmd.Parameters.AddWithValue("@Rreason", men.ReleasedReason);
                    cmd.Parameters.AddWithValue("@Rcomments", men.ReleaseComments);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
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
    
        // GET: MenteeController/Edit/5
        public ActionResult Edit(int id)
        {
            Mentee mentee = null;
            try
            {
                conn = new MySqlConnection(constr);
                cmd = new MySqlCommand("select *from Mentee where EmployeeId=@id");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    mentee = new Mentee
                    {
                        EmployeeId = (int)reader["EmployeeId"],
                        MenteeName = (string)reader["MenteeName"],
                        MenteeMail = (string)reader["MenteeMail"],
                        BenchAge = (int)reader["BenchAge"],
                        CurrentCity = (string)reader["CurrentCity"],
                        SelectedPrimTechSkills = (string)reader["SelectedPrimTechSkills"],
                        MentorEmpId = (int)reader["MentorEmpId"],
                        MentorComments = (string)reader["MentorComments"],
                        Unit = (string)reader["Unit"],
                        MentorshipStartDate = (DateTime)reader["MentorshipStartDate"],
                        MentorshipEndDate = (DateTime)reader["MentorshipEndDate"],
                        IsReleased = (bool)reader["IsReleased"],
                        ReleasedReason = (string)reader["ReleasedReason"],
                        ReleaseComments = (string)reader["ReleaseComments"]
                    };
                
                }
               
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(mentee);

        }
        // POST: MenteeController/Edit/5
        [HttpPost]
        public ActionResult Edit(Mentee mentee)
        {
            try
            {
                conn = new MySqlConnection(constr);
                cmd = new MySqlCommand("Update Mentee set MenteeName=@Mname,Unit=@unit,MenteeMail=@Mmail,BenchAge=@benchage,CurrentCity=@cc,SelectedPrimTechSkills=@skills,MentorEmpId=@memp,MentorComments=@mcomments,MentorshipStartDate=@mentstart,MentorshipEndDate=@mentend,IsReleased=@release,ReleasedReason=@rreason,ReleaseComments=@rcomments where EmployeeId=@id");
                cmd.Parameters.AddWithValue("@id",mentee.EmployeeId);
                cmd.Parameters.AddWithValue("@Mname",mentee.MenteeName);
                cmd.Parameters.AddWithValue("@unit",mentee.Unit);
                cmd.Parameters.AddWithValue("@Mmail",mentee.MenteeMail);
                cmd.Parameters.AddWithValue("@benchage", mentee.BenchAge);
                cmd.Parameters.AddWithValue("@cc", mentee.CurrentCity);
                cmd.Parameters.AddWithValue("@skills", mentee.SelectedPrimTechSkills);
                cmd.Parameters.AddWithValue("@memp", mentee.MentorEmpId);
                cmd.Parameters.AddWithValue("@mcomments", mentee.MentorComments);
                cmd.Parameters.AddWithValue("@mentstart", mentee.MentorshipStartDate);
                cmd.Parameters.AddWithValue("@mentend", mentee.MentorshipEndDate);
                cmd.Parameters.AddWithValue("@release", mentee.IsReleased);
                cmd.Parameters.AddWithValue("@rreason", mentee.ReleasedReason);
                cmd.Parameters.AddWithValue("@rcomments", mentee.ReleaseComments);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View();
        }

        // GET: MenteeController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (conn = new MySqlConnection(constr))
                {
                    conn.Open();
                    string q = "Delete from Mentee where EmployeeId=@id";
                    cmd = new MySqlCommand(q, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        
    }
}
