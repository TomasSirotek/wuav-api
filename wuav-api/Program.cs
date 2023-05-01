using Dapper;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using wuav_api.Configuration;
using wuav_api.Domain.Model;
using wuav_api.Identity;
using wuav_api.Identity.BindingModels;
using wuav_api.Services.Interface;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Configuration.AddDotNetEnv();

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


// PROJECTS 
// => /projects 
// => /projectByUserId

// // make endpoint to get all project by user id for the user 
// app.MapGet("api/projects/{id:int}", async (IProjectService projectService, int id) =>
// {
//     List<Project> projectList = await projectService.GetAllProjectsByUserIdAsync(id.ToString());
//     if (projectList.Count == 0)
//         return Results.BadRequest($"Could not find any projects for user with id: {id}");
//     return Results.Ok(projectList);
// }).WithName("GetProjectsByUserId");



// => /getProjectById 




// TODO : MAYBE LATER NOT NEEDED ATM 
// ROLE 
// => /role
// => /roleById
// => /roleByName



// => /uploadImage


app.UseHttpsRedirection();
app.Run();




