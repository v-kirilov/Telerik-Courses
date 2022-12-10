using MatchScore.Models;
using MatchScore.Models.Enums;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace MatchScore.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<SportClub> SportClubs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<TournamentParticipants> TournamentParticipants{ get; set; }
        public DbSet<Scores> Scores { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Standing> Standings { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //Seed database with tournaments
            List<Tournament> tournaments = new List<Tournament>
            {
                new Tournament
                {
                    Id=1,
                    Title = "Super Cup",
                    Format= TournamentFormat.Knockout,
                    Prize = TournamentPrize.Financial,
                    DirectorId = 1,
                    StartDate=new DateTime(2022,11,15),
                    EndDate=new DateTime(2022,12,10),
                    IsDeleted=false,
                    Status = Status.Past
                },
                //new Tournament
                //{
                //    Id=2,
                //    Title = "C# Cup",
                //    Format= TournamentFormat.Knockout,
                //    Prize = TournamentPrize.Financial,
                //    DirectorId = 2,
                //    StartDate=new DateTime(2022,8,16),
                //    EndDate=new DateTime(2022,8,20),
                //    IsDeleted=false,
                //    Status = Status.Future
                //},
                //new Tournament
                //{
                //    Id=3,
                //    Title = "Entain League",
                //    Format= TournamentFormat.League,
                //    Prize = TournamentPrize.Honorary,
                //    DirectorId = 3,
                //    StartDate=new DateTime(2022,9,22),
                //    EndDate=new DateTime(2022,9,30),
                //    IsDeleted=false,
                //    Status = Status.Future
                //},
                //new Tournament
                //{
                //    Id=4,
                //    Title = "Telerik League",
                //    Format= TournamentFormat.League,
                //    Prize = TournamentPrize.Financial,
                //    DirectorId = 4,
                //    StartDate=new DateTime(2022,12,25),
                //    EndDate=new DateTime(2022,12,31),
                //    IsDeleted=false,
                //    Status = Status.Future
                //}
            };
            modelBuilder.Entity<Tournament>().HasData(tournaments);

            //Seed database with Rounds

            List<Round> rounds = new List<Round>
            {
                new Round
                {
                    Id = 1,
                    RoundNumber = 1,
                    TournamentId=1,
                    IsDeleted=false,
                    Status = Status.Past
                },
                //new Round
                //{
                //    Id = 2,
                //    RoundNumber = 1,
                //    TournamentId=2,
                //    IsDeleted=false,
                //    Status = Status.Past
                //},
                //new Round
                //{
                //    Id = 3,
                //    RoundNumber = 1,
                //    TournamentId=3,
                //    IsDeleted=false,
                //    Status = Status.Past

                //},
                //new Round
                //{
                //    Id = 4,
                //    RoundNumber = 1,
                //    TournamentId=4,
                //    IsDeleted=false,
                //    Status = Status.Future
                //}
            };
            modelBuilder.Entity<Round>().HasData(rounds);

            // Seed database with matches
            List<Match> matches = new List<Match>
            {
                new Match
                {
                    Id = 1,
                    Date = new DateTime(2022, 10, 13),
                    Format = MatchFormat.ScoreLimited,
                    TournamentId = 1,
                    RoundId = 1,
                    DirectorId = 1,
                    Status = Status.Past,
                    IsDeleted = false
                },
                //new Match
                //{
                //    Id = 2,
                //    Date = new DateTime(2022, 8, 17),
                //    Format = MatchFormat.ScoreLimited,
                //    TournamentId = 2,
                //    RoundId = 2,
                //    DirectorId = 2,
                //    Status = Status.Past,
                //    IsDeleted = false
                //},
                //new Match
                //{
                //    Id = 3,
                //    Date = new DateTime(2022, 09, 23),
                //    Format = MatchFormat.TimeLimited,
                //    TournamentId = 3,
                //    RoundId = 3,
                //    DirectorId = 3,
                //    Status = Status.Past,
                //    IsDeleted = false
                //},
                //new Match
                //{
                //    Id = 4,
                //    Date = new DateTime(2022, 12, 26),
                //    Format = MatchFormat.ScoreLimited,
                //    TournamentId = 4,
                //    RoundId = 4,
                //    DirectorId = 4,
                //    Status = Status.Future,
                //    IsDeleted = false
                //}
            };
            modelBuilder.Entity<Match>().HasData(matches);

            // Seed database with players
            List<Player> players = new List<Player>
            {
                new Player
                {
                    Id = 1,
                    FullName = "Petar Petrov",
                    CountryId = 1,
                    SportClubId = 1,
                    UserId = 1,
                    IsDeleted = false
                },
                new Player
                {
                    Id = 2,
                    FullName = "Georgi Georgiev",
                    CountryId = 2,
                    SportClubId = 2,
                    IsDeleted = false
                },
                new Player
                {
                    Id = 3,
                    FullName = "Ivan Ivanov",
                    CountryId = 3,
                    SportClubId = 2,
                    IsDeleted = false
                },
                new Player
                {
                    Id = 4,
                    FullName = "Nikolai Nikolov",
                    CountryId = 1,
                    SportClubId = 3,
                    IsDeleted = false
                }
            };
            modelBuilder.Entity<Player>().HasData(players);

            // Seed database with users
            List<User> users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "pesho@gmail.com",
                    Password = "MzMz",
                    IsDeleted = false,
                    RoleId = 3
                },
                new User
                {
                    Id = 2,
                    Email = "joro@gmail.com",
                    Password = "MzMz",
                    IsDeleted = false,
                    RoleId = 4
                },
                new User
                {
                    Id = 3,
                    Email = "ivan@gmail.com",
                    Password = "MzMz",
                    IsDeleted = false,
                    RoleId = 2
                },
                new User
                {
                    Id = 4,
                    Email = "niki@gmail.com",
                    Password = "MzMz",
                    IsDeleted = false,
                    RoleId = 1
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            // Seed database with users
            List<SportClub> sportClubs = new List<SportClub>
            {
                new SportClub
                {
                    Id = 1,
                    Name = "Bayern Munich",
                    IsDeleted = false
                },
                new SportClub
                {
                    Id = 2,
                    Name = "Barcelona",
                    IsDeleted = false
                },
                new SportClub
                {
                    Id = 3,
                    Name = "Real Madrid",
                    IsDeleted = false
                },
            };
            modelBuilder.Entity<SportClub>().HasData(sportClubs);

            // Seed database with countries
            List<Country> countries = new List<Country>
            {
                new Country
                {
                    Id = 1,
                    Name = "Bulgaria",
                    IsDeleted = false
                },
                new Country
                {
                    Id = 2,
                    Name = "Spain",
                    IsDeleted = false
                },
                new Country
                {
                    Id = 3,
                    Name = "Germany",
                    IsDeleted = false
                },
            };
            modelBuilder.Entity<Country>().HasData(countries);
 
            // Seed database with roles
            List<Role> rolesss = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    IsDeleted = false
                },
                new Role
                {
                    Id = 2,
                    Name = "Director",
                    IsDeleted = false
                },
                new Role
                {
                    Id = 3,
                    Name = "Verified",
                    IsDeleted = false
                },
                new Role
                {
                    Id = 4,
                    Name = "Default",
                    IsDeleted = false
                }
            };
            modelBuilder.Entity<Role>().HasData(rolesss);

            // Seed database with requests
            List<Request> requests = new List<Request>
            {
                new Request
                {
                    Id = 1,
                    Status = RequestStatus.Waiting,
                    Type = RequestType.PromoteToDirector,
                    UserId = 2
                }
            };
            modelBuilder.Entity<Request>().HasData(requests);

            List<TournamentParticipants> tournamentParticipants = new List<TournamentParticipants>()
            {
                new TournamentParticipants() {Id=1,PlayerId=1,TournamentId=1},
                new TournamentParticipants() {Id=2,PlayerId=1,TournamentId=1},
                //new TournamentParticipants() {Id=3,PlayerId=2,TournamentId=1},
                //new TournamentParticipants() {Id=4,PlayerId=2,TournamentId=2},
                //new TournamentParticipants() {Id=5,PlayerId=3,TournamentId=3},
                //new TournamentParticipants() {Id=6,PlayerId=2,TournamentId=3},
            };
            modelBuilder.Entity<TournamentParticipants>().HasData(tournamentParticipants);



            List<Scores> scores = new List<Scores>()
            {
                new Scores() {Id = 1, PlayerId = 1, MatchId = 1, Value = 1},
                new Scores() {Id = 2, PlayerId = 2, MatchId = 1, Value = 3},
                //new Scores() {Id = 3, PlayerId = 1, MatchId = 2, Value = 3},
                //new Scores() {Id = 4, PlayerId = 2, MatchId = 2, Value = 2},
                //new Scores() {Id = 5, PlayerId = 1, MatchId = 3, Value = 2},
                //new Scores() {Id = 6, PlayerId = 2, MatchId = 3, Value = 3},
                //new Scores() {Id = 7, PlayerId = 2, MatchId = 4, Value = 2},
                //new Scores() {Id = 8, PlayerId = 3, MatchId = 4, Value = 0}
               
            };
            modelBuilder.Entity<Scores>().HasData(scores);

            //modelBuilder.Entity<Standing>(st => 
            //{ st.HasNoKey();
            //    st.Property(v => v.PlayerName).HasColumnName("Name");
            //});

            modelBuilder.Entity<Tournament>()
                .HasMany(x => x.TournamentPlayers)
                .WithMany(x => x.Tournaments)
                .UsingEntity<TournamentParticipants>(
                x => x.HasOne(x => x.Player)
                .WithMany().HasForeignKey(x => x.PlayerId),
                x => x.HasOne(x => x.Tournament)
                .WithMany().HasForeignKey(x => x.TournamentId));

            //modelBuilder.Entity<Tournament>().HasOne(x => x.Standings);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.User)
                .WithOne(u => u.Player)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
