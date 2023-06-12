using System.ComponentModel.DataAnnotations.Schema;

namespace AkiraBot.Domain.Models;

[Table("LogType")]
public class LogType : EntityBase
{
    public string Type { get; set; }
}