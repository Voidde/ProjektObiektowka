using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;
namespace WpfApp1
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DbSet<Przelewy> Przelewy { get; set; }
        public DbSet<Karty> Karty { get; set; }
        public DbSet<Adresy> Adresy { get; set; }

        public string ConnectionString { get; }

        public DatabaseContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnectionString);
        }
    }
  
    public class Users
    {
        public long UserID { get; set; }
        [Key]
        public string Imie{ get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string NrTel { get; set; }
        public string Haslo { get; set; }
        public string Saldo { get; set; }

    }
    public class Przelewy
    {
        public int PrzelewID { get; set; }
        [Key]
        public int UserID { get; set; }
        public DateTime DataPrzelewu {get; set; }
        public long Kwota { get; set; }

    }
    public class Karty
    {
        public int UserID { get; set; }
        public string NrKarty { get; set; }
        [Key]
        public DateTime DataWaznosci { get; set; }
        public string CVV { get; set; }
    }
    public class Adresy
    {
       
        public int UserID { get; set; }
       
        public string Adres { get; set; }
        [Key]
        public string Miasto { get; set; }
        public string KodPocztowy { get; set; }
    }

   

}
