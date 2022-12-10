using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services.Contracts
{
    public interface IPlayersService
    {
        public List<PlayerDto> GetAll();
        public PlayerDto GetById(int id);
        public PlayerDto GetByFullName(string fullName);
        public PaginatedList<PlayerDto> FilterBy(PlayerQueryParameters filterParameters);
        public PlayerDto Create(Player player, User authUser);
        public PlayerDto Update(int id, Player playerIncoming, User authUser);
        public void Delete(int id, User authUser);
        bool FullNameExists(string fullName);
    }
}
