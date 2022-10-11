using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace WebScraper2._0
{
    public class ChampionsModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public static string? ChampSurname;
        public static string? QTextBox;
        public static string? WTextBox;
        public static string? ETextBox;
        public static string? RTextBox;


        [BindProperty]
        public static string? NameValue { get; set; }


        public void OnPostUpdate()
        {
            NameValue = Request.Form["ChampionsListBox"];

            UpdateAbilities();
        }

        public ChampionsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private void UpdateAbilities()
        {
            string conn = _configuration.GetConnectionString("connection");
            using SqlConnection connection = new(conn);

            SqlDataReader dataReader;
            SqlCommand sqlCommand = new($"SELECT Champ, Q, W, E, R FROM ChampAbilities WHERE Champ = '{NameValue}'", connection);
            SqlCommand secondSqlCommand = new($"SELECT ChampName FROM Champs WHERE Champ = '{NameValue}'", connection);
            connection.Open();

            dataReader = secondSqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                ChampSurname = (string)dataReader["ChampName"];
            }
            dataReader.Close();

            dataReader = sqlCommand.ExecuteReader();
            while (dataReader.Read())
            {
                QTextBox = (string)dataReader["Q"];
                WTextBox = (string)dataReader["W"];
                ETextBox = (string)dataReader["E"];
                RTextBox = (string)dataReader["R"];
            }
            dataReader.Close();
            connection.Close();
        }
    }
}