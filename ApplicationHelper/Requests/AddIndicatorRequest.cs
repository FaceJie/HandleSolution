﻿
using System.ComponentModel.DataAnnotations;
using EnmuHelper;

namespace ApplicationHelper.Requests
{
    public class AddIndicatorRequest
    {
        [Required] public string IndicatorId { get; set; }
        [Required] public IndicatorType IndicatorType { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Formula { get; set; }
        [Required] public string[] Dependencies { get; set; }
    }
}
