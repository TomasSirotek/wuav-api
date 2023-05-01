using System.Net;
using Azure.Storage.Blobs;
using Dapper;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using wuav_api.Configuration;
using wuav_api.Domain.Model;
using wuav_api.Identity;
using wuav_api.Identity.BindingModels;
using wuav_api.Infrastructure.Repository.Interface;
using wuav_api.Services.Interface;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Configuration.AddDotNetEnv();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000);
});

DotNetEnv.Env.Load();

var app = builder.Build();
app.UseCors("AllowOrigin");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// AUTHENTICATE 
// => authenticate 

app.MapPost("api/authenticate", async (IUserService userService,[FromBody]  AuthenticateRequest request) =>
{
    AppUser user = await userService.GetAsyncByEmailAsync(request.Email);
            
    if (user == null! || request.Password != "123456")
       return Results.BadRequest("Email or password is incorrect");
    
    return Results.Ok(user);
}).WithName("Authenticate");





// USERS 
// getting users with they roles 
//  => /users 
app.MapGet("api/users", async (IUserService userService) =>
{ 
    List<AppUser> userList = await userService.GetAllUsersAsync();
    if (userList.Count == 0)
        return Results.BadRequest($"Could not find any users");
    return Results.Ok(userList);
}).WithName("GetUsers");

// => /userByEmail

app.MapGet("api/users/{email}", async (IUserService userService, string email) =>
{
    AppUser user = await userService.GetAsyncByEmailAsync(email);
    if (user == null)
        return Results.BadRequest($"Could not find user with email: {email}");
    return Results.Ok(user);
}).WithName("GetUserByEmail");

// => /userById
// has to be an int to retrive 

app.MapGet("api/users/{id:int}", async (IUserService userService, int id) =>
{
    AppUser user = await userService.GetUserByIdAsync(id.ToString());
    if (user == null)
        return Results.BadRequest($"Could not find user with id: {id}");
    return Results.Ok(user);
}).WithName("GetUserById");



app.MapPost("api/users/{userId}/projects/{projectId}/images", async (IUserService userService, int userId, int projectId, IFormFile image, IBlobRepository blobRepository) =>
{
    Console.WriteLine($"Received image for user ID {userId} and project ID {projectId}:");

    // Save the image to a temporary file
    var tempFilePath = Path.GetTempFileName();
    await using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
    {
        await image.CopyToAsync(fileStream);
    }

    Console.WriteLine($"Image size: {new FileInfo(tempFilePath).Length} bytes");
    Console.WriteLine($"Image content type: {image.ContentType}");
    Console.WriteLine($"Image file name: {image.FileName}");

    // Upload the image to Azure Blob Storage
   // var fileName = $"{userId}/{projectId}/{image.FileName}";
   //  var blobUrl = await blobRepository.UploadFileAsync(fileName, tempFilePath);

   // Console.WriteLine($"Image uploaded to Azure Blob Storage: {blobUrl}");

    // Clean up the temporary file
    File.Delete(tempFilePath);

    return Results.Ok();
});



// TODO : MAYBE LATER NOT NEEDED ATM 
// ROLE 
// => /role
// => /roleById
// => /roleByName



// => /uploadImage


app.UseHttpsRedirection();
app.Run();




