// <copyright file="ActiveUserSpecificationTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    using Ambev.DeveloperEvaluation.Domain.Enums;
    using Ambev.DeveloperEvaluation.Domain.Specifications;
    using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;

    using FluentAssertions;

    using Xunit;

    public class ActiveUserSpecificationTests
    {
        [Theory]
        [InlineData(UserStatus.Active, true)]
        [InlineData(UserStatus.Inactive, false)]
        [InlineData(UserStatus.Suspended, false)]
        public void IsSatisfiedBy_ShouldValidateUserStatus(UserStatus status, bool expectedResult)
        {
            // Arrange
            DeveloperEvaluation.Domain.Entities.User user = ActiveUserSpecificationTestData.GenerateUser(status);
            ActiveUserSpecification specification = new ActiveUserSpecification();

            // Act
            bool result = specification.IsSatisfiedBy(user);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
