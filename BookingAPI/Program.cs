using DataAccess.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Repositories.IService;
using Repositories.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddControllers().AddOData(option => option.Select().Filter()
          .Count().OrderBy().Expand().SetMaxTop(106).AddRouteComponents("odata", GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IBillService, BillService>();
builder.Services.AddTransient<IBookingRoomDetailService, BookingRoomDetailService>();
builder.Services.AddTransient<IBookingSeviceDetailService, BookingSeviceDetailService>();
builder.Services.AddTransient<IBookingTransportDetailService, BookingTransportDetailService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IRoomTypeSevice, RoomTypeSevice>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<ITransportService, TransportService>();
builder.Services.AddTransient<ITypeTransportService, TypeTransportService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseODataBatching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Account>("Accounts");
    builder.EntitySet<Bill>("Bill");
    builder.EntitySet<BookingRoomDetail>("BookingRoomDetails");
    builder.EntitySet<BookingSeviceDetail>("BookingSeviceDetails");
    builder.EntitySet<BookingTransportDetail>("BookingTransportDetails");
    builder.EntitySet<Comment>("Comments");
    builder.EntitySet<Room>("Rooms");
    builder.EntitySet<RoomType>("RoomTypes");
    builder.EntitySet<Service>("Services");
    builder.EntitySet<Transport>("Transports");
    builder.EntitySet<TypeTransport>("TypeTransports");


    return builder.GetEdmModel();

}