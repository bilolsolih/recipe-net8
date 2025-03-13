using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Recipes.DTOs;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Controllers;

[ApiController, Route("api/v1/reviews")]
public class MobileReviewController(RecipeDbContext context, IMapper mapper, IWebHostEnvironment webEnv)
    : ControllerBase
{
    [HttpPost("create"), Authorize]
    public async Task<ActionResult<Review>> CreateReview(ReviewCreateDto payload)
    {
        var userId = int.Parse(User.FindFirstValue("userid")!);
        var newReview = mapper.Map<Review>(payload);
        if (payload.Image != null)
        {
            var uploadsFolder = Path.Combine(webEnv.GetUploadBasePath(), "reviews");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = payload.Image.FileName;
            var filePath = Path.Combine(uploadsFolder, fileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await payload.Image.CopyToAsync(fileStream);
            newReview.Image = $"/reviews/{fileName}";
        }

        newReview.UserId = userId;
        context.Reviews.Add(newReview);
        await context.SaveChangesAsync();
        return newReview;
    }

    [HttpGet("list")]
    public async Task<ActionResult> ListReviews([FromQuery] int? recipeId)
    {
        var reviewsQuery = context.Reviews.AsQueryable();
        if (recipeId != null)
        {
            reviewsQuery = reviewsQuery.Where(review => review.RecipeId == recipeId);
        }

        var reviews = await reviewsQuery.ProjectTo<ReviewDetailDto>(mapper.ConfigurationProvider).ToListAsync();
        var baseUrl = HttpContext.GetUploadsBaseUrl();
        reviews.ForEach(review =>
        {
            review.Image = review.Image != null ? baseUrl + review.Image : string.Empty;
            review.User.ProfilePhoto =
                review.User.ProfilePhoto != null ? baseUrl + '/' + review.User.ProfilePhoto : string.Empty;
        });
        return Ok(reviews);
    }
}