using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs;

/// <summary>
/// Data-transfer-object for Contact DB model
/// </summary>
public class ContactDto
{
	public required Guid Id { get; set; }

	public required string Name { get; set; }

	public required string PhoneNumber { get; set; }

	public string? ImageUrl { get; set; }

	public string? Email { get; set; }
}
