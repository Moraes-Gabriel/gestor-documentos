using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IWantApp.Domain;

public abstract class Entity : Notifiable<Notification>
{

    [Key]
    [Required]
    public int Id { get; set; }
    [JsonIgnore]
    public DateTime CreatedOn { get; set; }
    public DateTime EditedOn { get; set; }
    [JsonIgnore]
    public DateTime DeletedOn { get; set; }
}
