﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Requests;

public class UpdateContactRequest : CreateContactRequest
{
	public Guid Id { get; set; }
}