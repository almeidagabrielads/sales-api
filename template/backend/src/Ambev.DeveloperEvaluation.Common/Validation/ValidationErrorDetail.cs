// <copyright file="ValidationErrorDetail.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Common.Validation;

using FluentValidation.Results;

public class ValidationErrorDetail
{
    public string Error { get; init; } = string.Empty;

    public string Detail { get; init; } = string.Empty;

    public static explicit operator ValidationErrorDetail(ValidationFailure validationFailure)
    {
        return new ValidationErrorDetail
        {
            Detail = validationFailure.ErrorMessage,
            Error = validationFailure.ErrorCode,
        };
    }
}
