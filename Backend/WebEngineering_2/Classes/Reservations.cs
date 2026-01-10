namespace WebEngineering_2.Classes;
using System;
using System.ComponentModel.DataAnnotations;

public class Reservation
{
    [Key]
    public Guid id { get; set; } 
    
    public DateTime from { get; set; } 
    
    public DateTime to { get; set; }

    public Guid room_id { get; set; } 

    public DateTime? deleted_at { get; set; } 
}