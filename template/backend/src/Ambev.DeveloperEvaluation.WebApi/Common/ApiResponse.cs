// <copyright file="ApiResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Common;

using Ambev.DeveloperEvaluation.Common.Validation;

public class ApiResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
}
