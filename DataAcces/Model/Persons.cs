using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Model
{
    public class Persons
    {
        private static List<Person> viewpersondtb = null;
        private static List<Person> p = null;
        private static List<Person> addtodtb = null;
        private static int _Counter = 8;
        public static int Counter { get { return ++_Counter; } }

        public static List<Person> GetFakeList
        {
            get
            {
                if (p == null)
                {
                    p = new List<Person>();
                    p.Add(new Person { Id = 1, FirstName = "Jan", LastName = "Ehrlich", Email = "honza.ehrlich@gmail.com", Birth = 1992 });
                    p.Add(new Person { Id = 2, FirstName = "Karel", LastName = "Maly", Email = "malyKaja@gmail.com", Birth = 1998 });
                    p.Add(new Person { Id = 3, FirstName = "Pavel", LastName = "Nedved", Email = "naja.sk@seznam.com", Birth = 1965 });
                    p.Add(new Person { Id = 4, FirstName = "Pepa", LastName = "Litevsky", Email = "polakposranej@virtuspro.pl", Birth = 1990 });
                    p.Add(new Person { Id = 5, FirstName = "Libor", LastName = "Ockowishz", Email = "asdd@gmail.com", Birth = 1954 });
                    p.Add(new Person { Id = 6, FirstName = "Olda", LastName = "Putin", Email = "master@gmail.com", Birth = 1945 });
                    p.Add(new Person { Id = 7, FirstName = "Jan", LastName = "Hemsky", Email = "hemsky@yahoo.com", Birth = 1988 });
                    p.Add(new Person { Id = 8, FirstName = "Johny", LastName = "Filipowsky", Email = "bulhrarec@jul.bg", Birth = 1989 });
                }

                return p;
            }
        }

        public static List<Person> GetEmpFromDTB
        {
            get
            {
                if (viewpersondtb == null)
                {
                    viewpersondtb = new List<Person>();

                    using (SqlConnection conn = new SqlConnection(connectionString: @"Data Source=CZ_EHRLICHJ\EHRLICHJ;Initial Catalog=My_ASP_Performance;Integrated Security=True;"))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM empDTB", conn))
                        {
                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Person dtb = new Person();
                                    dtb.Id = reader.GetInt32(0);
                                    dtb.FirstName = reader.GetString(1);
                                    dtb.LastName = reader.GetString(2);
                                    dtb.Email = reader.GetString(3);
                                    dtb.Birth = reader.GetInt32(4);
                                    dtb.Registred = reader.GetDateTime(5);

                                    viewpersondtb.Add(dtb);
                                }
                            }
                        }
                    }
                }
            return viewpersondtb;
            }
        }
        public static List<Person> AddEmpToDTB()
        {
            addtodtb = new List<Person>();

            using (SqlConnection conn = new SqlConnection(connectionString: @"Data Source=CZ_EHRLICHJ\EHRLICHJ;Initial Catalog=My_ASP_Performance;Integrated Security=True;"))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT into empDTB (Id, FirstName, LastName, Email, Birth, Registred) VALUES(@Id, @FirstName, @LastName, @Email, @Birth, @Registred)", conn))
                {
                    conn.Open();
                    Person adddtb = new Person();
                    cmd.Parameters.AddWithValue("@Id", null);
                    cmd.Parameters.AddWithValue("@FirstName", adddtb.Id);
                    cmd.Parameters.AddWithValue("@LastName", adddtb.LastName);
                    cmd.Parameters.AddWithValue("@Email", adddtb.Email);
                    cmd.Parameters.AddWithValue("@Birth", adddtb.Birth);
                    cmd.Parameters.AddWithValue("@Registred", adddtb.Registred);
                    viewpersondtb.Add(adddtb);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                }
            }

            return viewpersondtb;
        }
    }
}
