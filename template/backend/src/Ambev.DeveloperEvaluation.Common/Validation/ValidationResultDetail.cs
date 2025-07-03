// <copyright file="ValidationResultDetail.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Common.Validation;

using FluentValidation.Results;

public class ValidationResultDetail
{
    public ValidationResultDetail()
    {
    }

    public ValidationResultDetail(ValidationResult validationResult)
    {
        this.IsValid = validationResult.IsValid;
        this.Errors = validationResult.Errors.Select(o => (ValidationErrorDetail)o);
    }

    public bool IsValid { get; set; }

    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = Enumerable.Empty<ValidationErrorDetail>();
}