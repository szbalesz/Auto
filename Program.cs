using Auto.Model;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Auto
{
    public class Program
    {
        public static Connect conn = new Connect();
        public static List<Car> cars = new List<Car>();

        static void feltolt()
        {
            conn.Connection.Open();
            string sql = "SELECT * FROM `cars`";
            MySqlCommand cmd = new MySqlCommand(sql,conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car();
                car.Id = reader.GetInt32(0);
                car.Brand = reader.GetString(1);
                car.Type = reader.GetString(2);
                car.License = reader.GetString(3);
                car.Date = reader.GetInt32(4);
                cars.Add(car);
            }
            conn.Connection.Close();
        }
        public static void addNewCar()
        {
            conn.Connection.Open();
            string brand,type,license;
            int date;

            Console.Write("Kérem az autó márkáját: ");
            brand = Console.ReadLine();
            Console.Write("Kérem az autó típusát: ");
            type = Console.ReadLine();
            Console.Write("Kérem az autó motorszámát: ");
            license = Console.ReadLine();
            Console.Write("Kérem az auto gyártási évét: ");
            date = int.Parse(Console.ReadLine());
            string sql = $"INSERT INTO `cars`(`Brand`, `Type`, `License`, `Date`) VALUES ('{brand}','{type}','{license}',{date})";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();
            conn.Connection.Close();
        }
        public static void changeCarDate(int id,int ev)
        {
            conn.Connection.Open();
            string sql = $"UPDATE `cars` SET `Date`='{ev}' WHERE `Id` = {id}";
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();
            conn.Connection.Close();
        }
        static void Main(string[] args)
        {
            feltolt();
            foreach (Car car in cars)
            {
                Console.WriteLine($"Autó gyártója: {car.Brand}, azonosítója: {car.Id}");
            }
            addNewCar();
            changeCarDate(123,2024);
            Console.ReadLine();
        }
    }
}
