﻿using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class Floor
{
    [Key]
    public Guid Id { get; set; }

    public string? Description { get; set; }

    [Required]
    public string UsePurpose { get; set; } = default!;

    [Required]
    public int FloorNo { get; set; }

    [Required]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    public List<Room> Rooms { get; set; } = new();
}
