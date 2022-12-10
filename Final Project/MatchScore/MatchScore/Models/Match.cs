using MatchScore.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchScore.Models
{
    public class Match
    {
        [Key]
        public int Id { get;set; }
        [DataType(DataType.Date)]
        public DateTime Date { get;set; }
        public MatchFormat Format { get; set; }
        [ForeignKey("Tournament")]
        public int? TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        [ForeignKey("Round")]
        public int? RoundId { get; set; }
        public Round Round { get; set; }
        [ForeignKey("Director")]
        public int DirectorId { get; set; }
        public User Director { get; set; }  
        public Status Status { get; set; }
        public bool IsDeleted { get; set; }
        public List<Scores> Scores { get; set; }
    }
}
