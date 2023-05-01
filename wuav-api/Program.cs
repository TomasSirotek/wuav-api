using Dapper;
using DotNetEnv.Configuration;
using wuav_api.Configuration;
using wuav_api.Identity;
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

// USERS 
// getting users with they roles 
//  => /users 
app.MapGet("/users", async (IUserService userService) =>
{ 
    List<AppUser> userList = await userService.GetAllUsersAsync();
    if (userList.Count == 0)
        return Results.BadRequest($"Could not find any users");
    return Results.Ok(userList);
}).WithName("Get all users in the database");

// => /userByEmail

app.MapGet("/users/{email}", async (IUserService userService, string email) =>
{
    AppUser user = await userService.GetAsyncByEmailAsync(email);
    if (user == null)
        return Results.BadRequest($"Could not find user with email: {email}");
    return Results.Ok(user);
}).WithName("Get user by email");

// => /userById
// has to be an int to retrive 

app.MapGet("/users/{id:int}", async (IUserService userService, int id) =>
{
    AppUser user = await userService.GetUserByIdAsync(id.ToString());
    if (user == null)
        return Results.BadRequest($"Could not find user with id: {id}");
    return Results.Ok(user);
}).WithName("Get user by id");




// AUTHENTICATE 
// => authenticate 

// ROLE 
// => /role
// => /roleById
// => /roleByName

// PROJECTS 
// => /projects 
// => /projectByUserId
// => /getProjectById 

// => /uploadImage



    
    


 app.UseHttpsRedirection();
app.Run();




