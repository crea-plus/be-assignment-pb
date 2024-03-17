using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Configuration;

/// <summary>
/// Data-transfer-object for TokenValidation configuration
/// </summary>
public class TokenValidation
{
	public string Issuer { get; set; }
	public string Audience { get; set; }
	public string Key { get; set; }
}
