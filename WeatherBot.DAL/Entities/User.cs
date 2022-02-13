using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeatherBot.Core.Enums;

namespace WeatherBot.DAL.Entities;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; set; }
    public State State { get; set; } = State.Main;
}