// <copyright file="PaginatedList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.WebApi.Common;

using Microsoft.EntityFrameworkCore;

public class PaginatedList<T> : List<T>
{
    public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        this.TotalCount = count;
        this.PageSize = pageSize;
        this.CurrentPage = pageNumber;
        this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        this.AddRange(items);
    }

    public int CurrentPage { get; private set; }

    public int TotalPages { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public bool HasPrevious => this.CurrentPage > 1;

    public bool HasNext => this.CurrentPage < this.TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        int count = await source.CountAsync();
        List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}