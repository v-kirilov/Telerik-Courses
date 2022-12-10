using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.ViewModels;

namespace MatchScore.Helpers.Contracts
{
    public interface IModelMapper
    {
        TournamentDto MapTournamentToDto(Tournament tournament);
        TournamentDto MapTournamentToDtoDetails(Tournament tournament);
        Tournament MapDtoToTournament(TournamentDto dto,int userId);
        Tournament MapTournamentCreate(TournamentDto dto, int userId);
        RoundDto MapRoundToDto(Round round);
        Round MapDtoToRound(RoundDto dto);
        MatchDto MapMatchToDto(Match match);
        Match MapDtoToMatch(MatchDto matchDto);
        SportClub MapDtoToSportClub(SportClubDto sportClubDto);
        SportClubDto MapSportClubToDto(SportClub sportClub);
        CountryDto MapCountryToDto(Country country);
        Country MapDtoToCountry(CountryDto countryDto);
        Player MapDtoToPlayer(PlayerDto playerDto);
        Player MapCreateViewToPlayer(CreatePlayerViewModel viewModel);
        PlayerDto MapPlayerToDto(Player player);
        User MapRegViewToUser(RegisterViewModel regView);
        User MapCreateDtoToUser(CreateUserDto userDto);
        User MapEditViewToUser(EditUserViewModel editView);
        UserDto MapUserToDto(User user);
        User MapUpdateDtoToUser(UpdateUserDto userDto);
        User MapDtoToUser(UserDto userDto);
        EditUserViewModel MapUserDtoToEditView(UserDto dto);
        Request MapDtoToRequest(RequestDtoName dto);
        Request MapDtoToRequest(int id, RequestDtoName dto);
        RequestDto MapRequestToDto(Request request);
        RequestDtoName MapRequestToDtoWithName(Request request);
        EditPlayerViewModel MapPlayerDtoToEditPlayerViewModel(PlayerDto dto);
        Player MapEditPlayerViewModelToPlayer(EditPlayerViewModel viewModel);

    }
}
