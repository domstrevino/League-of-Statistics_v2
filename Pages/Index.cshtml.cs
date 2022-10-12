using Microsoft.AspNetCore.Mvc.RazorPages;
using HtmlAgilityPack;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;

namespace WebScraper2._0.Pages
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
        public string? NoInputMessage;
        public string? NoRegionMessage;
        public string? NoUsernameMessage;
        public string? NoUserMessage;

        //list to populate ListBox in the ChampionsModel
        public static List<string>? champions;

        
        public void OnPost()
        {
            //GetListBox();
            //Username = Request.Form["UserName"];
            Region = Request.Form["UserRegion"];
            
            //bool tryAgain = true;
            //while (tryAgain)
            //{
            //    try
            //    {
            //        CheckUser();

            //        if (Region == null && Username == string.Empty)
            //        {
            //            NoInputMessage = "Please enter a region and a username";
            //            tryAgain = false;
            //        }
            //        else if (Region == null)
            //        {
            //            NoRegionMessage = "Please enter a region.";
            //            tryAgain = false;
            //        }
            //        else if (Username == string.Empty)
            //        {
            //            NoUsernameMessage = "Please enter a username.";
            //            tryAgain = false;
            //        }
            //        else if (IsUser == false)
            //        {
            //            NoUserMessage = $"We couldn't find the summoner, \"{Username}\".\nDouble check your spelling or try a different region.";
            //            tryAgain = false;
            //        }
            //        else
            //        {
            //            GetRankedStatistics();
            //            Response.Redirect("/Home");

            //            break;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ApplicationException("Something went wrong while collecting the desired data. Please try again.", ex);
            //    }
            //}
        }

        //query database for champion names
        //protected void GetListBox()
        //{
        //    string conn = _configuration.GetConnectionString("connection");
        //    using SqlConnection connection = new(conn);

        //    champions = new List<string>();

        //    SqlDataReader dataReader;
        //    SqlCommand sqlCommand = new("SELECT * FROM Champs");
        //    sqlCommand.Connection = connection;

        //    connection.Open();

        //    dataReader = sqlCommand.ExecuteReader();
        //    while (dataReader.Read())
        //    {
        //        champions.Add((string)dataReader["Champ"]);
        //    }
        //    dataReader.Close();
        //    connection.Close();
        //}

        //check for user
        //static bool CheckUser()
        //{
        //    if (String.IsNullOrWhiteSpace(Username)) return false;
        //    newUsername = Username.Replace(" ", "%20");


        //    string url = $"https://www.op.gg/summoners/{Region}/{newUsername}";
        //    HtmlWeb website = new();
        //    HtmlDocument document = website.Load(url);

        //    HtmlNode node = document.DocumentNode.SelectSingleNode("//h2[@class = 'header__title']");

        //    if (node == null) IsUser = true;
            
        //    return IsUser;
        //}

        //get profile statistics
        //protected void GetRankedStatistics()
        //{
        //    if (Username != null)
        //    {
        //        newUsername = Username.Replace(" ", "%20");
        //    }


        //    string url = $"https://u.gg/lol/profile/{Region}1/{newUsername}/overview";
        //    HtmlWeb website = new();
        //    HtmlAgilityPack.HtmlDocument document = website.Load(url);

        //    HtmlNode ladderNode = document.DocumentNode.SelectSingleNode("//div[@class = 'summoner-profile_main']");     //selects the parent node to get the profile's level and ladder rank
        //    Level = ladderNode.SelectSingleNode("//div[@class = 'level-header']").InnerText;                             //     
        //    if (ladderNode.SelectSingleNode("//div[@class = 'ladder-ranking']//span//strong")?.InnerText == null)        //every profile will have these nodes, but 'ladder-ranking' could  
        //    {                                                                                                            //come back as null (if someone doesn't play ranked)
        //        LadderRank = "N/A";                                                                                      //
        //    }                                                                                                            //if it does come back as null, we set the text to "N/A"
        //    else
        //    {
        //        LadderRank = ladderNode.SelectSingleNode("//div[@class = 'ladder-ranking']//span//strong").InnerText;
        //    }

        //    List<HtmlNode> tierDatalist = document.DocumentNode.SelectNodes("//div[@class = 'queue-type']").ToList();    //selects the parent node to get the profile's solo/duo and flex stats
        //    foreach (HtmlNode tierNode in tierDatalist)                                                                  //
        //    {                                                                                                            //every profile will have a 'rank-text' node for solo/duo
        //        SoloDuoRank = tierNode.SelectSingleNode("(//div[@class = 'rank-text'])[1]").InnerText;                   //if it comes back as "Unranked" then we set the winrate and games to "N/A"
        //        if (SoloDuoRank == "Unranked")                                                                           //
        //        {                                                                                                        //if there is no node for Flex stats, which is possible, we set the related text to "N/A"
        //            SoloDuoWins = "N/A";
        //            SoloDuoGames = "N/A";
        //        }
        //        else
        //        {
        //            SoloDuoWins = tierNode.SelectSingleNode("(//div[@class = 'rank-wins'])[1]//strong").InnerHtml;
        //            SoloDuoGames = tierNode.SelectSingleNode("(//span[@class = 'total-games'])[1]").InnerText;
        //        }

        //        if (tierNode.SelectSingleNode("(//div[@class = 'rank-text'])[2]")?.InnerText == null)
        //        {
        //            FlexRank = "Unranked";
        //            FlexWins = "N/A";
        //            FlexGames = "N/A";
        //        }
        //        else
        //        {
        //            FlexRank = tierNode.SelectSingleNode("(//div[@class = 'rank-text'])[2]").InnerText;
        //            FlexWins = tierNode.SelectSingleNode("(//div[@class = 'rank-wins'])[2]//strong").InnerHtml;
        //            FlexGames = tierNode.SelectSingleNode("(//span[@class = 'total-games'])[2]").InnerText;
        //        }
        //    }

        //    string secondUrl = $"https://u.gg/lol/profile/{Region}1/{newUsername}/champion-stats";
        //    HtmlWeb secondWeb = new();
        //    HtmlAgilityPack.HtmlDocument secondDocument = secondWeb.Load(secondUrl);

        //    HtmlNode noData = secondDocument.DocumentNode.SelectSingleNode("//div[@class = 'no-table-data']");
        //    if (noData == null)
        //    {
        //        List<HtmlNode> championsDatalist = secondDocument.DocumentNode.SelectNodes("//div[@class = 'rt-tr-group']").ToList();
        //        foreach (HtmlNode championNode in championsDatalist)
        //        {
        //            FirstChamp = championNode.SelectSingleNode("(//span[@class = 'champion-name'])[1]").InnerText;
        //            SecondChamp = championNode.SelectSingleNode("(//span[@class = 'champion-name'])[2]").InnerText;
        //            ThirdChamp = championNode.SelectSingleNode("(//span[@class = 'champion-name'])[3]").InnerText;

        //            FirstKDA = championNode.SelectSingleNode("(//div[@class = 'kda'])[1]//span").InnerText;
        //            SecondKDA = championNode.SelectSingleNode("(//div[@class = 'kda'])[2]//span").InnerText;
        //            ThirdKDA = championNode.SelectSingleNode("(//div[@class = 'kda'])[3]//span").InnerText;

        //            FirstWR = championNode.SelectSingleNode("(//div[@class = 'champion-rates'])[1]").InnerText;
        //            SecondWR = championNode.SelectSingleNode("(//div[@class = 'champion-rates'])[2]").InnerText;
        //            ThirdWR = championNode.SelectSingleNode("(//div[@class = 'champion-rates'])[3]").InnerText;
        //        }
        //    }
        //    else
        //    {
        //        FirstChamp = "N/A";
        //        SecondChamp = "N/A";
        //        ThirdChamp = "N/A";

        //        FirstKDA = "N/A";
        //        SecondKDA = "N/A";
        //        ThirdKDA = "N/A";

        //        FirstWR = "N/A";
        //        SecondWR = "N/A";
        //        ThirdWR = "N/A";
        //    }
        //}
    }
}