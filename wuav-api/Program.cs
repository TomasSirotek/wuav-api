using DotNetEnv.Configuration;
using wuav_api.Configuration;
using wuav_api.Identity;
using wuav_api.Services.Interface;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


DotNetEnv.Env.Load();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Configuration.AddDotNetEnv();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// USERS 
//  => /users 
app.MapGet("/users", async (IUserService userService) =>
{
    List<AppUser> userList = await userService.GetAllUsersAsync();
    Console.WriteLine("FROM CONTROLELR " + userList[0].name);
    if (userList.Count == 0)
        return Results.BadRequest($"Could not find any users");
    return Results.Json(userList);
});

// => /userByEmail

// => /userById
// has to be an int to retrive 

app.MapGet("/users/{id}",(int id) =>
{
    return "Hello world";
});




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



    
    


// app.UseHttpsRedirection();
app.Run();




