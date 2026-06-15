
using FarmersLeague.ML.DTOs;
namespace FarmersLeague.ML.Interfaces
{
    public interface IPlayerDb
    {
        void AddPlayer(AdminPlayerDTO NewPlayer);
        List<AdminPlayerDTO> GetAllPlayersForAdmin();
        List<AdminPlayerDTO> GetPlayersWithNoTeam();
        void DeletePlayer(int playerID);
        void UpdatePlayer(AdminPlayerDTO updatedPlayer);
        AdminPlayerDTO GetPlayerByID(int playerID);
        List<AdminPlayerDTO> GetPlayersByTeamID(int teamID);
        void ChangePlayerTeam(int playerID, int teamID);
        void RemovePlayerFromTeam(int playerID);
        public void UpdatePlayerStartingStatus(int playerId, bool isStarting);
    }
}
