using HogwartsBackEndAPIs.Models;
using Microsoft.EntityFrameworkCore;

using HogwartsBackEndAPIs.Services.Contract;
using HogwartsBackEndAPIs.Services.Implementation;
using AutoMapper;
using HogwartsBackEndAPIs.DTOs;
using HogwartsBackEndAPIs.Utilities;


//



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbhogwartsContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")); });

builder.Services.AddScoped<IHouseService, HouseService>();
builder.Services.AddScoped<IWizardRequestService, WizardRequestService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



#region API REST REQUEST
app.MapGet("/House/list", async (
    IHouseService _houseService,
    IMapper _mapper
    ) =>
{
    var houseList = await _houseService.GetList();
    var houseListDTO = _mapper.Map<List<HouseDTO>>(houseList);

    if (houseListDTO.Count > 0)
    {
        return Results.Ok(houseListDTO);
    }
    else
    {
      return  Results.NotFound();
    }
});

app.MapGet("/WizardRequest/list", async (
    IWizardRequestService _wizardRequestService,
    IMapper _mapper
    ) =>
{
    var wizardRequestList = await _wizardRequestService.GetList();
    var wizardRequestListDTO = _mapper.Map<List<WizardRequestDTO>>(wizardRequestList);

    if (wizardRequestListDTO.Count > 0)
    {
        return Results.Ok(wizardRequestListDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPost("/WizardRequest/Save", async (
    WizardRequestDTO wizardRequest,
    IWizardRequestService _wizardRequestService,
    IMapper _mapper) => {
        var _wizardRequest = _mapper.Map<WizardRequest>(wizardRequest);
        var _wizardRequestCreated = await _wizardRequestService.Add(_wizardRequest);


        if (_wizardRequestCreated.WizardId != 0)
        {
            return Results.Ok(_mapper.Map<WizardRequestDTO>(_wizardRequestCreated));
        }
        else {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    });


app.MapPut("/WizardRequest/Update/{WizardId}", async (
    int WizardId, 
    WizardRequestDTO wizardRequest,
    IWizardRequestService _wizardRequestService,
    IMapper _mapper) => { 
    
        var _found = await _wizardRequestService.Get(WizardId);

        if (_found is null) {
            return Results.NotFound();
        }

        var _wizardRequest = _mapper.Map<WizardRequest>(wizardRequest);

        _found.WizardName = _wizardRequest.WizardName;
        _found.WizardLastName = _wizardRequest.WizardLastName;
        _found.WizardMuggleId = _wizardRequest.WizardMuggleId;
        _found.HouseId = _wizardRequest.HouseId;
        _found.WizardAge = _wizardRequest.WizardAge;

        var response = await _wizardRequestService.Update(_found);

        if (response)
        {
            return Results.Ok(_mapper.Map<WizardRequestDTO>(_found));
        }
        else {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

    });


app.MapDelete("/WizardRequest/Delete/{WizardId}", async (
    int WizardId,
    IWizardRequestService _wizardRequestService
    ) => {

        var _found = await _wizardRequestService.Get(WizardId);

        if (_found is null)
        {
            return Results.NotFound();
        }

        var response = await _wizardRequestService.Delete(_found);

        if (response ) {
            return Results.Ok();

        }
        else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        }

    });


#endregion

app.UseCors("CORSPolicy");
app.Run();
