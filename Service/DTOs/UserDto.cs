using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs;

/// <summary>
/// Data-transfer-object for User DB model
/// </summary>
public class UserDto
{
	public Guid Id { get; set; }

	public required string Username { get; set; }

	public required string Email { get; set; }

	public required Boolean IsAdmin { get; set; }
}
