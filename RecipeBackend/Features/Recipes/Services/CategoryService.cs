﻿using AutoMapper;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Filters;
using RecipeBackend.Features.Recipes.Models;
using RecipeBackend.Features.Recipes.Repositories;

namespace RecipeBackend.Features.Recipes.Services;

public class CategoryService(CategoryRepository repo, IMapper mapper, IWebHostEnvironment webEnv, IHttpContextAccessor httpContext) : ServiceBase("categories", webEnv)
{
    public async Task<Category> CreateCategoryAsync(CategoryCreateDto payload)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(CategoryService)}");
        if (await repo.CheckCategoryExistsAsync(title: payload.Title))
        {
            throw new AlreadyExistsException($"{nameof(Category)} with {nameof(payload.Title)}: {payload.Title} already exists.");
        }

        var fileName = await SaveUploadsFileAsync(payload.Image);
        var newCategory = new Category
        {
            Title = payload.Title,
            Image = fileName,
            Main = payload.Main
        };
        if (payload.Main)
        {
            await repo.MakeAllCategoriesNonMain();
        }
        return await repo.CreateCategoryAsync(newCategory);
    }

    public async Task<CategoryDetailDto?> GetCategoryByIdAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(CategoryService)}");
        var category = await repo.GetCategoryByIdAsync(id);
        DoesNotExistException.ThrowIfNull(category, $"{nameof(Category)} with {nameof(Category.Id)}: {id} does not exist.");

        var categoryDetailDto = mapper.Map<CategoryDetailDto>(category);
        categoryDetailDto.Image = httpContext.HttpContext.GetUploadsBaseUrl() + '/' + categoryDetailDto.Image;
        return categoryDetailDto;
    }

    public async Task<ICollection<CategoryListDto>> ListCategoriesAsync(CategoryFilters? filters = null)
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext, $"Error accessing the HttpContext inside the {nameof(CategoryService)}");
        var categories = await repo.ListCategoriesAsync(filters);
        
        var baseUrl = httpContext.HttpContext.GetUploadsBaseUrl() + '/';
        foreach (var category in categories)
        {
            category.Image = baseUrl + category.Image;
        }

        return categories;
    }

    public async Task<Category> UpdateCategoryAsync(int id, CategoryUpdateDto payload)
    {
        var category = await repo.GetCategoryByIdAsync(id);

        DoesNotExistException.ThrowIfNull(category, $"{nameof(Category)} with {nameof(Category.Id)}: {id} does not exist.");
        // AlreadyExistsException.ThrowIf(payload.Title == category.Title, $"{nameof(Category)} with {nameof(payload.Title)}: {payload.Title} already exists.");
        

        if (payload.Title != null)
        {
            category.Title = payload.Title;
        }

        if (payload.Image != null)
        {
            DeleteUploadsFile(category.Image);
            var fileName = await SaveUploadsFileAsync(payload.Image);
            category.Image = fileName;
        }

        if (payload.Main != null)
        {
            if (payload.Main == true)
            {
                await repo.MakeAllCategoriesNonMain();
            }
        }

        category = await repo.UpdateCategoryAsync(category);

        return category;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await repo.GetCategoryByIdAsync(id);
        DoesNotExistException.ThrowIfNull(category, $"{nameof(Category)} with {nameof(Category.Id)}: {id} does not exist.");

        DeleteUploadsFile(category.Image);
        await repo.DeleteCategoryAsync(category);
    }
}