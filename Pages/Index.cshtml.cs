using Microsoft.AspNetCore.Mvc.RazorPages;

namespace League_of_Statistics.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static bool IsUser = false;
        public static string? Region = "NA";
        public static string? Username;
        public static string? newUsername;

        //string variables used in the web scraping process
        public static string? Level, LadderRank;
        public static string? SoloDuoRank, SoloDuoWins, SoloDuoGames;
        public static string? FlexRank, FlexWins, FlexGames;
        public static string? FirstChamp, FirstKDA, FirstWR;
        public static string? SecondChamp, SecondKDA, SecondWR;
        public static string? ThirdChamp, ThirdKDA, ThirdWR;

        //string variables for error messages
        public string? ErrorMessage = "";

        //list to populate ListBox in the ChampionsModel
        public static List<string>? champions;


        public void OnPost()
        {
            Region = Request.Form["region"];
            Username = Request.Form["username"];

            //CheckSummoner();

            bool valid_user = true;
            while (valid_user)
            {
                try
                {
                    if (Region == null && Username == string.Empty)
                    {
                        ErrorMessage = "Please enter a region and a username";
                        valid_user = false;
                    }
                    else if (Region == null)
                    {
                        ErrorMessage = "Please enter a region.";
                        valid_user = false;
                    }
                    else if (Username == string.Empty)
                    {
                        ErrorMessage = "Please enter a username.";
                        valid_user = false;
                    }
                    else if (IsUser == false)
                    {
                        ErrorMessage = $"We couldn't find the summoner, \"{Username}\".\nDouble check your spelling or try a different region.";
                        valid_user = false;
                    }
                    else
                    {
                        Response.Redirect("/Home");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Something went wrong while collecting the desired data. Please try again.", ex);
                }
            }
        }

        //query database for champion names
        protected void GetListBox()
        {

        }

        //check for user
        //static bool CheckSummoner()
        //{

        //}

        //get profile statistics
        protected void GetRankedStatistics()
        {

        }
    }
}