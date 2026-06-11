using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmersLeague.ML.DTOs;
namespace FarmersLeague.ML.Interfaces
{
    public interface IPlayerDb
    {
        void AddPlayer(string Name, int Age, string Position, int BaseAttack, int BaseDefence, double MarketValue, bool IsAvaible, bool IsStarting, int Condition, int Happiness, int Composure, int Aggression, int SeasonGoals, int SeasonAssists, int YellowCards, int RedCards);
        List<AdminPlayerDTO> GetAllPlayersForAdmin();
        List<AdminPlayerDTO> GetPlayersWithNoTeam();
        void DeletePlayer(int playerID);
        void UpdatePlayer(AdminPlayerDTO updatedPlayer);
        AdminPlayerDTO GetPlayerByID(int playerID);
        List<AdminPlayerDTO> GetPlayersByTeamID(int teamID);
        void ChangePlayerTeam(int playerID, int teamID);
        void RemovePlayerFromTeam(int playerID);
    }
}
