using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs;

/// <summary>
/// Data-transfer-object for Favourite DB model
/// </summary>
public class FavouriteDto
{
	public string Username { get; set; }
	public List<ContactDto> Contacts { get; set; }
}
