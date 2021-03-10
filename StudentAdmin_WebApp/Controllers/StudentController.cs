using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentAdmin_WebApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace StudentAdmin_WebApp.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        string connectionString = @"Data Source=LAPTOP-CE3DN0MT\SQLEXPRESS;Initial Catalog=StudentAdmin;Integrated Security=True";
        public ActionResult Index()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string q = @"SELECT * FROM Student";
                SqlDataAdapter da = new SqlDataAdapter(q, conn);
                da.Fill(dt);
            }
            return View(dt);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View(new Student());
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student mystudent)
        {
            
            try
            {
                // TODO: Add insert logic here
                using (SqlConnection conn = new  SqlConnection(connectionString))
                {
                    conn.Open();
                    string insertStudent = $"INSERT INTO STUDENT (student_name, specialization, qualification, courses)" +
                        $"  VALUES ( '{mystudent.student_name}', '{mystudent.specialization}', '{mystudent.qualification}',{mystudent.courses})";
                    SqlCommand cmd = new SqlCommand(insertStudent, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }


                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            Student myStudent = new Student();
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string editStudent = $"SELECT * FROM Student where student_id = {id}";
                SqlDataAdapter da = new SqlDataAdapter(editStudent, conn);
                da.Fill(dt);
            }

            if (dt.Rows.Count == 1)
            {
                myStudent.student_name = dt.Rows[0][1].ToString();
                myStudent.specialization = dt.Rows[0][2].ToString();
                myStudent.qualification = dt.Rows[0][3].ToString();
                myStudent.courses = Convert.ToInt32(dt.Rows[0][4]);

                return View(myStudent);
            }
            return RedirectToAction("Index");
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Student mystudent)
        {
            
            try
            {
                // TODO: Add update logic here
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateStudent = $"UPDATE Student SET student_name = {mystudent.student_name}, specialization = {mystudent.specialization}, " +
                        $"qualification = {mystudent.qualification}, courses = {mystudent.courses}";
                    SqlCommand cmd = new SqlCommand(updateStudent, conn);
                    cmd.Parameters.Add(new SqlParameter(@"student_name", mystudent.student_name));
                    cmd.Parameters.Add(new SqlParameter(@"specialization", mystudent.specialization));
                    cmd.Parameters.Add(new SqlParameter(@"qualification", mystudent.qualification));
                    cmd.Parameters.Add(new SqlParameter(@"courses", mystudent.courses));

                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string delStudent = $"delete from student where student_id = {id} ";
                SqlCommand cmd = new SqlCommand(delStudent, conn);
                cmd.Parameters.Add(new SqlParameter(@"student_id", id));
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Student/Delete/5
       
    }
}
